using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace intraweb_rev3.Models
{
    public class Ecommerce_DB
    {
        public static SqlConnection DBConnect()
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["EcommerceConnectionString"].ConnectionString;
                return new SqlConnection(connStr);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.DBConnect()");
            }
        }

        public static void ExecuteSql(string szSql)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = Ecommerce_DB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand(szSql, connection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.ExecuteSql()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static void AddUserInput(string type, string item = "", string storecode = "")
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = Ecommerce_DB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("general_UserInput_Insert", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = type;
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = item;
                    sqlCommand.Parameters.Add("@storecode", SqlDbType.VarChar).Value = storecode;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.AddUserInput()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static DataTable ProductGet(
          string action,
          string code = "",
          int classId = 0,
          int productId = 0)
        {
            SqlConnection connection = new SqlConnection();
            DataTable table = new DataTable();
            try
            {
                connection = Ecommerce_DB.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("ecommerce_Product_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@pAction", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@pCode", SqlDbType.VarChar).Value = code;
                    selectCommand.Parameters.Add("@pClassId", SqlDbType.Int).Value = classId;
                    selectCommand.Parameters.Add("@pProductId", SqlDbType.Int).Value = productId;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(table);
                }
                return table;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.ProductGet()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static DataTable CustomerClassGet(string action, string code = "")
        {
            SqlConnection connection = new SqlConnection();
            DataTable table = new DataTable();
            try
            {
                connection = action == "gpCustomer" ? App.DBConnect() : Ecommerce_DB.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("ecommerce_CustomerClass_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@class", SqlDbType.VarChar).Value = code;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(table);
                }
                return table;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.CustomerClassGet()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static void ProductControl(string action, int classId = 0, int productId = 0)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = Ecommerce_DB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("ecommerce_ProductControl_Update", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pAction", SqlDbType.VarChar).Value = action;
                    sqlCommand.Parameters.Add("@pClassId", SqlDbType.Int).Value = classId;
                    sqlCommand.Parameters.Add("@pProductId", SqlDbType.Int).Value = productId;
                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.ProductControl()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        public static DataTable MaintenanceGet(string action)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = (action == "missingContactInGP" ? App.DBConnect() : Ecommerce_DB.DBConnect());
                using (SqlCommand selectCommand = new SqlCommand("ecommerce_Maintenance_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.MaintenanceGet()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static void MaintenanceUpdate(
          string action,
          string customerNo = "",
          string userId = "",
          string customerName = "")
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = (action == "updateMissingContactInGP" ? App.DBConnect() : Ecommerce_DB.DBConnect());
                using (SqlCommand sqlCommand = new SqlCommand("ecommerce_Maintenance_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pAction", SqlDbType.VarChar).Value = action;
                    sqlCommand.Parameters.Add("@pCustomerNo", SqlDbType.VarChar).Value = customerNo;
                    sqlCommand.Parameters.Add("@pUserId", SqlDbType.NVarChar).Value = userId;
                    sqlCommand.Parameters.Add("@pCustomerName", SqlDbType.VarChar).Value = customerName;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.MaintenanceUpdate()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static DataTable UserLoginGet(string action)
        {
            SqlConnection connection = new SqlConnection();
            DataTable table = new DataTable();
            try
            {
                connection = Ecommerce_DB.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("ecommerce_UserLogin_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(table);
                }
                return table;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.UserLoginGet()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static void UserLoginUpdate(string userAccess, string storecode)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = Ecommerce_DB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("ecommerce_UserLogin_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pUserAccess", SqlDbType.VarChar).Value = userAccess;
                    sqlCommand.Parameters.Add("@pStorecode", SqlDbType.VarChar).Value = storecode;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.UserLoginUpdate()" + storecode);
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static void ItemStatusUpdate(Ecommerce_Class.Item item)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = Ecommerce_DB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("ecommerce_ItemStatus_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@code", SqlDbType.VarChar).Value = item.Code;
                    sqlCommand.Parameters.Add("@status", SqlDbType.Int).Value = item.Status == "no" ? 0 : 1;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.ItemStatusUpdate()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static DataTable Analytics(string action)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = Ecommerce_DB.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("ecommerce_Analytics_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.WebAnalytics()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static DataTable ItemResetStatus(string action, Ecommerce_Class.Item item)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = Ecommerce_DB.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("ecommerce_ItemResetStatus_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@id", SqlDbType.Int).Value = item.Id;
                    selectCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = item.Code;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.ItemResetStatus()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static void ItemResetStatusUpdate(string action, Ecommerce_Class.Item item)
        {
            SqlConnection conn = null;
            try
            {
                conn = Ecommerce_DB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("ecommerce_ItemResetStatus_Update", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = item.Code;
                    sqlCommand.Parameters.Add("@description", SqlDbType.VarChar).Value = item.Description;
                    sqlCommand.Parameters.Add("@isactive", SqlDbType.VarChar).Value = item.IsActive;
                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.ItemResetStatusUpdate()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }




    }
}