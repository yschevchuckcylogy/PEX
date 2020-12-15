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
        public long MonthlyPerUserCap { get; set; }
        public long MonthlyCap { get; set; }
        public bool Enabled { get; set; }

    }
}