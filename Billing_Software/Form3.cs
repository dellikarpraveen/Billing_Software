using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Billing_Software
{
    public partial class Form3 : Form
    {
        private System.Threading.CancellationTokenSource? _importCts;

        public Form3()
        {
            InitializeComponent();
            // Designer now contains the Dry-run checkbox, progress bar, log textbox and cancel button.
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            _importCts = null;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Designer-time guard
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;

            if (textBox1 != null) { textBox1.ReadOnly = false; textBox1.Enabled = true; }
            if (textBox2 != null) { textBox2.ReadOnly = false; textBox2.Enabled = true; }
            if (textBox3 != null) { textBox3.ReadOnly = false; textBox3.Enabled = true; }
            if (textBox7 != null) textBox7.ReadOnly = false;
        }

        private void StartImportFlow(string connectionString, string csvPath, bool dryRun)
        {
            try
            {
                // Parse and show preview dialog
               
                var (rows, parseErrors) = Billing_Software.Services.MasterCompanyService.ParseGroupsFromCsv(csvPath);
                using (var preview = new PreviewForm(rows, parseErrors))
                {
                    var pr = preview.ShowDialog(this);
                    if (pr != DialogResult.OK)
                    {
                        MessageBox.Show("Import cancelled by user (preview).", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // Modal progress dialog flow
                _importCts = new System.Threading.CancellationTokenSource();
                var token = _importCts.Token;

                using (var progressForm = new ProgressForm())
                {
                    // Wire cancel
                    progressForm.CancelRequested += (s, ev) =>
                    {
                        if (_importCts != null && !_importCts.IsCancellationRequested) _importCts.Cancel();
                    };

                    // Create progress reporter that updates the progress form
                    IProgress<Billing_Software.Services.MasterCompanyService.ProgressReport> progress = new Progress<Billing_Software.Services.MasterCompanyService.ProgressReport>(r =>
                    {
                        if (r == null) return;
                        if (!string.IsNullOrEmpty(r.Message)) progressForm.AppendLog(r.Message);
                        if (r.Total > 0)
                        {
                            var pct = (int)((r.Completed * 100L) / r.Total);
                            progressForm.SetProgress(pct);
                        }
                    });

                    // Start import task
                    var importTask = Task.Run(() =>
                    {
                        try
                        {
                            Billing_Software.Services.MasterCompanyService.ImportGroupsFromCsv(connectionString, csvPath, createLedgersForAllGroups: true, dryRun: dryRun, progress: progress, cancellationToken: token);
                            progress.Report(new Billing_Software.Services.MasterCompanyService.ProgressReport { Completed = 0, Total = 0, Message = "Import finished." });
                        }
                        catch (OperationCanceledException)
                        {
                            progress.Report(new Billing_Software.Services.MasterCompanyService.ProgressReport { Completed = 0, Total = 0, Message = "Import cancelled by user." });
                        }
                        catch (Exception ex)
                        {
                            progress.Report(new Billing_Software.Services.MasterCompanyService.ProgressReport { Completed = 0, Total = 0, Message = "Import failed: " + ex.Message });
                        }
                        finally
                        {
                            // Close the modal progress form from UI thread
                            try { progressForm.BeginInvoke(new Action(() => progressForm.Close())); } catch { }
                        }
                    }, token);

                    // Show modal dialog - this blocks until progressForm.Close() is called
                    progressForm.ShowDialog(this);

                    // Wait for task to finish to ensure any exceptions observed
                    try { importTask.Wait(); } catch { }

                    _importCts = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Import failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (_importCts != null && !_importCts.IsCancellationRequested)
                {
                    _importCts.Cancel();
                    txtLog.AppendText("Cancellation requested..." + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to cancel import: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Resolve and validate the SQL Connection String parameters safely
                if (!Billing_Software.Config.AppConfigurationProvider.TryGetConnectionString("BillingSoftwareDB", out var cs) || string.IsNullOrEmpty(cs))
                {
                    MessageBox.Show("Connection string 'BillingSoftwareDB' not found or is empty.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Identify the file source pathway (Check for local app CSV fallback vs manually selected File Dialog pointer)
                var appCsv = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mastercompanyservice.csv");
                string csvPath = null;

                if (System.IO.File.Exists(appCsv))
                {
                    var dr = MessageBox.Show("Found mastercompanyservice.csv in application folder. Import it?", "Import CSV", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes) csvPath = appCsv;
                }

                if (string.IsNullOrEmpty(csvPath))
                {
                    using (var dlg = new OpenFileDialog())
                    {
                        dlg.Filter = "Tab/CSV Files (*.csv;*.txt;*.tsv)|*.csv;*.txt;*.tsv|All files (*.*)|*.*";
                        dlg.Title = "Select master company CSV to import (or cancel for system defaults)";
                        if (dlg.ShowDialog() == DialogResult.OK) csvPath = dlg.FileName;
                    }
                }

                // 3. Preparation: Reset standard reporting visual widgets if present on Form3 surface layout
                if (progressBarImport != null) progressBarImport.Value = 0;
                if (txtLog != null) txtLog.Clear();
                bool dryRun = chkDryRun?.Checked ?? false;

                // Container to hold our structured chart of accounts data models
                List<Billing_Software.Services.MasterCompanyService.GroupDefinition> finalTargetRows;

                if (!string.IsNullOrEmpty(csvPath))
                {
                    // --- OPTION A: PARSE FROM DATA FILE POINTER ---
                    if (txtLog != null) txtLog.AppendText($"Parsing file source: {csvPath}{Environment.NewLine}");

                    // Execute the file string splitting logic we optimized safely on a worker thread
                    var parseResult = await Task.Run(() => Billing_Software.Services.MasterCompanyService.ParseGroupsFromCsv(csvPath));

                    // Log validation results to log text display control
                    if (txtLog != null && parseResult.Errors.Any())
                    {
                        txtLog.AppendText($"--- Parse Messages & Errors Found ---{Environment.NewLine}");
                        txtLog.AppendText(string.Join(Environment.NewLine, parseResult.Errors) + Environment.NewLine);
                    }

                    // Block migration if data corruption validation issues exist
                    if (parseResult.Errors.Exists(err => !err.Contains("recommended")))
                    {
                        MessageBox.Show("Critical file structure issues found. Review the logs box panel for structural alignment errors.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    finalTargetRows = parseResult.Rows;
                }
                else
                {
                    // --- OPTION B: FALLBACK ON SECURE MASTER SYSTEM PRESETS ---
                    if (txtLog != null) txtLog.AppendText($"No file input chosen. Generating standard financial group structures...{Environment.NewLine}");

                    finalTargetRows = new List<Billing_Software.Services.MasterCompanyService.GroupDefinition>
            {
                new() { SourceId = 1, GroupName = "Assets", ParentSourceId = null, GroupType = "Asset" },
                new() { SourceId = 2, GroupName = "Liabilities", ParentSourceId = null, GroupType = "Liability" },
                new() { SourceId = 3, GroupName = "Equity", ParentSourceId = null, GroupType = "Equity" },
                new() { SourceId = 4, GroupName = "Income", ParentSourceId = null, GroupType = "Revenue" },
                new() { SourceId = 5, GroupName = "Expenses", ParentSourceId = null, GroupType = "Expense" },
                new() { SourceId = 6, GroupName = "Capital Account", ParentSourceId = 3, GroupType = "Equity" },
                new() { SourceId = 7, GroupName = "Current Assets", ParentSourceId = 1, GroupType = "Asset" },
                new() { SourceId = 8, GroupName = "Current Liabilities", ParentSourceId = 2, GroupType = "Liability" },
                new() { SourceId = 9, GroupName = "Fixed Assets", ParentSourceId = 1, GroupType = "Asset" },
                new() { SourceId = 10, GroupName = "Investments", ParentSourceId = 1, GroupType = "Asset" },
                new() { SourceId = 11, GroupName = "Direct Expenses", ParentSourceId = 5, GroupType = "Expense" },
                new() { SourceId = 12, GroupName = "Direct Income", ParentSourceId = 4, GroupType = "Revenue" },
                new() { SourceId = 13, GroupName = "Purchase Account", ParentSourceId = 5, GroupType = "Expense" },
                new() { SourceId = 14, GroupName = "Sales Account", ParentSourceId = 4, GroupType = "Revenue" },
                new() { SourceId = 15, GroupName = "Suspense Account", ParentSourceId = 1, GroupType = "Asset" },
                new() { SourceId = 16, GroupName = "Bank Accounts", ParentSourceId = 7, GroupType = "Asset" },
                new() { SourceId = 17, GroupName = "Deposit (Assets)", ParentSourceId = 7, GroupType = "Asset" },
                new() { SourceId = 18, GroupName = "Stock in Hand", ParentSourceId = 7, GroupType = "Asset" },
                new() { SourceId = 19, GroupName = "Sundry Debtors", ParentSourceId = 7, GroupType = "Asset" },
                new() { SourceId = 20, GroupName = "Bank OD Account", ParentSourceId = 8, GroupType = "Liability" },
                new() { SourceId = 21, GroupName = "Duties and Taxes", ParentSourceId = 8, GroupType = "Liability" },
                new() { SourceId = 22, GroupName = "Provisions", ParentSourceId = 8, GroupType = "Liability" },
                new() { SourceId = 23, GroupName = "Sundry Creditors", ParentSourceId = 8, GroupType = "Liability" }
            };
                }

                // 4. Execution Panel: Write records live into SQL Server using our hierarchical transaction logic loop
                if (dryRun)
                {
                    MessageBox.Show($"Dry Run verified successfully! Prepared to create {finalTargetRows.Count} charts.", "Dry Run Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txtLog != null) txtLog.AppendText($"Executing safe transactional saving loops on SQL...{Environment.NewLine}");

                // Build a dynamic progress tracker link to bind to our WinForms layout progressBar
                var progressTracker = new Progress<Billing_Software.Services.MasterCompanyService.ProgressReport>(report =>
                {
                    if (progressBarImport != null && report.Total > 0)
                    {
                        // Constrain percentages inside safe WinForms visual limits (0 to 100)
                        int pct = (int)((double)report.Completed / report.Total * 100);
                        progressBarImport.Value = Math.Min(Math.Max(pct, 0), 100);
                    }
                    if (txtLog != null && !string.IsNullOrEmpty(report.Message))
                    {
                        txtLog.AppendText($"{report.Message}{Environment.NewLine}");
                    }
                });

                // Run the heavy hierarchical multi-insert loop asynchronously to prevent interface freezing
                await Task.Run(() =>
                {
                    Billing_Software.Services.MasterCompanyService.CreateOrUpdateGroupsAndLedgers(
                        cs,
                        finalTargetRows,
                        createLedgersForAllGroups: true,
                        progress: progressTracker
                    );
                });

                MessageBox.Show("Master company structural groups and records initialized/updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Safely exit form window view panel on complete
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to complete master company provisioning operation: {ex.Message}", "Database Migration Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SeedCompanyWithGroups(string connectionString, string targetCompanyName)  //,string Sqlconnection, string SqlCommand)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                // SQL instruction copies properties, cross-joining your structural configuration records
                const string sqlCommand = @"
            INSERT INTO Company_Creation (
                Company_Name, Address, Address2, State, Pincode, District, Phone_No, Email, 
                Maintian_Accounts, Financial_Year_Start_Date, 
                GroupID, GroupName, ParentGroupID, GroupType
            )
            SELECT 
                c.Company_Name, c.Address, c.Address2, c.State, c.Pincode, c.District, c.Phone_No, c.Email, 
                c.Maintian_Accounts, c.Financial_Year_Start_Date,
                a.GroupID, a.GroupName, a.ParentGroupID, a.GroupType
            FROM AccountGroups a
            CROSS JOIN (
                SELECT TOP 1 Company_Name, Address, Address2, State, Pincode, District, Phone_No, Email, Maintian_Accounts, Financial_Year_Start_Date 
                FROM Company_Creation 
                WHERE Company_Name = @CompName
            ) c;";

                using (var cmd = new SqlCommand(sqlCommand, conn, tx))
                {
                    cmd.Parameters.Add("@CompName", SqlDbType.VarChar, 150).Value = targetCompanyName;
                    cmd.ExecuteNonQuery();
                }

                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //  private void Form3_Load(object sender, EventArgs e)
        //{
        //    _cts = new CancellationTokenSource();

        //       using var loading = new loadingForm();
        //            loading.Show(this);


    }
    }
//}
