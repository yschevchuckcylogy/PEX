using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEX.Models
{
    public class ValidateTransactionResponse
    {
        public string TransactionDateTime { get; set; }
        public string UserId { get; set; }
        public string VendorId { get; set; }
        public long TransactionAmount { get; set; }
        public bool Approved { get; set; }
        public long CurrentMonthUserSpend { get; set; }
        public string DenialReason { get; set; }
    }
}