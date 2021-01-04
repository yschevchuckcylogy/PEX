using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PEX.Models
{
    public class DenialReasons
    {
        public static string TransactionOverMonthlyLimit => "Transaction over monthly limit";
        public static string VendorNotEnabled => "Vendor not enabled";
        public static string VendorNotFound => "Vendor not found";
    }
}