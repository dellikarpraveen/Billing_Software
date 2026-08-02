#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Billing_Software
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
        }

        private void ll_MasterInventory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open Form3 when the link is clicked. If an instance already exists, bring it to front.
            var existing = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f is Form3) as Form3;
            if (existing != null)
            {
                try { existing.BringToFront(); existing.Focus(); }
                catch { }
                return;
            }

            var f3 = new Form3();
            // If a Navigator is initialized, use it to switch main forms; otherwise just show Form3.
            try
            {
                // Use Navigator if available to manage main form switching.
                var navType = typeof(Navigator);
                // If Navigator has been initialized elsewhere, call SwitchTo.
                Navigator.SwitchTo(f3);
            }
            catch
            {
                // Fallback: show the form normally
                f3.Show();
            }
        }
    }
}
