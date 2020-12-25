using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

namespace intraweb_rev3.Models
{
    public class BOD_DB
    {        public static DataTable GetProduct(string action)
        {
            SqlConnection connection = (SqlConnection)null;
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("bod_Product_Get", connection))
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
                throw Utilities.ErrHandler(ex, "BOD_DB.GetProduct()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable Commission(string type, string datestring = "", string storecode = "")
        {
            MySqlConnection mySqlConnection = (MySqlConnection)null;
            DataTable dataTable = new DataTable();
            try
            {
                mySqlConnection = AFC.DBConnect();
                using (MySqlCommand selectCommand = new MySqlCommand())
                {
                    selectCommand.Connection = mySqlConnection;
                    selectCommand.CommandText = "CALL bod_Commission_Get(@p1, @p2, @p3);";
                    selectCommand.Parameters.AddWithValue("@p1", (object)type);
                    selectCommand.Parameters.AddWithValue("@p2", (object)Convert.ToDateTime(datestring));
                    selectCommand.Parameters.AddWithValue("@p3", !string.IsNullOrEmpty(storecode) ? (object)storecode.ToUpper() : (object)"");
                    mySqlConnection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(selectCommand))
                        mySqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "BOD_DB.Commission()");
            }
            finally
            {
                mySqlConnection?.Close();
            }
        }
    }
}