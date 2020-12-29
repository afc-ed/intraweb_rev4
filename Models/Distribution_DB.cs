using System;
using System.Data;
using System.Data.SqlClient;

namespace intraweb_rev3.Models
{
    public class Distribution_DB
    {
        public static DataTable Item(string action, string item = "", string lot = "", string location = "")
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_Item_Get_Rev2", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar, 50).Value = (object)action;
                    selectCommand.Parameters.Add("@item", SqlDbType.VarChar, 50).Value = (object)item;
                    selectCommand.Parameters.Add("@lot", SqlDbType.VarChar, 50).Value = (object)lot;
                    selectCommand.Parameters.Add("@location", SqlDbType.VarChar, 50).Value = (object)location;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.GetItem()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static DataTable Sales(
          string action,
          string type = "",
          string item = "",
          string start = "",
          string end = "",
          int uomqty = 0,
          string uom = "",
          string location = "")
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_Sales_Get_Rev2", connection))
                {
                    selectCommand.CommandTimeout = 540;
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@pAction", SqlDbType.VarChar, 50).Value = (object)action;
                    selectCommand.Parameters.Add("@pType", SqlDbType.VarChar, 20).Value = (object)type;
                    selectCommand.Parameters.Add("@pItem", SqlDbType.VarChar, 20).Value = (object)item;
                    selectCommand.Parameters.Add("@pStart", SqlDbType.VarChar, 15).Value = (object)start;
                    selectCommand.Parameters.Add("@pEnd", SqlDbType.VarChar, 15).Value = (object)end;
                    selectCommand.Parameters.Add("@pUomQty", SqlDbType.Int).Value = (object)uomqty;
                    selectCommand.Parameters.Add("@pUOM", SqlDbType.VarChar, 20).Value = (object)uom;
                    selectCommand.Parameters.Add("@location", SqlDbType.VarChar, 50).Value = (object)location;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.GetSales()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static DataTable BatchPicklist(
          string action,
          string batchId = "",
          string type = "",
          string orderNo = "")
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_BatchPicklist_Get_1", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@batchId", SqlDbType.VarChar).Value = (object)batchId;
                    selectCommand.Parameters.Add("@type", SqlDbType.VarChar).Value = (object)type;
                    selectCommand.Parameters.Add("@orderNo", SqlDbType.VarChar).Value = (object)orderNo;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.BatchPicklist()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static DataTable BatchOrder(string action, string id = "")
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_BatchOrder_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@id", SqlDbType.VarChar).Value = (object)id;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.BatchOrder()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static void BatchOrderUpdate(string action, string orderNo = "", string batchId = "")
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_BatchOrder_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = (object)action;
                    sqlCommand.Parameters.Add("@OrderNo", SqlDbType.VarChar).Value = (object)orderNo;
                    sqlCommand.Parameters.Add("@BatchId", SqlDbType.VarChar).Value = (object)batchId;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.BatchOrderUpdate()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static DataTable Promo(
          string action,
          int id = 0,
          string storecode = "",
          string state = "",
          int byStoreFlag = 0)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_Promo_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@id", SqlDbType.Int).Value = (object)id;
                    selectCommand.Parameters.Add("@storecode", SqlDbType.VarChar).Value = (object)storecode;
                    selectCommand.Parameters.Add("@state", SqlDbType.VarChar).Value = (object)state;
                    selectCommand.Parameters.Add("@byStoreFlag", SqlDbType.Int).Value = (object)byStoreFlag;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.GetPromo()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static int PromoUpdate(string action, Distribution_Class.Promo promo)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            int num = 0;
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_Promo_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = (object)promo.Id;
                    sqlCommand.Parameters.Add("@start", SqlDbType.VarChar).Value = (object)promo.Startdate;
                    sqlCommand.Parameters.Add("@end", SqlDbType.VarChar).Value = (object)promo.Enddate;
                    sqlCommand.Parameters.Add("@prefix", SqlDbType.VarChar).Value = string.IsNullOrEmpty(promo.Storeprefix) ? (object)"" : (object)promo.Storeprefix.ToUpper();
                    sqlCommand.Parameters.Add("@description", SqlDbType.VarChar).Value = (object)promo.Description;
                    sqlCommand.Parameters.Add("@state", SqlDbType.VarChar).Value = string.IsNullOrEmpty(promo.State) ? (object)"" : (object)promo.State.ToUpper();
                    sqlCommand.Parameters.Add("@isActive", SqlDbType.Int).Value = (object)Convert.ToInt32(promo.IsActive);
                    connection.Open();
                    if (action == "create")
                        num = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    else
                        sqlCommand.ExecuteNonQuery();
                    return num;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.PromoUpdate()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static void PromoItemUpdate(int promoId, Distribution_Class.Item item)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_PromoItem_Insert", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@PromoID", SqlDbType.Int).Value = (object)promoId;
                    sqlCommand.Parameters.Add("@Item", SqlDbType.VarChar).Value = (object)item.Number;
                    sqlCommand.Parameters.Add("@UOM", SqlDbType.VarChar).Value = (object)item.UOM;
                    sqlCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = (object)item.Sold;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.PromoItemUpdate()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static void PromoByStoreInsert(int promoId, string storeCode)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_PromoByStore_Insert", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = (object)promoId;
                    sqlCommand.Parameters.Add("@store", SqlDbType.VarChar).Value = (object)storeCode;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.PromoByStoreInsert()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable PurchaseGet(string action, Distribution_Class.FormInput form)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_Purchase_Get_Rev2", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@vendor", SqlDbType.VarChar).Value = !string.IsNullOrEmpty(form.Vendor) ? (object)form.Vendor : (object)"";
                    selectCommand.Parameters.Add("@start", SqlDbType.VarChar).Value = !string.IsNullOrEmpty(form.StartDate) ? (object)form.StartDate : (object)"";
                    selectCommand.Parameters.Add("@end", SqlDbType.VarChar).Value = !string.IsNullOrEmpty(form.EndDate) ? (object)form.EndDate : (object)"";
                    selectCommand.Parameters.Add("@poptype", SqlDbType.Int).Value = (object)(!string.IsNullOrEmpty(form.Type) ? Convert.ToInt32(form.Type) : 0);
                    selectCommand.Parameters.Add("@location", SqlDbType.VarChar).Value = !string.IsNullOrEmpty(form.Location) ? (object)form.Location : (object)"1";
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.PurchaseGet()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable ItemLot(
          string action,
          string item = "",
          string documentNo = "",
          int lineSeq = 0)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_ItemLot_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = (object)item;
                    selectCommand.Parameters.Add("@docNumber", SqlDbType.VarChar).Value = (object)documentNo;
                    selectCommand.Parameters.Add("@lineSeq", SqlDbType.Int).Value = (object)lineSeq;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.ItemLot()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static string ItemLotUpdate(string action, Distribution_Class.Item item)
        {
            SqlConnection connection = (SqlConnection)null;
            string str = "";
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_ItemLot_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = (object)item.Number;
                    sqlCommand.Parameters.Add("@docNumber", SqlDbType.VarChar).Value = (object)item.OrderNumber;
                    sqlCommand.Parameters.Add("@lineSeq", SqlDbType.Int).Value = (object)item.LineSeq;
                    sqlCommand.Parameters.Add("@lot", SqlDbType.VarChar).Value = (object)item.Lot;
                    sqlCommand.Parameters.Add("@qty", SqlDbType.Int).Value = (object)item.LotQty;
                    sqlCommand.Parameters.Add("@dateRecd", SqlDbType.VarChar).Value = (object)item.LotDateReceived;
                    sqlCommand.Parameters.Add("@dateSeq", SqlDbType.Int).Value = (object)item.LotDateSequence;
                    connection.Open();
                    if (action == "item_lot_insert")
                        str = sqlCommand.ExecuteScalar().ToString();
                    else
                        sqlCommand.ExecuteNonQuery();
                }
                return str;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.ItemLotUpdate()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable Dropship(
          string action,
          int id = 0,
          string invoiceNumber = "",
          string itemNumber = "")
        {
            SqlConnection connection = (SqlConnection)null;
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_Dropship_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@id", SqlDbType.Int).Value = (object)id;
                    selectCommand.Parameters.Add("@invoice", SqlDbType.VarChar).Value = (object)invoiceNumber;
                    selectCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = (object)itemNumber;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.Dropship()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static int DropshipUpdate(string action, Distribution_Class.Dropship drop)
        {
            SqlConnection connection = (SqlConnection)null;
            int num = 0;
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_Dropship_Update_Rev2", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = (object)drop.Id;
                    sqlCommand.Parameters.Add("@description", SqlDbType.VarChar).Value = (object)drop.Description;
                    sqlCommand.Parameters.Add("@rateType", SqlDbType.VarChar).Value = (object)drop.RateType;
                    sqlCommand.Parameters.Add("@rate", SqlDbType.Decimal).Value = (object)drop.Rate;
                    sqlCommand.Parameters.Add("@batch", SqlDbType.VarChar).Value = (object)(drop.Batch ?? "");
                    sqlCommand.Parameters.Add("@companyId", SqlDbType.Int).Value = (object)drop.CompanyId;
                    sqlCommand.Parameters.Add("@invoiceAppend", SqlDbType.VarChar).Value = (object)(drop.InvoiceAppend ?? "");
                    sqlCommand.Parameters.Add("@itemPrefix", SqlDbType.VarChar).Value = (object)(drop.ItemPrefix ?? "");
                    sqlCommand.Parameters.Add("@replaceAFCInCustomerNo", SqlDbType.VarChar).Value = (object)(drop.ReplaceAFCInCustomerNo ?? "");
                    sqlCommand.Parameters.Add("@freightMarker", SqlDbType.VarChar).Value = (object)(drop.FreightMarker ?? "");
                    sqlCommand.Parameters.Add("@createPayable", SqlDbType.VarChar).Value = (object)(drop.CreatePayable ?? "");
                    sqlCommand.Parameters.Add("@itemNumber", SqlDbType.VarChar).Value = (object)(drop.ItemNumber ?? "");
                    sqlCommand.Parameters.Add("@vendorNumber", SqlDbType.VarChar).Value = (object)(drop.VendorNumber ?? "");
                    sqlCommand.Parameters.Add("@poNumber", SqlDbType.VarChar).Value = (object)(drop.PONumber ?? "");
                    sqlCommand.Parameters.Add("@freight", SqlDbType.Decimal).Value = (object)drop.Freight;
                    sqlCommand.Parameters.Add("@customer", SqlDbType.VarChar).Value = (object)(drop.Customer ?? "");
                    sqlCommand.Parameters.Add("@invoice", SqlDbType.VarChar).Value = (object)(drop.Invoice ?? "");
                    sqlCommand.Parameters.Add("@invoiceDate", SqlDbType.VarChar).Value = (object)(drop.InvoiceDate ?? "");
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = (object)(drop.Item ?? "");
                    sqlCommand.Parameters.Add("@itemDesc", SqlDbType.VarChar).Value = (object)(drop.ItemDesc ?? "");
                    sqlCommand.Parameters.Add("@uom", SqlDbType.VarChar).Value = (object)(drop.UOM ?? "");
                    sqlCommand.Parameters.Add("@quantity", SqlDbType.VarChar).Value = (object)(drop.Quantity ?? "");
                    sqlCommand.Parameters.Add("@cost", SqlDbType.VarChar).Value = (object)(drop.Cost ?? "");
                    sqlCommand.Parameters.Add("@extendedCost", SqlDbType.VarChar).Value = (object)(drop.ExtendedCost ?? "");
                    sqlCommand.Parameters.Add("@tax", SqlDbType.VarChar).Value = (object)(drop.Tax ?? "");
                    sqlCommand.Parameters.Add("@return", SqlDbType.VarChar).Value = (object)(drop.Return ?? "");
                    sqlCommand.Parameters.Add("@vendor", SqlDbType.VarChar).Value = (object)(drop.Vendor ?? "");
                    connection.Open();
                    if (action == "create")
                        num = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    else
                        sqlCommand.ExecuteNonQuery();
                }
                return num;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.DropshipUpdate()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static void DropshipVendorUpdate(string action, Distribution_Class.DropshipVendor vendor)
        {
            SqlConnection connection = (SqlConnection)null;
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_DropshipVendor_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = (object)(action == "create" ? vendor.DropId : vendor.Id);
                    sqlCommand.Parameters.Add("@source", SqlDbType.VarChar).Value = (object)vendor.Source.ToUpper();
                    sqlCommand.Parameters.Add("@destination", SqlDbType.VarChar).Value = (object)vendor.Destination.ToUpper();
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.DropshipVendorUpdate()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static void DropshipDataInsert(Distribution_Class.DropshipItem item)
        {
            SqlConnection connection = (SqlConnection)null;
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_DropshipData_Insert", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = (object)item.DropId;
                    sqlCommand.Parameters.Add("@customer", SqlDbType.VarChar).Value = (object)item.Customer;
                    sqlCommand.Parameters.Add("@invoice", SqlDbType.VarChar).Value = (object)item.Invoice;
                    sqlCommand.Parameters.Add("@invoiceDate", SqlDbType.VarChar).Value = (object)item.Date;
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = (object)item.Item;
                    sqlCommand.Parameters.Add("@itemDesc", SqlDbType.VarChar).Value = (object)item.ItemDesc;
                    sqlCommand.Parameters.Add("@uom", SqlDbType.VarChar).Value = (object)item.UOM;
                    sqlCommand.Parameters.Add("@quantity", SqlDbType.Decimal).Value = (object)item.Quantity;
                    sqlCommand.Parameters.Add("@cost", SqlDbType.Decimal).Value = (object)item.Cost;
                    sqlCommand.Parameters.Add("@extCost", SqlDbType.Decimal).Value = (object)item.ExtCost;
                    sqlCommand.Parameters.Add("@tax", SqlDbType.Decimal).Value = (object)item.Tax;
                    sqlCommand.Parameters.Add("@returnFlag", SqlDbType.TinyInt).Value = (object)item.ReturnFlag;
                    sqlCommand.Parameters.Add("@freightFlag", SqlDbType.TinyInt).Value = (object)item.FreightFlag;
                    sqlCommand.Parameters.Add("@vendor", SqlDbType.VarChar).Value = (object)item.Vendor;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.DropshipDataInsert()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable ItemTurnover(string action, string location = "1")
        {
            SqlConnection connection = (SqlConnection)null;
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_ItemTurnover_Get_Rev2", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@location", SqlDbType.VarChar).Value = (object)location;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.ItemTurnover()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static void ItemVarianceUpdate(string action, Distribution_Class.Item item)
        {
            SqlConnection connection = (SqlConnection)null;
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_ItemVariance_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = (object)item.Number;
                    sqlCommand.Parameters.Add("@Lot", SqlDbType.VarChar).Value = (object)item.Lot;
                    sqlCommand.Parameters.Add("@lotDateReceived", SqlDbType.VarChar).Value = (object)item.LotDateReceived;
                    sqlCommand.Parameters.Add("@site", SqlDbType.VarChar).Value = (object)item.Location;
                    sqlCommand.Parameters.Add("@uom", SqlDbType.VarChar).Value = (object)item.UOM;
                    sqlCommand.Parameters.Add("@available", SqlDbType.Decimal).Value = (object)item.Available;
                    sqlCommand.Parameters.Add("@actual", SqlDbType.Decimal).Value = (object)item.QtyEntered;
                    sqlCommand.Parameters.Add("@variance", SqlDbType.Decimal).Value = (object)item.Variance;
                    sqlCommand.Parameters.Add("@cost", SqlDbType.Decimal).Value = (object)item.UnitCost;
                    sqlCommand.Parameters.Add("@itemType", SqlDbType.VarChar).Value = (object)item.Category;
                    sqlCommand.Parameters.Add("@lineSeq", SqlDbType.Int).Value = (object)item.LineSeq;
                    sqlCommand.Parameters.Add("@docNumber", SqlDbType.VarChar).Value = (object)item.DocumentNumber;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.ItemVarianceUpdate()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable ItemVariance(string action, Distribution_Class.Item item)
        {
            SqlConnection connection = (SqlConnection)null;
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_ItemVariance_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@itemType", SqlDbType.VarChar).Value = (object)item.Category;
                    selectCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = (object)item.Number;
                    selectCommand.Parameters.Add("@lot", SqlDbType.VarChar).Value = (object)item.Lot;
                    selectCommand.Parameters.Add("@lotDateReceived", SqlDbType.VarChar).Value = (object)item.LotDateReceived;
                    selectCommand.Parameters.Add("@site", SqlDbType.VarChar).Value = (object)item.Location;
                    selectCommand.Parameters.Add("@variance", SqlDbType.Decimal).Value = (object)item.Variance;
                    selectCommand.Parameters.Add("@batchId", SqlDbType.VarChar).Value = (object)item.Batch;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.ItemVariance()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable ItemBin(string action, Distribution_Class.ItemBin itemBin)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_ItemBin_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar, 50).Value = (object)action;
                    selectCommand.Parameters.Add("@id", SqlDbType.Int).Value = (object)itemBin.Id;
                    selectCommand.Parameters.Add("@item", SqlDbType.VarChar, 50).Value = (object)itemBin.Item;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.ItemBin()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static void ItemBinUpdate(string action, Distribution_Class.ItemBin itemBin)
        {
            SqlConnection connection = (SqlConnection)null;
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_ItemBin_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = (object)itemBin.Id;
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = (object)itemBin.Item;
                    sqlCommand.Parameters.Add("@location", SqlDbType.VarChar).Value = (object)itemBin.Location;
                    sqlCommand.Parameters.Add("@bincap", SqlDbType.VarChar).Value = (object)itemBin.BinCap;
                    sqlCommand.Parameters.Add("@secondary", SqlDbType.VarChar).Value = (object)(itemBin.Secondary ?? "");
                    sqlCommand.Parameters.Add("@third", SqlDbType.VarChar).Value = (object)(itemBin.Third ?? "");
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.ItemBinUpdate()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable DropLabel(string action, Distribution_Class.DropLabel drop)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_DropLabel_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@id", SqlDbType.Int).Value = (object)drop.Id;
                    selectCommand.Parameters.Add("@city", SqlDbType.VarChar).Value = (object)drop.City;
                    selectCommand.Parameters.Add("@state", SqlDbType.VarChar).Value = (object)drop.State;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.DropLabel()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static void DropLabelUpdate(string action, Distribution_Class.DropLabel drop)
        {
            SqlConnection connection = (SqlConnection)null;
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_DropLabel_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = (object)drop.Id;
                    sqlCommand.Parameters.Add("@city", SqlDbType.VarChar).Value = (object)drop.City.ToUpper();
                    sqlCommand.Parameters.Add("@state", SqlDbType.VarChar).Value = (object)drop.State.ToUpper();
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.DropLabelUpdate()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable InTransitBillOfLading(
          string action,
          Distribution_Class.BillofLading lading)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_InTransitBillOfLading_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@docId", SqlDbType.VarChar).Value = (object)lading.DocNumber;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.InTransitBillOfLading()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable ExternalDistributionCenter(string action, string id)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_ExternalDistributionCenter_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@id", SqlDbType.VarChar).Value = (object)id;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "AppDataProvider.ExternalDistributionCenter()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable SalesBillofLading(string action, string batchId)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_SalesBillofLading_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@batchId", SqlDbType.VarChar).Value = (object)batchId;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.SalesBillofLading()");
            }
            finally
            {
                connection?.Close();
            }
        }

        public static DataTable WarehouseMgmtSystem(
          string action,
          Distribution_Class.FormInput form)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_WarehouseMgmtSystem_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = (object)action;
                    selectCommand.Parameters.Add("@start", SqlDbType.VarChar).Value = (object)form.StartDate;
                    selectCommand.Parameters.Add("@end", SqlDbType.VarChar).Value = (object)form.EndDate;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.WarehouseMgmtSystem()");
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}