#nullable enable
namespace Billing_Software
{
    partial class PreviewForm
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
            this.listViewRows = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.lstErrors = new System.Windows.Forms.ListBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewRows
            // 
            this.listViewRows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewRows.FullRowSelect = true;
            this.listViewRows.GridLines = true;
            this.listViewRows.HideSelection = false;
            this.listViewRows.Location = new System.Drawing.Point(12, 12);
            this.listViewRows.Name = "listViewRows";
            this.listViewRows.Size = new System.Drawing.Size(760, 300);
            this.listViewRows.TabIndex = 0;
            this.listViewRows.UseCompatibleStateImageBehavior = false;
            this.listViewRows.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "SourceId";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "GroupName";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ParentSourceId";
            this.columnHeader3.Width = 90;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "GroupType";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "LedgerName";
            this.columnHeader5.Width = 140;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "CreateLedger";
            this.columnHeader6.Width = 80;
            // 
            // lstErrors
            // 
            this.lstErrors.FormattingEnabled = true;
            this.lstErrors.ItemHeight = 16;
            this.lstErrors.Location = new System.Drawing.Point(12, 320);
            this.lstErrors.Name = "lstErrors";
            this.lstErrors.Size = new System.Drawing.Size(760, 84);
            this.lstErrors.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(616, 412);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 30);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(697, 412);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PreviewForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 454);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lstErrors);
            this.Controls.Add(this.listViewRows);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CSV Preview";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListView? listViewRows;
        private System.Windows.Forms.ColumnHeader? columnHeader1;
        private System.Windows.Forms.ColumnHeader? columnHeader2;
        private System.Windows.Forms.ColumnHeader? columnHeader3;
        private System.Windows.Forms.ColumnHeader? columnHeader4;
        private System.Windows.Forms.ColumnHeader? columnHeader5;
        private System.Windows.Forms.ColumnHeader? columnHeader6;
        private System.Windows.Forms.ListBox? lstErrors;
        private System.Windows.Forms.Button? btnOk;
        private System.Windows.Forms.Button? btnCancel;
    }
}
