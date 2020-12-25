using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace intraweb_rev3.Models
{
    public class Distribution
    {
        public static object MenuList()
        {
            try
            {
                List<Distribution_Class.Menu> menuList = new List<Distribution_Class.Menu>();
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "LowStock",
                    Name = "Low Stock"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "Recall",
                    Name = "Recall"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "ItemLevel",
                    Name = "Item Level"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "PriceList",
                    Name = "Price List"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "StoreSales",
                    Name = "Store Sales"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "Sales",
                    Name = "Sales"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "ItemSales",
                    Name = "Item Sales"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "BatchPicklist",
                    Name = "Batch Picklist"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "InventoryQuantity",
                    Name = "Inventory Quantity"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "BatchOrder",
                    Name = "Batch Order"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "Promo",
                    Name = "Promo"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "Purchases",
                    Name = "Purchases"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "Dropship",
                    Name = "Dropship"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "ItemBin",
                    Name = "Item Bin"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "DropLabel",
                    Name = "Drop Label By City and State for shipping tags."
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "InTransitBillofLading",
                    Name = "In-Transit Bill of Lading"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "WMSTrxLog",
                    Name = "WMS Trx Log"
                });
                menuList.Sort((Comparison<Distribution_Class.Menu>)((x, y) => x.Name.CompareTo(y.Name)));
                return (object)menuList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.MenuList()");
            }
        }

        public static object LowStock(string filePath, Distribution_Class.FormInput form)
        {
            try
            {
                Distribution_Class.Item obj = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Item("lowstock", location: form.Location).Rows)
                {
                    obj.Number = row["item"].ToString();
                    obj.Description = row["itemdesc"].ToString();
                    obj.OnHand = Convert.ToInt32(row["onhand"]);
                    obj.Allocated = Convert.ToInt32(row["allocated"]);
                    obj.Available = Convert.ToInt32(row["available"]);
                    obj.OnOrder = Convert.ToInt32(row["onorder"]);
                    obj.Ratio = Math.Round(Convert.ToDecimal(row["ratio"]) / 0.01M, 0).ToString() + "%";
                    obj.Location = row["location"].ToString();
                    itemList.Add(obj);
                    obj = new Distribution_Class.Item();
                }
                Distribution.WriteLowStockFile(filePath, itemList);
                return (object)itemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.LowStock()");
            }
        }

        private static void WriteLowStockFile(string filePath, List<Distribution_Class.Item> itemList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("ItemNumber" + str + "Description" + str + "OnHand" + str + "Allocated" + str + "Available" + str + "PO. OnOrder" + str + "Ratio" + str + "Warehouse");
                    foreach (Distribution_Class.Item obj in itemList)
                        streamWriter.WriteLine(obj.Number + str + obj.Description.Replace(',', '.') + str + (object)obj.OnHand + str + (object)obj.Allocated + str + (object)obj.Available + str + (object)obj.OnOrder + str + obj.Ratio + str + obj.Location);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WriteLowStockFile()");
            }
        }

        public static object PriceList(string filePath)
        {
            try
            {
                Distribution_Class.Item obj = new Distribution_Class.Item();
                List<Distribution_Class.Item> priceList = new List<Distribution_Class.Item>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Item("pricelist").Rows)
                {
                    obj.Number = row["item"].ToString().Trim();
                    obj.Description = row["itemdesc"].ToString().Trim();
                    obj.UOM = row["uom"].ToString().Trim();
                    obj.UOMQty = Convert.ToInt32(row["uomqty"]);
                    obj.Cost = Convert.ToDecimal(row["uomcost"]);
                    obj.Price = Convert.ToDecimal(row["price"]);
                    priceList.Add(obj);
                    obj = new Distribution_Class.Item();
                }
                Distribution.WritePriceListFile(filePath, priceList);
                return (object)priceList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PriceList()");
            }
        }

        private static void WritePriceListFile(string filePath, List<Distribution_Class.Item> priceList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + str + "Description" + str + "UOM" + str + "UOM Qty" + str + "Cost" + str + "Price");
                    foreach (Distribution_Class.Item price in priceList)
                        streamWriter.WriteLine(price.Number + str + price.Description.Replace(',', '.') + str + price.UOM + str + (object)price.UOMQty + str + (object)price.Cost + str + (object)price.Price);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WritePriceListFile()");
            }
        }

        private static Distribution_Class.Item ItemInfo(string itemNo)
        {
            try
            {
                Distribution_Class.Item obj = new Distribution_Class.Item();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Item("item", itemNo).Rows)
                {
                    obj.Number = row["item"].ToString().Trim();
                    obj.Description = row["itemdesc"].ToString().Replace(',', ' ').Trim();
                    obj.UOM = row["uom"].ToString().Trim();
                    obj.Price = Convert.ToDecimal(row["price"]);
                    obj.UOMQty = Convert.ToInt32(row["uomqty"]);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemInfo()");
            }
        }

        public static object RecallItem(Distribution_Class.FormInput form, string filePath)
        {
            try
            {
                Distribution_Class.Recall recall = new Distribution_Class.Recall();
                List<Distribution_Class.Recall> recallList = new List<Distribution_Class.Recall>();
                DataTable dataTable = new DataTable();
                Distribution_Class.Item obj = Distribution.ItemInfo(form.Item);
                foreach (DataRow row1 in (InternalDataCollectionBase)Distribution_DB.Item("recall", form.Item.Trim(), form.Lot.Trim(), form.Location).Rows)
                {
                    recall.InvoiceNo = row1["invoiceno"].ToString().Trim();
                    recall.DocDate = ((DateTime)row1["docdate"]).ToString("MM/dd/yyyy").Trim();
                    recall.ShipDate = row1["shipdate"].ToString().Contains("1900") ? "" : ((DateTime)row1["shipdate"]).ToString("MM/dd/yyyy").Trim();
                    recall.Item = row1["item"].ToString().Trim();
                    recall.UOM = obj.UOM;
                    recall.Lot = row1["lot"].ToString().Trim();
                    recall.Quantity = Math.Round(Convert.ToDecimal(row1["qty"]), 1);
                    recall.Return = Math.Round(Convert.ToDecimal(row1["rtn"]), 1);
                    recall.Storecode = row1["customerno"].ToString().Trim();
                    foreach (DataRow row2 in (InternalDataCollectionBase)AFC.GetStoreRecallInfo(recall.Storecode).Rows)
                    {
                        recall.Storename = row2["Name"].ToString().Trim();
                        recall.Address = row2["Address"].ToString().Trim();
                        recall.City = row2["City"].ToString().Trim();
                        recall.State = row2["StateId"].ToString().Trim();
                        recall.Zip = row2["Zip"].ToString().Trim();
                        recall.RM = row2["rm"].ToString().Trim();
                        recall.RMcell = Utilities.FormatPhone(row2["rmcell"].ToString().Trim());
                        recall.RMeMail = row2["rmemail"].ToString().Trim();
                        recall.FCId = row2["fcid"].ToString().Trim();
                        recall.FC = row2["fc"].ToString().Trim();
                        recall.FCcell = Utilities.FormatPhone(row2["fccell"].ToString().Trim());
                        recall.FCeMail = row2["fcemail"].ToString().Trim();
                        recall.Region = row2["region"].ToString().Trim();
                        recall.Storegroup = row2["storegroup"].ToString().Trim();
                        recall.Storecorp = row2["storecorp"].ToString().Trim();
                    }
                    recallList.Add(recall);
                    recall = new Distribution_Class.Recall();
                }
                Distribution.WriteRecallFile(filePath, recallList);
                return (object)recallList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.GetRecallItem()");
            }
        }

        public static void WriteRecallFile(string filePath, List<Distribution_Class.Recall> recallList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Invoice No." + str + "Doc. Date" + str + "Ship Date" + str + "Item" + str + "UOM" + str + "Lot" + str + "Qty" + str + "Return" + str + "Storecode" + str + "Store" + str + "Address" + str + "City" + str + "ST" + str + "Zipcode" + str + "RM" + str + "RM Cell" + str + "RM eMail" + str + "FCID" + str + "FC" + str + "FC Cell" + str + "FC eMail" + str + "Region" + str + "StoreGroup" + str + "StoreCorpGroup");
                    foreach (Distribution_Class.Recall recall in recallList)
                        streamWriter.WriteLine(recall.InvoiceNo + str + recall.DocDate + str + recall.ShipDate + str + recall.Item + str + recall.UOM + str + recall.Lot.Replace(',', ' ') + str + (object)recall.Quantity + str + (object)recall.Return + str + recall.Storecode + str + (!string.IsNullOrEmpty(recall.Storename) ? (object)recall.Storename.Replace(',', ' ') : (object)"") + str + (!string.IsNullOrEmpty(recall.Address) ? (object)recall.Address.Replace(',', ' ') : (object)"") + str + (!string.IsNullOrEmpty(recall.City) ? (object)recall.City.Replace(',', ' ') : (object)"") + str + recall.State + str + recall.Zip + str + (!string.IsNullOrEmpty(recall.RM) ? (object)recall.RM.Replace(',', ' ') : (object)"") + str + recall.RMcell + str + (!string.IsNullOrEmpty(recall.RMeMail) ? (object)recall.RMeMail.Replace(',', '.') : (object)"") + str + recall.FCId + str + recall.FC + str + recall.FCcell + str + (!string.IsNullOrEmpty(recall.FCeMail) ? (object)recall.FCeMail.Replace(',', '.') : (object)"") + str + recall.Region + str + (!string.IsNullOrEmpty(recall.Storegroup) ? (object)recall.Storegroup.Replace(',', ' ') : (object)"") + str + (!string.IsNullOrEmpty(recall.Storecorp) ? (object)recall.Storecorp.Replace(',', ' ') : (object)""));
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WriteRecallFile()");
            }
        }

        private static Distribution_Class.Item ItemQuantity(
          Distribution_Class.Item item,
          Distribution_Class.FormInput form)
        {
            try
            {
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Sales("itemquantity", item: item.Number, start: form.StartDate, end: form.EndDate, uomqty: item.UOMQty, location: form.Location).Rows)
                {
                    item.Receipt = Convert.ToInt32(!Utilities.isNull(row["rct"]) ? row["rct"] : (object)0);
                    item.Sold = Convert.ToInt32(!Utilities.isNull(row["sold"]) ? row["sold"] : (object)0);
                    item.Adjust = Convert.ToInt32(!Utilities.isNull(row["adj"]) ? row["adj"] : (object)0);
                    item.Transfer = Convert.ToInt32(!Utilities.isNull(row["trn"]) ? row["trn"] : (object)0);
                    item.Return = Convert.ToInt32(!Utilities.isNull(row["rtn"]) ? row["rtn"] : (object)0);
                    item.LocationsSoldAt = Convert.ToInt32(!Utilities.isNull(row["loc"]) ? row["loc"] : (object)0);
                }
                return item;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemQuantity()");
            }
        }

        private static Distribution_Class.Item ItemSold(
          Distribution_Class.Item item,
          Distribution_Class.FormInput form)
        {
            try
            {
                DataTable dataTable = Distribution_DB.Sales("itemsold", item: item.Number, start: form.StartDate, end: form.EndDate, uomqty: item.UOMQty, location: form.Location);
                item.Receipt = 0;
                item.Adjust = 0;
                item.Transfer = 0;
                item.Return = 0;
                foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                {
                    item.Sold = Convert.ToInt32(!Utilities.isNull(row["sold"]) ? row["sold"] : (object)0);
                    item.LocationsSoldAt = Convert.ToInt32(!Utilities.isNull(row["loc"]) ? row["loc"] : (object)0);
                }
                return item;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemSold()");
            }
        }

        public static object ItemLevel(string filePath, Distribution_Class.FormInput form)
        {
            try
            {
                Decimal totalSales = 0M;
                Distribution_Class.Item obj1 = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                DataTable dataTable = new DataTable();
                string[] strArray1 = new string[0];
                string str1 = "";
                if (string.IsNullOrEmpty(form.Item))
                    dataTable = Distribution_DB.Item("pricelist");
                else if (form.Item.Contains(","))
                {
                    string[] strArray2 = Utilities.RemoveWhiteSpace(form.Item).Split(',');
                    if (!string.IsNullOrEmpty(strArray2[0]))
                    {
                        App.ExecuteSql("delete from App.dbo.UserInput");
                        foreach (string str2 in strArray2)
                            App.AddUserInput("item", str2);
                        dataTable = Distribution_DB.Item("itemperuserinput", location: form.Location);
                    }
                }
                else if (form.Item.Contains("-"))
                {
                    string[] strArray2 = Utilities.RemoveWhiteSpace(form.Item).Split('-');
                    dataTable = !string.IsNullOrEmpty(strArray2[1]) ? App.GetRow("SELECT [item], [ITEMDESC], [uom], [price], [uomqty], [uomcost] FROM [APP].[dbo].[viewItemPrice] WHERE item >= '" + strArray2[0] + "' and item <= '" + strArray2[1] + "' ORDER BY item") : throw new Exception("Item range is missing second value.");
                }
                else
                    dataTable = Distribution_DB.Item("item", form.Item, location: form.Location);
                foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                {
                    obj1.Number = row["item"].ToString().Trim();
                    obj1.Description = row["itemdesc"].ToString().Trim().Replace(',', '.');
                    obj1.UOM = row["uom"].ToString().Trim();
                    obj1.Price = Convert.ToDecimal(row["price"]);
                    obj1.UOMQty = Convert.ToInt32(row["uomqty"]);
                    obj1.Cost = Convert.ToDecimal(row["uomcost"]);
                    Distribution_Class.Item obj2;
                    if (obj1.Number != str1)
                    {
                        obj2 = Distribution.ItemQuantity(obj1, form);
                        obj2.Sales = (Decimal)obj2.Sold * obj2.Price;
                        str1 = obj2.Number;
                    }
                    else
                    {
                        obj2 = Distribution.ItemSold(obj1, form);
                        obj2.Sales = (Decimal)obj2.Sold * obj2.Price;
                    }
                    totalSales += obj2.Sales;
                    itemList.Add(obj2);
                    obj1 = new Distribution_Class.Item();
                }
                Distribution.WriteItemLevelFile(filePath, itemList, totalSales);
                return (object)new List<object>()
        {
          (object) itemList,
          (object) totalSales
        };
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemLevel()");
            }
        }

        private static void WriteItemLevelFile(
          string filePath,
          List<Distribution_Class.Item> itemList,
          Decimal totalSales)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + str + "Description" + str + "UOM" + str + "UOM Qty" + str + "Cost" + str + "Price" + str + "Receipt" + str + "Sold" + str + "Adjustment" + str + "Transfer" + str + "Return" + str + "No. of Locs." + str + "Item Sales Amount");
                    foreach (Distribution_Class.Item obj in itemList)
                        streamWriter.WriteLine(obj.Number + str + obj.Description.Replace(',', '.') + str + obj.UOM + str + (object)obj.UOMQty + str + (object)obj.Cost + str + (object)obj.Price + str + (object)obj.Receipt + str + (object)obj.Sold + str + (object)obj.Adjust + str + (object)obj.Transfer + str + (object)obj.Return + str + (object)obj.LocationsSoldAt + str + (object)obj.Sales);
                    streamWriter.WriteLine(Environment.NewLine + "Total sales = " + (object)Math.Round(totalSales, 2));
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WriteItemLevelFile()");
            }
        }

        public static object Sales(string filePath, Distribution_Class.FormInput form)
        {
            try
            {
                Decimal totalSales = 0M;
                Distribution_Class.Item obj1 = new Distribution_Class.Item();
                string[] strArray1 = new string[0];
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                DataTable dataTable = new DataTable();
                if (string.IsNullOrEmpty(form.Item))
                    dataTable = Distribution_DB.Item("pricelist");
                else if (form.Item.Contains(","))
                {
                    string[] strArray2 = Utilities.RemoveWhiteSpace(form.Item).Split(',');
                    if (!string.IsNullOrEmpty(strArray2[0]))
                    {
                        App.ExecuteSql("delete from App.dbo.UserInput");
                        foreach (string str in strArray2)
                            App.AddUserInput("item", str);
                        dataTable = Distribution_DB.Item("itemperuserinput", location: form.Location);
                    }
                }
                else if (form.Item.Contains("-"))
                {
                    string[] strArray2 = Utilities.RemoveWhiteSpace(form.Item).Split('-');
                    dataTable = !string.IsNullOrEmpty(strArray2[1]) ? App.GetRow("SELECT [item], [ITEMDESC], [uom], [price], [uomqty], [uomcost] FROM [APP].[dbo].[viewItemPrice] WHERE item >= '" + strArray2[0] + "' and item <= '" + strArray2[1] + "' ORDER BY item") : throw new Exception("Item range is missing second value.");
                }
                else
                    dataTable = Distribution_DB.Item("item", form.Item, location: form.Location);
                foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                {
                    obj1.Number = row["item"].ToString().Trim();
                    obj1.Description = row["itemdesc"].ToString().Trim().Replace(',', '.');
                    obj1.UOM = row["uom"].ToString().Trim();
                    obj1.Price = Convert.ToDecimal(row["price"]);
                    obj1.UOMQty = Convert.ToInt32(row["uomqty"]);
                    obj1.Cost = Convert.ToDecimal(row["uomcost"]);
                    Distribution_Class.Item obj2 = Distribution.ItemSold(obj1, form);
                    obj2.Sales = (Decimal)obj2.Sold * obj2.Price;
                    obj2.UnitsSold = obj2.UOMQty * obj2.Sold;
                    totalSales += obj2.Sales;
                    itemList.Add(obj2);
                    obj1 = new Distribution_Class.Item();
                }
                Distribution.WriteSalesFile(filePath, itemList, totalSales);
                return (object)new List<object>()
        {
          (object) itemList,
          (object) totalSales
        };
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.Sales()");
            }
        }

        private static void WriteSalesFile(
          string filePath,
          List<Distribution_Class.Item> itemList,
          Decimal totalSales)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + str + "Description" + str + "UOM" + str + "UOM Qty" + str + "Cost" + str + "Price" + str + "Sold" + str + "Units Sold" + str + "Sales Amount");
                    foreach (Distribution_Class.Item obj in itemList)
                        streamWriter.WriteLine(obj.Number + str + obj.Description.Replace(',', '.') + str + obj.UOM + str + (object)obj.UOMQty + str + (object)obj.Cost + str + (object)obj.Price + str + (object)obj.Sold + str + (object)obj.UnitsSold + str + (object)obj.Sales);
                    streamWriter.WriteLine(Environment.NewLine + "Total sales = " + (object)Math.Round(totalSales, 2));
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WriteSalesFile()");
            }
        }

        public static object StoreSales(string filePath, Distribution_Class.FormInput form)
        {
            try
            {
                int totalSold = 0;
                string[] strArray1 = new string[0];
                Distribution_Class.Item obj = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                DataTable dataTable = new DataTable();
                string[] strArray2;
                if (string.IsNullOrEmpty(form.Item))
                    strArray2 = Distribution_DB.Item("pricelist").Rows.OfType<DataRow>().Select<DataRow, string>((Func<DataRow, string>)(k => k[0].ToString().Trim())).ToArray<string>();
                else if (form.Item.Contains(","))
                    strArray2 = Utilities.RemoveWhiteSpace(form.Item).Split(',');
                else if (form.Item.Contains("-"))
                {
                    string[] strArray3 = Utilities.RemoveWhiteSpace(form.Item).Split('-');
                    if (strArray3.Length != 2)
                        throw new Exception("Item range is missing second value.");
                    strArray2 = App.GetRow("SELECT [item] FROM [APP].[dbo].[viewItemPrice] WHERE item >= '" + strArray3[0] + "' and item <= '" + strArray3[1] + "' ORDER BY item asc").Rows.OfType<DataRow>().Select<DataRow, string>((Func<DataRow, string>)(k => k[0].ToString().Trim())).ToArray<string>();
                }
                else
                    strArray2 = form.Item.Split(' ');
                string type;
                if (string.IsNullOrEmpty(form.Store))
                {
                    type = "all";
                }
                else
                {
                    type = "lookup";
                    App.ExecuteSql("delete from App.dbo.UserInput");
                    string store = form.Store;
                    char[] chArray = new char[1] { ',' };
                    foreach (string storecode in store.Split(chArray))
                        App.AddUserInput("storecode", storecode: storecode);
                }
                for (int index = 0; strArray2.Length > index; ++index)
                {
                    obj.Number = strArray2[index].Trim();
                    string number = obj.Number;
                    foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Sales("store", type, number, form.StartDate, form.EndDate, location: form.Location).Rows)
                    {
                        obj.Storecode = row["customer"].ToString().Trim();
                        obj.Sold = Convert.ToInt32(row["qty"]);
                        obj.UOMQty = Convert.ToInt32(row["uomqty"]);
                        obj.Weight = Convert.ToDecimal(row["shipwt"]) * 0.01M;
                        obj.ShipWt = Math.Round((Decimal)(obj.Sold * obj.UOMQty) * obj.Weight, 2);
                        obj.Description = row["itemdesc"].ToString().Trim();
                        obj.UOM = row["uom"].ToString().Trim();
                        obj.UnitsSold = obj.Sold * obj.UOMQty;
                        totalSold += obj.Sold;
                        itemList.Add(obj);
                        obj = new Distribution_Class.Item();
                        obj.Number = number;
                    }
                }
                Distribution.WriteStoreSalesFile(filePath, itemList, totalSold);
                return (object)new List<object>()
        {
          (object) itemList,
          (object) totalSold
        };
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.StoreSales()");
            }
        }

        private static void WriteStoreSalesFile(
          string filePath,
          List<Distribution_Class.Item> itemList,
          int totalSold)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + str + "Description" + str + "Storecode" + str + "Qty Sold" + str + "UOM Qty" + str + "UOM" + str + "Units Sold" + str + "Ship Wt");
                    foreach (Distribution_Class.Item obj in itemList)
                        streamWriter.WriteLine(obj.Number + str + obj.Description.Replace(',', '.') + str + obj.Storecode + str + (object)obj.Sold + str + (object)obj.UOMQty + str + obj.UOM + str + (object)obj.UnitsSold + str + (object)obj.ShipWt);
                    streamWriter.WriteLine(Environment.NewLine + "Total sold = " + (object)totalSold);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WriteStoreSalesFile()");
            }
        }

        public static object InventoryQuantity(string filePath, Distribution_Class.FormInput form)
        {
            try
            {
                Distribution_Class.Item obj = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Item("quantitylist", location: form.Location).Rows)
                {
                    obj.Number = row["item"].ToString().Trim();
                    obj.Description = row["description"].ToString().Trim();
                    obj.UOM = row["uom"].ToString().Trim();
                    obj.Available = Convert.ToInt32(row["available"]);
                    obj.OnHand = Convert.ToInt32(row["onhand"]);
                    obj.Allocated = Convert.ToInt32(row["allocated"]);
                    obj.OnOrder = Convert.ToInt32(row["onorder"]);
                    obj.Location = row["location"].ToString().Trim();
                    obj.Cost = Math.Round(Convert.ToDecimal(row["cost"]), 2);
                    if (form.RemoveZeroAmount == "true")
                    {
                        if (obj.Available + obj.OnHand + obj.Allocated + obj.OnOrder > 0)
                            itemList.Add(obj);
                    }
                    else
                        itemList.Add(obj);
                    obj = new Distribution_Class.Item();
                }
                Distribution.WriteInventoryQuantityFile(filePath, itemList);
                return (object)itemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.InventoryQuantity()");
            }
        }

        private static void WriteInventoryQuantityFile(
          string filePath,
          List<Distribution_Class.Item> itemList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + str + "Description" + str + "UOM" + str + "Available" + str + "OnHand" + str + "Allocated" + str + "OnOrder" + str + "WHS" + str + "Cost");
                    foreach (Distribution_Class.Item obj in itemList)
                        streamWriter.WriteLine(obj.Number + str + obj.Description.Replace(',', '.') + str + obj.UOM + str + (object)obj.Available + str + (object)obj.OnHand + str + (object)obj.Allocated + str + (object)obj.OnOrder + str + obj.Location + str + (object)obj.Cost);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WriteInventoryQuantityFile()");
            }
        }

        public static void ItemSales(string filePath, Distribution_Class.FormInput form)
        {
            try
            {
                string str1 = ",";
                Distribution_Class.Item obj = new Distribution_Class.Item();
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Category" + str1 + "Item#" + str1 + "Description" + str1 + "Ship Date" + str1 + "Qty." + str1 + "UOM" + str1 + "Ext.Price" + str1 + "Ship Wt." + str1 + "Ship Method" + str1 + "Customer#" + str1 + "Ship Address" + str1 + "City" + str1 + "State" + str1 + "Zipcode");
                    foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Sales("item_sales", start: form.StartDate, end: form.EndDate, location: form.Location).Rows)
                    {
                        obj.Category = row["category"].ToString().Trim();
                        obj.Number = row["item"].ToString().Trim();
                        obj.Description = row["itemdesc"].ToString().Trim().Replace(',', '.');
                        obj.Date = row["shipdate"].ToString().Trim();
                        obj.Sold = Convert.ToInt32(row["qty"]);
                        obj.UOM = row["uom"].ToString().Trim();
                        obj.UOMQty = Convert.ToInt32(row["uomqty"]);
                        obj.Weight = Convert.ToDecimal(row["shipwt"]) * 0.01M;
                        obj.ShipWt = Math.Round((Decimal)(obj.Sold * obj.UOMQty) * obj.Weight, 2);
                        string str2 = row["shipmthd"].ToString().Trim();
                        obj.Storecode = row["shiptoname"].ToString().Trim().Replace(',', '.');
                        obj.Price = Convert.ToDecimal(row["extprice"]);
                        string str3 = row["address1"].ToString().Trim().Replace(',', ' ');
                        string str4 = row["city"].ToString().Trim().Replace(',', ' ');
                        string str5 = row["state"].ToString().Trim().Replace(',', ' ');
                        string str6 = row["zipcode"].ToString().Trim();
                        streamWriter.WriteLine(obj.Category + str1 + obj.Number + str1 + obj.Description + str1 + obj.Date + str1 + (object)obj.Sold + str1 + obj.UOM + str1 + (object)obj.Price + str1 + (object)obj.ShipWt + str1 + str2 + str1 + obj.Storecode + str1 + str3 + str1 + str4 + str1 + str5 + str1 + str6);
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemSales()");
            }
        }

        public static void ItemTurnover(string filePath, Distribution_Class.FormInput form)
        {
            try
            {
                string str = ",";
                Distribution_Class.Item obj = new Distribution_Class.Item();
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item No." + str + "Item Description" + str + "Base UOM" + str + "UOM" + str + "Piece/Case" + str + "Sales Month 1" + str + "Sales Month 2" + str + "Sales Month 3" + str + "Sales Month 4" + str + "Sales Month 5" + str + "Sales Month 6" + str + "Total Sales" + str + "Sale In Last 3 Month Avg." + str + "UOM OnHand WH-1" + str + "UOM OnHand WH-5" + str + "UOM OnHand Total" + str + "Inventory Turn (Last 3 Month Avg)" + str + "Intransit" + str + "Order Placed" + str + "Current Unit Cost/STD Cost" + str + "Current OnHand Extended Cost" + str + "(Last 3 Month Avg) Cost of Sale" + str + "Inventory Turn (Month)" + str + "Unit Sales Cost" + str + "Unit Sales Price" + str + "Gross Margin Per Unit" + str + "Gross Margin Ratio");
                    DataTable dataTable1 = Distribution_DB.ItemTurnover("item", form.Location);
                    DataTable dataTable2 = Distribution_DB.ItemTurnover("onhand", form.Location);
                    foreach (DataRow row1 in (InternalDataCollectionBase)dataTable1.Rows)
                    {
                        obj.Number = row1["item#"].ToString().Trim();
                        obj.Description = row1["Item_Description"].ToString().Trim().Replace(',', '.');
                        obj.UOMBase = row1["BASE_UOM"].ToString().Trim();
                        obj.UOM = row1["SALES_UOM"].ToString().Trim();
                        obj.UOMQty = Convert.ToInt32(row1["QTYBSUOM"]);
                        obj.Sold = Convert.ToInt32(row1["SALES_QTY_BASEuom"]) / obj.UOMQty;
                        obj.Cost = Math.Round(Convert.ToDecimal(row1["current_cost"]), 2);
                        obj.UnitPrice = Math.Round(Convert.ToDecimal(row1["price"]), 2);
                        obj.SalesMonth1 = Convert.ToInt32(row1["Month1"]);
                        obj.SalesMonth2 = Convert.ToInt32(row1["Month2"]);
                        obj.SalesMonth3 = Convert.ToInt32(row1["Month3"]);
                        obj.SalesMonth4 = Convert.ToInt32(row1["Month4"]);
                        obj.SalesMonth5 = Convert.ToInt32(row1["Month5"]);
                        obj.SalesMonth6 = Convert.ToInt32(row1["Month6"]);
                        obj.Sales = (Decimal)(obj.SalesMonth1 + obj.SalesMonth2 + obj.SalesMonth3 + obj.SalesMonth4 + obj.SalesMonth5 + obj.SalesMonth6);
                        Decimal num1 = (Decimal)((obj.SalesMonth1 + obj.SalesMonth2 + obj.SalesMonth3) / 3);
                        int num2 = 0;
                        int num3 = 0;
                        int num4 = 0;
                        Decimal num5 = 0M;
                        foreach (DataRow row2 in (InternalDataCollectionBase)dataTable2.Rows)
                        {
                            if (obj.Number == row2["item#"].ToString())
                            {
                                int int32_1 = Convert.ToInt32(row2["wh1"]);
                                int int32_2 = Convert.ToInt32(row2["wh5"]);
                                num2 = int32_1 / obj.UOMQty;
                                num3 = int32_2 / obj.UOMQty;
                                num4 = num2 + num3;
                                if (num1 != 0M)
                                {
                                    num5 = Math.Round((Decimal)num4 / num1, 2);
                                    break;
                                }
                                break;
                            }
                        }
                        Decimal num6 = Math.Round((Decimal)num4 * obj.Cost, 2);
                        Decimal num7 = Math.Round(num1 * obj.Cost, 2);
                        Decimal num8 = 0M;
                        if (num7 != 0M)
                            num8 = Math.Round(num6 / num7, 2);
                        streamWriter.WriteLine(obj.Number + str + obj.Description + str + obj.UOMBase + str + obj.UOM + str + (object)obj.UOMQty + str + (object)obj.SalesMonth1 + str + (object)obj.SalesMonth2 + str + (object)obj.SalesMonth3 + str + (object)obj.SalesMonth4 + str + (object)obj.SalesMonth5 + str + (object)obj.SalesMonth6 + str + (object)obj.Sales + str + (object)num1 + str + (object)num2 + str + (object)num3 + str + (object)num4 + str + (object)num5 + str + str + (object)obj.Sold + str + (object)obj.Cost + str + (object)num6 + str + (object)num7 + str + (object)num8 + str + (object)obj.Cost + str + (object)obj.UnitPrice + str + (object)(obj.UnitPrice - obj.Cost) + str + (object)Math.Round((obj.UnitPrice - obj.Cost) / obj.UnitPrice, 2));
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemTurnover()");
            }
        }

        public static object StoreDropList()
        {
            try
            {
                Distribution_Class.Option option = new Distribution_Class.Option();
                List<Distribution_Class.Option> optionList = new List<Distribution_Class.Option>();
                foreach (DataRow row in (InternalDataCollectionBase)AFC.GetStoreList().Rows)
                {
                    option.Id = row["storecode"].ToString().Trim();
                    option.Name = row["name"].ToString().Trim() + " : " + row["storecode"].ToString().Trim();
                    optionList.Add(option);
                    option = new Distribution_Class.Option();
                }
                return (object)optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.StoreDropList()");
            }
        }

        public static object VendorDropList()
        {
            try
            {
                Distribution_Class.Option option = new Distribution_Class.Option();
                List<Distribution_Class.Option> optionList = new List<Distribution_Class.Option>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.PurchaseGet("vendor", new Distribution_Class.FormInput()).Rows)
                {
                    option.Id = row["vendorid"].ToString().Trim();
                    option.Name = row["vendname"].ToString().Trim();
                    optionList.Add(option);
                    option = new Distribution_Class.Option();
                }
                return (object)optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.VendorDropList()");
            }
        }

        public static object BatchPicklistIds()
        {
            try
            {
                Distribution_Class.Option option = new Distribution_Class.Option();
                List<Distribution_Class.Option> optionList = new List<Distribution_Class.Option>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.BatchPicklist("batchIds").Rows)
                {
                    option.Id = option.Name = row["bachnumb"].ToString().Trim();
                    optionList.Add(option);
                    option = new Distribution_Class.Option();
                }
                return (object)optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchPicklistIds()");
            }
        }

        public static List<Distribution_Class.BatchListStore> BatchPicklistStores(
          string batch)
        {
            try
            {
                Distribution_Class.BatchListStore batchListStore = new Distribution_Class.BatchListStore();
                List<Distribution_Class.BatchListStore> batchListStoreList = new List<Distribution_Class.BatchListStore>();
                DataTable dataTable = Distribution_DB.BatchPicklist("stores", batch);
                if (dataTable.Rows.Count > 10)
                    throw new Exception("Number of stores exceeds limit of 10.");
                foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                {
                    batchListStore.OrderNo = row["sopnumbe"].ToString().Trim();
                    batchListStore.Code = row["custnmbr"].ToString().Trim();
                    batchListStore.Name = row["custname"].ToString().Trim();
                    batchListStore.State = row["state"].ToString().Trim();
                    batchListStore.ShipMethod = row["shipmthd"].ToString().Trim();
                    batchListStoreList.Add(batchListStore);
                    batchListStore = new Distribution_Class.BatchListStore();
                }
                for (int index = dataTable.Rows.Count + 1; index <= 10; ++index)
                {
                    batchListStore.OrderNo = "";
                    batchListStore.Code = "";
                    batchListStore.Name = "";
                    batchListStore.State = "";
                    batchListStore.ShipMethod = "";
                    batchListStoreList.Add(batchListStore);
                }
                return batchListStoreList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchPicklistStores()");
            }
        }

        private static Distribution_Class.PickListStore BatchPicklistByOrderNumber(
          List<Distribution_Class.BatchListStore> storeList)
        {
            try
            {
                return new Distribution_Class.PickListStore()
                {
                    Store1 = storeList[0].OrderNo,
                    Store2 = storeList[1].OrderNo,
                    Store3 = storeList[2].OrderNo,
                    Store4 = storeList[3].OrderNo,
                    Store5 = storeList[4].OrderNo,
                    Store6 = storeList[5].OrderNo,
                    Store7 = storeList[6].OrderNo,
                    Store8 = storeList[7].OrderNo,
                    Store9 = storeList[8].OrderNo,
                    Store10 = storeList[9].OrderNo
                };
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchPicklistByOrderNumber()");
            }
        }

        private static Distribution_Class.PicklistItem BatchPicklistQty(
          Distribution_Class.PickListStore store,
          Distribution_Class.PicklistItem pick,
          Distribution_Class.Item item)
        {
            try
            {
                Decimal num1;
                if (store.Store1 == item.OrderNumber)
                {
                    Decimal num2;
                    pick.LineTotal += num2 = Distribution.BatchPicklistSetQty(item);
                    num1 = num2 + (pick.Qty1 != "" ? Convert.ToDecimal(pick.Qty1) : 0M);
                    pick.Qty1 = num1.ToString();
                }
                if (store.Store2 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = Distribution.BatchPicklistSetQty(item);
                    num1 += pick.Qty2 != "" ? Convert.ToDecimal(pick.Qty2) : 0M;
                    pick.Qty2 = num1.ToString();
                }
                if (store.Store3 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = Distribution.BatchPicklistSetQty(item);
                    num1 += pick.Qty3 != "" ? Convert.ToDecimal(pick.Qty3) : 0M;
                    pick.Qty3 = num1.ToString();
                }
                if (store.Store4 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = Distribution.BatchPicklistSetQty(item);
                    num1 += pick.Qty4 != "" ? Convert.ToDecimal(pick.Qty4) : 0M;
                    pick.Qty4 = num1.ToString();
                }
                if (store.Store5 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = Distribution.BatchPicklistSetQty(item);
                    num1 += pick.Qty5 != "" ? Convert.ToDecimal(pick.Qty5) : 0M;
                    pick.Qty5 = num1.ToString();
                }
                if (store.Store6 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = Distribution.BatchPicklistSetQty(item);
                    num1 += pick.Qty6 != "" ? Convert.ToDecimal(pick.Qty6) : 0M;
                    pick.Qty6 = num1.ToString();
                }
                if (store.Store7 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = Distribution.BatchPicklistSetQty(item);
                    num1 += pick.Qty7 != "" ? Convert.ToDecimal(pick.Qty7) : 0M;
                    pick.Qty7 = num1.ToString();
                }
                if (store.Store8 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = Distribution.BatchPicklistSetQty(item);
                    num1 += pick.Qty8 != "" ? Convert.ToDecimal(pick.Qty8) : 0M;
                    pick.Qty8 = num1.ToString();
                }
                if (store.Store9 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = Distribution.BatchPicklistSetQty(item);
                    num1 += pick.Qty9 != "" ? Convert.ToDecimal(pick.Qty9) : 0M;
                    pick.Qty9 = num1.ToString();
                }
                if (store.Store10 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = Distribution.BatchPicklistSetQty(item);
                    num1 += pick.Qty10 != "" ? Convert.ToDecimal(pick.Qty10) : 0M;
                    pick.Qty10 = num1.ToString();
                }
                return pick;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchPicklistQty()");
            }
        }

        private static Decimal BatchPicklistSetQty(Distribution_Class.Item item)
        {
            try
            {
                return !item.UOM.ToLower().Contains("lb") ? (Decimal)item.Sold : (Decimal)(item.Lot != "" ? item.LotQty : item.Sold * item.UOMQty);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchPicklistSetQty()");
            }
        }

        public static List<Distribution_Class.PicklistItem> BatchPicklistItems(
          string filePath,
          Distribution_Class.FormInput form,
          List<Distribution_Class.BatchListStore> storeList)
        {
            try
            {
                Distribution_Class.PicklistItem pick = new Distribution_Class.PicklistItem();
                List<Distribution_Class.PicklistItem> picklistItemList = new List<Distribution_Class.PicklistItem>();
                string str1 = "";
                string str2 = "";
                Distribution_Class.PickListStore store = Distribution.BatchPicklistByOrderNumber(storeList);
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.BatchPicklist("items", form.Batch, form.Type).Rows)
                {
                    Distribution_Class.Item obj1 = new Distribution_Class.Item();
                    obj1.OrderNumber = row["SOPNUMBE"].ToString();
                    obj1.Number = row["item"].ToString();
                    obj1.Description = row["itemdesc"].ToString();
                    obj1.UOM = row["uom"].ToString();
                    obj1.Sold = Convert.ToInt32(row["qty"]);
                    obj1.UOMQty = Convert.ToInt32(row["uomqty"]);
                    obj1.Category = row["category"].ToString();
                    obj1.Lot = row["lot"].ToString();
                    obj1.LotQty = Convert.ToInt32(row["lotqty"]);
                    obj1.Fulfilled = Convert.ToInt32(row["fulfilled"]);
                    if (str1 != obj1.Number)
                    {
                        if (!string.IsNullOrEmpty(str1))
                        {
                            picklistItemList.Add(pick);
                            pick = new Distribution_Class.PicklistItem();
                        }
                        pick.Name = obj1.Description;
                        pick.Lot = obj1.Lot;
                        pick = Distribution.BatchPicklistQty(store, pick, obj1);
                        str1 = obj1.Number;
                        str2 = obj1.Lot;
                    }
                    else if (str2 == obj1.Lot)
                        pick = Distribution.BatchPicklistQty(store, pick, obj1);
                    else if (str2 != obj1.Lot)
                    {
                        picklistItemList.Add(pick);
                        pick = new Distribution_Class.PicklistItem();
                        pick.Name = obj1.Description;
                        pick.Lot = obj1.Lot;
                        pick = Distribution.BatchPicklistQty(store, pick, obj1);
                        str2 = obj1.Lot;
                    }
                    Distribution_Class.Item obj2 = new Distribution_Class.Item();
                }
                picklistItemList.Add(pick);
                return picklistItemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchPicklistItems()");
            }
        }

        public static void WriteBatchPickListFile(
          string filePath,
          List<Distribution_Class.PicklistItem> pickList,
          List<Distribution_Class.BatchListStore> storeList)
        {
            try
            {
                string str1 = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + str1 + "Lot" + str1 + "1" + str1 + "2" + str1 + "3" + str1 + "4" + str1 + "5" + str1 + "6" + str1 + "7" + str1 + "8" + str1 + "9" + str1 + "10" + str1 + "Total");
                    string str2 = (str1 ?? "") ?? "";
                    foreach (Distribution_Class.BatchListStore store in storeList)
                    {
                        str2 += str1;
                        str2 += store.Code;
                    }
                    streamWriter.WriteLine(str2);
                    foreach (Distribution_Class.PicklistItem pick in pickList)
                        streamWriter.WriteLine(pick.Name.Replace(",", " ") + str1 + pick.Lot + str1 + pick.Qty1 + str1 + pick.Qty2 + str1 + pick.Qty3 + str1 + pick.Qty4 + str1 + pick.Qty5 + str1 + pick.Qty6 + str1 + pick.Qty7 + str1 + pick.Qty8 + str1 + pick.Qty9 + str1 + pick.Qty10 + str1 + (object)pick.LineTotal);
                    streamWriter.WriteLine();
                    int num = 1;
                    foreach (Distribution_Class.BatchListStore store in storeList)
                        streamWriter.WriteLine(num++.ToString() + ". " + store.Name + ", " + store.State + str1 + store.ShipMethod);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WriteBatchPickListFile()");
            }
        }

        public static object BatchOrderIds()
        {
            try
            {
                Distribution_Class.Option option = new Distribution_Class.Option();
                List<Distribution_Class.Option> optionList = new List<Distribution_Class.Option>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.BatchOrder("batchlist").Rows)
                {
                    option.Id = option.Name = row["bachnumb"].ToString().Trim();
                    option.Count = Convert.ToInt32(row["ordercount"]);
                    optionList.Add(option);
                    option = new Distribution_Class.Option();
                }
                return (object)optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchOrderIds()");
            }
        }

        public static object BatchOrderData(Distribution_Class.FormInput form)
        {
            try
            {
                Distribution_Class.Order order = new Distribution_Class.Order();
                List<Distribution_Class.Order> orderList = new List<Distribution_Class.Order>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.BatchOrder("order", form.Batch).Rows)
                {
                    order.Number = row["orderno"].ToString().Trim();
                    order.OrderDate = row["orderdate"].ToString().Trim();
                    order.Storecode = row["storecode"].ToString().Trim();
                    order.Storename = row["storename"].ToString().Trim();
                    order.PurchaseOrderNo = row["ponumber"].ToString().Trim();
                    order.StoreState = row["state"].ToString().Trim();
                    order.ShipMethod = row["shipmthd"].ToString().Trim();
                    order.ShipWt = row["shipwt"].ToString().Trim();
                    order.Comment = row["comment"].ToString().Trim();
                    order.Location = row["location"].ToString().Trim();
                    order.Allocated = Convert.ToInt32(row["allocated"]);
                    orderList.Add(order);
                    order = new Distribution_Class.Order();
                }
                return (object)orderList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchOrderData()");
            }
        }

        public static string BatchOrderUpdateRun(Distribution_Class.FormInput form)
        {
            try
            {
                string batchId;
                if (!string.IsNullOrEmpty(form.NewBatch))
                {
                    batchId = form.NewBatch.Trim().ToUpper();
                    Distribution_DB.BatchOrderUpdate("insert_batchid", batchId: batchId);
                }
                else
                    batchId = form.SelectedBatch;
                string[] strArray = form.Order.Split(',');
                if (!string.IsNullOrEmpty(batchId))
                {
                    foreach (string orderNo in strArray)
                        Distribution_DB.BatchOrderUpdate("order", orderNo, batchId);
                    Distribution_DB.BatchOrderUpdate("batch_total", batchId: batchId);
                    Distribution_DB.BatchOrderUpdate("batch_total", batchId: form.Batch.ToUpper());
                }
                if (form.Allocate == "True")
                {
                    foreach (string orderNumber in strArray)
                    {
                        List<Distribution_Class.Item> itemList = (List<Distribution_Class.Item>)Distribution.OrderItemsList(orderNumber);
                        GP.OrderAllocateFulfill(orderNumber, itemList);
                    }
                }
                return "Done.";
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchOrderUpdateRun()");
            }
        }

        public static void BatchOrderChangeSiteID(Distribution_Class.FormInput form)
        {
            try
            {
                Distribution_Class.Order order = new Distribution_Class.Order();
                order.Batch = form.Batch;
                order.Location = form.Location;
                foreach (DataRow row1 in (InternalDataCollectionBase)Distribution_DB.BatchOrder("order", order.Batch).Rows)
                {
                    order.Number = row1["orderno"].ToString();
                    if (Convert.ToInt32(row1["allocaby"]) > 0)
                        throw new Exception("Exception: Allocated orders cannot change SiteID.");
                    Distribution_Class.Item obj = new Distribution_Class.Item();
                    List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                    foreach (DataRow row2 in (InternalDataCollectionBase)Distribution_DB.BatchOrder("order_item", order.Number).Rows)
                    {
                        obj.Number = row2["item"].ToString();
                        obj.Description = row2["itemdesc"].ToString();
                        obj.UOM = row2["uom"].ToString();
                        obj.LineSeq = Convert.ToInt32(row2["lineseq"]);
                        obj.Sold = Convert.ToInt32(row2["qty"]);
                        itemList.Add(obj);
                        obj = new Distribution_Class.Item();
                    }
                    GP.OrderSiteChange(order, itemList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object PromoRecords()
        {
            try
            {
                Distribution_Class.Promo promo = new Distribution_Class.Promo();
                List<Distribution_Class.Promo> promoList = new List<Distribution_Class.Promo>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Promo("records").Rows)
                {
                    promo.Id = Convert.ToInt32(row["promoid"]);
                    promo.Startdate = row["startdate"].ToString();
                    promo.Enddate = row["enddate"].ToString();
                    promo.Description = row["description"].ToString();
                    promo.Storeprefix = row["storeprefix"].ToString();
                    promo.State = row["state"].ToString();
                    promo.Ordercount = Convert.ToInt32(row["ordercount"]);
                    promo.Invoicecount = Convert.ToInt32(row["invoicecount"]);
                    promo.IsActive = row["isActive"].ToString();
                    promoList.Add(promo);
                    promo = new Distribution_Class.Promo();
                }
                return (object)promoList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoRecords()");
            }
        }

        public static object PromoDetail(int promoId)
        {
            try
            {
                Distribution_Class.Promo promo = new Distribution_Class.Promo();
                List<Distribution_Class.Promo> promoList = new List<Distribution_Class.Promo>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Promo("detail", promoId).Rows)
                {
                    promo.Id = promoId;
                    promo.Startdate = row["StartDate"].ToString();
                    promo.Enddate = row["EndDate"].ToString();
                    promo.Description = row["Description"].ToString();
                    promo.IsActive = row["IsActive"].ToString();
                    promo.Storeprefix = row["Storeprefix"].ToString();
                    promo.State = row["state"].ToString();
                    promo.Storecode = Distribution.PromoByStoreList(promoId);
                    promoList.Add(promo);
                }
                return (object)promoList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoDetail()");
            }
        }

        public static object PromoItemList(Distribution_Class.Promo promo, string filePath)
        {
            try
            {
                Distribution_Class.Item obj = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Promo("promo_item", promo.Id).Rows)
                {
                    obj.Number = row["item"].ToString();
                    obj.Description = row["itemdesc"].ToString();
                    obj.UOM = row["uom"].ToString();
                    obj.Sold = Convert.ToInt32(row["quantity"]);
                    obj.Cost = Convert.ToDecimal(row["uomcost"]);
                    obj.Price = Convert.ToDecimal(row["price"]);
                    obj.Sales = (Decimal)obj.Sold * obj.Price;
                    itemList.Add(obj);
                    obj = new Distribution_Class.Item();
                }
                Distribution.WritePromoItemListFile(filePath, itemList);
                return (object)itemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoItemList()");
            }
        }

        private static void WritePromoItemListFile(
          string filePath,
          List<Distribution_Class.Item> itemList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + str + "Description" + str + "UOM" + str + "Qty" + str + "Cost" + str + "Price" + str + "Ext Price");
                    foreach (Distribution_Class.Item obj in itemList)
                        streamWriter.WriteLine(obj.Number + str + obj.Description.Replace(',', '.') + str + obj.UOM + str + (object)obj.Sold + str + (object)obj.Cost + str + (object)obj.Price + str + (object)obj.Sales);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WritePromoItemListFile()");
            }
        }

        public static object PromoAddedToOrderList(Distribution_Class.Promo promo, string filePath)
        {
            try
            {
                Distribution_Class.Order order = new Distribution_Class.Order();
                List<Distribution_Class.Order> orderList = new List<Distribution_Class.Order>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Promo("orders_with_promo", promo.Id).Rows)
                {
                    order.Batch = row["bachnumb"].ToString();
                    order.Number = row["sopnumbe"].ToString();
                    order.OrderDate = row["docdate"].ToString();
                    order.ShipDate = row["shipdate"].ToString();
                    order.Storecode = row["custnmbr"].ToString();
                    order.Storename = row["custname"].ToString();
                    order.StoreState = row["state"].ToString();
                    order.FCID = row["fcid"].ToString();
                    order.DocAmount = Convert.ToDecimal(row["docamnt"]);
                    order.ShipMethod = row["shipmthd"].ToString();
                    order.Location = row["location"].ToString();
                    order.Allocated = Convert.ToInt32(row["allocated"]);
                    orderList.Add(order);
                    order = new Distribution_Class.Order();
                }
                Distribution.WritePromoAddedToOrderList(filePath, orderList);
                return (object)orderList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoAddedToOrderList()");
            }
        }

        private static void WritePromoAddedToOrderList(
          string filePath,
          List<Distribution_Class.Order> orderList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Batch" + str + "Order No." + str + "Doc.Date" + str + "Ship Date" + str + "Storecode" + str + "Storename" + str + "State" + str + "FCID" + str + "Total Amount" + str + "Ship Method" + str + "Location" + str + "Alloc");
                    foreach (Distribution_Class.Order order in orderList)
                        streamWriter.WriteLine(order.Batch + str + order.Number + str + order.OrderDate + str + order.ShipDate + str + order.Storecode + str + order.Storename.Replace(',', '.') + str + order.StoreState + str + order.FCID + str + (object)order.DocAmount + str + order.ShipMethod + str + order.Location + str + (order.Allocated > 0 ? (object)"X" : (object)""));
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WritePromoAddedToOrderList()");
            }
        }

        public static object PromoAddedToInvoiceList(Distribution_Class.Promo promo, string filePath)
        {
            try
            {
                Distribution_Class.Order order = new Distribution_Class.Order();
                List<Distribution_Class.Order> orderList = new List<Distribution_Class.Order>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Promo("invoices_with_promo", promo.Id).Rows)
                {
                    order.Batch = row["bachnumb"].ToString();
                    order.Number = row["sopnumbe"].ToString();
                    order.OrderDate = row["docdate"].ToString();
                    order.ShipDate = row["shipdate"].ToString();
                    order.Storecode = row["custnmbr"].ToString();
                    order.Storename = row["custname"].ToString();
                    order.StoreState = row["state"].ToString();
                    order.FCID = row["fcid"].ToString();
                    order.DocAmount = Convert.ToDecimal(row["docamnt"]);
                    order.ShipMethod = row["shipmthd"].ToString();
                    order.Location = row["location"].ToString();
                    order.OriginalNumber = row["orignumb"].ToString();
                    orderList.Add(order);
                    order = new Distribution_Class.Order();
                }
                Distribution.WritePromoAddedToInvoiceList(filePath, orderList);
                return (object)orderList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoAddedToInvoiceList()");
            }
        }

        private static void WritePromoAddedToInvoiceList(
          string filePath,
          List<Distribution_Class.Order> orderList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Batch" + str + "Invoice No." + str + "Orig.Number" + str + "Doc.Date" + str + "Ship Date" + str + "Storecode" + str + "Storename" + str + "State" + str + "FCID" + str + "Total Amount" + str + "Ship Method" + str + "Location");
                    foreach (Distribution_Class.Order order in orderList)
                        streamWriter.WriteLine(order.Batch + str + order.Number + str + order.OriginalNumber + str + order.OrderDate + str + order.ShipDate + str + order.Storecode + str + order.Storename.Replace(',', '.') + str + order.StoreState + str + order.FCID + str + (object)order.DocAmount + str + order.ShipMethod + str + order.Location);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WritePromoAddedToInvoiceList()");
            }
        }

        public static void PromoItemSave(string filePath, Distribution_Class.Promo promo)
        {
            try
            {
                Distribution_Class.Item obj = new Distribution_Class.Item();
                Distribution_DB.PromoUpdate("item_delete", promo);
                foreach (DataRow row in (InternalDataCollectionBase)Utilities.GetExcelData(filePath, "sheet1$").Rows)
                {
                    if (!Utilities.isNull(row["item"]))
                    {
                        obj.Number = row["item"].ToString().Trim();
                        obj.UOM = row["uom"].ToString().ToUpper().Trim();
                        obj.Sold = Convert.ToInt32(row["qty"]);
                        Distribution_DB.PromoItemUpdate(promo.Id, obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoItemSave()");
            }
        }

        public static void PromoStoreCreateSalesOrder(
          string filePath,
          Distribution_Class.Promo promo,
          Distribution_Class.FormInput form)
        {
            try
            {
                Decimal num = 0M;
                Distribution_Class.Item obj = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                Distribution_Class.StoreSalesOrder storeSalesOrder = new Distribution_Class.StoreSalesOrder();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Promo("sop_promo_item", promo.Id).Rows)
                {
                    obj.Number = row["item"].ToString().Trim();
                    obj.UOM = row["uom"].ToString().Trim();
                    obj.Sold = Convert.ToInt32(row["quantity"]);
                    obj.Weight = Convert.ToDecimal(row["itemshwt"]);
                    num += Math.Round((Decimal)obj.Sold * obj.Weight * 0.01M, 2);
                    itemList.Add(obj);
                    obj = new Distribution_Class.Item();
                }
                if (itemList.Count == 0)
                    throw new Exception("No items found for promo.  Cannot continue.");
                foreach (DataRow row in (InternalDataCollectionBase)Utilities.GetExcelData(filePath, "sheet1$").Rows)
                {
                    if (!Utilities.isNull(row["storecode"]))
                    {
                        storeSalesOrder.Code = row["storecode"].ToString().ToUpper().Trim();
                        storeSalesOrder.ShipWeight = num;
                        Distribution_Class.StoreSalesOrder infoForSalesOrder = AFC.GetFranchiseeInfoForSalesOrder(storeSalesOrder);
                        infoForSalesOrder.DocumentDate = DateTime.Now.ToString("yyyy-MM-dd", (IFormatProvider)CultureInfo.CreateSpecificCulture("en-US"));
                        infoForSalesOrder.Flag = promo.Id.ToString() + "p";
                        GP.PromoSalesOrder(form, itemList, infoForSalesOrder);
                    }
                    storeSalesOrder = new Distribution_Class.StoreSalesOrder();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoStoresCreateSalesOrder()");
            }
        }

        public static void PromoByStore(string filePath, Distribution_Class.Promo promo)
        {
            try
            {
                DataTable excelData = Utilities.GetExcelData(filePath, "sheet1$");
                foreach (DataRow row in (InternalDataCollectionBase)excelData.Rows)
                {
                    if (!Utilities.isNull(row["storecode"]))
                    {
                        string storeCode = row["storecode"].ToString().ToUpper().Trim();
                        Distribution_DB.PromoByStoreInsert(promo.Id, storeCode);
                    }
                }
                if (excelData.Rows.Count <= 0)
                    return;
                Distribution_DB.PromoUpdate("set_by_store", promo);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoByStore()");
            }
        }

        public static string PromoByStoreList(int promoId)
        {
            try
            {
                string str = "";
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Promo("by_store_list", promoId).Rows)
                {
                    if (str != "")
                        str += ", ";
                    str += row["storecode"].ToString();
                }
                return str;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoByStoreList()");
            }
        }

        public static object StateDroplist()
        {
            try
            {
                Distribution_Class.Option option1 = new Distribution_Class.Option();
                List<Distribution_Class.Option> optionList = new List<Distribution_Class.Option>();
                DataTable states = AFC.GetStates();
                option1.Id = option1.Name = "";
                optionList.Add(option1);
                Distribution_Class.Option option2 = new Distribution_Class.Option();
                foreach (DataRow row in (InternalDataCollectionBase)states.Rows)
                {
                    option2.Id = row["StateID"].ToString().Trim();
                    option2.Name = row["Statename"].ToString().Trim();
                    optionList.Add(option2);
                    option2 = new Distribution_Class.Option();
                }
                return (object)optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.StateDroplist()");
            }
        }

        public static void PurchaseList(string filePath, Distribution_Class.FormInput form)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Vendor" + str + "Receipt No." + str + "PO Type" + str + "Receipt Date" + str + "Item" + str + "Description" + str + "UOM" + str + "UOM Qty" + str + "Cost" + str + "Ext. Cost" + str + "PO Number" + str + "Qty Received" + str + "Units Received");
                    foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.PurchaseGet("history_list", form).Rows)
                        streamWriter.WriteLine(row["vendorid"].ToString().Trim() + str + row["POPRCTNM"].ToString().Trim() + str + Distribution.POPTypeLabel(Convert.ToInt32(row["poptype"])) + str + Convert.ToDateTime(row["receiptdate"]).ToString("MM/dd/yyyy").Trim() + str + row["itemnmbr"].ToString().Trim() + str + row["itemdesc"].ToString().Trim().Replace(',', '.') + str + row["UOFM"].ToString().Trim() + str + (object)Convert.ToInt32(row["umqtyinb"]) + str + (object)Convert.ToDecimal(row["unitcost"]) + str + (object)Convert.ToDecimal(row["extdcost"]) + str + row["ponumber"].ToString().Trim() + str + (object)Math.Round(Convert.ToDecimal(row["QTYSHPPD"]), 2) + str + (object)Math.Round(Convert.ToDecimal(row["QTYSHPPD"]) * (Decimal)Convert.ToInt32(row["umqtyinb"]), 2));
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PurchaseList()");
            }
        }

        private static string POPTypeLabel(int type)
        {
            try
            {
                string str = (string)null;
                switch (type)
                {
                    case 1:
                        str = "Shipment";
                        break;
                    case 2:
                        str = "Invoice";
                        break;
                    case 3:
                        str = "Shipment/Invoice";
                        break;
                }
                return str;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.POPTypeLabel()");
            }
        }

        public static object OrderItemsList(string orderNumber)
        {
            try
            {
                Distribution_Class.Item obj = new Distribution_Class.Item();
                List<Distribution_Class.Item> objList = new List<Distribution_Class.Item>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.BatchOrder("order_item", orderNumber).Rows)
                {
                    obj.Number = row["item"].ToString().Trim();
                    obj.Description = row["itemdesc"].ToString().Trim();
                    obj.UOM = row["uom"].ToString().Trim();
                    obj.UOMQty = Convert.ToInt32(row["uomqty"]);
                    obj.Sold = Convert.ToInt32(row["qty"]);
                    obj.LineSeq = Convert.ToInt32(row["lineseq"]);
                    obj.Category = row["category"].ToString().Trim();
                    obj.Location = row["location"].ToString();
                    obj.Allocated = Convert.ToInt32(row["allocated"]);
                    objList.Add(obj);
                    obj = new Distribution_Class.Item();
                }
                return (object)objList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.OrderItemsList()");
            }
        }

        public static object OrderItemsLotAvailable(string itemNumber)
        {
            try
            {
                Distribution_Class.Lot lot = new Distribution_Class.Lot();
                List<Distribution_Class.Lot> lotList = new List<Distribution_Class.Lot>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.ItemLot("available_lot", itemNumber).Rows)
                {
                    lot.Id = row["lot"].ToString().Trim();
                    lot.DateReceived = row["recd"].ToString();
                    lot.Onhand = Convert.ToInt32(row["onhand"]);
                    lot.DateSequence = Convert.ToInt32(row["dateseq"]);
                    lotList.Add(lot);
                    lot = new Distribution_Class.Lot();
                }
                return (object)lotList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.OrderItemsLotAvailable()");
            }
        }

        public static object OrderItemsLotAssigned(Distribution_Class.Item item)
        {
            try
            {
                List<object> objectList = new List<object>();
                double num = 0.0;
                Distribution_Class.Lot lot = new Distribution_Class.Lot();
                List<Distribution_Class.Lot> lotList = new List<Distribution_Class.Lot>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.ItemLot("item_lot", item.Number, item.OrderNumber, item.LineSeq).Rows)
                {
                    lot.Id = row["lot"].ToString().Trim();
                    lot.Qty = Convert.ToInt32(row["qty"]);
                    lot.DateReceived = row["recd"].ToString();
                    lot.DateSequence = Convert.ToInt32(row["dateseq"]);
                    num += (double)lot.Qty;
                    lotList.Add(lot);
                    lot = new Distribution_Class.Lot();
                }
                objectList.Add((object)num);
                objectList.Add((object)lotList);
                return (object)objectList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.OrderItemsLotAssigned()");
            }
        }

        public static int OrderItemNextLineSequence(string orderNumber)
        {
            try
            {
                int num = 1;
                DataTable dataTable = Distribution_DB.BatchOrder("item_next_line_sequence", orderNumber);
                if (dataTable.Rows.Count > 0)
                    num = Convert.ToInt32(dataTable.Rows[0]["lineseq"]) + 10;
                return num;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.OrderItemNextLineSequence()");
            }
        }

        public static object DropshipRecords()
        {
            try
            {
                Distribution_Class.Dropship dropship = new Distribution_Class.Dropship();
                List<Distribution_Class.Dropship> dropshipList = new List<Distribution_Class.Dropship>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Dropship("records").Rows)
                {
                    dropship.Id = Convert.ToInt32(row["DropshipID"]);
                    dropship.Description = row["Description"].ToString();
                    dropship.RateType = row["RateType"].ToString();
                    dropship.Rate = Convert.ToDecimal(row["Rate"]);
                    dropship.Company = row["company"].ToString();
                    dropshipList.Add(dropship);
                    dropship = new Distribution_Class.Dropship();
                }
                return (object)dropshipList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.DropshipRecords()");
            }
        }

        public static object DropshipEditRecord(int Id)
        {
            try
            {
                Distribution_Class.Dropship dropship = new Distribution_Class.Dropship();
                dropship.Id = Id;
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Dropship("detail", Id).Rows)
                {
                    dropship.Description = row["Description"].ToString();
                    dropship.RateType = row["RateType"].ToString();
                    dropship.Rate = Convert.ToDecimal(row["Rate"]);
                    dropship.Batch = row["batch"].ToString();
                    dropship.CompanyId = Convert.ToInt32(row["companyid"]);
                    dropship.InvoiceAppend = row["invoiceappend"].ToString();
                    dropship.ItemPrefix = row["itemprefix"].ToString();
                    dropship.ReplaceAFCInCustomerNo = row["replaceafcincustomerno"].ToString();
                    dropship.FreightMarker = row["freightmarker"].ToString();
                    dropship.CreatePayable = row["createpayable"].ToString();
                    dropship.ItemNumber = row["itemnumber"].ToString();
                    dropship.ItemCost = Convert.ToDecimal(row["itemcost"]);
                    dropship.VendorNumber = row["vendornumber"].ToString();
                    dropship.PONumber = row["ponumber"].ToString();
                    dropship.Freight = Convert.ToDecimal(row["freight"]);
                    dropship.Customer = row["Customer"].ToString();
                    dropship.Invoice = row["Invoice"].ToString();
                    dropship.InvoiceDate = row["InvoiceDate"].ToString();
                    dropship.Item = row["Item"].ToString();
                    dropship.ItemDesc = row["ItemDesc"].ToString();
                    dropship.UOM = row["UOM"].ToString();
                    dropship.Quantity = row["Quantity"].ToString();
                    dropship.Cost = row["Cost"].ToString();
                    dropship.ExtendedCost = row["ExtendedCost"].ToString();
                    dropship.Tax = row["Tax"].ToString();
                    dropship.Return = row["ReturnMap"].ToString();
                    dropship.Vendor = row["Vendor"].ToString();
                }
                return (object)dropship;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.DropshipEditRecord()");
            }
        }

        public static object DropshipVendorRecord(int dropId)
        {
            try
            {
                Distribution_Class.DropshipVendor dropshipVendor = new Distribution_Class.DropshipVendor();
                List<Distribution_Class.DropshipVendor> dropshipVendorList = new List<Distribution_Class.DropshipVendor>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Dropship("vendor", dropId).Rows)
                {
                    dropshipVendor.Id = Convert.ToInt32(row["DropshipVendorID"]);
                    dropshipVendor.Source = row["Source"].ToString();
                    dropshipVendor.Destination = row["Destination"].ToString();
                    dropshipVendorList.Add(dropshipVendor);
                    dropshipVendor = new Distribution_Class.DropshipVendor();
                }
                return (object)dropshipVendorList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.DropshipVendorRecord()");
            }
        }

        public static object DropshipCompany()
        {
            try
            {
                Distribution_Class.Company company = new Distribution_Class.Company();
                List<Distribution_Class.Company> companyList = new List<Distribution_Class.Company>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Dropship("company").Rows)
                {
                    company.Id = Convert.ToInt32(row["CMPANYID"]);
                    company.Name = row["name"].ToString();
                    companyList.Add(company);
                    company = new Distribution_Class.Company();
                }
                return (object)companyList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.DropshipCompany()");
            }
        }

        public static object DropshipCopyFrom()
        {
            try
            {
                Distribution_Class.Option option = new Distribution_Class.Option();
                List<Distribution_Class.Option> optionList = new List<Distribution_Class.Option>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.Dropship("copy_from").Rows)
                {
                    option.Id = row["DropshipID"].ToString();
                    option.Name = row["Description"].ToString();
                    optionList.Add(option);
                    option = new Distribution_Class.Option();
                }
                return (object)optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.DropshipCopyFrom()");
            }
        }

        public static void DropshipCopyFromTemplateToNewOne(Distribution_Class.Dropship drop)
        {
            try
            {
                Distribution_Class.Dropship drop1 = (Distribution_Class.Dropship)Distribution.DropshipEditRecord(drop.CopyFromId);
                drop1.Id = drop.Id;
                drop1.Description = drop.Description;
                drop1.Rate = drop.Rate;
                drop1.RateType = drop.RateType;
                drop1.Batch = drop.Batch;
                drop1.CompanyId = drop.CompanyId;
                Distribution_DB.DropshipUpdate("edit", drop1);
                List<Distribution_Class.DropshipVendor> dropshipVendorList = (List<Distribution_Class.DropshipVendor>)Distribution.DropshipVendorRecord(drop.CopyFromId);
                Distribution_Class.DropshipVendor vendor = new Distribution_Class.DropshipVendor();
                foreach (Distribution_Class.DropshipVendor dropshipVendor in dropshipVendorList)
                {
                    vendor.DropId = drop.Id;
                    vendor.Source = dropshipVendor.Source;
                    vendor.Destination = dropshipVendor.Destination;
                    Distribution_DB.DropshipVendorUpdate("create", vendor);
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.DropshipCopyFromTemplateToNewOne()");
            }
        }

        public static void DropshipImport(string filePath, int dropId)
        {
            try
            {
                Distribution_Class.DropshipItem dropshipItem = new Distribution_Class.DropshipItem();
                Distribution_Class.Dropship dropship = (Distribution_Class.Dropship)Distribution.DropshipEditRecord(dropId);
                DataTable dataTable1 = Distribution_DB.Dropship("vendor", dropId);
                DataTable dataTable2 = new DataTable();
                if (dropship.RateType == "PriceByQuantity")
                {
                    switch (dropship.CompanyId)
                    {
                        case 2:
                            dataTable2 = Distribution_DB.Dropship("pricelistqty_us", itemNumber: dropship.ItemNumber);
                            break;
                        case 3:
                            dataTable2 = Distribution_DB.Dropship("pricelistqty_canada", itemNumber: dropship.ItemNumber);
                            break;
                    }
                }
                foreach (DataRow row1 in (InternalDataCollectionBase)Utilities.GetExcelData(filePath, "sheet1$").Rows)
                {
                    if (!Utilities.isNull(row1[dropship.Customer]))
                    {
                        dropshipItem.DropId = dropId;
                        dropshipItem.Customer = !(dropship.ReplaceAFCInCustomerNo != "") ? row1[dropship.Customer].ToString().Trim() : Regex.Replace(row1[dropship.Customer].ToString().Trim(), "afc", dropship.ReplaceAFCInCustomerNo, RegexOptions.IgnoreCase);
                        if (string.IsNullOrEmpty(dropshipItem.Customer))
                            throw new Exception("Customer ID not found. Cannot continue with import.");
                        if (dropship.Invoice != "")
                            dropshipItem.Invoice = row1[dropship.Invoice].ToString().Trim() + dropship.InvoiceAppend;
                        dropshipItem.Date = !(dropship.InvoiceDate != "") ? DateTime.UtcNow.ToString("yyyy-MM-dd") : row1[dropship.InvoiceDate].ToString().Trim();
                        if (dropship.ItemNumber != "")
                        {
                            dropshipItem.Item = dropship.ItemNumber;
                        }
                        else
                        {
                            if (!(dropship.Item != ""))
                                throw new Exception("Item number was not found. Cannot continue with import.");
                            dropshipItem.Item = dropship.ItemPrefix + row1[dropship.Item].ToString().Trim();
                        }
                        if (dropship.ItemDesc != "")
                            dropshipItem.ItemDesc = row1[dropship.ItemDesc].ToString().Trim();
                        if (dropship.UOM != "")
                        {
                            dropshipItem.UOM = row1[dropship.UOM].ToString().ToUpper().Trim();
                            dropshipItem.UOM = dropshipItem.UOM == "EA" ? "EACH" : dropshipItem.UOM;
                        }
                        if (dropship.Quantity != "")
                            dropshipItem.Quantity = Convert.ToDecimal(row1[dropship.Quantity]);
                        if (dropship.ItemNumber != "")
                        {
                            if (dropship.RateType == "PriceByQuantity")
                            {
                                foreach (DataRow row2 in (InternalDataCollectionBase)dataTable2.Rows)
                                {
                                    if (dropshipItem.Quantity >= Convert.ToDecimal(row2["fromqty"]) && dropshipItem.Quantity <= Convert.ToDecimal(row2["toqty"]))
                                    {
                                        dropshipItem.Cost = Convert.ToDecimal(row2["price"]);
                                        break;
                                    }
                                }
                            }
                            else
                                dropshipItem.Cost = dropship.ItemCost;
                            dropshipItem.ExtCost = dropshipItem.Quantity * dropshipItem.Cost;
                        }
                        else if (dropship.ExtendedCost != "")
                        {
                            dropshipItem.ExtCost = Convert.ToDecimal(row1[dropship.ExtendedCost]);
                            dropshipItem.Cost = dropshipItem.ExtCost / dropshipItem.Quantity;
                        }
                        else if (dropship.Cost != "")
                        {
                            dropshipItem.Cost = Convert.ToDecimal(row1[dropship.Cost]);
                            dropshipItem.ExtCost = dropshipItem.Quantity * dropshipItem.Cost;
                        }
                        if (dropship.Tax != "")
                            dropshipItem.Tax = Convert.ToDecimal(row1[dropship.Tax]);
                        if (dropship.Return != "")
                            dropshipItem.ReturnFlag = Convert.ToInt32(row1[dropship.Return]);
                        if (dropship.FreightMarker != "" && dropshipItem.Item.Contains(dropship.FreightMarker))
                        {
                            dropshipItem.Item = row1[dropship.Item].ToString().Trim();
                            dropshipItem.Tax = dropship.CompanyId == 3 ? Math.Round(dropshipItem.Cost * 0.05M, 2) : 0M;
                            dropshipItem.FreightFlag = 1;
                        }
                        if (dropship.VendorNumber != "")
                        {
                            dropshipItem.Vendor = dropship.VendorNumber;
                            if (dropshipItem.Quantity > 0M && dropshipItem.Cost > 0M)
                                Distribution_DB.DropshipDataInsert(dropshipItem);
                            else
                                throw new Exception("Customer: " + dropshipItem.Customer + ", Item# " + dropshipItem.Item + " cost and quantity cannot be less than or equal to 0.");
                        }
                        else
                        {
                            if (!(dropship.Vendor != ""))
                                throw new Exception("No Vendor ID found.  Cannot continue with import.");
                            string str = row1[dropship.Vendor].ToString().Split(new string[2]
                            {
                "(",
                ")"
                            }, StringSplitOptions.RemoveEmptyEntries)[0];
                            foreach (DataRow row2 in (InternalDataCollectionBase)dataTable1.Rows)
                            {
                                if (str.Contains(row2["source"].ToString()))
                                {
                                    dropshipItem.Vendor = row2["destination"].ToString().Trim();
                                    if (dropshipItem.Quantity > 0M && dropshipItem.Cost > 0M)
                                    {
                                        Distribution_DB.DropshipDataInsert(dropshipItem);
                                        break;
                                    }
                                    throw new Exception("Customer: " + dropshipItem.Customer + ", Item# " + dropshipItem.Item + " cost and quantity cannot be less than or equal to 0.");
                                }
                            }
                        }
                        dropshipItem = new Distribution_Class.DropshipItem();
                    }
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex);
            }
        }

        public static string DropshipCustomerCheck(Distribution_Class.Dropship drop)
        {
            try
            {
                string str1 = "";
                DataTable dataTable1 = Distribution_DB.Dropship("import_customer", drop.Id);
                if (dataTable1.Rows.Count == 0)
                    throw new Exception("No import data found.");
                DataTable dataTable2 = Distribution_DB.Dropship("gp_customer", drop.Id);
                foreach (DataRow row1 in (InternalDataCollectionBase)dataTable1.Rows)
                {
                    bool flag = false;
                    string str2 = row1["customer"].ToString();
                    foreach (DataRow row2 in (InternalDataCollectionBase)dataTable2.Rows)
                    {
                        if (str2 == row2["customer"].ToString())
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        str1 += str1 != "" ? ", " : "";
                        str1 += str2;
                    }
                }
                return str1;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.DropshipCustomerCheck()");
            }
        }

        public static void DropshipGPInvoice(
          Distribution_Class.Dropship drop,
          DataTable invoiceTable,
          string documentType)
        {
            try
            {
                DataTable dataTable = new DataTable();
                Distribution_Class.DropshipItem header = new Distribution_Class.DropshipItem();
                Distribution_Class.DropshipItem dropshipItem = new Distribution_Class.DropshipItem();
                List<Distribution_Class.DropshipItem> itemList = new List<Distribution_Class.DropshipItem>();
                foreach (DataRow row1 in (InternalDataCollectionBase)invoiceTable.Rows)
                {
                    header.Customer = row1["customer"].ToString();
                    header.Invoice = row1["invoice"].ToString();
                    header.Date = row1["invoicedate"].ToString();
                    header.Total = Math.Round(Convert.ToDecimal(row1["total"]), 2);
                    if (header.Total == 0M)
                        throw new Exception("Cost/Extended Cost was not found.  Cannot continue.");
                    header.Vendor = row1["vendor"].ToString();
                    header.Tax = Math.Round(Convert.ToDecimal(row1["tax"]), 2);
                    header.TaxSchedule = row1["taxSchedule"].ToString();
                    if (header.Tax > 0M && header.TaxSchedule == "")
                        throw new Exception(header.Customer + " does not have a tax schedule.  Cannot continue with integration.");
                    if (documentType == "INVOICE")
                        dataTable = Distribution_DB.Dropship("invoice_item", drop.Id, header.Invoice);
                    else if (documentType == "DRTN")
                        dataTable = Distribution_DB.Dropship("invoice_item_return", drop.Id, header.Invoice);
                    foreach (DataRow row2 in (InternalDataCollectionBase)dataTable.Rows)
                    {
                        dropshipItem.Item = row2["item"].ToString();
                        dropshipItem.ItemDesc = row2["itemdesc"].ToString();
                        dropshipItem.Price = dropshipItem.Cost = Math.Round(Convert.ToDecimal(row2["cost"]), 2);
                        dropshipItem.Quantity = Convert.ToDecimal(row2["quantity"]);
                        dropshipItem.UOM = row2["uom"].ToString();
                        dropshipItem.Tax = Math.Round(Convert.ToDecimal(row2["tax"]), 2);
                        dropshipItem.FreightFlag = Convert.ToInt32(row2["freightflag"]);
                        if (dropshipItem.FreightFlag == 0)
                        {
                            string rateType = drop.RateType;
                            if (!(rateType == "CustomerMarkup"))
                            {
                                if (rateType == "CustomerDiscount")
                                {
                                    dropshipItem.Price = Math.Round(dropshipItem.Cost * (1M - drop.Rate * 0.01M), 2);
                                    dropshipItem.Tax = Math.Round(dropshipItem.Tax * (1M - drop.Rate * 0.01M), 2);
                                }
                            }
                            else
                            {
                                dropshipItem.Price = Math.Round(dropshipItem.Cost * (1M + drop.Rate * 0.01M), 2);
                                dropshipItem.Tax = Math.Round(dropshipItem.Tax * (1M + drop.Rate * 0.01M), 2);
                            }
                        }
                        dropshipItem.ExtCost = Math.Round(dropshipItem.Cost * dropshipItem.Quantity, 2);
                        dropshipItem.ExtPrice = Math.Round(dropshipItem.Price * dropshipItem.Quantity, 2);
                        if ((uint)dropshipItem.FreightFlag > 0U)
                        {
                            dropshipItem.ExtCost += dropshipItem.Tax;
                            dropshipItem.ExtPrice += dropshipItem.Tax;
                        }
                        itemList.Add(dropshipItem);
                        dropshipItem = new Distribution_Class.DropshipItem();
                    }
                    string str = documentType;
                    if (!(str == "INVOICE"))
                    {
                        if (str == "DRTN")
                        {
                            GP.DropshipSalesReturn(drop, header, itemList);
                            if (drop.CreatePayable.ToLower() == "yes")
                                GP.DropshipPayablesCreditMemo(drop, header);
                        }
                    }
                    else
                    {
                        GP.DropshipSalesInvoice(drop, header, itemList);
                        if (drop.CreatePayable.ToLower() == "yes")
                            GP.DropshipPayablesInvoice(drop, header);
                    }
                    header = new Distribution_Class.DropshipItem();
                    itemList = new List<Distribution_Class.DropshipItem>();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.DropshipGPInvoice()");
            }
        }

        public static void DropshipGPNoInvoiceFromVendor(
          Distribution_Class.Dropship drop,
          DataTable invoiceTable,
          string documentType)
        {
            try
            {
                DataTable dataTable = new DataTable();
                Distribution_Class.DropshipItem header = new Distribution_Class.DropshipItem();
                Distribution_Class.DropshipItem dropshipItem = new Distribution_Class.DropshipItem();
                foreach (DataRow row1 in (InternalDataCollectionBase)invoiceTable.Rows)
                {
                    header.Vendor = row1["vendor"].ToString();
                    header.Date = row1["invoicedate"].ToString();
                    header.Total = Math.Round(Convert.ToDecimal(row1["total"]), 2);
                    header.CustomerCount = Convert.ToInt32(row1["customercount"]);
                    if (documentType == "INVOICE")
                        dataTable = Distribution_DB.Dropship("no_invoice_item", drop.Id, header.Invoice);
                    else if (documentType == "DRTN")
                        dataTable = Distribution_DB.Dropship("no_invoice_item_return", drop.Id, header.Invoice);
                    foreach (DataRow row2 in (InternalDataCollectionBase)dataTable.Rows)
                    {
                        dropshipItem.Customer = row2["customer"].ToString();
                        dropshipItem.Date = row2["invoicedate"].ToString();
                        dropshipItem.Item = row2["item"].ToString();
                        dropshipItem.Price = dropshipItem.Cost = Math.Round(Convert.ToDecimal(row2["cost"]), 2);
                        dropshipItem.Quantity = Convert.ToDecimal(row2["quantity"]);
                        string rateType = drop.RateType;
                        if (!(rateType == "CustomerMarkup"))
                        {
                            if (rateType == "CustomerDiscount")
                                dropshipItem.Price = Math.Round(dropshipItem.Cost * (1M - drop.Rate * 0.01M), 2);
                        }
                        else
                            dropshipItem.Price = Math.Round(dropshipItem.Cost * (1M + drop.Rate * 0.01M), 2);
                        dropshipItem.ExtPrice = Math.Round(dropshipItem.Quantity * dropshipItem.Price, 2);
                        if (documentType.ToLower() == "invoice")
                            GP.DropshipSalesNoVendorInvoice(drop, header, dropshipItem);
                        else
                            GP.DropshipSalesNoVendorReturn(drop, header, dropshipItem);
                    }
                    if (drop.CreatePayable.ToLower() == "yes")
                        GP.DropshipPayablesForNoVendorInvoice(drop, header, dropshipItem);
                    header = new Distribution_Class.DropshipItem();
                    dropshipItem = new Distribution_Class.DropshipItem();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.DropshipGPNoInvoiceFromVendor()");
            }
        }

        public static string ItemAdjustmentRun(string filePath)
        {
            try
            {
                Distribution_Class.Item obj = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                Distribution_DB.ItemVarianceUpdate("delete_import_data", obj);
                obj.Batch = "INVADJ-" + DateTime.Now.ToString("MMddyy", (IFormatProvider)CultureInfo.CreateSpecificCulture("en-US"));
                if (Convert.ToInt32(Distribution_DB.ItemVariance("check_for_existing_batch", obj).Rows[0]["recordcount"]) > 0)
                    throw new Exception("Integration halted.   Found an existing batch: " + obj.Batch + ".   The batch must be deleted in GP before continuing.");
                Distribution.ItemAdjustmentImportFrozen(filePath);
                Distribution.ItemAdjustmentImportDryWithLot(filePath);
                Distribution.ItemAdjustmentImportDry(filePath);
                string[] strArray = new string[3]
                {
          "FROZ",
          "DRYLOT",
          "DRYNON"
                };
                string documentNumber = Distribution.ItemAdjustmentNextDocumentNumber();
                string documentDate = DateTime.Now.ToString("yyyy-MM-dd", (IFormatProvider)CultureInfo.CreateSpecificCulture("en-US"));
                int num = 1;
                for (int index = 0; index < strArray.Length; ++index)
                {
                    obj.Category = strArray[index];
                    foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.ItemVariance("import_data", obj).Rows)
                    {
                        obj.Category = strArray[index];
                        obj.DocumentNumber = documentNumber;
                        obj.Number = row["item"].ToString();
                        obj.Lot = row["lot"].ToString();
                        obj.LotDateReceived = row["lotdatereceived"].ToString();
                        obj.Location = row["location"].ToString();
                        obj.Available = Convert.ToInt32(row["available"]);
                        obj.QtyEntered = Convert.ToInt32(row["actual"]);
                        obj.Variance = Convert.ToInt32(row["Variance"]);
                        obj.UOM = row["uom"].ToString();
                        obj.Cost = Convert.ToDecimal(row["cost"]);
                        obj.LineSeq = num;
                        ++num;
                        itemList.Add(obj);
                        obj = new Distribution_Class.Item();
                    }
                }
                if (itemList.Count <= 0)
                    throw new Exception("No items were found for inventory adjustment.");
                GP.ItemVariance(itemList, documentNumber, documentDate);
                Distribution_DB.ItemVarianceUpdate("reason_code", new Distribution_Class.Item()
                {
                    DocumentNumber = documentNumber
                });
                Distribution_DB.ItemVarianceUpdate("delete_import_data", new Distribution_Class.Item());
                return "Done.";
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex);
            }
        }

        private static string ItemAdjustmentNextDocumentNumber()
        {
            try
            {
                string str = "";
                DataTable dataTable = Distribution_DB.ItemVariance("next_doc_number", new Distribution_Class.Item());
                if (dataTable.Rows.Count > 0)
                    str = dataTable.Rows[0]["docnumber"].ToString();
                return str;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemAdjustmentNextDocumentNumber()");
            }
        }

        private static bool ItemAdjustmentCheckLotAvailable(Distribution_Class.Item item)
        {
            try
            {
                bool flag = false;
                if (item.Variance < 0)
                {
                    DataTable dataTable = Distribution_DB.ItemVariance("check_lot_available", item);
                    if (dataTable.Rows.Count > 0 && Convert.ToInt32(dataTable.Rows[0]["recordcount"]) > 0)
                        flag = true;
                }
                else
                    flag = true;
                return flag;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemAdjustmentCheckLotAvailable()");
            }
        }

        private static void ItemAdjustmentImportFrozen(string filePath)
        {
            try
            {
                Distribution_Class.Item obj = new Distribution_Class.Item();
                int result = 0;
                foreach (DataRow row in (InternalDataCollectionBase)Utilities.GetExcelData(filePath, "Work Sheet Frozen$").Rows)
                {
                    if (string.IsNullOrEmpty(row["Item_Number"].ToString()))
                        break;
                    obj.Category = "FROZ";
                    obj.Number = row["Item_Number"].ToString().Trim();
                    obj.Lot = row["Lot_No"].ToString().Trim();
                    obj.LotDateReceived = Convert.ToDateTime(row["Lot_Received_Date"]).ToString("MM/dd/yyyy");
                    obj.Location = row["Location_Code"].ToString().Trim();
                    obj.Available = int.TryParse(row["Qty_Available"].ToString(), out result) ? Convert.ToInt32(row["Qty_Available"]) : 0;
                    obj.Variance = int.TryParse(row["Variance"].ToString(), out result) ? Convert.ToInt32(row["Variance"]) : 0;
                    obj.QtyEntered = int.TryParse(row["Actual Aailable"].ToString(), out result) ? Convert.ToInt32(row["Actual Aailable"]) : 0;
                    obj.UOM = row["BASEUOFM"].ToString().Trim();
                    obj.UnitCost = Convert.ToDecimal(row["UnitCost"]);
                    if ((uint)obj.Variance > 0U)
                    {
                        if (Distribution.ItemAdjustmentCheckLotAvailable(obj))
                            Distribution_DB.ItemVarianceUpdate("import_data", obj);
                        else
                            throw new Exception("Import Frozen: Quantity available is not >= variance for the following Item: " + obj.Number + ", Lot: " + obj.Lot + ", Date Recd: " + obj.LotDateReceived + ", Variance: " + (object)obj.Variance);
                    }
                    obj = new Distribution_Class.Item();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemAdjustmentImportFrozen()");
            }
        }

        private static void ItemAdjustmentImportDryWithLot(string filePath)
        {
            try
            {
                Distribution_Class.Item obj = new Distribution_Class.Item();
                int result = 0;
                foreach (DataRow row in (InternalDataCollectionBase)Utilities.GetExcelData(filePath, "Work Sheet Dry Lot#$").Rows)
                {
                    if (string.IsNullOrEmpty(row["Item_Number"].ToString()))
                        break;
                    obj.Category = "DRYLOT";
                    obj.Number = row["Item_Number"].ToString().Trim();
                    obj.Lot = row["Lot_No"].ToString().Trim();
                    obj.LotDateReceived = Convert.ToDateTime(row["Lot_Received_Date"]).ToString("MM/dd/yyyy");
                    obj.Location = row["WH"].ToString().Trim();
                    obj.Available = int.TryParse(row["Qty_Available"].ToString(), out result) ? Convert.ToInt32(row["Qty_Available"]) : 0;
                    obj.Variance = int.TryParse(row["Variance"].ToString(), out result) ? Convert.ToInt32(row["Variance"]) : 0;
                    obj.QtyEntered = int.TryParse(row["Actual Aailable"].ToString(), out result) ? Convert.ToInt32(row["Actual Aailable"]) : 0;
                    obj.UOM = row["BASEUOFM"].ToString().Trim();
                    obj.UnitCost = Convert.ToDecimal(row["UnitCost"]);
                    if ((uint)obj.Variance > 0U)
                    {
                        if (Distribution.ItemAdjustmentCheckLotAvailable(obj))
                            Distribution_DB.ItemVarianceUpdate("import_data", obj);
                        else
                            throw new Exception("Import Dry with Lot: Quantity available is not >= variance for the following Item: " + obj.Number + ", Lot: " + obj.Lot + ", Date Recd: " + obj.LotDateReceived + ", Variance: " + (object)obj.Variance);
                    }
                    obj = new Distribution_Class.Item();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemAdjustmentImportDryWithLot()");
            }
        }

        private static void ItemAdjustmentImportDry(string filePath)
        {
            try
            {
                Distribution_Class.Item obj = new Distribution_Class.Item();
                int result = 0;
                foreach (DataRow row in (InternalDataCollectionBase)Utilities.GetExcelData(filePath, "Work Sheet Dry NON Lot#$").Rows)
                {
                    if (string.IsNullOrEmpty(row["Item Number"].ToString()))
                        break;
                    obj.Category = "DRYNON";
                    obj.Number = row["Item Number"].ToString().Trim();
                    obj.Location = row["WH"].ToString().Trim();
                    obj.Variance = int.TryParse(row["Variance"].ToString(), out result) ? Convert.ToInt32(row["Variance"]) : 0;
                    obj.UOM = row["Base UOM"].ToString().Trim();
                    obj.UnitCost = Convert.ToDecimal(row["UnitCost"]);
                    if ((uint)obj.Variance > 0U)
                        Distribution_DB.ItemVarianceUpdate("import_data", obj);
                    obj = new Distribution_Class.Item();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemAdjustmentImportDry()");
            }
        }

        public static object ItemBin(string filePath)
        {
            try
            {
                Distribution_Class.ItemBin itemBin = new Distribution_Class.ItemBin();
                List<Distribution_Class.ItemBin> itemBinList = new List<Distribution_Class.ItemBin>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.ItemBin("records", itemBin).Rows)
                {
                    itemBin.Id = Convert.ToInt32(row["itembinid"]);
                    itemBin.Item = row["item"].ToString();
                    itemBin.Location = row["location"].ToString();
                    itemBin.BinCap = row["bincap"].ToString();
                    itemBin.Secondary = row["scnd"].ToString();
                    itemBin.Third = row["third"].ToString();
                    itemBin.ItemDesc = row["itemdesc"].ToString();
                    itemBinList.Add(itemBin);
                    itemBin = new Distribution_Class.ItemBin();
                }
                Distribution.WriteItemBinFile(filePath, itemBinList);
                return (object)itemBinList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemBin()");
            }
        }

        private static void WriteItemBinFile(
          string filePath,
          List<Distribution_Class.ItemBin> itemBinList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + str + "Item Description" + str + "Location" + str + "Bin Cap" + str + "Secondary" + str + "Third");
                    foreach (Distribution_Class.ItemBin itemBin in itemBinList)
                        streamWriter.WriteLine(itemBin.Item + str + itemBin.ItemDesc.Replace(',', ' ') + str + itemBin.Location.Replace(',', ' ') + str + itemBin.BinCap + str + itemBin.Secondary + str + itemBin.Third);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WriteItemBinFile()");
            }
        }

        public static object DropLabel(string filePath)
        {
            try
            {
                Distribution_Class.DropLabel drop = new Distribution_Class.DropLabel();
                List<Distribution_Class.DropLabel> dropList = new List<Distribution_Class.DropLabel>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.DropLabel("records", drop).Rows)
                {
                    drop.Id = Convert.ToInt32(row["droplabelid"]);
                    drop.City = row["city"].ToString();
                    drop.State = row["state"].ToString();
                    dropList.Add(drop);
                    drop = new Distribution_Class.DropLabel();
                }
                Distribution.WriteDropLabelFile(filePath, dropList);
                return (object)dropList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemBin()");
            }
        }

        private static void WriteDropLabelFile(
          string filePath,
          List<Distribution_Class.DropLabel> dropList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("City" + str + "State");
                    foreach (Distribution_Class.DropLabel drop in dropList)
                        streamWriter.WriteLine(drop.City.Replace(',', ' ') + str + drop.State);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WriteDropLabelFile()");
            }
        }

        public static object InTransitBillOfLading()
        {
            try
            {
                Distribution_Class.BillofLading lading = new Distribution_Class.BillofLading();
                List<Distribution_Class.BillofLading> billofLadingList = new List<Distribution_Class.BillofLading>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.InTransitBillOfLading("docids", lading).Rows)
                {
                    lading.DocNumber = row["docid"].ToString();
                    lading.DocDate = row["docdate"].ToString();
                    billofLadingList.Add(lading);
                    lading = new Distribution_Class.BillofLading();
                }
                return (object)billofLadingList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.InTransitBillOfLading()");
            }
        }

        public static object ExternalDistributionCenterBatchRecords(string externalCenterId)
        {
            try
            {
                Distribution_Class.Order order = new Distribution_Class.Order();
                List<Distribution_Class.Order> orderList = new List<Distribution_Class.Order>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.ExternalDistributionCenter("records", externalCenterId).Rows)
                {
                    order.Batch = row["batch"].ToString();
                    order.BatchDate = row["batchdate"].ToString();
                    order.BatchCount = Convert.ToInt32(row["batchcount"]);
                    orderList.Add(order);
                    order = new Distribution_Class.Order();
                }
                return (object)orderList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ExternalDistributionCenterBatch()");
            }
        }

        public static object ExternalDistributionCenterBatchDetail(
          Distribution_Class.Order order,
          string filePath)
        {
            try
            {
                List<Distribution_Class.Order> orderList = new List<Distribution_Class.Order>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.ExternalDistributionCenter("record_detail", order.Batch).Rows)
                {
                    order.Batch = row["batch"].ToString();
                    order.BatchDate = row["batchdate"].ToString();
                    order.Number = row["OrderNumber"].ToString();
                    order.OrderDate = row["OrderDate"].ToString();
                    order.Storecode = row["Customer"].ToString();
                    order.Storename = row["CustomerName"].ToString();
                    order.StoreState = row["ST"].ToString();
                    order.OriginalBatch = row["OriginalBatch"].ToString();
                    order.OriginalSite = row["OriginalSite"].ToString();
                    orderList.Add(order);
                    order = new Distribution_Class.Order();
                }
                Distribution.WriteExternalDistributionCenterBatchFile(filePath, orderList);
                return (object)orderList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ExternalDistributionCenterBatchDetail()");
            }
        }

        private static void WriteExternalDistributionCenterBatchFile(
          string filePath,
          List<Distribution_Class.Order> orderList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Batch" + str + "Batch Date" + str + "Order No." + str + "Order Date" + str + "Store" + str + "Store Name" + str + "ST" + str + "Orig. Batch" + str + "Orig. Site");
                    foreach (Distribution_Class.Order order in orderList)
                        streamWriter.WriteLine(order.Batch + str + order.BatchDate + str + order.Number + str + order.OrderDate + str + order.Storecode + str + order.Storename.Replace(',', ' ') + str + order.StoreState + str + order.OriginalBatch + str + order.OriginalSite);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WriteExternalDistributionCenterBatchFile()");
            }
        }

        public static object WMSTrxLogData(Distribution_Class.FormInput form, string filePath)
        {
            try
            {
                Distribution_Class.WarehouseMgmtSystem warehouseMgmtSystem = new Distribution_Class.WarehouseMgmtSystem();
                List<Distribution_Class.WarehouseMgmtSystem> wmsList = new List<Distribution_Class.WarehouseMgmtSystem>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.WarehouseMgmtSystem("transaction_log", form).Rows)
                {
                    warehouseMgmtSystem.Status = row["status"].ToString();
                    warehouseMgmtSystem.TrxType = row["trxtype"].ToString();
                    warehouseMgmtSystem.UserId = row["userid"].ToString();
                    warehouseMgmtSystem.TerminalId = row["terminalid"].ToString();
                    warehouseMgmtSystem.TrxDate = row["trxdate"].ToString();
                    warehouseMgmtSystem.DocNumber = row["docno"].ToString();
                    warehouseMgmtSystem.Item = row["item"].ToString();
                    warehouseMgmtSystem.Lot = row["lot"].ToString();
                    warehouseMgmtSystem.Qty = Convert.ToInt32(row["qty"]);
                    warehouseMgmtSystem.FromSite = row["fromsite"].ToString();
                    warehouseMgmtSystem.FromBin = row["frombin"].ToString();
                    warehouseMgmtSystem.ToSite = row["tosite"].ToString();
                    warehouseMgmtSystem.ToBin = row["tobin"].ToString();
                    wmsList.Add(warehouseMgmtSystem);
                    warehouseMgmtSystem = new Distribution_Class.WarehouseMgmtSystem();
                }
                Distribution.WriteWMSTrxFile(filePath, wmsList);
                return (object)wmsList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WMSTrxLogData()");
            }
        }

        private static void WriteWMSTrxFile(
          string filePath,
          List<Distribution_Class.WarehouseMgmtSystem> wmsList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Status" + str + "Trx Type" + str + "User Id" + str + "Terminal Id" + str + "Trx Date" + str + "Doc. Number" + str + "Item" + str + "Lot" + str + "Qty" + str + "From Site" + str + "From Bin" + str + "To Site" + str + "To Bin");
                    foreach (Distribution_Class.WarehouseMgmtSystem wms in wmsList)
                        streamWriter.WriteLine(wms.Status + str + wms.TrxType + str + wms.UserId + str + wms.TerminalId + str + wms.TrxDate + str + wms.DocNumber + str + wms.Item + str + wms.Lot.Replace(',', ' ') + str + (object)wms.Qty + str + wms.FromSite + str + wms.FromBin + str + wms.ToSite + str + wms.ToBin);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WriteWMSTrxFile()");
            }
        }

        public static void PickTicketLanter(Distribution_Class.FormInput form, string filePath)
        {
            try
            {
                string str1 = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("BatchId" + str1 + "OrderNo" + str1 + "DocDate" + str1 + "CustomerId" + str1 + "CustomerName" + str1 + "Address1" + str1 + "Address2" + str1 + "City" + str1 + "ST" + str1 + "Zip" + str1 + "Phone" + str1 + "Comment" + str1 + "FCID" + str1 + "FCPhone" + str1 + "ItemNo" + str1 + "ItemDesc" + str1 + "UOM" + str1 + "UOMQty" + str1 + "LineSeq" + str1 + "OrderQty" + str1 + "Unit/Each" + str1 + "PickQty");
                    foreach (DataRow row1 in (InternalDataCollectionBase)Distribution_DB.BatchPicklist("orderpicklist_store", form.Batch).Rows)
                    {
                        string orderNo = row1["sopnumbe"].ToString();
                        string str2 = form.Batch + str1 + orderNo + str1 + row1["docdate"].ToString() + str1 + row1["custnmbr"].ToString() + str1 + row1["custname"].ToString().Replace(",", " ") + str1 + row1["address1"].ToString().Replace(",", " ") + str1 + row1["address2"].ToString().Replace(",", " ") + str1 + row1["city"].ToString().Replace(",", " ") + str1 + row1["state"].ToString() + str1 + row1["zipcode"].ToString() + str1 + Utilities.FormatPhone(row1["phnumbr1"].ToString()) + str1 + Utilities.CleanForCSV(row1["cmmttext"].ToString()) + str1 + row1["fcid"].ToString() + str1 + row1["fcphone"].ToString();
                        foreach (DataRow row2 in (InternalDataCollectionBase)Distribution_DB.BatchPicklist("orderpicklist_item_ver2", orderNo: orderNo).Rows)
                        {
                            Distribution_Class.Item obj = new Distribution_Class.Item();
                            obj.Number = row2["item"].ToString();
                            obj.Description = row2["itemdesc"].ToString().Replace(",", " ");
                            obj.UOM = row2["uom"].ToString();
                            obj.UOMQty = Convert.ToInt32(row2["uomqty"]);
                            obj.LineSeq = Convert.ToInt32(row2["lineseq"]);
                            int num = obj.Sold = Convert.ToInt32(row2["qty"]);
                            obj.Category = row2["lanterpick"].ToString();
                            if (obj.Category == "Each")
                                num = obj.Sold * obj.UOMQty;
                            string str3 = obj.Number + str1 + obj.Description + str1 + obj.UOM + str1 + (object)obj.UOMQty + str1 + (object)obj.LineSeq + str1 + (object)obj.Sold + str1 + obj.Category + str1 + (object)num;
                            streamWriter.WriteLine(str2 + str1 + str3);
                        }
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PickTicketExport()");
            }
        }

        private static List<Distribution_Class.Lot> SalesOrderItemLots(
          string orderNo)
        {
            try
            {
                Distribution_Class.Lot lot = new Distribution_Class.Lot();
                List<Distribution_Class.Lot> lotList = new List<Distribution_Class.Lot>();
                foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.BatchPicklist("orderpicklist_item_lot", orderNo: orderNo).Rows)
                {
                    lot.ItemNo = row["item"].ToString();
                    lot.ItemLineSeq = Convert.ToInt32(row["lineseq"]);
                    lot.Id = row["lot"].ToString();
                    lot.Qty = Convert.ToInt32(row["qty"]);
                    lotList.Add(lot);
                    lot = new Distribution_Class.Lot();
                }
                return lotList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.SalesOrderItemLots()");
            }
        }
    }
}