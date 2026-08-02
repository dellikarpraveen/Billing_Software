#nullable enable
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Billing_Software
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox_State = new System.Windows.Forms.ComboBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonConvertExcel = new System.Windows.Forms.Button();
            this.buttonImportCsv = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox_District = new System.Windows.Forms.ComboBox();
            this.comboBox_Taluk = new System.Windows.Forms.ComboBox();
            this.txtExcelPath = new System.Windows.Forms.TextBox();
            this.btnBrowseExcel = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chkDryRun = new System.Windows.Forms.CheckBox();
            this.progressBarImport = new System.Windows.Forms.ProgressBar();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnCancelImport = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(861, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 524);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Company Name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(151, 68);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(302, 26);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Address ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Address";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "Address";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 281);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "District";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 19);
            this.label6.TabIndex = 7;
            this.label6.Text = "State";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 210);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 19);
            this.label7.TabIndex = 8;
            this.label7.Text = "Pin Code";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 316);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 19);
            this.label8.TabIndex = 9;
            this.label8.Text = "Phone Number";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 351);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 19);
            this.label9.TabIndex = 10;
            this.label9.Text = "Email";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 386);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 19);
            this.label10.TabIndex = 11;
            this.label10.Text = "Maintain A/C";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 421);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(130, 19);
            this.label11.TabIndex = 12;
            this.label11.Text = "Financail Year From";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 456);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 19);
            this.label12.TabIndex = 13;
            this.label12.Text = "label12";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(151, 103);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(302, 26);
            this.textBox2.TabIndex = 14;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(151, 138);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(302, 26);
            this.textBox3.TabIndex = 15;
            // 
            // comboBox_State
            // 
            this.comboBox_State.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_State.ForeColor = System.Drawing.Color.Navy;
            this.comboBox_State.FormattingEnabled = true;
            this.comboBox_State.Location = new System.Drawing.Point(152, 172);
            this.comboBox_State.Name = "comboBox_State";
            this.comboBox_State.Size = new System.Drawing.Size(302, 27);
            this.comboBox_State.TabIndex = 38;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(151, 313);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(302, 26);
            this.textBox7.TabIndex = 41;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(151, 348);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(302, 26);
            this.textBox8.TabIndex = 42;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(151, 383);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(302, 26);
            this.textBox9.TabIndex = 43;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(151, 418);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(302, 26);
            this.textBox10.TabIndex = 44;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(151, 453);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(302, 26);
            this.textBox11.TabIndex = 45;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.buttonConvertExcel);
            this.panel2.Controls.Add(this.buttonImportCsv);
            this.panel2.Location = new System.Drawing.Point(12, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(830, 44);
            this.panel2.TabIndex = 46;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(296, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(146, 30);
            this.button2.TabIndex = 1;
            this.button2.Text = "Export CSV";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // buttonConvertExcel
            // 
            this.buttonConvertExcel.Location = new System.Drawing.Point(152, 6);
            this.buttonConvertExcel.Name = "buttonConvertExcel";
            this.buttonConvertExcel.Size = new System.Drawing.Size(123, 31);
            this.buttonConvertExcel.TabIndex = 0;
            this.buttonConvertExcel.Text = "Convert Excel";
            this.buttonConvertExcel.UseVisualStyleBackColor = true;
            // 
            // buttonImportCsv
            // 
            this.buttonImportCsv.Location = new System.Drawing.Point(10, 6);
            this.buttonImportCsv.Name = "buttonImportCsv";
            this.buttonImportCsv.Size = new System.Drawing.Size(120, 30);
            this.buttonImportCsv.TabIndex = 0;
            this.buttonImportCsv.Text = "Import CSV";
            this.buttonImportCsv.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.ForeColor = System.Drawing.Color.Navy;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(152, 206);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(302, 27);
            this.comboBox1.TabIndex = 0;
            // 
            // comboBox_District
            // 
            this.comboBox_District.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_District.ForeColor = System.Drawing.Color.Navy;
            this.comboBox_District.FormattingEnabled = true;
            this.comboBox_District.Location = new System.Drawing.Point(152, 277);
            this.comboBox_District.Name = "comboBox_District";
            this.comboBox_District.Size = new System.Drawing.Size(302, 27);
            this.comboBox_District.TabIndex = 47;
            // 
            // comboBox_Taluk
            // 
            this.comboBox_Taluk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Taluk.ForeColor = System.Drawing.Color.Navy;
            this.comboBox_Taluk.FormattingEnabled = true;
            this.comboBox_Taluk.Location = new System.Drawing.Point(152, 241);
            this.comboBox_Taluk.Name = "comboBox_Taluk";
            this.comboBox_Taluk.Size = new System.Drawing.Size(302, 27);
            this.comboBox_Taluk.TabIndex = 48;
            // 
            // txtExcelPath
            // 
            this.txtExcelPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExcelPath.Location = new System.Drawing.Point(12, 12);
            this.txtExcelPath.Name = "txtExcelPath";
            this.txtExcelPath.Size = new System.Drawing.Size(360, 26);
            this.txtExcelPath.TabIndex = 0;
            // 
            // btnBrowseExcel
            // 
            this.btnBrowseExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseExcel.Location = new System.Drawing.Point(378, 11);
            this.btnBrowseExcel.Name = "btnBrowseExcel";
            this.btnBrowseExcel.Size = new System.Drawing.Size(75, 25);
            this.btnBrowseExcel.TabIndex = 1;
            this.btnBrowseExcel.Text = "Browse...";
            this.btnBrowseExcel.UseVisualStyleBackColor = true;
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(671, 417);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(124, 38);
            this.button_Save.TabIndex = 49;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(587, 229);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 75);
            this.button1.TabIndex = 50;
            this.button1.Text = "Open Invoice  Voucher";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // chkDryRun
            // 
            this.chkDryRun.AutoSize = true;
            this.chkDryRun.Location = new System.Drawing.Point(12, 520);
            this.chkDryRun.Name = "chkDryRun";
            this.chkDryRun.Size = new System.Drawing.Size(182, 23);
            this.chkDryRun.TabIndex = 51;
            this.chkDryRun.Text = "Dry-run (no DB changes)";
            this.chkDryRun.UseVisualStyleBackColor = true;
            // 
            // progressBarImport
            // 
            this.progressBarImport.Location = new System.Drawing.Point(200, 520);
            this.progressBarImport.Name = "progressBarImport";
            this.progressBarImport.Size = new System.Drawing.Size(400, 23);
            this.progressBarImport.TabIndex = 52;
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 555);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(760, 120);
            this.txtLog.TabIndex = 54;
            // 
            // btnCancelImport
            // 
            this.btnCancelImport.Location = new System.Drawing.Point(610, 516);
            this.btnCancelImport.Name = "btnCancelImport";
            this.btnCancelImport.Size = new System.Drawing.Size(100, 30);
            this.btnCancelImport.TabIndex = 53;
            this.btnCancelImport.Text = "Cancel";
            this.btnCancelImport.UseVisualStyleBackColor = true;
            this.btnCancelImport.Click += new System.EventHandler(this.btnCancelImport_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1029, 589);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.chkDryRun);
            this.Controls.Add(this.progressBarImport);
            this.Controls.Add(this.btnCancelImport);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.comboBox_Taluk);
            this.Controls.Add(this.comboBox_District);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.comboBox_State);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtExcelPath);
            this.Controls.Add(this.btnBrowseExcel);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Company Creation Page";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form3_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Form3_Load handler is implemented in Form3.cs (kept out of designer file)

        #endregion

        private Panel panel1;
        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private TextBox textBox2;
        private TextBox textBox3;
        private ComboBox comboBox_State;
        private TextBox textBox7;
        private TextBox textBox8;
        private TextBox textBox9;
        private TextBox textBox10;
        private TextBox textBox11;
        private Panel panel2;
        private ComboBox comboBox1;
        private ComboBox comboBox_District;
        private ComboBox comboBox_Taluk;
        private Button buttonImportCsv;
        private Button buttonConvertExcel;
        private TextBox txtExcelPath;
        private Button btnBrowseExcel;
        private Button button_Save;
        private Button button1;
        private CheckBox chkDryRun;
        private ProgressBar progressBarImport;
        private TextBox txtLog;
        private Button btnCancelImport;
        private Button button2;
    }
}