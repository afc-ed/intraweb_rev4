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
                    sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = action;
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = memo.Id;
                    sqlCommand.Parameters.Add("@Title", SqlDbType.VarChar).Value = memo.Title;
                    sqlCommand.Parameters.Add("@PageContent", SqlDbType.VarChar).Value = memo.PageContent;
                    sqlCommand.Parameters.Add("@ActiveFlag", SqlDbType.Int).Value = memo.Active;
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

        public static DataTable FilterGrid(Connect_Class.Filter filter)
        {
            SqlConnection conn = new SqlConnection();
            DataTable table = new DataTable();
            try
            {
                conn = AFCDB.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("Connect.uspAdminFilterGet", conn))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@Type", SqlDbType.VarChar).Value = filter.Type;
                    selectCommand.Parameters.Add("@RegionId", SqlDbType.VarChar).Value = filter.RegionId ?? "";
                    selectCommand.Parameters.Add("@StateId", SqlDbType.VarChar).Value = filter.StateId ?? "";
                    selectCommand.Parameters.Add("@StoreGroupId", SqlDbType.VarChar).Value = filter.StoregroupId ?? "";
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
                throw Utilities.ErrHandler(ex, "Connect_DB.Filter()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        public static void FilterUpdate(Connect_Class.Filter filter)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = AFCDB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("Connect.uspAdminFilterUpdate", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Type", SqlDbType.VarChar).Value = filter.Parent;
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = filter.Id;
                    sqlCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = filter.State ?? "";
                    sqlCommand.Parameters.Add("@Region", SqlDbType.VarChar).Value = filter.Region ?? "";
                    sqlCommand.Parameters.Add("@Storegroup", SqlDbType.VarChar).Value = filter.Storegroup ?? "";
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

        public static DataTable Announcement(string type, int id = 0)
        {
            SqlConnection conn = new SqlConnection();
            DataTable table = new DataTable();
            try
            {
                conn = AFCDB.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("Connect.uspAdminAnnouncementGet", conn))
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
                throw Utilities.ErrHandler(ex, "Connect_DB.Announcement()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        public static int AnnouncementUpdate(string action, Connect_Class.Announcement announcement)
        {
            SqlConnection connection = new SqlConnection();
            int insertid = 0;
            try
            {
                connection = AFCDB.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("Connect.uspAdminAnnouncementUpdate", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = action;
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = announcement.Id;
                    sqlCommand.Parameters.Add("@Title", SqlDbType.VarChar).Value = announcement.Title;
                    sqlCommand.Parameters.Add("@PageContent", SqlDbType.VarChar).Value = announcement.PageContent;
                    sqlCommand.Parameters.Add("@ActiveFlag", SqlDbType.Int).Value = announcement.Active;
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
                throw Utilities.ErrHandler(ex, "Connect_DB.AnnouncementUpdate()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }






    }
}