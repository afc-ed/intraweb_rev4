using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace intraweb_rev3.Models
{
    public class AFCGP
    {
        public static SqlConnection DBConnect()
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["AFCGPConnectionString"].ConnectionString;
                return new SqlConnection(connStr);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFCGP.DBConnect()");
            }
        }


    }
}