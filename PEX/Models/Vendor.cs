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
        public float MonthlyPerUserCapDollars
        {
            get
            {
                return (float)MonthlyPerUserCap / 100;
            }
            set { MonthlyPerUserCap = Convert.ToInt64(value * 100); }
        }
        public long MonthlyCap { get; set; }
        public float MonthlyCapDollars => (float)MonthlyCap / 100;
        public bool Enabled { get; set; }


    }
}