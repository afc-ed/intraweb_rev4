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
            DataTable table = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_Item_Get_Rev2", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar, 50).Value = action;
                    selectCommand.Parameters.Add("@item", SqlDbType.VarChar, 50).Value = item;
                    selectCommand.Parameters.Add("@lot", SqlDbType.VarChar, 50).Value = lot;
                    selectCommand.Parameters.Add("@location", SqlDbType.VarChar, 50).Value = location;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(table);
                    return table;
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

        public static DataTable Sales(string action, string type = "", string item = "", string start = "", string end = "", int uomqty = 0, string uom = "", string location = "")
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
                    selectCommand.Parameters.Add("@pAction", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@pType", SqlDbType.VarChar).Value = type;
                    selectCommand.Parameters.Add("@pItem", SqlDbType.VarChar).Value = item;
                    selectCommand.Parameters.Add("@pStart", SqlDbType.VarChar).Value = start;
                    selectCommand.Parameters.Add("@pEnd", SqlDbType.VarChar).Value = end;
                    selectCommand.Parameters.Add("@pUomQty", SqlDbType.Int).Value = uomqty;
                    selectCommand.Parameters.Add("@pUOM", SqlDbType.VarChar).Value = uom;
                    selectCommand.Parameters.Add("@location", SqlDbType.VarChar).Value = location;
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

        public static DataTable StoreSales(string reportType, string startDate = "", string endDate = "")
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_StoreSales_Get", connection))
                {
                    //selectCommand.CommandTimeout = 540;
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@report_type", SqlDbType.VarChar).Value = reportType;
                    selectCommand.Parameters.Add("@start", SqlDbType.VarChar).Value = startDate;
                    selectCommand.Parameters.Add("@end", SqlDbType.VarChar).Value = endDate;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                    {
                        sqlDataAdapter.Fill(dataTable);
                    }
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.StoreSales()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static DataTable BatchPicklist(string action, string batchId = "", string type = "", string orderNo = "")
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_BatchPicklist_Get_1", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@batchId", SqlDbType.VarChar).Value = batchId;
                    selectCommand.Parameters.Add("@type", SqlDbType.VarChar).Value = type;
                    selectCommand.Parameters.Add("@orderNo", SqlDbType.VarChar).Value = orderNo;
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
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
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
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_BatchOrder_Update", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@OrderNo", SqlDbType.VarChar).Value = orderNo;
                    selectCommand.Parameters.Add("@BatchId", SqlDbType.VarChar).Value = batchId.ToUpper();
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
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

        public static DataTable Promo(string action, int id = 0, string storecode = "", string state = "", int byStoreFlag = 0)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_Promo_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    selectCommand.Parameters.Add("@storecode", SqlDbType.VarChar).Value = storecode;
                    selectCommand.Parameters.Add("@state", SqlDbType.VarChar).Value = state;
                    selectCommand.Parameters.Add("@byStoreFlag", SqlDbType.Int).Value = byStoreFlag;
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
            int insertid = 0;
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_Promo_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = promo.Id;
                    sqlCommand.Parameters.Add("@start", SqlDbType.VarChar).Value = promo.Startdate;
                    sqlCommand.Parameters.Add("@end", SqlDbType.VarChar).Value = promo.Enddate;
                    sqlCommand.Parameters.Add("@prefix", SqlDbType.VarChar).Value = string.IsNullOrEmpty(promo.Storeprefix) ? "" : promo.Storeprefix.ToUpper();
                    sqlCommand.Parameters.Add("@description", SqlDbType.VarChar).Value = promo.Description;
                    sqlCommand.Parameters.Add("@state", SqlDbType.VarChar).Value = string.IsNullOrEmpty(promo.State) ? "" : promo.State.ToUpper();
                    sqlCommand.Parameters.Add("@isActive", SqlDbType.Int).Value = Convert.ToInt32(promo.IsActive);
                    connection.Open();
                    if (action == "create")
                        insertid = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    else
                        sqlCommand.ExecuteNonQuery();
                    return insertid;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.PromoUpdate()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
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
                    sqlCommand.Parameters.Add("@PromoID", SqlDbType.Int).Value = promoId;
                    sqlCommand.Parameters.Add("@Item", SqlDbType.VarChar).Value = item.Number;
                    sqlCommand.Parameters.Add("@UOM", SqlDbType.VarChar).Value = item.UOM;
                    sqlCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = item.Sold;
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
                connection?.Dispose();
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
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = promoId;
                    sqlCommand.Parameters.Add("@store", SqlDbType.VarChar).Value = storeCode;
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
                connection?.Dispose();
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
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@vendor", SqlDbType.VarChar).Value = !string.IsNullOrEmpty(form.Vendor) ? form.Vendor : "";
                    selectCommand.Parameters.Add("@start", SqlDbType.VarChar).Value = !string.IsNullOrEmpty(form.StartDate) ? form.StartDate : "";
                    selectCommand.Parameters.Add("@end", SqlDbType.VarChar).Value = !string.IsNullOrEmpty(form.EndDate) ? form.EndDate : "";
                    selectCommand.Parameters.Add("@poptype", SqlDbType.Int).Value = (!string.IsNullOrEmpty(form.Type) ? Convert.ToInt32(form.Type) : 0);
                    selectCommand.Parameters.Add("@location", SqlDbType.VarChar).Value = !string.IsNullOrEmpty(form.Location) ? form.Location : "1";
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
                connection?.Dispose();
            }
        }

        public static DataTable ItemLot(string action, string item = "", string documentNo = "", int lineSeq = 0)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_ItemLot_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = item;
                    selectCommand.Parameters.Add("@docNumber", SqlDbType.VarChar).Value = documentNo;
                    selectCommand.Parameters.Add("@lineSeq", SqlDbType.Int).Value = lineSeq;
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
                connection?.Dispose();
            }
        }

        public static string ItemLotUpdate(string action, Distribution_Class.Item item)
        {
            SqlConnection connection = new SqlConnection();
            string str = "";
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_ItemLot_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = item.Number;
                    sqlCommand.Parameters.Add("@docNumber", SqlDbType.VarChar).Value = item.OrderNumber;
                    sqlCommand.Parameters.Add("@lineSeq", SqlDbType.Int).Value = item.LineSeq;
                    sqlCommand.Parameters.Add("@lot", SqlDbType.VarChar).Value = item.Lot;
                    sqlCommand.Parameters.Add("@qty", SqlDbType.Int).Value = item.LotQty;
                    sqlCommand.Parameters.Add("@dateRecd", SqlDbType.VarChar).Value = item.LotDateReceived;
                    sqlCommand.Parameters.Add("@dateSeq", SqlDbType.Int).Value = item.LotDateSequence;
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
                connection?.Dispose();
            }
        }

        public static DataTable Dropship(string action, int id = 0, string invoiceNumber = "", string itemNumber = "")
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_Dropship_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    selectCommand.Parameters.Add("@invoice", SqlDbType.VarChar).Value = invoiceNumber;
                    selectCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = itemNumber;
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
                connection?.Dispose();
            }
        }

        public static int DropshipUpdate(string action, Distribution_Class.Dropship drop)
        {
            SqlConnection connection = new SqlConnection();
            int insertedId = 0;
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_Dropship_Update_Rev2", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = drop.Id;
                    sqlCommand.Parameters.Add("@description", SqlDbType.VarChar).Value = drop.Description;
                    sqlCommand.Parameters.Add("@rateType", SqlDbType.VarChar).Value = drop.RateType;
                    sqlCommand.Parameters.Add("@rate", SqlDbType.Decimal).Value = drop.Rate;
                    sqlCommand.Parameters.Add("@batch", SqlDbType.VarChar).Value = drop.Batch ?? "";
                    sqlCommand.Parameters.Add("@companyId", SqlDbType.Int).Value = drop.CompanyId;
                    sqlCommand.Parameters.Add("@invoiceAppend", SqlDbType.VarChar).Value = drop.InvoiceAppend ?? "";
                    sqlCommand.Parameters.Add("@itemPrefix", SqlDbType.VarChar).Value = drop.ItemPrefix ?? "";
                    sqlCommand.Parameters.Add("@replaceAFCInCustomerNo", SqlDbType.VarChar).Value = drop.ReplaceAFCInCustomerNo ?? "";
                    sqlCommand.Parameters.Add("@freightMarker", SqlDbType.VarChar).Value = drop.FreightMarker ?? "";
                    sqlCommand.Parameters.Add("@createPayable", SqlDbType.VarChar).Value = drop.CreatePayable ?? "";
                    sqlCommand.Parameters.Add("@itemNumber", SqlDbType.VarChar).Value = drop.ItemNumber ?? "";
                    sqlCommand.Parameters.Add("@vendorNumber", SqlDbType.VarChar).Value = drop.VendorNumber ?? "";
                    sqlCommand.Parameters.Add("@poNumber", SqlDbType.VarChar).Value = drop.PONumber ?? "";
                    sqlCommand.Parameters.Add("@freight", SqlDbType.Decimal).Value = drop.Freight;
                    sqlCommand.Parameters.Add("@customer", SqlDbType.VarChar).Value = drop.Customer ?? "";
                    sqlCommand.Parameters.Add("@invoice", SqlDbType.VarChar).Value = drop.Invoice ?? "";
                    sqlCommand.Parameters.Add("@invoiceDate", SqlDbType.VarChar).Value = drop.InvoiceDate ?? "";
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = drop.Item ?? "";
                    sqlCommand.Parameters.Add("@itemDesc", SqlDbType.VarChar).Value = drop.ItemDesc ?? "";
                    sqlCommand.Parameters.Add("@uom", SqlDbType.VarChar).Value = drop.UOM ?? "";
                    sqlCommand.Parameters.Add("@quantity", SqlDbType.VarChar).Value = drop.Quantity ?? "";
                    sqlCommand.Parameters.Add("@cost", SqlDbType.VarChar).Value = drop.Cost ?? "";
                    sqlCommand.Parameters.Add("@extendedCost", SqlDbType.VarChar).Value = drop.ExtendedCost ?? "";
                    sqlCommand.Parameters.Add("@tax", SqlDbType.VarChar).Value = drop.Tax ?? "";
                    sqlCommand.Parameters.Add("@return", SqlDbType.VarChar).Value = drop.Return ?? "";
                    sqlCommand.Parameters.Add("@vendor", SqlDbType.VarChar).Value = drop.Vendor ?? "";
                    connection.Open();
                    if (action == "create")
                        insertedId = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    else
                        sqlCommand.ExecuteNonQuery();
                }
                return insertedId;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.DropshipUpdate()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public static void DropshipVendorUpdate(string action, Distribution_Class.DropshipVendor vendor)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_DropshipVendor_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = (action == "create" ? vendor.DropId : vendor.Id);
                    sqlCommand.Parameters.Add("@source", SqlDbType.VarChar).Value = vendor.Source.ToUpper();
                    sqlCommand.Parameters.Add("@destination", SqlDbType.VarChar).Value = vendor.Destination.ToUpper();
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
                connection?.Dispose();
            }
        }

        public static void DropshipDataInsert(Distribution_Class.DropshipItem item)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_DropshipData_Insert", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = item.DropId;
                    sqlCommand.Parameters.Add("@customer", SqlDbType.VarChar).Value = item.Customer;
                    sqlCommand.Parameters.Add("@invoice", SqlDbType.VarChar).Value = item.Invoice;
                    sqlCommand.Parameters.Add("@invoiceDate", SqlDbType.VarChar).Value = item.Date;
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = item.Item;
                    sqlCommand.Parameters.Add("@itemDesc", SqlDbType.VarChar).Value = item.ItemDesc;
                    sqlCommand.Parameters.Add("@uom", SqlDbType.VarChar).Value = item.UOM;
                    sqlCommand.Parameters.Add("@quantity", SqlDbType.Decimal).Value = item.Quantity;
                    sqlCommand.Parameters.Add("@cost", SqlDbType.Decimal).Value = item.Cost;
                    sqlCommand.Parameters.Add("@extCost", SqlDbType.Decimal).Value = item.ExtCost;
                    sqlCommand.Parameters.Add("@tax", SqlDbType.Decimal).Value = item.Tax;
                    sqlCommand.Parameters.Add("@returnFlag", SqlDbType.TinyInt).Value = item.ReturnFlag;
                    sqlCommand.Parameters.Add("@freightFlag", SqlDbType.TinyInt).Value = item.FreightFlag;
                    sqlCommand.Parameters.Add("@vendor", SqlDbType.VarChar).Value = item.Vendor;
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
                connection?.Dispose();
            }
        }

        public static DataTable ItemTurnover(string action, string location = "1")
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_ItemTurnover_Get_Rev2", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@location", SqlDbType.VarChar).Value = location;
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
                connection?.Dispose();
            }
        }

        //public static void ItemVarianceUpdate(string action, Distribution_Class.Item item)
        //{
        //    SqlConnection connection = new SqlConnection();
        //    try
        //    {
        //        connection = App.DBConnect();
        //        using (SqlCommand sqlCommand = new SqlCommand("distribution_ItemVariance_Update", connection))
        //        {
        //            sqlCommand.CommandType = CommandType.StoredProcedure;
        //            sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
        //            sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = item.Number;
        //            sqlCommand.Parameters.Add("@Lot", SqlDbType.VarChar).Value = item.Lot;
        //            sqlCommand.Parameters.Add("@lotDateReceived", SqlDbType.VarChar).Value = item.LotDateReceived;
        //            sqlCommand.Parameters.Add("@site", SqlDbType.VarChar).Value = item.Location;
        //            sqlCommand.Parameters.Add("@uom", SqlDbType.VarChar).Value = item.UOM;
        //            sqlCommand.Parameters.Add("@available", SqlDbType.Decimal).Value = item.Available;
        //            sqlCommand.Parameters.Add("@actual", SqlDbType.Decimal).Value = item.QtyEntered;
        //            sqlCommand.Parameters.Add("@variance", SqlDbType.Decimal).Value = item.Variance;
        //            sqlCommand.Parameters.Add("@cost", SqlDbType.Decimal).Value = item.UnitCost;
        //            sqlCommand.Parameters.Add("@itemType", SqlDbType.VarChar).Value = item.Category;
        //            sqlCommand.Parameters.Add("@lineSeq", SqlDbType.Int).Value = item.LineSeq;
        //            sqlCommand.Parameters.Add("@docNumber", SqlDbType.VarChar).Value = item.DocumentNumber;
        //            connection.Open();
        //            sqlCommand.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw Utilities.ErrHandler(ex, "Distribution_DB.ItemVarianceUpdate()");
        //    }
        //    finally
        //    {
        //        connection?.Close();
        //        connection?.Dispose();
        //    }
        //}

        //public static DataTable ItemVariance(string action, Distribution_Class.Item item)
        //{
        //    SqlConnection connection = new SqlConnection();
        //    DataTable dataTable = new DataTable();
        //    try
        //    {
        //        connection = App.DBConnect();
        //        using (SqlCommand selectCommand = new SqlCommand("distribution_ItemVariance_Get", connection))
        //        {
        //            selectCommand.CommandType = CommandType.StoredProcedure;
        //            selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
        //            selectCommand.Parameters.Add("@itemType", SqlDbType.VarChar).Value = item.Category;
        //            selectCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = item.Number;
        //            selectCommand.Parameters.Add("@lot", SqlDbType.VarChar).Value = item.Lot;
        //            selectCommand.Parameters.Add("@lotDateReceived", SqlDbType.VarChar).Value = item.LotDateReceived;
        //            selectCommand.Parameters.Add("@site", SqlDbType.VarChar).Value = item.Location;
        //            selectCommand.Parameters.Add("@variance", SqlDbType.Decimal).Value = item.Variance;
        //            selectCommand.Parameters.Add("@batchId", SqlDbType.VarChar).Value = item.Batch;
        //            connection.Open();
        //            selectCommand.ExecuteNonQuery();
        //            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
        //                sqlDataAdapter.Fill(dataTable);
        //            return dataTable;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw Utilities.ErrHandler(ex, "Distribution_DB.ItemVariance()");
        //    }
        //    finally
        //    {
        //        connection?.Close();
        //        connection?.Dispose();
        //    }
        //}

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
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@id", SqlDbType.Int).Value = itemBin.Id;
                    selectCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = itemBin.Item;
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
                connection?.Dispose();
            }
        }

        public static void ItemBinUpdate(string action, Distribution_Class.ItemBin itemBin)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_ItemBin_Update_Rev2", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = itemBin.Id;
                    sqlCommand.Parameters.Add("@item", SqlDbType.VarChar).Value = itemBin.Item;
                    sqlCommand.Parameters.Add("@location", SqlDbType.VarChar).Value = itemBin.Location;
                    sqlCommand.Parameters.Add("@bincap", SqlDbType.VarChar).Value = itemBin.BinCap;
                    sqlCommand.Parameters.Add("@secondary", SqlDbType.VarChar).Value = (itemBin.Secondary ?? "");
                    sqlCommand.Parameters.Add("@third", SqlDbType.VarChar).Value = (itemBin.Third ?? "");
                    sqlCommand.Parameters.Add("@priority", SqlDbType.VarChar).Value = (itemBin.Priority ?? "");
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
                connection?.Dispose();
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
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@id", SqlDbType.Int).Value = drop.Id;
                    selectCommand.Parameters.Add("@city", SqlDbType.VarChar).Value = drop.City;
                    selectCommand.Parameters.Add("@state", SqlDbType.VarChar).Value = drop.State;
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
                connection?.Dispose();
            }
        }

        public static void DropLabelUpdate(string action, Distribution_Class.DropLabel drop)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand sqlCommand = new SqlCommand("distribution_DropLabel_Update", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = drop.Id;
                    sqlCommand.Parameters.Add("@city", SqlDbType.VarChar).Value = drop.City.ToUpper();
                    sqlCommand.Parameters.Add("@state", SqlDbType.VarChar).Value = drop.State.ToUpper();
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
                connection?.Dispose();
            }
        }

        public static DataTable InTransitBillOfLading(string action, Distribution_Class.BillofLading lading)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_InTransitBillOfLading_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@docId", SqlDbType.VarChar).Value = lading.DocNumber;
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
                connection?.Dispose();
            }
        }

        //public static DataTable ExternalDistributionCenter(string action, string id)
        //{
        //    SqlConnection connection = new SqlConnection();
        //    DataTable dataTable = new DataTable();
        //    try
        //    {
        //        connection = App.DBConnect();
        //        using (SqlCommand selectCommand = new SqlCommand("distribution_ExternalDistributionCenter_Get", connection))
        //        {
        //            selectCommand.CommandType = CommandType.StoredProcedure;
        //            selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
        //            selectCommand.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
        //            connection.Open();
        //            selectCommand.ExecuteNonQuery();
        //            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
        //                sqlDataAdapter.Fill(dataTable);
        //            return dataTable;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw Utilities.ErrHandler(ex, "AppDataProvider.ExternalDistributionCenter()");
        //    }
        //    finally
        //    {
        //        connection?.Close();
        //    }
        //}

        public static DataTable SalesBillofLading(string batchId)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_SalesBillofLading_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@batchId", SqlDbType.VarChar).Value = batchId;
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
                connection?.Dispose();
            }
        }

        public static DataTable WarehouseMgmtSystem(string action, Distribution_Class.FormInput form)
        {
            SqlConnection connection = new SqlConnection();
            DataTable dataTable = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_WarehouseMgmtSystem_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@start", SqlDbType.VarChar).Value = form.StartDate;
                    selectCommand.Parameters.Add("@end", SqlDbType.VarChar).Value = form.EndDate;
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
                connection?.Dispose();
            }
        }

        public static DataTable Lanter(string action, string start = "", string end = "")
        {
            SqlConnection connection = new SqlConnection();
            DataTable table = new DataTable();
            try
            {
                connection = App.DBConnect();
                using (SqlCommand selectCommand = new SqlCommand("distribution_Lanter_Get", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = action;
                    selectCommand.Parameters.Add("@start", SqlDbType.VarChar).Value = start;
                    selectCommand.Parameters.Add("@end", SqlDbType.VarChar).Value = end;
                    connection.Open();
                    selectCommand.ExecuteNonQuery();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                        sqlDataAdapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Distribution_DB.Lanter()");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }




    }
}