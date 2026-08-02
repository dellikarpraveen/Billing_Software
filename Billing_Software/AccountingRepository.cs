#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Billing_Software
{
    public class AccountingRepository
    {
        private readonly string _connectionString = "BillingSoftwareDB";

        // 1. Create a single new ledger account
        public void CreateLedger(LedgerMaster ledger)
        {
            string query = @"INSERT INTO LedgerMasters (GroupID, LedgerName, OpeningBalance, BalanceType, IsActive) 
                         VALUES (@GroupID, @LedgerName, @OpeningBalance, @BalanceType, @IsActive);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GroupID", ledger.GroupID);
                    cmd.Parameters.AddWithValue("@LedgerName", ledger.LedgerName);
                    cmd.Parameters.AddWithValue("@OpeningBalance", ledger.OpeningBalance);
                    cmd.Parameters.AddWithValue("@BalanceType", ledger.BalanceType);
                    cmd.Parameters.AddWithValue("@IsActive", ledger.IsActive);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 2. Fast Bulk Import Engine for processing lists of ledgers
        public void BulkImportLedgers(List<LedgerMaster> ledgerList)
        {
            // Convert List to DataTable structure required for Bulk Copy
            DataTable table = new DataTable();
            table.Columns.Add("GroupID", typeof(int));
            table.Columns.Add("LedgerName", typeof(string));
            table.Columns.Add("OpeningBalance", typeof(decimal));
            table.Columns.Add("BalanceType", typeof(string));
            table.Columns.Add("IsActive", typeof(bool));

            foreach (var item in ledgerList)
            {
                table.Rows.Add(item.GroupID, item.LedgerName, item.OpeningBalance, item.BalanceType, item.IsActive);
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                {
                    bulkCopy.DestinationTableName = "LedgerMasters";

                    // Explicit schema mapping
                    bulkCopy.ColumnMappings.Add("GroupID", "GroupID");
                    bulkCopy.ColumnMappings.Add("LedgerName", "LedgerName");
                    bulkCopy.ColumnMappings.Add("OpeningBalance", "OpeningBalance");
                    bulkCopy.ColumnMappings.Add("BalanceType", "BalanceType");
                    bulkCopy.ColumnMappings.Add("IsActive", "IsActive");

                    bulkCopy.WriteToServer(table);
                }
            }
        }
    }

}
