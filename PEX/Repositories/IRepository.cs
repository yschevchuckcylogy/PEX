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
    }
}