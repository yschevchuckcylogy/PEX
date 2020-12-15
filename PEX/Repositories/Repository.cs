using PEX.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PEX.Repositories
{
    public class Repository : IRepository
    {
        private SqlConnection PEXSqlConnection => new SqlConnection(ConfigurationManager.ConnectionStrings["PEX"].ConnectionString);

        public List<Vendor> GetVendors()
        {
            using (SqlConnection connection = PEXSqlConnection)
            {
                var vendors = new List<Vendor>();
                connection.Open();
                using (SqlCommand c = new SqlCommand($"SELECT E.Id FROM [dbo].Vendor AS V", connection))
                {
                    var a = c.ExecuteReader();
                    while (a.Read())
                    {
                        vendors.Add(new Vendor()
                        {
                            VendorID = a.GetString(0).Trim(),
                            //StudentId = a.GetInt32(1),
                            //StudentName = a.GetString(2).Trim(),
                            //StudentLastName = a.GetString(3).Trim(),
                            //Grade = a.GetInt32(4),
                            //SubjectId = a.GetInt32(5),
                            //SubjectTitle = a.GetString(6).Trim(),
                        });
                    }
                }
                return vendors;
            }
        }

    }
}