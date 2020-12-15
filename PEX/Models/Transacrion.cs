using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEX.Models
{
    public class Transacrion
    {
        public string USerID { get; set; }
        public string VendorID { get; set; }
        public int TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}