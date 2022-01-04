using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace intraweb_rev3.Models
{
    public class Connect_DB
    {
        public static DataTable Memo(string type, int id=0)
        {
            SqlConnection conn = new SqlConnection();
            DataTable table = new DataTable();
            try
            {
                conn = AFCDB.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("Connect.uspAdminMemoGet", conn))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@Type", SqlDbType.VarChar).Value = type;
                    selectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    conn.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                    {
                        sqlDataAdapter.Fill(table);
                    }
                }
                return table;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Connect_DB.Memo()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        public static int MemoUpdate(string action, Connect_Class.Memo memo)
        {
            SqlConnection connection = new SqlConnection();
            int insertid = 0;
            try
            {
                connection = AFCDB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("Connect.uspAdminMemoUpdate", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = memo.Id;
                    sqlCommand.Parameters.Add("@Title", SqlDbType.VarChar).Value = memo.Title;
                    sqlCommand.Parameters.Add("@PageContent", SqlDbType.VarChar).Value = memo.PageContent;
                    sqlCommand.Parameters.Add("@ActiveFlag", SqlDbType.Int).Value = memo.Active == "yes" ? 1 : 0;
                    connection.Open();
                    if (action == "create")
                    {
                        insertid = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    }
                    else
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    return insertid;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Connect_DB.MemoUpdate()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static void FilterUpdate(Connect_Class.Filter filter, int recordId = 0, string type = "")
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = AFCDB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("Connect.uspAdminFilterUpdate", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Type", SqlDbType.VarChar).Value = type;
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = recordId;
                    sqlCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = filter.State;
                    sqlCommand.Parameters.Add("@Region", SqlDbType.VarChar).Value = filter.Region;
                    sqlCommand.Parameters.Add("@Storegroup", SqlDbType.VarChar).Value = filter.Storegroup;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Legal_DB.ConnectFilterUpdate()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }



    }
}