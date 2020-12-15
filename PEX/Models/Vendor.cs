using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEX.Models
{
    public class Vendor
    {
        public string VendorID {get; set;}
        public string VendorName { get; set; }
        public int MonthlyPerUserCap { get; set; }
        public int MonthlyCap { get; set; }
        public bool Enabled { get; set; }

    }
}