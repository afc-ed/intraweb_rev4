using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace intraweb_rev3.Models
{
    public class AFCDB
    {
        public static SqlConnection DBConnect()
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["AFCDBConnectionString"].ConnectionString;
                return new SqlConnection(connStr);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFCDB.DBConnect()");
            }
        }

        public static DataTable GetRow(string szSql)
        {
            SqlConnection conn = new SqlConnection();
            DataTable table = new DataTable();
            try
            {
                conn = AFCDB.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand(szSql, conn))
                {
                    selectCommand.CommandType = CommandType.Text;
                    conn.Open();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFCDB.GetRow()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        public static DataTable GetRowSp(string szSql)
        {
            SqlConnection conn = new SqlConnection();
            DataTable table = new DataTable();
            try
            {
                conn = AFCDB.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand(szSql, conn))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFCDB.GetRowSp()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        public static void ExecuteSql(string szSql)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = AFCDB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand(szSql, conn))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFCDB.ExecuteSql()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        public static void ExecuteSp(string szSql)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = AFCDB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand(szSql, conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFCDB.ExecuteSp()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

    }
}