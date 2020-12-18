using PEX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEX.Repositories
{
    public interface IRepository
    {
        List<Vendor> GetVendors();
        void UpdateVendor(Vendor vendor);
        Vendor GetVendorById(string vendorId);
        long GetTransactionSum(string vendorId, string userId, DateTime date);
        ValidateTransactionResponse InsertTranzaction(ValidateTransactionRequest transaction);
    }
}