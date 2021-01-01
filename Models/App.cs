using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace intraweb_rev3.Models
{
    public class App
    {
        public static SqlConnection DBConnect()
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["APPConnectionString"].ConnectionString;
                return new SqlConnection(connStr);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "App.DBConnect()");
            }
        }

        public static DataTable GetRow(string szSql)
        {
            SqlConnection conn = new SqlConnection();
            DataTable table = new DataTable();
            try
            {
                conn = App.DBConnect();
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
                throw Utilities.ErrHandler(ex, "App.GetRow()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        public static DataTable GetRowSp(string szSql)
        {
            SqlConnection conn =  new SqlConnection();
            DataTable table = new DataTable();
            try
            {
                conn = App.DBConnect();
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
                throw Utilities.ErrHandler(ex, "App.GetRowSp()");
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
                conn = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand(szSql, conn))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "App.ExecuteSql()");
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
                conn = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand(szSql, conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "App.ExecuteSp()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        public static void AddUserInput(string type, string item = "", string storecode = "")
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("general_UserInput_Insert", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)type;
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = (object)item;
                    sqlCommand.Parameters.Add("@storecode", SqlDbType.VarChar).Value = (object)storecode;
                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "App.AddUserInput()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }



    }
}