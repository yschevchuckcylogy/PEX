using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEX.Models
{
    public class Transacrion
    {
        public string UserID { get; set; }
        public string VendorID { get; set; }
        public long TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}