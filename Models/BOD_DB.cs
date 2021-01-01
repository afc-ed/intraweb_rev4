using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

namespace intraweb_rev3.Models
{
    public class BOD_DB
    {       
        public static DataTable GetProduct(string action)
        {
            SqlConnection conn = new SqlConnection();
            DataTable table = new DataTable();
            try
            {
                conn = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("bod_Product_Get", conn))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    conn.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(table);
                }
                return table;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "BOD_DB.GetProduct()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        public static DataTable Commission(string type, string datestring = "", string storecode = "")
        {
            MySqlConnection mySqlConnection = new MySqlConnection();
            DataTable table = new DataTable();
            try
            {
                mySqlConnection = AFC.DBConnect();
                using (MySqlCommand selectCommand = new MySqlCommand())
                {
                    selectCommand.Connection = mySqlConnection;
                    selectCommand.CommandText = "CALL bod_Commission_Get(@p1, @p2, @p3);";
                    selectCommand.Parameters.AddWithValue("@p1", type);
                    selectCommand.Parameters.AddWithValue("@p2", Convert.ToDateTime(datestring));
                    selectCommand.Parameters.AddWithValue("@p3", !string.IsNullOrEmpty(storecode) ? storecode.ToUpper() : "");
                    mySqlConnection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(selectCommand))
                        mySqlDataAdapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "BOD_DB.Commission()");
            }
            finally
            {
                mySqlConnection?.Close();
                mySqlConnection?.Dispose();
            }
        }



    }
}