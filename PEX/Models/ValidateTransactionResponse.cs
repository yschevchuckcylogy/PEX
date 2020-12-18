using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEX.Models
{
    public class ValidateTransactionResponse
    {
        public string transactionDateTime { get; set; }
        public string userId { get; set; }
        public string vendorId { get; set; }
        public long transactionAmount { get; set; }
        public bool approved { get; set; }
        public long currentMonthUserSpend { get; set; }
    }
}