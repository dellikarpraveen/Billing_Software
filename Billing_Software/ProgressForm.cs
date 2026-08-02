using System;
using System.Windows.Forms;

namespace Billing_Software
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
            // No-op constructor retained to satisfy nullable analysis and design-time requirements.
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
        }

        public void AppendLog(string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => AppendLog(text)));
                return;
            }
            if (txtLog != null)
            {
                txtLog.AppendText(text + Environment.NewLine);
                txtLog.SelectionStart = txtLog.TextLength;
                txtLog.ScrollToCaret();
            }
        }

        public void SetProgress(int percent)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => SetProgress(percent)));
                return;
            }
            if (progressBar != null)
            {
                progressBar.Value = Math.Min(100, Math.Max(0, percent));
            }
        }

        // Nullable event to avoid design-time contract issues
        public event EventHandler? CancelRequested;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
