#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing_Software
{
    public class AccountGroup
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public int? ParentGroupID { get; set; }
        public string GroupType { get; set; } = string.Empty;
    }

    public class LedgerMaster
    {
        public int LedgerID { get; set; }
        public int GroupID { get; set; }
        public string LedgerName { get; set; } = string.Empty;
        public decimal OpeningBalance { get; set; }
        public string BalanceType { get; set; } = string.Empty; // "DR" or "CR"
        public bool IsActive { get; set; } = true;
    }

}
