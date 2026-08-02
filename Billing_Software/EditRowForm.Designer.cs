#nullable enable
namespace Billing_Software
{
    partial class EditRowForm
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblSourceId = new System.Windows.Forms.Label();
            this.txtSourceId = new System.Windows.Forms.TextBox();
            this.lblGroupName = new System.Windows.Forms.Label();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.lblParentId = new System.Windows.Forms.Label();
            this.txtParentId = new System.Windows.Forms.TextBox();
            this.lblGroupType = new System.Windows.Forms.Label();
            this.txtGroupType = new System.Windows.Forms.TextBox();
            this.lblLedgerName = new System.Windows.Forms.Label();
            this.txtLedgerName = new System.Windows.Forms.TextBox();
            this.chkCreateLedger = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSourceId
            // 
            this.lblSourceId.AutoSize = true;
            this.lblSourceId.Location = new System.Drawing.Point(12, 9);
            this.lblSourceId.Name = "lblSourceId";
            this.lblSourceId.Size = new System.Drawing.Size(50, 13);
            this.lblSourceId.TabIndex = 0;
            this.lblSourceId.Text = "SourceId";
            // 
            // txtSourceId
            // 
            this.txtSourceId.Location = new System.Drawing.Point(110, 6);
            this.txtSourceId.Name = "txtSourceId";
            this.txtSourceId.ReadOnly = true;
            this.txtSourceId.Size = new System.Drawing.Size(200, 20);
            this.txtSourceId.TabIndex = 1;
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Location = new System.Drawing.Point(12, 40);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(67, 13);
            this.lblGroupName.TabIndex = 2;
            this.lblGroupName.Text = "Group Name";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(110, 37);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(400, 20);
            this.txtGroupName.TabIndex = 3;
            // 
            // lblParentId
            // 
            this.lblParentId.AutoSize = true;
            this.lblParentId.Location = new System.Drawing.Point(12, 72);
            this.lblParentId.Name = "lblParentId";
            this.lblParentId.Size = new System.Drawing.Size(49, 13);
            this.lblParentId.TabIndex = 4;
            this.lblParentId.Text = "ParentID";
            // 
            // txtParentId
            // 
            this.txtParentId.Location = new System.Drawing.Point(110, 69);
            this.txtParentId.Name = "txtParentId";
            this.txtParentId.Size = new System.Drawing.Size(200, 20);
            this.txtParentId.TabIndex = 5;
            // 
            // lblGroupType
            // 
            this.lblGroupType.AutoSize = true;
            this.lblGroupType.Location = new System.Drawing.Point(12, 104);
            this.lblGroupType.Name = "lblGroupType";
            this.lblGroupType.Size = new System.Drawing.Size(60, 13);
            this.lblGroupType.TabIndex = 6;
            this.lblGroupType.Text = "GroupType";
            // 
            // txtGroupType
            // 
            this.txtGroupType.Location = new System.Drawing.Point(110, 101);
            this.txtGroupType.Name = "txtGroupType";
            this.txtGroupType.Size = new System.Drawing.Size(200, 20);
            this.txtGroupType.TabIndex = 7;
            // 
            // lblLedgerName
            // 
            this.lblLedgerName.AutoSize = true;
            this.lblLedgerName.Location = new System.Drawing.Point(12, 136);
            this.lblLedgerName.Name = "lblLedgerName";
            this.lblLedgerName.Size = new System.Drawing.Size(71, 13);
            this.lblLedgerName.TabIndex = 8;
            this.lblLedgerName.Text = "Ledger Name";
            // 
            // txtLedgerName
            // 
            this.txtLedgerName.Location = new System.Drawing.Point(110, 133);
            this.txtLedgerName.Name = "txtLedgerName";
            this.txtLedgerName.Size = new System.Drawing.Size(400, 20);
            this.txtLedgerName.TabIndex = 9;
            // 
            // chkCreateLedger
            // 
            this.chkCreateLedger.AutoSize = true;
            this.chkCreateLedger.Location = new System.Drawing.Point(110, 163);
            this.chkCreateLedger.Name = "chkCreateLedger";
            this.chkCreateLedger.Size = new System.Drawing.Size(93, 17);
            this.chkCreateLedger.TabIndex = 10;
            this.chkCreateLedger.Text = "Create Ledger";
            this.chkCreateLedger.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(354, 200);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 30);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(435, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditRowForm
            // 
            this.ClientSize = new System.Drawing.Size(534, 242);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chkCreateLedger);
            this.Controls.Add(this.txtLedgerName);
            this.Controls.Add(this.lblLedgerName);
            this.Controls.Add(this.txtGroupType);
            this.Controls.Add(this.lblGroupType);
            this.Controls.Add(this.txtParentId);
            this.Controls.Add(this.lblParentId);
            this.Controls.Add(this.txtGroupName);
            this.Controls.Add(this.lblGroupName);
            this.Controls.Add(this.txtSourceId);
            this.Controls.Add(this.lblSourceId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditRowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Row";
            this.Load += new System.EventHandler(this.EditRowForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label? lblSourceId;
        private System.Windows.Forms.TextBox? txtSourceId;
        private System.Windows.Forms.Label? lblGroupName;
        private System.Windows.Forms.TextBox? txtGroupName;
        private System.Windows.Forms.Label? lblParentId;
        private System.Windows.Forms.TextBox? txtParentId;
        private System.Windows.Forms.Label? lblGroupType;
        private System.Windows.Forms.TextBox? txtGroupType;
        private System.Windows.Forms.Label? lblLedgerName;
        private System.Windows.Forms.TextBox? txtLedgerName;
        private System.Windows.Forms.CheckBox? chkCreateLedger;
        private System.Windows.Forms.Button? btnOk;
        private System.Windows.Forms.Button? btnCancel;
    }
}
