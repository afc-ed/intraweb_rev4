using System;
using System.Data;
using System.Data.SqlClient;

namespace intraweb_rev3.Models
{
    public class RnD_DB
    {
        public static int CustomerClassInsert(string classId)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                int num = 0;
                if (!string.IsNullOrEmpty(classId))
                {
                    conn = App.DBConnect();
                    RnD_DB.CustomerClassInsertCall(conn, classId);
                    conn = Ecommerce_DB.DBConnect();
                    num = RnD_DB.CustomerClassInsertCall(conn, classId);
                }
                return num;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "RnD_DB.CustomerClassInsert()");
            }
            finally
            {
                conn?.Close();
            }
        }

        private static int CustomerClassInsertCall(SqlConnection conn, string classId)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand("rnd_CustomerClass_Insert", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pClassId", SqlDbType.VarChar, 15).Value = (object)classId;
                    conn.Open();
                    return (int)sqlCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "RnD_DB.CustomerClassInsertCall()");
            }
        }

        public static void CustomerClassDelete(int classId = 0, string className = "")
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = App.DBConnect();
                RnD_DB.CustomerClassDeleteCall(conn, classId, className);
                conn = Ecommerce_DB.DBConnect();
                RnD_DB.CustomerClassDeleteCall(conn, classId, className);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "RnD_DB.CustomerClassDelete()");
            }
            finally
            {
                conn?.Close();
            }
        }

        private static void CustomerClassDeleteCall(SqlConnection conn, int classId = 0, string className = "")
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand("rnd_CustomerClass_Delete", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@classId", SqlDbType.Int).Value = (object)classId;
                    sqlCommand.Parameters.Add("@className", SqlDbType.VarChar).Value = (object)className;
                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "RnD_DB.CustomerClassDeleteCall()");
            }
        }

        public static void CustomerClassUpdate(int classId, string className, string storecode)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = App.DBConnect();
                RnD_DB.CustomerClassUpdateCall(conn, classId, className, storecode);
                conn = Ecommerce_DB.DBConnect();
                RnD_DB.CustomerClassUpdateCall(conn, classId, className, storecode);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "RnD_DB.CustomerClassUpdate()");
            }
            finally
            {
                conn?.Close();
            }
        }

        private static void CustomerClassUpdateCall(
          SqlConnection conn,
          int classId,
          string className,
          string storecode)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand("rnd_CustomerClass_Update", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@classId", SqlDbType.Int).Value = (object)classId;
                    sqlCommand.Parameters.Add("@className", SqlDbType.VarChar).Value = (object)className;
                    sqlCommand.Parameters.Add("@storecode", SqlDbType.VarChar).Value = (object)storecode;
                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "RnD_DB.CustomerClassUpdateCall()");
            }
        }

        public static DataTable GetClass(string action)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("rnd_Class_Get", connection))
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
                throw Utilities.ErrHandler(ex, "RnD_DB.GetClass()");
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}