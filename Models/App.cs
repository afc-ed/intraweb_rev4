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
            SqlConnection connection = (SqlConnection)null;
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand(szSql, connection))
                {
                    selectCommand.CommandType = CommandType.Text;
                    connection.Open();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "App.GetRow()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable GetRowSp(string szSql)
        {
            SqlConnection connection = (SqlConnection)null;
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand(szSql, connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "App.GetRowSp()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static void ExecuteSql(string szSql)
        {
            SqlConnection connection = (SqlConnection)null;
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand(szSql, connection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "App.ExecuteSql()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static void ExecuteSp(string szSql)
        {
            SqlConnection connection = (SqlConnection)null;
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand(szSql, connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "App.ExecuteSp()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static void AddUserInput(string type, string item = "", string storecode = "")
        {
            SqlConnection connection = (SqlConnection)null;
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("general_UserInput_Insert", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)type;
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = (object)item;
                    sqlCommand.Parameters.Add("@storecode", SqlDbType.VarChar).Value = (object)storecode;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "App.AddUserInput()");
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}