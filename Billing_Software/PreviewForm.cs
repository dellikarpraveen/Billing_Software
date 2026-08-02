using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Billing_Software
{
    public partial class PreviewForm : Form
    {
        public PreviewForm()
        {
            InitializeComponent();
            // Attach double-click handler once; it will operate on ListViewItem.Tag which is set in Populate
            if (listViewRows != null) listViewRows.DoubleClick += ListViewRows_DoubleClick;
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
        }

        public PreviewForm(List<Billing_Software.Services.MasterCompanyService.GroupDefinition> rows, List<string> errors) : this()
        {
            Populate(rows, errors);
        }

        private void Populate(List<Billing_Software.Services.MasterCompanyService.GroupDefinition> rows, List<string> errors)
        {
            if (lstErrors != null) lstErrors.Items.Clear();
            if (listViewRows != null) listViewRows.Items.Clear();
            if (lstErrors != null)
            {
                foreach (var e in errors)
                    lstErrors.Items.Add(e);
            }

            if (listViewRows != null)
            {
                foreach (var r in rows)
                {
                    var item = new ListViewItem(new[] { r.SourceId?.ToString() ?? string.Empty, r.GroupName ?? string.Empty, r.ParentSourceId?.ToString() ?? string.Empty, r.GroupType ?? string.Empty, r.LedgerName ?? string.Empty, (r.CreateLedger.HasValue ? r.CreateLedger.Value.ToString() : string.Empty) });
                    item.Tag = r; // keep reference for editing
                    listViewRows.Items.Add(item);
                }
            }
        }

        private void ListViewRows_DoubleClick(object? sender, EventArgs e)
        {
            if (listViewRows == null) return;
            if (listViewRows.SelectedItems == null || listViewRows.SelectedItems.Count == 0) return;
            var si = listViewRows.SelectedItems[0];
            var tag = si.Tag as Billing_Software.Services.MasterCompanyService.GroupDefinition;
            if (tag == null) return;
            using (var dlg = new EditRowForm())
            {
                dlg.InitializeWith(tag);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    // update list view
                    if (si.SubItems != null && si.SubItems.Count >= 6)
                    {
                        si.SubItems[1].Text = dlg.Edited.GroupName;
                        si.SubItems[2].Text = dlg.Edited.ParentSourceId?.ToString() ?? string.Empty;
                        si.SubItems[3].Text = dlg.Edited.GroupType;
                        si.SubItems[4].Text = dlg.Edited.LedgerName ?? string.Empty;
                        si.SubItems[5].Text = dlg.Edited.CreateLedger.HasValue ? dlg.Edited.CreateLedger.Value.ToString() : string.Empty;
                    }
                    si.Tag = dlg.Edited;
                    // re-validate rows when edits happen
                    if (listViewRows.Items != null)
                    {
                        var editedRows = listViewRows.Items.Cast<ListViewItem>().Select(i => i.Tag as Billing_Software.Services.MasterCompanyService.GroupDefinition).Where(x => x != null).Select(x => x!).ToList();
                        var re = Billing_Software.Services.MasterCompanyService.ValidateParsedRows(editedRows);
                        if (lstErrors != null)
                        {
                            lstErrors.Items.Clear();
                            foreach (var err in re) lstErrors.Items.Add(err);
                        }
                    }
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
