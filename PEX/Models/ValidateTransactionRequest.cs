using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEX.Models
{
    public class ValidateTransactionRequest
    {
        public string userid { get; set; }
        public string vendorid { get; set; }
        public long transactionamount { get; set; }
    }
}