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
                int insertedId = 0;
                if (!string.IsNullOrEmpty(classId))
                {
                    conn = App.DBConnect();
                    CustomerClassInsertCall(conn, classId);
                    conn = Ecommerce_DB.DBConnect();
                    insertedId = CustomerClassInsertCall(conn, classId);
                }
                return insertedId;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "RnD_DB.CustomerClassInsert()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        private static int CustomerClassInsertCall(SqlConnection conn, string classId)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand("rnd_CustomerClass_Insert", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pClassId", SqlDbType.VarChar).Value = classId;
                    conn.Open();
                    return (int)sqlCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "RnD_DB.CustomerClassInsertCall()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        public static void CustomerClassDelete(int classId = 0, string className = "")
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = App.DBConnect();
                CustomerClassDeleteCall(conn, classId, className);
                conn = Ecommerce_DB.DBConnect();
                CustomerClassDeleteCall(conn, classId, className);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "RnD_DB.CustomerClassDelete()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        private static void CustomerClassDeleteCall(SqlConnection conn, int classId = 0, string className = "")
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand("rnd_CustomerClass_Delete", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@classId", SqlDbType.Int).Value = classId;
                    sqlCommand.Parameters.Add("@className", SqlDbType.VarChar).Value = className;
                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "RnD_DB.CustomerClassDeleteCall()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        public static void CustomerClassUpdate(int classId, string className, string storecode)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = App.DBConnect();
                CustomerClassUpdateCall(conn, classId, className, storecode);
                conn = Ecommerce_DB.DBConnect();
                CustomerClassUpdateCall(conn, classId, className, storecode);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "RnD_DB.CustomerClassUpdate()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        private static void CustomerClassUpdateCall(SqlConnection conn, int classId, string className, string storecode)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand("rnd_CustomerClass_Update", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@classId", SqlDbType.Int).Value = classId;
                    sqlCommand.Parameters.Add("@className", SqlDbType.VarChar).Value = className;
                    sqlCommand.Parameters.Add("@storecode", SqlDbType.VarChar).Value = storecode;
                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "RnD_DB.CustomerClassUpdateCall()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }

        public static DataTable GetClass(string action)
        {
            SqlConnection conn = new SqlConnection();
            DataTable table = new DataTable();
            try
            {
                conn = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("rnd_Class_Get", conn))
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
                throw Utilities.ErrHandler(ex, "RnD_DB.GetClass()");
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }




    }
}