#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Billing_Software
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void MainWithArgs(string[] args)
        {
            var importer = new AccountingImportEngine();

            // Pass Excel path configuration layout
            string excelPath = @"C:\Imports\LedgerMasterSetup.xlsx";
            importer.ImportLedgersFromFile(excelPath);

            // Pass CSV configuration layout (EPPlus handles CSV text parsing out of the box)
            string csvPath = @"C:\Imports\LedgerMasterSetup.csv";
            importer.ImportLedgersFromFile(csvPath);
        }
    }
}
