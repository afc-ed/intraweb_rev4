using System;
using System.Data;
using System.Data.SqlClient;

namespace intraweb_rev3.Models
{
    public class Ecommerce_DB
    {
        public static SqlConnection DBConnect()
        {
            try
            {
                return new SqlConnection("server=10.100.1.30; database=APP; user id=sa; password=Afc1desu;");
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
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)type;
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = (object)item;
                    sqlCommand.Parameters.Add("@storecode", SqlDbType.VarChar).Value = (object)storecode;
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
            }
        }

        public static DataTable ProductGet(
          string action,
          string code = "",
          int classId = 0,
          int productId = 0)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = Ecommerce_DB.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("ecommerce_Product_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@pAction", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@pCode", SqlDbType.VarChar).Value = (object)code;
                    selectCommand.Parameters.Add("@pClassId", SqlDbType.Int).Value = (object)classId;
                    selectCommand.Parameters.Add("@pProductId", SqlDbType.Int).Value = (object)productId;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.ProductGet()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable CustomerClassGet(string action, string code = "")
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = action == "gpCustomer" ? App.DBConnect() : Ecommerce_DB.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("ecommerce_CustomerClass_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@class", SqlDbType.VarChar).Value = (object)code;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.CustomerClassGet()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static void ProductControl(string action, int classId = 0, int productId = 0)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = Ecommerce_DB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("ecommerce_ProductControl_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pAction", SqlDbType.VarChar).Value = (object)action;
                    sqlCommand.Parameters.Add("@pClassId", SqlDbType.Int).Value = (object)classId;
                    sqlCommand.Parameters.Add("@pProductId", SqlDbType.Int).Value = (object)productId;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.ProductControl()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable MaintenanceGet(string action)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = action == "missingContactInGP" ? App.DBConnect() : Ecommerce_DB.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("ecommerce_Maintenance_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
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
                connection = action == "updateMissingContactInGP" ? App.DBConnect() : Ecommerce_DB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("ecommerce_Maintenance_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pAction", SqlDbType.VarChar).Value = (object)action;
                    sqlCommand.Parameters.Add("@pCustomerNo", SqlDbType.VarChar).Value = (object)customerNo;
                    sqlCommand.Parameters.Add("@pUserId", SqlDbType.NVarChar).Value = (object)userId;
                    sqlCommand.Parameters.Add("@pCustomerName", SqlDbType.VarChar).Value = (object)customerName;
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
            }
        }

        public static DataTable UserLoginGet(string action)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = Ecommerce_DB.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("ecommerce_UserLogin_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.UserLoginGet()");
            }
            finally
            {
                connection?.Close();
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
                    sqlCommand.Parameters.Add("@pUserAccess", SqlDbType.VarChar).Value = (object)userAccess;
                    sqlCommand.Parameters.Add("@pStorecode", SqlDbType.VarChar).Value = (object)storecode;
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
                    sqlCommand.Parameters.Add("@code", SqlDbType.VarChar).Value = (object)item.Code;
                    sqlCommand.Parameters.Add("@status", SqlDbType.Int).Value = (object)(item.Status == "no" ? 0 : 1);
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
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
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
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@id", SqlDbType.Int).Value = (object)item.Id;
                    selectCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = (object)item.Code;
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
            }
        }

        public static void ItemResetStatusUpdate(string action, Ecommerce_Class.Item item)
        {
            SqlConnection connection = (SqlConnection)null;
            try
            {
                connection = Ecommerce_DB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("ecommerce_ItemResetStatus_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = (object)item.Code;
                    sqlCommand.Parameters.Add("@description", SqlDbType.VarChar).Value = (object)item.Description;
                    sqlCommand.Parameters.Add("@isactive", SqlDbType.VarChar).Value = (object)item.IsActive;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Ecommerce_DB.ItemResetStatusUpdate()");
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}