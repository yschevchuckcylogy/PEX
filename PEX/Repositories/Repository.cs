using PEX.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
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
                using (SqlCommand c = new SqlCommand($"SELECT V.VendorID, V.VendorName, V.MonthlyPerUserCap, V.MonthlyCap, V.Enabled FROM [dbo].Vendor AS V", connection))
                {
                    var a = c.ExecuteReader();
                    while (a.Read())
                    {
                        vendors.Add(new Vendor()
                        {
                            VendorID = a.GetString(0).Trim(),
                            VendorName = a.GetString(1).Trim(),
                            MonthlyPerUserCap = a.GetInt64(2),
                            MonthlyCap = a.GetInt64(3),
                            Enabled = a.GetBoolean(4)
                        });
                    }
                }
                return vendors;
            }
        }

        public void UpdateVendor(Vendor vendor)
        {
            using (SqlConnection connection = PEXSqlConnection)
            {
                connection.Open();
                using (SqlCommand c = new SqlCommand($"UPDATE [dbo].[Vendor] SET  VendorName = '{vendor.VendorName}', MonthlyPerUserCap = '{vendor.MonthlyPerUserCap}', Enabled = '{vendor.Enabled}' WHERE VendorID = {vendor.VendorID}", connection))
                {
                    c.ExecuteScalar();
                }
            }
        }

        public List<Transacrion> GetTransactionsByDate(DateTime date)
        {
            using (SqlConnection connection = PEXSqlConnection)
            {
                var transactions = new List<Transacrion>();
                connection.Open();
                using (SqlCommand c = new SqlCommand($"SELECT T.UserID, T.VendorID, T.TransactionAmount, T.TransactionDate FROM [dbo].Transaction AS T WHERE T.TransactionDate = {date}", connection))
                {
                    var a = c.ExecuteReader();
                    while (a.Read())
                    {
                        transactions.Add(new Transacrion()
                        {
                            UserID = a.GetString(0).Trim(),
                            VendorID = a.GetString(1).Trim(),
                            TransactionAmount = a.GetInt64(2),
                            TransactionDate = a.GetDateTime(3),
                        });
                    }
                }
                return transactions;
            }
        }

        public Vendor GetVendorById(string vendorId)
        {
            using (SqlConnection connection = PEXSqlConnection)
            {
                var vendor = new Vendor();
                connection.Open();
                using (SqlCommand c = new SqlCommand($"SELECT V.VendorID, V.VendorName, V.MonthlyPerUserCap, V.MonthlyCap, V.Enabled FROM [dbo].Vendor AS V WHERE V.VendorID = '{vendorId}'", connection))
                {
                    var a = c.ExecuteReader();
                    if (a.Read())
                    {
                        vendor.VendorID = a.GetString(0).Trim();
                        vendor.VendorName = a.GetString(1).Trim();
                        vendor.MonthlyPerUserCap = a.GetInt64(2);
                        vendor.MonthlyCap = a.GetInt64(3);
                        vendor.Enabled = a.GetBoolean(4);
                    }

                }
                return vendor;
            }
        }

        public long GetTransactionSum(string vendorId, string userId, DateTime date)
        {
            using (SqlConnection connection = PEXSqlConnection)
            {
                long sum = 0;
                connection.Open();
                using (SqlCommand c = 
                    new SqlCommand($"SELECT SUM(T.TransactionAmount) FROM [dbo].Vendor AS V INNER JOIN[dbo].[Transaction] T ON(V.VendorID = T.VendorID) WHERE V.VendorID = '{vendorId}' AND T.UserID = '{userId}' AND YEAR(T.TransactionDate) = '{date.Year}' AND MONTH(T.TransactionDate) = '{date.Month}'", connection))
                {
                    var a = c.ExecuteReader();
                    if (a.Read())
                    {
                        sum = a.GetInt64(0);
                    }

                }
                return sum;
            }
        }

        public ValidateTransactionResponse InsertTranzaction(ValidateTransactionRequest transaction)
        {
            using (SqlConnection connection = PEXSqlConnection)
            {
                var t = new ValidateTransactionResponse();
                connection.Open();
                using (SqlCommand c = new SqlCommand($"INSERT INTO [dbo].[Transaction] OUTPUT Inserted.UserId, Inserted.VendorID, Inserted.TransactionDate, Inserted.TransactionAmount VALUES('{transaction.userid}', '{transaction.vendorid}', {transaction.transactionamount}, GETDATE())", connection))
                {
                    var a = c.ExecuteReader();
                    if (a.Read())
                    {
                        t.userId= a.GetString(0).Trim();
                        t.vendorId = a.GetString(1).Trim();
                        t.transactionDateTime = a.GetDateTime(2).ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", CultureInfo.InvariantCulture); ;
                        t.transactionAmount = a.GetInt64(3);
                    }
                }
                return t;
            }
        }

    }
}