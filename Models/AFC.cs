using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

namespace intraweb_rev3.Models
{
    public class AFC
    {
        public static MySqlConnection DBConnect()
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["AFCConnectionString"].ConnectionString;
                return new MySqlConnection(connStr);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFC.DBConnect()");
            }
        }

        public static DataTable GetRow(string sql)
        {
            MySqlConnection connection = (MySqlConnection)null;
            DataTable dataTable = new DataTable();
            try
            {
                connection = AFC.DBConnect();
                using (MySqlCommand selectCommand = new MySqlCommand(sql, connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(selectCommand))
                    {
                        mySqlDataAdapter.Fill(dataTable);
                    }
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFC.GetRow()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static DataTable QueryRow(string sql)
        {
            MySqlConnection connection = (MySqlConnection)null;
            DataTable dataTable = new DataTable();
            try
            {
                connection = AFC.DBConnect();
                using (MySqlCommand selectCommand = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(selectCommand))
                    {
                        mySqlDataAdapter.Fill(dataTable);
                    }
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFC.GetRow()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static void ExecuteSql(string szSql)
        {
            MySqlConnection connection = (MySqlConnection)null;
            try
            {
                connection = AFC.DBConnect();
                using (MySqlCommand mySqlCommand = new MySqlCommand(szSql, connection))
                {
                    mySqlCommand.CommandType = CommandType.Text;
                    connection.Open();
                    mySqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFC.ExecuteSql()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static DataTable GetStoreRecallInfo(string storecode)
        {
            MySqlConnection connection = (MySqlConnection)null;
            DataTable dataTable = new DataTable();
            try
            {
                connection = AFC.DBConnect();
                using (MySqlCommand selectCommand = new MySqlCommand("RecallItem", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("pType", MySqlDbType.VarChar, 30).Value = (object)"store";
                    selectCommand.Parameters.Add("pStorecode", MySqlDbType.VarChar, 50).Value = (object)storecode;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(selectCommand))
                        mySqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFC.GetStoreInfo()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static string GetStoreRegion(string storecode)
        {
            try
            {
                string region = "";
                DataTable dataTable = AFC.QueryRow("select rg.RegionShorten from Store as st inner join Region as rg on st.RegionID = rg.RegionID where st.storecode = '" + storecode + "'");
                if (dataTable.Rows.Count > 0)
                    region = dataTable.Rows[0]["RegionShorten"].ToString();
                return region;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFC.GetStoreRegion()");
            }
        }

        public static DataTable GetRegions()
        {
            try
            {
                return AFC.QueryRow("select RegionID, RegionShorten, RegionName from Region where regionactiveflag <> 0 and regioncountry like 'usa' order by RegionShorten asc");
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFC.GetRegions()");
            }
        }
        // returns storecode and region id for all current regions in usa.
        public static DataTable GetAllUSAStoreRegions()
        {
            try
            {
                return AFC.QueryRow("select st.Storecode, re.RegionShorten from Store as st inner join Region as re " +
                    "on st.RegionID = re.RegionID where re.regionactiveflag <> 0 and re.regioncountry like 'usa' " +
                    "order by st.Storecode asc");
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFC.GetRegions()");
            }
        }

        public static DataTable GetStoreList()
        {
            try
            {
                return AFC.QueryRow("select st.storecode, st.name from Store as st where st.openflag <> 0 order by st.name asc");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error getting data from AFC database.", ex.Message.ToString()));
            }
        }

        public static DataTable GetStates()
        {
            try
            {
                return AFC.QueryRow("select StateID, StateName from State where Country like 'usa' order by StateID asc");
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFC.GetStates()");
            }
        }

        public static Distribution_Class.StoreSalesOrder GetFranchiseeInfoForSalesOrder(Distribution_Class.StoreSalesOrder storeSalesOrder)
        {
            try
            {
                storeSalesOrder.FCPhone = "NA";
                storeSalesOrder.FCId = "NA";
                DataTable dataTable = AFC.QueryRow("SELECT pe.Cellarphone, pe.Homephone, fr.OldId, pe.Scndphone FROM Store as st INNER JOIN Person as pe on st.PersonID = pe.PersonID INNER JOIN Franchisee as fr on pe.PersonID = fr.PersonID WHERE st.StoreCode = '" + storeSalesOrder.Code + "'");
                if (dataTable.Rows.Count > 0)
                {
                    storeSalesOrder.FCId = dataTable.Rows[0]["oldid"].ToString();
                    string str = dataTable.Rows[0]["cellarphone"].ToString().Trim();
                    if (string.IsNullOrEmpty(str))
                        str = dataTable.Rows[0]["homephone"].ToString().Trim();
                    if (string.IsNullOrEmpty(str))
                        str = dataTable.Rows[0]["scndphone"].ToString().Trim();
                    storeSalesOrder.FCPhone = Utilities.FormatPhone(str);
                }
                return storeSalesOrder;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFC.GetFranchiseeInfoForSalesOrder()");
            }
        }

        public static void SafewayItemInsert(RnD_Class.Safeway safeway)
        {
            MySqlConnection connection = (MySqlConnection)null;
            try
            {
                connection = AFC.DBConnect();
                using (MySqlCommand mySqlCommand = new MySqlCommand())
                {
                    mySqlCommand.Connection = connection;
                    mySqlCommand.CommandText = "CALL SafewayItemMovementInsert(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13);";
                    mySqlCommand.Parameters.AddWithValue("@p1", (object)safeway.Storenumber);
                    mySqlCommand.Parameters.AddWithValue("@p2", (object)safeway.Salesdate);
                    mySqlCommand.Parameters.AddWithValue("@p3", (object)safeway.UPC);
                    mySqlCommand.Parameters.AddWithValue("@p4", (object)safeway.Item);
                    mySqlCommand.Parameters.AddWithValue("@p5", (object)safeway.Quantity);
                    mySqlCommand.Parameters.AddWithValue("@p6", (object)safeway.Gross);
                    mySqlCommand.Parameters.AddWithValue("@p7", (object)safeway.Markdown);
                    mySqlCommand.Parameters.AddWithValue("@p8", (object)safeway.Net);
                    mySqlCommand.Parameters.AddWithValue("@p9", (object)safeway.Category);
                    mySqlCommand.Parameters.AddWithValue("@p10", (object)safeway.Storecode);
                    mySqlCommand.Parameters.AddWithValue("@p11", (object)safeway.Region);
                    mySqlCommand.Parameters.AddWithValue("@p12", (object)safeway.Division);
                    mySqlCommand.Parameters.AddWithValue("@p13", (object)safeway.Brand);
                    connection.Open();
                    mySqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFC.SafewayItemInsert()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static void SafewayItemRecode(RnD_Class.Safeway safeway)
        {
            MySqlConnection connection = (MySqlConnection)null;
            try
            {
                connection = AFC.DBConnect();
                using (MySqlCommand mySqlCommand = new MySqlCommand())
                {
                    mySqlCommand.Connection = connection;
                    mySqlCommand.CommandText = "CALL SafewayItemMovementItemRecode(@p1, @p2, @p3, @p4);";
                    mySqlCommand.Parameters.AddWithValue("@p1", (object)safeway.Storecode);
                    mySqlCommand.Parameters.AddWithValue("@p2", (object)safeway.Salesdate);
                    mySqlCommand.Parameters.AddWithValue("@p3", (object)safeway.UPC);
                    mySqlCommand.Parameters.AddWithValue("@p4", (object)safeway.Category);
                    connection.Open();
                    mySqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFC.SafewayItemRecode()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static void SafewayDeleteByDate(RnD_Class.Safeway safeway)
        {
            MySqlConnection connection = (MySqlConnection)null;
            DataTable dataTable = new DataTable();
            try
            {
                connection = AFC.DBConnect();
                using (MySqlCommand mySqlCommand = new MySqlCommand("CALL SafewayItemMovement('delete_by_date', @p1, @p2, '');", connection))
                {
                    mySqlCommand.Parameters.AddWithValue("@p1", (object)safeway.Startdate);
                    mySqlCommand.Parameters.AddWithValue("@p2", (object)safeway.Enddate);
                    connection.Open();
                    mySqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFC.SafewayDeleteByDate()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static void TunaShipQtyUpdate(Distribution_Class.TunaShip tuna)
        {
            MySqlConnection connection = (MySqlConnection)null;
            try
            {
                connection = AFC.DBConnect();
                using (MySqlCommand selectCommand = new MySqlCommand("Distribution_TunaShipQtyUpdate", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("pId", MySqlDbType.Int32).Value = tuna.ID;
                    selectCommand.Parameters.Add("pQty", MySqlDbType.Int32).Value = tuna.QtyEntered;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AFC.TunaShipQtyUpdate()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }






    }
}