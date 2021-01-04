using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEX.Models
{
    public class ValidateTransactionRequest
    {
        public string UserId { get; set; }
        public string VendorId { get; set; }
        public long TransactionAmount { get; set; }
    }
}