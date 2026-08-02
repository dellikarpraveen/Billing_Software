#nullable enable
using System;
using System.CodeDom;
using System.Windows.Forms;

namespace Billing_Software
{
    public partial class EditRowForm : Form
    {
        private object someObject;

        public Billing_Software.Services.MasterCompanyService.GroupDefinition Edited { get; private set; } = new Billing_Software.Services.MasterCompanyService.GroupDefinition();

        public EditRowForm()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
        }

        public void InitializeWith(Billing_Software.Services.MasterCompanyService.GroupDefinition source)
        {
            // clone to avoid editing original until OK
            Edited = new Billing_Software.Services.MasterCompanyService.GroupDefinition
            {
                SourceId = source.SourceId,
                SourceLineNumber = source.SourceLineNumber,
                GroupName = source.GroupName,
                ParentSourceId = source.ParentSourceId,
                GroupType = source.GroupType,
                LedgerName = source.LedgerName,
                CreateLedger = source.CreateLedger
            };

            txtSourceId.Text = Edited.SourceId?.ToString() ?? string.Empty;
            txtGroupName.Text = Edited.GroupName;
            txtParentId.Text = Edited.ParentSourceId?.ToString() ?? string.Empty;
            txtGroupType.Text = Edited.GroupType;
            txtLedgerName.Text = Edited.LedgerName ?? string.Empty;
            chkCreateLedger.Checked = Edited.CreateLedger.GetValueOrDefault(false);

            // FIX: Intercept processing if context data properties are uninstantiated
            if (someObject == null)
            {
                MessageBox.Show("No data record selected to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Validate and update Edited
            Edited.GroupName = txtGroupName.Text.Trim();
            if (int.TryParse(txtParentId.Text.Trim(), out var pid)) Edited.ParentSourceId = pid; else Edited.ParentSourceId = null;
            Edited.GroupType = txtGroupType.Text.Trim();
            Edited.LedgerName = string.IsNullOrWhiteSpace(txtLedgerName.Text) ? null : txtLedgerName.Text.Trim();
            Edited.CreateLedger = chkCreateLedger.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void EditRowForm_Load(object sender, EventArgs e)
        {

        }
    }
}
