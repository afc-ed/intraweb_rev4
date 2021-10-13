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
                //menuList.Add(new Distribution_Class.Menu()
                //{
                //    Id = "ItemAdjustment",
                //    Name = "Item Adjustment"
                //});
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
                //menuList.Add(new Distribution_Class.Menu()
                //{
                //    Id = "LanterReconcile",
                //    Name = "Lanter Reconcile"
                //});
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "WarehouseInvoiceReconcile",
                    Name = "Warehouse Invoice Reconcile"
                });
                menuList.Add(new Distribution_Class.Menu()
                {
                    Id = "TunaShip",
                    Name = "Tuna Ship"
                });
                menuList.Sort((x, y) => x.Name.CompareTo(y.Name));
                return menuList;
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
                Distribution_Class.Item item = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                DataTable table = Distribution_DB.Item("lowstock", location: form.Location);
                foreach (DataRow row in table.Rows)
                {
                    item.Number = row["item"].ToString();
                    item.Description = row["itemdesc"].ToString();
                    item.OnHand = Convert.ToInt32(row["onhand"]);
                    item.Allocated = Convert.ToInt32(row["allocated"]);
                    item.Available = Convert.ToInt32(row["available"]);
                    item.OnOrder = Convert.ToInt32(row["onorder"]);
                    item.Ratio = Math.Round(Convert.ToDecimal(row["ratio"]) / 0.01M, 0).ToString() + "%";
                    item.Location = row["location"].ToString();
                    itemList.Add(item);
                    item = new Distribution_Class.Item();
                }
                WriteLowStockFile(filePath, itemList);
                return itemList;
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
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("ItemNumber" + delim + "Description" + delim + "OnHand" + delim + "Allocated" + delim + "Available" + delim + 
                        "PO. OnOrder" + delim + "Ratio" + delim + "Warehouse");
                    foreach (Distribution_Class.Item obj in itemList)
                        streamWriter.WriteLine(
                            obj.Number + delim + 
                            obj.Description.Replace(',', '.') + delim + 
                            obj.OnHand + delim + 
                            obj.Allocated + delim + 
                            obj.Available + delim + 
                            obj.OnOrder + delim + 
                            obj.Ratio + delim + 
                            obj.Location);
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
                Distribution_Class.Item item = new Distribution_Class.Item();
                List<Distribution_Class.Item> priceList = new List<Distribution_Class.Item>();
                DataTable table = Distribution_DB.Item("pricelist");
                foreach (DataRow row in table.Rows)
                {
                    item.Number = row["item"].ToString().Trim();
                    item.Description = row["itemdesc"].ToString().Trim();
                    item.UOM = row["uom"].ToString().Trim();
                    item.UOMQty = Convert.ToInt32(row["uomqty"]);
                    item.Cost = Convert.ToDecimal(row["uomcost"]);
                    item.Price = Convert.ToDecimal(row["price"]);
                    priceList.Add(item);
                    item = new Distribution_Class.Item();
                }
                WritePriceListFile(filePath, priceList);
                return priceList;
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
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + delim + "Description" + delim + "UOM" + delim + "UOM Qty" + delim + "Cost" + delim + "Price");
                    foreach (Distribution_Class.Item price in priceList)
                        streamWriter.WriteLine(
                            price.Number + delim +
                            price.Description.Replace(',', '.') + delim +
                            price.UOM + delim +
                            price.UOMQty + delim +
                            price.Cost + delim +
                            price.Price 
                            );
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WritePriceListFile()");
            }
        }

        public static object PriceListWithLevel(string filePath, Distribution_Class.FormInput form)
        {
            try
            {
                Distribution_Class.Item item = new Distribution_Class.Item();
                List<Distribution_Class.Item> priceList = new List<Distribution_Class.Item>();
                DataTable table = Distribution_DB.Item("pricelist_with_level", location: (string.IsNullOrEmpty(form.PriceLevel) ? "STD" : form.PriceLevel));
                foreach (DataRow row in table.Rows)
                {
                    item.Number = row["item"].ToString().Trim();
                    item.Description = row["itemdesc"].ToString().Trim();
                    item.UOM = row["uom"].ToString().Trim();
                    item.UOMQty = Convert.ToInt32(row["uomqty"]);
                    item.Cost = Convert.ToDecimal(row["uomcost"]);
                    item.Price = Convert.ToDecimal(row["price"]);
                    item.PriceLevel = row["prclevel"].ToString();
                    priceList.Add(item);
                    item = new Distribution_Class.Item();
                }
                WritePriceListWithLevelFile(filePath, priceList);
                return priceList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PriceListWithLevel()");
            }
        }

        private static void WritePriceListWithLevelFile(string filePath, List<Distribution_Class.Item> priceList)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + delim + "Description" + delim + "UOM" + delim + "UOM Qty" + delim + "Cost" + delim + "Price" + delim + "Price Level");
                    foreach (Distribution_Class.Item price in priceList)
                        streamWriter.WriteLine(
                            price.Number + delim + 
                            price.Description.Replace(',', '.') + delim + 
                            price.UOM + delim + 
                            price.UOMQty + delim + 
                            price.Cost + delim + 
                            price.Price + delim +
                            price.PriceLevel
                            );
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WritePriceListWithLevelFile()");
            }
        }

        private static Distribution_Class.Item ItemInfo(string itemNo)
        {
            try
            {
                Distribution_Class.Item item = new Distribution_Class.Item();
                DataTable table = Distribution_DB.Item("item", itemNo);
                foreach (DataRow row in table.Rows)
                {
                    item.Number = row["item"].ToString().Trim();
                    item.Description = row["itemdesc"].ToString().Replace(',', ' ').Trim();
                    item.UOM = row["uom"].ToString().Trim();
                    item.Price = Convert.ToDecimal(row["price"]);
                    item.UOMQty = Convert.ToInt32(row["uomqty"]);
                }
                return item;
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
                Distribution_Class.Item item = ItemInfo(form.Item);
                DataTable table1 = Distribution_DB.Item("recall", form.Item.Trim(), form.Lot.Trim(), form.Location);
                foreach (DataRow row1 in table1.Rows)
                {
                    recall.InvoiceNo = row1["invoiceno"].ToString().Trim();
                    recall.DocDate = ((DateTime)row1["docdate"]).ToString("MM/dd/yyyy").Trim();
                    recall.ShipDate = row1["shipdate"].ToString().Contains("1900") ? "" : ((DateTime)row1["shipdate"]).ToString("MM/dd/yyyy").Trim();
                    recall.Item = row1["item"].ToString().Trim();
                    recall.UOM = item.UOM;
                    recall.Lot = row1["lot"].ToString().Trim();
                    recall.Quantity = Math.Round(Convert.ToDecimal(row1["qty"]), 1);
                    recall.Return = Math.Round(Convert.ToDecimal(row1["rtn"]), 1);
                    recall.Storecode = row1["customerno"].ToString().Trim();
                    DataTable table2 = AFC.GetStoreRecallInfo(recall.Storecode);
                    foreach (DataRow row2 in table2.Rows)
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
                WriteRecallFile(filePath, recallList);
                return recallList;
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
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Invoice No." + delim + "Doc. Date" + delim + "Ship Date" + delim + "Item" + delim + "UOM" + delim + 
                        "Lot" + delim + "Qty" + delim + "Return" + delim + "Storecode" + delim + "Store" + delim + "Address" + delim + "City" + delim + 
                        "ST" + delim + "Zipcode" + delim + "RM" + delim + "RM Cell" + delim + "RM eMail" + delim + "FCID" + delim + "FC" + delim + 
                        "FC Cell" + delim + "FC eMail" + delim + "Region" + delim + "StoreGroup" + delim + "StoreCorpGroup");
                    foreach (Distribution_Class.Recall recall in recallList)
                        streamWriter.WriteLine(
                            recall.InvoiceNo + delim +
                            recall.DocDate + delim + 
                            recall.ShipDate + delim + 
                            recall.Item + delim + 
                            recall.UOM + delim + 
                            recall.Lot.Replace(',', ' ') + delim + 
                            recall.Quantity + delim + 
                            recall.Return + delim + 
                            recall.Storecode + delim + 
                            (!string.IsNullOrEmpty(recall.Storename) ? recall.Storename.Replace(',', ' ') : "") + delim + 
                            (!string.IsNullOrEmpty(recall.Address) ? recall.Address.Replace(',', ' ') : "") + delim + 
                            (!string.IsNullOrEmpty(recall.City) ? recall.City.Replace(',', ' ') : "") + delim + 
                            recall.State + delim + 
                            recall.Zip + delim + 
                            (!string.IsNullOrEmpty(recall.RM) ? recall.RM.Replace(',', ' ') : "") + delim + 
                            recall.RMcell + delim + 
                            (!string.IsNullOrEmpty(recall.RMeMail) ? recall.RMeMail.Replace(',', '.') : "") + delim + 
                            recall.FCId + delim +
                            recall.FC + delim + 
                            recall.FCcell + delim + 
                            (!string.IsNullOrEmpty(recall.FCeMail) ? recall.FCeMail.Replace(',', '.') : "") + delim + 
                            recall.Region + delim + 
                            (!string.IsNullOrEmpty(recall.Storegroup) ? recall.Storegroup.Replace(',', ' ') : "") + delim + 
                            (!string.IsNullOrEmpty(recall.Storecorp) ? recall.Storecorp.Replace(',', ' ') : "")
                            );
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WriteRecallFile()");
            }
        }

        private static Distribution_Class.Item ItemQuantity(Distribution_Class.Item item, Distribution_Class.FormInput form)
        {
            try
            {
                DataTable table = Distribution_DB.Sales("itemquantity", item: item.Number, start: form.StartDate, end: form.EndDate, uomqty: item.UOMQty, location: form.Location);
                foreach (DataRow row in table.Rows)
                {
                    item.Receipt = Convert.ToInt32(!Utilities.isNull(row["rct"]) ? row["rct"] : 0);
                    item.Sold = Convert.ToInt32(!Utilities.isNull(row["sold"]) ? row["sold"] : 0);
                    item.Adjust = Convert.ToInt32(!Utilities.isNull(row["adj"]) ? row["adj"] : 0);
                    item.Transfer = Convert.ToInt32(!Utilities.isNull(row["trn"]) ? row["trn"] : 0);
                    item.Return = Convert.ToInt32(!Utilities.isNull(row["rtn"]) ? row["rtn"] : 0);
                    item.LocationsSoldAt = Convert.ToInt32(!Utilities.isNull(row["loc"]) ? row["loc"] : 0);
                }
                return item;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemQuantity()");
            }
        }

        private static Distribution_Class.Item ItemSold(Distribution_Class.Item item, Distribution_Class.FormInput form)
        {
            try
            {
                DataTable table = Distribution_DB.Sales("itemsold", item: item.Number, start: form.StartDate, end: form.EndDate, uomqty: item.UOMQty, location: form.Location);
                item.Receipt = 0;
                item.Adjust = 0;
                item.Transfer = 0;
                item.Return = 0;
                foreach (DataRow row in table.Rows)
                {
                    item.Sold = Convert.ToInt32(!Utilities.isNull(row["sold"]) ? row["sold"] : 0);
                    item.LocationsSoldAt = Convert.ToInt32(!Utilities.isNull(row["loc"]) ? row["loc"] : 0);
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
                decimal totalSales = 0M;
                Distribution_Class.Item item = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                DataTable table = new DataTable();
                string previousItem = "";
                if (string.IsNullOrEmpty(form.Item))  // get all items.
                {
                    table = Distribution_DB.Item("pricelist");
                }
                else if (form.Item.Contains(","))  // items separated by commas.
                {
                    string[] itemArray = Utilities.RemoveWhiteSpace(form.Item).Split(',');
                    if (!string.IsNullOrEmpty(itemArray[0]))
                    {
                        App.ExecuteSql("delete from App.dbo.UserInput");
                        // save items to table.
                        foreach (string itemInArray in itemArray)
                        {
                            App.AddUserInput("item", itemInArray);
                        }
                        table = Distribution_DB.Item("itemperuserinput", location: form.Location);
                    }
                }
                else if (form.Item.Contains("-"))  // item range.
                {
                    string[] itemArray2 = Utilities.RemoveWhiteSpace(form.Item).Split('-');
                    if (!string.IsNullOrEmpty(itemArray2[1]))
                    {
                        table = App.GetRow("SELECT [item], [ITEMDESC], [uom], [price], [uomqty], [uomcost] FROM [APP].[dbo].[viewItemPrice] WHERE item >= '" + itemArray2[0] + "' and item <= '" + itemArray2[1] + "' ORDER BY item");
                    }
                    else
                    {
                        throw new Exception("Item range is missing second value.");
                    }
                }
                else
                {
                    table = Distribution_DB.Item("item", form.Item, location: form.Location);
                }
                foreach (DataRow row in table.Rows)
                {
                    item.Number = row["item"].ToString().Trim();
                    item.Description = row["itemdesc"].ToString().Trim().Replace(',', '.');
                    item.UOM = row["uom"].ToString().Trim();
                    item.Price = Convert.ToDecimal(row["price"]);
                    item.UOMQty = Convert.ToInt32(row["uomqty"]);
                    item.Cost = Convert.ToDecimal(row["uomcost"]);
                    Distribution_Class.Item item2;
                    if (item.Number != previousItem)
                    {
                        item2 = Distribution.ItemQuantity(item, form);
                        item2.Sales = item2.Sold * item2.Price;
                        previousItem = item2.Number;
                    }
                    else
                    {
                        item2 = Distribution.ItemSold(item, form);
                        item2.Sales = item2.Sold * item2.Price;
                    }
                    totalSales += item2.Sales;
                    itemList.Add(item2);
                    item = new Distribution_Class.Item();
                }
                WriteItemLevelFile(filePath, itemList, totalSales);
                return new List<object>() {itemList, totalSales};
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemLevel()");
            }
        }

        private static void WriteItemLevelFile(string filePath, List<Distribution_Class.Item> itemList, decimal totalSales)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + delim + "Description" + delim + "UOM" + delim + "UOM Qty" + delim + "Cost" + delim + "Price" + delim + "Receipt" + delim + "Sold" + delim + "Adjustment" + delim + "Transfer" + delim + "Return" + delim + "No. of Locs." + delim + "Item Sales Amount");
                    foreach (Distribution_Class.Item item in itemList)
                        streamWriter.WriteLine(
                            item.Number + delim + 
                            item.Description.Replace(',', '.') + delim + 
                            item.UOM + delim + 
                            item.UOMQty + delim + 
                            item.Cost + delim + 
                            item.Price + delim + 
                            item.Receipt + delim + 
                            item.Sold + delim + 
                            item.Adjust + delim + 
                            item.Transfer + delim + 
                            item.Return + delim + 
                            item.LocationsSoldAt + delim + 
                            item.Sales
                            );
                    streamWriter.WriteLine(Environment.NewLine + "Total sales = " + Math.Round(totalSales, 2));
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
                decimal totalSales = 0M;
                Distribution_Class.Item item = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                DataTable table = new DataTable();
                if (string.IsNullOrEmpty(form.Item))  // all items.
                {
                    table = Distribution_DB.Item("pricelist");
                }
                else if (form.Item.Contains(","))  // items with comma separator.
                {
                    string[] itemArray = Utilities.RemoveWhiteSpace(form.Item).Split(',');
                    if (!string.IsNullOrEmpty(itemArray[0]))
                    {
                        App.ExecuteSql("delete from App.dbo.UserInput");
                        // add each item in array to table.
                        foreach (string itemInArray in itemArray)
                        {
                            App.AddUserInput("item", itemInArray);
                        }
                        table = Distribution_DB.Item("itemperuserinput", location: form.Location);
                    }
                }
                else if (form.Item.Contains("-"))  // item range.
                {
                    string[] itemArray2 = Utilities.RemoveWhiteSpace(form.Item).Split('-');
                    if (!string.IsNullOrEmpty(itemArray2[1]))
                    {
                        table = App.GetRow("SELECT [item], [ITEMDESC], [uom], [price], [uomqty], [uomcost] FROM [APP].[dbo].[viewItemPrice] WHERE item >= '" + itemArray2[0] + "' and item <= '" + itemArray2[1] + "' ORDER BY item");
                    }
                    else
                    {
                        throw new Exception("Item range is missing second value.");
                    }
                }
                else
                {
                    table = Distribution_DB.Item("item", form.Item, location: form.Location);
                }
                foreach (DataRow row in table.Rows)
                {
                    item.Number = row["item"].ToString().Trim();
                    item.Description = row["itemdesc"].ToString().Trim().Replace(',', '.');
                    item.UOM = row["uom"].ToString().Trim();
                    item.Price = Convert.ToDecimal(row["price"]);
                    item.UOMQty = Convert.ToInt32(row["uomqty"]);
                    item.Cost = Convert.ToDecimal(row["uomcost"]);
                    Distribution_Class.Item item2 = Distribution.ItemSold(item, form);
                    item2.Sales = item2.Sold * item2.Price;
                    item2.UnitsSold = item2.UOMQty * item2.Sold;
                    totalSales += item2.Sales;
                    itemList.Add(item2);
                    item = new Distribution_Class.Item();
                }
                WriteSalesFile(filePath, itemList, totalSales);
                return new List<object>() {itemList, totalSales};
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.Sales()");
            }
        }

        private static void WriteSalesFile(string filePath, List<Distribution_Class.Item> itemList, Decimal totalSales)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + delim + "Description" + delim + "UOM" + delim + "UOM Qty" + delim + "Cost" + delim + "Price" + delim + "Sold" + delim + "Units Sold" + delim + "Sales Amount");
                    foreach (Distribution_Class.Item item in itemList)
                        streamWriter.WriteLine(
                            item.Number + delim + 
                            item.Description.Replace(',', '.') + delim + 
                            item.UOM + delim + 
                            item.UOMQty + delim + 
                            item.Cost + delim + 
                            item.Price + delim + 
                            item.Sold + delim + 
                            item.UnitsSold + delim +
                            item.Sales
                            );
                    streamWriter.WriteLine(Environment.NewLine + "Total sales = " + Math.Round(totalSales, 2));
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
                Distribution_Class.Item item = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                DataTable dataTable = new DataTable();
                string[] itemArray;
                if (string.IsNullOrEmpty(form.Item))  // all items
                {
                    itemArray = Distribution_DB.Item("pricelist").Rows.OfType<DataRow>().Select(k => k[0].ToString().Trim()).ToArray();
                }
                else if (form.Item.Contains(","))  // comma separator
                {
                    itemArray = Utilities.RemoveWhiteSpace(form.Item).Split(',');
                }
                else if (form.Item.Contains("-"))  // range of items.
                {
                    itemArray = Utilities.RemoveWhiteSpace(form.Item).Split('-');
                    if (itemArray.Length != 2)
                    {
                        throw new Exception("Item range is missing second value.");
                    }
                    else
                    {
                        itemArray = App.GetRow("SELECT [item] FROM [APP].[dbo].[viewItemPrice] WHERE item >= '" + itemArray[0] + "' and item <= '" + itemArray[1] + "' ORDER BY item asc").Rows.OfType<DataRow>().Select(k => k[0].ToString().Trim()).ToArray();
                    }
                }
                else
                {
                    itemArray = form.Item.Split(' ');
                }
                string type;
                if (string.IsNullOrEmpty(form.Store))
                {
                    type = "all";
                }
                else
                {
                    type = "lookup";
                    App.ExecuteSql("delete from App.dbo.UserInput");
                    string[] storeArray = form.Store.Split(',');
                    foreach (string storecode in storeArray)
                    {
                        App.AddUserInput("storecode", storecode: storecode);
                    }
                }
                for (int index = 0; itemArray.Length > index; ++index)
                {
                    item.Number = itemArray[index].Trim();
                    string number = item.Number;
                    DataTable table = Distribution_DB.Sales("store", type, number, form.StartDate, form.EndDate, location: form.Location);
                    foreach (DataRow row in table.Rows)
                    {
                        item.Storecode = row["customer"].ToString().Trim();
                        item.Sold = Convert.ToInt32(row["qty"]);
                        item.UOMQty = Convert.ToInt32(row["uomqty"]);
                        item.Weight = Convert.ToDecimal(row["shipwt"]) * 0.01M;
                        item.ShipWt = Math.Round((Decimal)(item.Sold * item.UOMQty) * item.Weight, 2);
                        item.Description = row["itemdesc"].ToString().Trim();
                        item.UOM = row["uom"].ToString().Trim();
                        item.UnitsSold = item.Sold * item.UOMQty;
                        totalSold += item.Sold;
                        itemList.Add(item);
                        item = new Distribution_Class.Item();
                        item.Number = number;
                    }
                }
                WriteStoreSalesFile(filePath, itemList, totalSold);
                return new List<object>() {itemList, totalSold};
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.StoreSales()");
            }
        }

        private static void WriteStoreSalesFile(string filePath, List<Distribution_Class.Item> itemList, int totalSold)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + delim + "Description" + delim + "Storecode" + delim + "Qty Sold" + delim + "UOM Qty" + delim + "UOM" + delim + "Units Sold" + delim + "Ship Wt");
                    foreach (Distribution_Class.Item item in itemList)
                        streamWriter.WriteLine(
                            item.Number + delim + 
                            item.Description.Replace(',', '.') + delim + 
                            item.Storecode + delim + 
                            item.Sold + delim + 
                            item.UOMQty + delim + 
                            item.UOM + delim + 
                            item.UnitsSold + delim + 
                            item.ShipWt
                            );
                    streamWriter.WriteLine(Environment.NewLine + "Total sold = " + totalSold);
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
                Distribution_Class.Item item = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                DataTable lotTable = Distribution_DB.Item("lot_by_location", location: form.Location);
                DataTable table = Distribution_DB.Item("quantitylist", location: form.Location);
                foreach (DataRow row in table.Rows)
                {
                    item.Number = row["item"].ToString().Trim();
                    item.Description = row["description"].ToString().Trim();
                    item.UOM = row["uom"].ToString().Trim();
                    item.Available = Convert.ToInt32(row["available"]);
                    item.OnHand = Convert.ToInt32(row["onhand"]);
                    item.Allocated = Convert.ToInt32(row["allocated"]);
                    item.OnOrder = Convert.ToInt32(row["onorder"]);
                    item.Location = row["location"].ToString().Trim();
                    item.Cost = Math.Round(Convert.ToDecimal(row["cost"]), 2);
                    bool isFound = false;
                    // build string for lots by comparing item#'s.
                    foreach (DataRow lotRow in lotTable.Rows)
                    {
                        string itemNo = lotRow["item"].ToString();
                        if (item.Number == itemNo)
                        {
                            item.Lot += !string.IsNullOrEmpty(item.Lot) ? " | " : "";
                            item.Lot += lotRow["lot"].ToString() + " = " + Math.Round(Convert.ToDecimal(lotRow["avail"]), 0);
                            isFound = true;
                        }
                        // once we found a match the next item not matched will break the loop. 
                        else if (isFound)
                        {
                            break;
                        }
                    }
                    if (string.Compare(form.RemoveZeroAmount, "true", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        if (item.Available + item.OnHand + item.Allocated + item.OnOrder > 0)
                        {
                            itemList.Add(item);
                        }
                    }
                    else
                    {
                        itemList.Add(item);
                    }
                    item = new Distribution_Class.Item();
                }
                WriteInventoryQuantityFile(filePath, itemList);
                return itemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.InventoryQuantity()");
            }
        }

        private static void WriteInventoryQuantityFile(string filePath, List<Distribution_Class.Item> itemList)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + delim + "Description" + delim + "UOM" + delim + "Available" + delim + "OnHand" + delim + "Allocated" + delim + "OnOrder" + delim + "WHS" + delim + "Cost" + delim + "Lot");
                    foreach (Distribution_Class.Item item in itemList)
                        streamWriter.WriteLine(item.Number + delim + 
                            item.Description.Replace(',', '.') + delim + 
                            item.UOM + delim + 
                            item.Available + delim + 
                            item.OnHand + delim + 
                            item.Allocated + delim + 
                            item.OnOrder + delim + 
                            item.Location + delim + 
                            item.Cost + delim +
                            item.Lot
                            );
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
                string delim = ",";
                Distribution_Class.Item item = new Distribution_Class.Item();
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Category" + delim + "Item#" + delim + "Description" + delim + "Ship Date" + delim + "Qty." + delim + "UOM" + delim + "Ext.Price" + delim + "Ship Wt." + delim + "Ship Method" + delim + "Customer#" + delim + "Ship Address" + delim + "City" + delim + "State" + delim + "Zipcode");
                    DataTable table = Distribution_DB.Sales("item_sales", start: form.StartDate, end: form.EndDate, location: form.Location);
                    foreach (DataRow row in table.Rows)
                    {
                        item.Category = row["category"].ToString().Trim();
                        item.Number = row["item"].ToString().Trim();
                        item.Description = row["itemdesc"].ToString().Trim().Replace(',', '.');
                        item.Date = row["shipdate"].ToString().Trim();
                        item.Sold = Convert.ToInt32(row["qty"]);
                        item.UOM = row["uom"].ToString().Trim();
                        item.UOMQty = Convert.ToInt32(row["uomqty"]);
                        item.Weight = Convert.ToDecimal(row["shipwt"]) * 0.01M;
                        item.ShipWt = Math.Round((Decimal)(item.Sold * item.UOMQty) * item.Weight, 2);
                        string shipMethod = row["shipmthd"].ToString().Trim();
                        item.Storecode = row["shiptoname"].ToString().Trim().Replace(',', '.');
                        item.Price = Convert.ToDecimal(row["extprice"]);
                        string street = row["address1"].ToString().Trim().Replace(',', ' ');
                        string city = row["city"].ToString().Trim().Replace(',', ' ');
                        string state = row["state"].ToString().Trim().Replace(',', ' ');
                        string zip = row["zipcode"].ToString().Trim();
                        streamWriter.WriteLine(
                            item.Category + delim + 
                            item.Number + delim + 
                            item.Description + delim + 
                            item.Date + delim + 
                            item.Sold + delim + 
                            item.UOM + delim + 
                            item.Price + delim + 
                            item.ShipWt + delim + 
                            shipMethod + delim + 
                            item.Storecode + delim + 
                            street + delim + 
                            city + delim +
                            state + delim +
                            zip
                            );
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
                string delim = ",";
                Distribution_Class.Item item = new Distribution_Class.Item();
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item No." + delim + "Item Description" + delim + "Base UOM" + delim + "UOM" + delim + "Piece/Case" + delim + "Sales Month 1" + delim + "Sales Month 2" + delim + "Sales Month 3" + delim + "Sales Month 4" + delim + "Sales Month 5" + delim + "Sales Month 6" + delim + "Total Sales" + delim + "Sale In Last 3 Month Avg." + delim + "UOM OnHand WH-1" + delim + "UOM OnHand WH-5" + delim + "UOM OnHand Total" + delim + "Inventory Turn (Last 3 Month Avg)" + delim + "Intransit" + delim + "Order Placed" + delim + "Current Unit Cost/STD Cost" + delim + "Current OnHand Extended Cost" + delim + "(Last 3 Month Avg) Cost of Sale" + delim + "Inventory Turn (Month)" + delim + "Unit Sales Cost" + delim + "Unit Sales Price" + delim + "Gross Margin Per Unit" + delim + "Gross Margin Ratio");
                    DataTable table1 = Distribution_DB.ItemTurnover("item", form.Location);
                    DataTable table2 = Distribution_DB.ItemTurnover("onhand", form.Location);
                    foreach (DataRow row1 in table1.Rows)
                    {
                        item.Number = row1["item#"].ToString().Trim();
                        item.Description = row1["Item_Description"].ToString().Trim().Replace(',', '.');
                        item.UOMBase = row1["BASE_UOM"].ToString().Trim();
                        item.UOM = row1["SALES_UOM"].ToString().Trim();
                        item.UOMQty = Convert.ToInt32(row1["QTYBSUOM"]);
                        item.Sold = Convert.ToInt32(row1["SALES_QTY_BASEuom"]) / item.UOMQty;
                        item.Cost = Math.Round(Convert.ToDecimal(row1["current_cost"]), 2);
                        item.UnitPrice = Math.Round(Convert.ToDecimal(row1["price"]), 2);
                        item.SalesMonth1 = Convert.ToInt32(row1["Month1"]);
                        item.SalesMonth2 = Convert.ToInt32(row1["Month2"]);
                        item.SalesMonth3 = Convert.ToInt32(row1["Month3"]);
                        item.SalesMonth4 = Convert.ToInt32(row1["Month4"]);
                        item.SalesMonth5 = Convert.ToInt32(row1["Month5"]);
                        item.SalesMonth6 = Convert.ToInt32(row1["Month6"]);
                        item.Sales = item.SalesMonth1 + item.SalesMonth2 + item.SalesMonth3 + item.SalesMonth4 + item.SalesMonth5 + item.SalesMonth6;
                        decimal last3MonthAvg = (item.SalesMonth1 + item.SalesMonth2 + item.SalesMonth3) / 3;
                        int UOMOnhandWh1 = 0;
                        int UOMOnhandWh5 = 0;
                        int UOMOnhandTotal = 0;
                        decimal inventoryTurnLast3MonthAvg = 0;
                        foreach (DataRow row2 in table2.Rows)
                        {
                            if (item.Number == row2["item#"].ToString())
                            {
                                int wh1 = Convert.ToInt32(row2["wh1"]);
                                int wh5 = Convert.ToInt32(row2["wh5"]);
                                UOMOnhandWh1 = wh1 / item.UOMQty;
                                UOMOnhandWh5 = wh5 / item.UOMQty;
                                UOMOnhandTotal = UOMOnhandWh1 + UOMOnhandWh5;
                                if (last3MonthAvg != 0M)
                                {
                                    inventoryTurnLast3MonthAvg = Math.Round((decimal)UOMOnhandTotal / last3MonthAvg, 2);
                                    break;
                                }
                                break;
                            }
                        }
                        decimal UOMOnhandExtCost = Math.Round((decimal)UOMOnhandTotal * item.Cost, 2);
                        decimal last3MonthAvgCostofSale = Math.Round(last3MonthAvg * item.Cost, 2);
                        decimal inventoryTurnPerMonth = 0M;
                        if (last3MonthAvgCostofSale != 0M)
                        {
                            inventoryTurnPerMonth = Math.Round(UOMOnhandExtCost / last3MonthAvgCostofSale, 2);
                        }
                        streamWriter.WriteLine(
                            item.Number + delim + 
                            item.Description + delim + 
                            item.UOMBase + delim + 
                            item.UOM + delim + 
                            item.UOMQty + delim + 
                            item.SalesMonth1 + delim + 
                            item.SalesMonth2 + delim + 
                            item.SalesMonth3 + delim + 
                            item.SalesMonth4 + delim +
                            item.SalesMonth5 + delim + 
                            item.SalesMonth6 + delim + 
                            item.Sales + delim + 
                            last3MonthAvg + delim + 
                            UOMOnhandWh1 + delim + 
                            UOMOnhandWh5 + delim + 
                            UOMOnhandTotal + delim + 
                            inventoryTurnLast3MonthAvg + delim + 
                            delim + 
                            item.Sold + delim + 
                            item.Cost + delim + 
                            UOMOnhandExtCost + delim + 
                            last3MonthAvgCostofSale + delim + 
                            inventoryTurnPerMonth + delim + 
                            item.Cost + delim + 
                            item.UnitPrice + delim + 
                            (item.UnitPrice - item.Cost) + delim + 
                            Math.Round((item.UnitPrice - item.Cost) / item.UnitPrice, 2)
                            );
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
        // returns region Id based on comparing storecode.
        private static string FindRegionBasedOnStorecode(string storeCode, DataTable regionTable)
        {
            try
            {
                string region = "";
                foreach (DataRow row in regionTable.Rows)
                {
                    if (row["storecode"].ToString().Trim() == storeCode)
                    {
                        region = row["regionshorten"].ToString().Trim();
                        break;
                    }
                }
                return region;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.FindRegionBasedOnStorecode()");
            }
        }

        public static object StoreDropList()
        {
            try
            {
                Distribution_Class.Option option = new Distribution_Class.Option();
                List<Distribution_Class.Option> optionList = new List<Distribution_Class.Option>();
                DataTable table = AFC.GetStoreList();
                foreach (DataRow row in table.Rows)
                {
                    option.Id = row["storecode"].ToString().Trim();
                    option.Name = row["name"].ToString().Trim() + " : " + row["storecode"].ToString().Trim();
                    optionList.Add(option);
                    option = new Distribution_Class.Option();
                }
                return optionList;
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
                DataTable table = Distribution_DB.PurchaseGet("vendor", new Distribution_Class.FormInput());
                foreach (DataRow row in table.Rows)
                {
                    option.Id = row["vendorid"].ToString().Trim();
                    option.Name = row["vendname"].ToString().Trim();
                    optionList.Add(option);
                    option = new Distribution_Class.Option();
                }
                return optionList;
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
                DataTable table = Distribution_DB.BatchPicklist("batchIds");
                foreach (DataRow row in table.Rows)
                {
                    option.Id = option.Name = row["bachnumb"].ToString().Trim();
                    optionList.Add(option);
                    option = new Distribution_Class.Option();
                }
                return optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchPicklistIds()");
            }
        }

        public static List<Distribution_Class.BatchListStore> BatchPicklistStores(string batch)
        {
            try
            {
                Distribution_Class.BatchListStore batchListStore = new Distribution_Class.BatchListStore();
                List<Distribution_Class.BatchListStore> batchListStoreList = new List<Distribution_Class.BatchListStore>();
                DataTable table = Distribution_DB.BatchPicklist("stores", batch);
                if (table.Rows.Count > 10)
                {
                    throw new Exception("Number of stores exceeds limit of 10.");
                }
                foreach (DataRow row in table.Rows)
                {
                    batchListStore.OrderNo = row["sopnumbe"].ToString().Trim();
                    batchListStore.Code = row["custnmbr"].ToString().Trim();
                    batchListStore.Name = row["custname"].ToString().Trim();
                    batchListStore.State = row["state"].ToString().Trim();
                    batchListStore.ShipMethod = row["shipmthd"].ToString().Trim();
                    batchListStoreList.Add(batchListStore);
                    batchListStore = new Distribution_Class.BatchListStore();
                }
                for (int index = table.Rows.Count + 1; index <= 10; ++index)
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

        private static Distribution_Class.PickListStore BatchPicklistByOrderNumber(List<Distribution_Class.BatchListStore> storeList)
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

        private static Distribution_Class.PicklistItem BatchPicklistQty(Distribution_Class.PickListStore store, Distribution_Class.PicklistItem pick, Distribution_Class.Item item)
        {
            try
            {
                decimal num1;
                if (store.Store1 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = BatchPicklistSetQty(item);
                    num1 += (pick.Qty1 != "" ? Convert.ToDecimal(pick.Qty1) : 0M);
                    pick.Qty1 = num1.ToString();
                }
                if (store.Store2 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = BatchPicklistSetQty(item);
                    num1 += pick.Qty2 != "" ? Convert.ToDecimal(pick.Qty2) : 0M;
                    pick.Qty2 = num1.ToString();
                }
                if (store.Store3 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = BatchPicklistSetQty(item);
                    num1 += pick.Qty3 != "" ? Convert.ToDecimal(pick.Qty3) : 0M;
                    pick.Qty3 = num1.ToString();
                }
                if (store.Store4 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = BatchPicklistSetQty(item);
                    num1 += pick.Qty4 != "" ? Convert.ToDecimal(pick.Qty4) : 0M;
                    pick.Qty4 = num1.ToString();
                }
                if (store.Store5 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = BatchPicklistSetQty(item);
                    num1 += pick.Qty5 != "" ? Convert.ToDecimal(pick.Qty5) : 0M;
                    pick.Qty5 = num1.ToString();
                }
                if (store.Store6 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = BatchPicklistSetQty(item);
                    num1 += pick.Qty6 != "" ? Convert.ToDecimal(pick.Qty6) : 0M;
                    pick.Qty6 = num1.ToString();
                }
                if (store.Store7 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = BatchPicklistSetQty(item);
                    num1 += pick.Qty7 != "" ? Convert.ToDecimal(pick.Qty7) : 0M;
                    pick.Qty7 = num1.ToString();
                }
                if (store.Store8 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = BatchPicklistSetQty(item);
                    num1 += pick.Qty8 != "" ? Convert.ToDecimal(pick.Qty8) : 0M;
                    pick.Qty8 = num1.ToString();
                }
                if (store.Store9 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = BatchPicklistSetQty(item);
                    num1 += pick.Qty9 != "" ? Convert.ToDecimal(pick.Qty9) : 0M;
                    pick.Qty9 = num1.ToString();
                }
                if (store.Store10 == item.OrderNumber)
                {
                    pick.LineTotal += num1 = BatchPicklistSetQty(item);
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

        private static decimal BatchPicklistSetQty(Distribution_Class.Item item)
        {
            try
            {
                return !item.UOM.ToLower().Contains("lb") ? (decimal)item.Sold : (decimal)(item.Lot != "" ? item.LotQty : item.Sold * item.UOMQty);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchPicklistSetQty()");
            }
        }

        public static List<Distribution_Class.PicklistItem> BatchPicklistItems(string filePath, Distribution_Class.FormInput form, List<Distribution_Class.BatchListStore> storeList)
        {
            try
            {
                Distribution_Class.PicklistItem pick = new Distribution_Class.PicklistItem();
                List<Distribution_Class.PicklistItem> picklistItemList = new List<Distribution_Class.PicklistItem>();
                string itemInProgress = "";
                string lotInProgress = "";
                Distribution_Class.PickListStore store = BatchPicklistByOrderNumber(storeList);
                DataTable table = Distribution_DB.BatchPicklist("items", form.Batch, form.Type);
                foreach (DataRow row in table.Rows)
                {
                    Distribution_Class.Item item = new Distribution_Class.Item();
                    item.OrderNumber = row["SOPNUMBE"].ToString();
                    item.Number = row["item"].ToString();
                    item.Description = row["itemdesc"].ToString();
                    item.UOM = row["uom"].ToString();
                    item.Sold = Convert.ToInt32(row["qty"]);
                    item.UOMQty = Convert.ToInt32(row["uomqty"]);
                    item.Category = row["category"].ToString();
                    item.Lot = row["lot"].ToString();
                    item.LotQty = Convert.ToInt32(row["lotqty"]);
                    item.Fulfilled = Convert.ToInt32(row["fulfilled"]);
                    if (itemInProgress != item.Number)
                    {
                        if (!string.IsNullOrEmpty(itemInProgress))
                        {
                            picklistItemList.Add(pick);
                            pick = new Distribution_Class.PicklistItem();
                        }
                        pick.Name = item.Description;
                        pick.Lot = item.Lot;
                        pick = BatchPicklistQty(store, pick, item);
                        itemInProgress = item.Number;
                        lotInProgress = item.Lot;
                    }
                    else if (lotInProgress == item.Lot)
                    {
                        pick = BatchPicklistQty(store, pick, item);
                    }
                    else if (lotInProgress != item.Lot)
                    {
                        picklistItemList.Add(pick);
                        pick = new Distribution_Class.PicklistItem();
                        pick.Name = item.Description;
                        pick.Lot = item.Lot;
                        pick = BatchPicklistQty(store, pick, item);
                        lotInProgress = item.Lot;
                    }                    
                }
                // for last item.
                picklistItemList.Add(pick);
                return picklistItemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchPicklistItems()");
            }
        }

        public static void WriteBatchPickListFile(string filePath, List<Distribution_Class.PicklistItem> pickList, List<Distribution_Class.BatchListStore> storeList)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + delim + "Lot" + delim + "1" + delim + "2" + delim + "3" + delim + "4" + delim + "5" + delim + "6" + delim + "7" + delim + "8" + delim + "9" + delim + "10" + delim + "Total");
                    string storeCodeLine = delim;
                    foreach (Distribution_Class.BatchListStore store in storeList)
                    {
                        storeCodeLine += delim;
                        storeCodeLine += store.Code;
                    }
                    streamWriter.WriteLine(storeCodeLine);
                    foreach (Distribution_Class.PicklistItem pick in pickList)
                        streamWriter.WriteLine(
                            pick.Name.Replace(",", " ") + delim + 
                            pick.Lot + delim + 
                            pick.Qty1 + delim + 
                            pick.Qty2 + delim + 
                            pick.Qty3 + delim + 
                            pick.Qty4 + delim + 
                            pick.Qty5 + delim + 
                            pick.Qty6 + delim + 
                            pick.Qty7 + delim + 
                            pick.Qty8 + delim + 
                            pick.Qty9 + delim + 
                            pick.Qty10 + delim + 
                            pick.LineTotal
                            );
                    streamWriter.WriteLine();
                    int num = 1;
                    foreach (Distribution_Class.BatchListStore store in storeList)
                        streamWriter.WriteLine(num++.ToString() + ". " + store.Name + ", " + store.State + delim + store.ShipMethod);
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
                DataTable table = Distribution_DB.BatchOrder("batchlist");
                foreach (DataRow row in table.Rows)
                {
                    option.Id = option.Name = row["bachnumb"].ToString().Trim();
                    option.Count = Convert.ToInt32(row["ordercount"]);
                    optionList.Add(option);
                    option = new Distribution_Class.Option();
                }
                return optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchOrderIds()");
            }
        }

        public static object BatchOrderData(Distribution_Class.FormInput form, string filePath)
        {
            try
            {
                Distribution_Class.Order order = new Distribution_Class.Order();
                List<Distribution_Class.Order> orderList = new List<Distribution_Class.Order>();
                // get all usa regions.
                DataTable regionTable = AFC.GetAllUSAStoreRegions();
                // get orders for batch id.
                DataTable table = Distribution_DB.BatchOrder("order", form.Batch);
                foreach (DataRow row in table.Rows)
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
                    order.DocAmount = Convert.ToDecimal(row["ordertotal"]);
                    order.Region = FindRegionBasedOnStorecode(order.Storecode, regionTable);
                    orderList.Add(order);
                    order = new Distribution_Class.Order();
                }
                WriteBatchOrderFile(filePath, orderList);
                return orderList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchOrderData()");
            }
        }

        public static void WriteBatchOrderFile(string filePath, List<Distribution_Class.Order> orderList)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Storecode" + delim + "Store Name" + delim + "ST" + delim + "Region" + delim + "Ship Mthd" + delim + "Total");
                    foreach (Distribution_Class.Order order in orderList)
                    {
                        streamWriter.WriteLine(
                            order.Storecode + delim +
                            order.Storename + delim +
                            order.StoreState + delim +
                            order.Region + delim +
                            order.ShipMethod + delim +
                            order.DocAmount
                            );
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WriteBatchOrderFile()");
            }
        }


        public static string BatchOrderUpdateRun(Distribution_Class.FormInput form)
        {
            try
            {
                string newBatch = string.Empty;
                // check if old batch, if not then write to control table.
                BatchOrderRunControl(form.Batch);
                // if new batch entered then create it, else an existing batch was selected.
                if (!string.IsNullOrEmpty(form.NewBatch))
                {
                    newBatch = form.NewBatch.Trim().ToUpper();
                    Distribution_DB.BatchInsert(newBatch);
                }
                else
                {
                    newBatch = form.SelectedBatch;
                }
                // check if new batch is in use, if not then write to control table.
                BatchOrderRunControl(newBatch);
                string[] orders = form.Order.Split(',');
                // iterate through each order and update batch.
                foreach (string orderNo in orders)
                {
                    Distribution_DB.BatchOrderIDUpdate(orderNo, newBatch);
                    // new batch
                    Distribution_DB.BatchTotalUpdate(newBatch);
                    // old batch
                    Distribution_DB.BatchTotalUpdate(form.Batch);
                }
                // delete old and new batch from control table.
                Distribution_DB.BatchControlUpdate("run_control_delete", newBatch);
                Distribution_DB.BatchControlUpdate("run_control_delete", form.Batch);
                return "Done.";
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.BatchOrderUpdateRun()");
            }
        }

        private static void BatchOrderRunControl(string batchId)
        {
            try
            {
                DataTable table = Distribution_DB.BatchOrder("run_control", batchId);
                if (Convert.ToInt32(table.Rows[0]["recordcount"]) == 0)
                {
                    Distribution_DB.BatchControlUpdate("run_control_insert", batchId);
                }
                else
                {
                    throw new Exception(batchId + " batch is currently being updated by another user.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void BatchOrderChangeSiteID(Distribution_Class.FormInput form)
        {
            try
            {
                Distribution_Class.Order order = new Distribution_Class.Order();
                order.Batch = form.Batch;
                order.Location = form.Location;
                DataTable table1 = Distribution_DB.BatchOrder("order", order.Batch);
                foreach (DataRow row1 in table1.Rows)
                {
                    order.Number = row1["orderno"].ToString();
                    if (Convert.ToInt32(row1["allocaby"]) > 0)
                    {
                        throw new Exception("Exception: Allocated orders cannot change SiteID.");
                    }
                    Distribution_Class.Item item = new Distribution_Class.Item();
                    List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                    DataTable table2 = Distribution_DB.BatchOrder("order_item", order.Number);
                    foreach (DataRow row2 in table2.Rows)
                    {
                        item.Number = row2["item"].ToString();
                        item.Description = row2["itemdesc"].ToString();
                        item.UOM = row2["uom"].ToString();
                        item.LineSeq = Convert.ToInt32(row2["lineseq"]);
                        item.Sold = Convert.ToInt32(row2["qty"]);
                        itemList.Add(item);
                        item = new Distribution_Class.Item();
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
                DataTable table = Distribution_DB.Promo("records");
                foreach (DataRow row in table.Rows)
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
                return promoList;
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
                DataTable table = Distribution_DB.Promo("detail", promoId);
                foreach (DataRow row in table.Rows)
                {
                    promo.Id = promoId;
                    promo.Startdate = row["StartDate"].ToString();
                    promo.Enddate = row["EndDate"].ToString();
                    promo.Description = row["Description"].ToString();
                    promo.IsActive = row["IsActive"].ToString();
                    promo.Storeprefix = row["Storeprefix"].ToString();
                    promo.State = row["state"].ToString();
                    promo.Storecode = PromoByStoreList(promoId);
                    promoList.Add(promo);
                }
                return promoList;
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
                Distribution_Class.Item item = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                DataTable table = Distribution_DB.Promo("promo_item", promo.Id);
                foreach (DataRow row in table.Rows)
                {
                    item.Number = row["item"].ToString();
                    item.Description = row["itemdesc"].ToString();
                    item.UOM = row["uom"].ToString();
                    item.Sold = Convert.ToInt32(row["quantity"]);
                    item.Cost = Convert.ToDecimal(row["uomcost"]);
                    item.Price = Convert.ToDecimal(row["price"]);
                    item.Sales = (decimal)item.Sold * item.Price;
                    itemList.Add(item);
                    item = new Distribution_Class.Item();
                }
                Distribution.WritePromoItemListFile(filePath, itemList);
                return itemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoItemList()");
            }
        }

        private static void WritePromoItemListFile(string filePath, List<Distribution_Class.Item> itemList)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + delim + "Description" + delim + "UOM" + delim + "Qty" + delim + "Cost" + delim + "Price" + delim + "Ext Price");
                    foreach (Distribution_Class.Item item in itemList)
                        streamWriter.WriteLine(
                            item.Number + delim + 
                            item.Description.Replace(',', '.') + delim + 
                            item.UOM + delim + 
                            item.Sold + delim + 
                            item.Cost + delim + 
                            item.Price + delim + 
                            item.Sales
                            );
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
                DataTable dt = Distribution_DB.Promo("orders_with_promo", promo.Id);
                foreach (DataRow row in dt.Rows)
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
                return orderList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoAddedToOrderList()");
            }
        }

        private static void WritePromoAddedToOrderList(string filePath, List<Distribution_Class.Order> orderList)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Batch" + delim + "Order No." + delim + "Doc.Date" + delim + "Ship Date" + delim + "Storecode" + delim + "Storename" + delim + "State" + delim + "FCID" + delim + "Total Amount" + delim + "Ship Method" + delim + "Location" + delim + "Alloc");
                    foreach (Distribution_Class.Order order in orderList)
                        streamWriter.WriteLine(
                            order.Batch + delim + 
                            order.Number + delim + 
                            order.OrderDate + delim + 
                            order.ShipDate + delim + 
                            order.Storecode + delim + 
                            order.Storename.Replace(',', '.') + delim + 
                            order.StoreState + delim + 
                            order.FCID + delim + 
                            order.DocAmount + delim + 
                            order.ShipMethod + delim +
                            order.Location + delim + 
                            (order.Allocated > 0 ? "X" : "")
                            );
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
                DataTable table = Distribution_DB.Promo("invoices_with_promo", promo.Id);
                foreach (DataRow row in table.Rows)
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
                return orderList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoAddedToInvoiceList()");
            }
        }

        private static void WritePromoAddedToInvoiceList(string filePath, List<Distribution_Class.Order> orderList)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Batch" + delim + "Invoice No." + delim + "Orig.Number" + delim + "Doc.Date" + delim + "Ship Date" + delim + "Storecode" + delim + "Storename" + delim + "State" + delim + "FCID" + delim + "Total Amount" + delim + "Ship Method" + delim + "Location");
                    foreach (Distribution_Class.Order order in orderList)
                        streamWriter.WriteLine(
                            order.Batch + delim + 
                            order.Number + delim + 
                            order.OriginalNumber + delim + 
                            order.OrderDate + delim + 
                            order.ShipDate + delim + 
                            order.Storecode + delim + 
                            order.Storename.Replace(',', '.') + delim +
                            order.StoreState + delim + 
                            order.FCID + delim + 
                            order.DocAmount + delim + 
                            order.ShipMethod + delim + 
                            order.Location
                            );
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
                Distribution_Class.Item item = new Distribution_Class.Item();
                Distribution_DB.PromoUpdate("item_delete", promo);
                DataTable table = Utilities.GetExcelData(filePath, "sheet1$");
                foreach (DataRow row in table.Rows)
                {
                    if (!Utilities.isNull(row["item"]))
                    {
                        item.Number = row["item"].ToString().Trim();
                        item.UOM = row["uom"].ToString().ToUpper().Trim();
                        item.Sold = Convert.ToInt32(row["qty"]);
                        Distribution_DB.PromoItemUpdate(promo.Id, item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoItemSave()");
            }
        }

        public static void PromoStoreCreateSalesOrder(string filePath, Distribution_Class.Promo promo, Distribution_Class.FormInput form)
        {
            try
            {
                Decimal totalWeight = 0M;
                Distribution_Class.Item item = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                Distribution_Class.StoreSalesOrder storeSalesOrder = new Distribution_Class.StoreSalesOrder();
                DataTable table = Distribution_DB.Promo("sop_promo_item", promo.Id);
                foreach (DataRow row in table.Rows)
                {
                    item.Number = row["item"].ToString().Trim();
                    item.UOM = row["uom"].ToString().Trim();
                    item.Sold = Convert.ToInt32(row["quantity"]);
                    item.Weight = Convert.ToDecimal(row["itemshwt"]);
                    totalWeight += Math.Round((decimal)item.Sold * item.Weight * 0.01M, 2);
                    itemList.Add(item);
                    item = new Distribution_Class.Item();
                }
                if (itemList.Count == 0)
                {
                    throw new Exception("No items found for promo.  Cannot continue.");
                }
                DataTable dt2 = Utilities.GetExcelData(filePath, "sheet1$");
                foreach (DataRow row2 in dt2.Rows)
                {
                    if (!Utilities.isNull(row2["storecode"]))
                    {
                        storeSalesOrder.Code = row2["storecode"].ToString().ToUpper().Trim();
                        storeSalesOrder.ShipWeight = totalWeight;
                        Distribution_Class.StoreSalesOrder infoForSalesOrder = AFC.GetFranchiseeInfoForSalesOrder(storeSalesOrder);
                        infoForSalesOrder.DocumentDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"));
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
                DataTable table = Utilities.GetExcelData(filePath, "sheet1$");
                foreach (DataRow row in table.Rows)
                {
                    if (!Utilities.isNull(row["storecode"]))
                    {
                        string storeCode = row["storecode"].ToString().ToUpper().Trim();
                        Distribution_DB.PromoByStoreInsert(promo.Id, storeCode);
                    }
                }
                // set ByStoreFlag = 1, if records are found.
                if (table.Rows.Count > 0)
                {
                    Distribution_DB.PromoUpdate("set_by_store", promo);
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoByStore()");
            }
        }
        // param: promoId - csv file
        // param: form - user input
        // return: void
        // creates csv file for Purchase List download.
        public static string PromoByStoreList(int promoId)
        {
            try
            {
                string storecodes = "";
                DataTable table = Distribution_DB.Promo("by_store_list", promoId);
                foreach (DataRow row in table.Rows)
                {
                    if (storecodes != "")
                    {
                        storecodes += ", ";
                    }
                    storecodes += row["storecode"].ToString();
                }
                return storecodes;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PromoByStoreList()");
            }
        }
        // param: none
        // return: object - states
        // list of states for droplist.
        public static object StateDroplist()
        {
            try
            {
                Distribution_Class.Option option = new Distribution_Class.Option();
                List<Distribution_Class.Option> optionList = new List<Distribution_Class.Option>();
                DataTable states = AFC.GetStates();
                option.Id = option.Name = "";
                optionList.Add(option);                
                foreach (DataRow row in states.Rows)
                {
                    option.Id = row["StateID"].ToString().Trim();
                    option.Name = row["Statename"].ToString().Trim();
                    optionList.Add(option);
                    option = new Distribution_Class.Option();
                }
                return optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.StateDroplist()");
            }
        }
        // param: filePath - csv file
        // param: form - user input
        // return: void
        // creates csv file for Purchase List download.
        public static void PurchaseList(string filePath, Distribution_Class.FormInput form)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Vendor" + delim + "Receipt No." + delim + "PO Type" + delim + "Receipt Date" + delim + "Item" + delim + 
                        "Description" + delim + "UOM" + delim + "UOM Qty" + delim + "Cost" + delim + "Ext. Cost" + delim + "PO Number" + delim + "Qty Received" + delim + "Units Received");
                    DataTable table = Distribution_DB.PurchaseGet("history_list", form);
                    foreach (DataRow row in table.Rows)
                        streamWriter.WriteLine(
                            row["vendorid"].ToString().Trim() + delim + 
                            row["POPRCTNM"].ToString().Trim() + delim + 
                            Distribution.POPTypeLabel(Convert.ToInt32(row["poptype"])) + delim + 
                            Convert.ToDateTime(row["receiptdate"]).ToString("MM/dd/yyyy").Trim() + delim + 
                            row["itemnmbr"].ToString().Trim() + delim + 
                            row["itemdesc"].ToString().Trim().Replace(',', '.') + delim + 
                            row["UOFM"].ToString().Trim() + delim + 
                            Convert.ToInt32(row["umqtyinb"]) + delim + 
                            Convert.ToDecimal(row["unitcost"]) + delim + 
                            Convert.ToDecimal(row["extdcost"]) + delim + 
                            row["ponumber"].ToString().Trim() + delim + 
                            Math.Round(Convert.ToDecimal(row["QTYSHPPD"]), 2) + delim +
                            Math.Round(Convert.ToDecimal(row["QTYSHPPD"]) * (decimal)Convert.ToInt32(row["umqtyinb"]), 2)
                            );
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.PurchaseList()");
            }
        }
        // param: type
        // return: POP label
        // convert the number type to label
        private static string POPTypeLabel(int type)
        {
            try
            {
                string label = string.Empty;
                switch (type)
                {
                    case 1:
                        label = "Shipment";
                        break;
                    case 2:
                        label = "Invoice";
                        break;
                    case 3:
                        label = "Shipment/Invoice";
                        break;
                }
                return label;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.POPTypeLabel()");
            }
        }
        // param: orderNumber
        // return: object - items
        // get list of items for order
        public static object OrderItemsList(string orderNumber)
        {
            try
            {
                Distribution_Class.Item item = new Distribution_Class.Item();
                List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
                DataTable table = Distribution_DB.BatchOrder("order_item", orderNumber);
                foreach (DataRow row in table.Rows)
                {
                    item.Number = row["item"].ToString().Trim();
                    item.Description = row["itemdesc"].ToString().Trim();
                    item.UOM = row["uom"].ToString().Trim();
                    item.UOMQty = Convert.ToInt32(row["uomqty"]);
                    item.Sold = Convert.ToInt32(row["qty"]);
                    item.LineSeq = Convert.ToInt32(row["lineseq"]);
                    item.Category = row["category"].ToString().Trim();
                    item.Location = row["location"].ToString();
                    item.Allocated = Convert.ToInt32(row["allocated"]);
                    itemList.Add(item);
                    item = new Distribution_Class.Item();
                }
                return itemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.OrderItemsList()");
            }
        }
        // param: itemNumber
        // return: object - list of lots
        // get lots available for the item
        public static object OrderItemsLotAvailable(string itemNumber)
        {
            try
            {
                Distribution_Class.Lot lot = new Distribution_Class.Lot();
                List<Distribution_Class.Lot> lotList = new List<Distribution_Class.Lot>();
                DataTable table = Distribution_DB.ItemLot("available_lot", itemNumber);
                foreach (DataRow row in table.Rows)
                {
                    lot.Id = row["lot"].ToString().Trim();
                    lot.DateReceived = row["recd"].ToString();
                    lot.Onhand = Convert.ToInt32(row["onhand"]);
                    lot.DateSequence = Convert.ToInt32(row["dateseq"]);
                    lotList.Add(lot);
                    lot = new Distribution_Class.Lot();
                }
                return lotList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.OrderItemsLotAvailable()");
            }
        }
         
        // param: item
        // return: object - list of lots
        // get lots assigned to item        
        public static object OrderItemsLotAssigned(Distribution_Class.Item item)
        {
            try
            {
                List<object> objectList = new List<object>();
                double totalLotQty = 0.0;
                Distribution_Class.Lot lot = new Distribution_Class.Lot();
                List<Distribution_Class.Lot> lotList = new List<Distribution_Class.Lot>();
                DataTable table = Distribution_DB.ItemLot("item_lot", item.Number, item.OrderNumber, item.LineSeq);
                foreach (DataRow row in table.Rows)
                {
                    lot.Id = row["lot"].ToString().Trim();
                    lot.Qty = Convert.ToInt32(row["qty"]);
                    lot.DateReceived = row["recd"].ToString();
                    lot.DateSequence = Convert.ToInt32(row["dateseq"]);
                    totalLotQty += (double)lot.Qty;
                    lotList.Add(lot);
                    lot = new Distribution_Class.Lot();
                }
                objectList.Add(totalLotQty);
                objectList.Add(lotList);
                return objectList;
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
                int lineseq = 1;
                DataTable table = Distribution_DB.BatchOrder("item_next_line_sequence", orderNumber);
                if (table.Rows.Count > 0)
                {
                    lineseq = Convert.ToInt32(table.Rows[0]["lineseq"]) + 10;
                }
                return lineseq;
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
                DataTable table = Distribution_DB.Dropship("records");
                foreach (DataRow row in table.Rows)
                {
                    dropship.Id = Convert.ToInt32(row["DropshipID"]);
                    dropship.Description = row["Description"].ToString();
                    dropship.RateType = row["RateType"].ToString();
                    dropship.Rate = Convert.ToDecimal(row["Rate"]);
                    dropship.Company = row["company"].ToString();
                    dropshipList.Add(dropship);
                    dropship = new Distribution_Class.Dropship();
                }
                return dropshipList;
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
                DataTable table = Distribution_DB.Dropship("detail", id: Id);
                foreach (DataRow row in table.Rows)
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
                return dropship;
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
                DataTable table = Distribution_DB.Dropship("vendor", id: dropId);
                foreach (DataRow row in table.Rows)
                {
                    dropshipVendor.Id = Convert.ToInt32(row["DropshipVendorID"]);
                    dropshipVendor.Source = row["Source"].ToString();
                    dropshipVendor.Destination = row["Destination"].ToString();
                    dropshipVendorList.Add(dropshipVendor);
                    dropshipVendor = new Distribution_Class.DropshipVendor();
                }
                return dropshipVendorList;
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
                DataTable table = Distribution_DB.Dropship("company");
                foreach (DataRow row in table.Rows)
                {
                    company.Id = Convert.ToInt32(row["CMPANYID"]);
                    company.Name = row["name"].ToString();
                    companyList.Add(company);
                    company = new Distribution_Class.Company();
                }
                return companyList;
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
                DataTable table = Distribution_DB.Dropship("copy_from");
                foreach (DataRow row in table.Rows)
                {
                    option.Id = row["DropshipID"].ToString();
                    option.Name = row["Description"].ToString();
                    optionList.Add(option);
                    option = new Distribution_Class.Option();
                }
                return optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.DropshipCopyFrom()");
            }
        }
        // copy existing template for new dropship.
        public static void DropshipCopyFromTemplateToNewOne(Distribution_Class.Dropship drop)
        {
            try
            {
                Distribution_Class.Dropship drop1 = (Distribution_Class.Dropship)DropshipEditRecord(drop.CopyFromId);
                drop1.Id = drop.Id;
                drop1.Description = drop.Description;
                drop1.Rate = drop.Rate;
                drop1.RateType = drop.RateType;
                drop1.Batch = drop.Batch;
                drop1.CompanyId = drop.CompanyId;
                Distribution_DB.DropshipUpdate("edit", drop1);
                // recast object to list.
                List<Distribution_Class.DropshipVendor> dropshipVendorList = (List<Distribution_Class.DropshipVendor>)DropshipVendorRecord(drop.CopyFromId);
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
        // imports dropship data into table based on template fields, missing items or cost and qty not > 0 are ignored.
        public static void DropshipImport(string filePath, int dropId)
        {
            string customerIdErrorCheck = string.Empty;
            try
            {
                Distribution_Class.DropshipItem dropshipItem = new Distribution_Class.DropshipItem();
                // this holds the template info.
                Distribution_Class.Dropship dropship = (Distribution_Class.Dropship)Distribution.DropshipEditRecord(dropId);
                DataTable dataTable1 = Distribution_DB.Dropship("vendor", dropId);
                DataTable dataTable2 = new DataTable();
                if (string.Compare(dropship.RateType, "PriceByQuantity", StringComparison.OrdinalIgnoreCase) == 0 )
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
                // get excel data and based on field maps import to table.
                DataTable table1 = Utilities.GetExcelData(filePath, "sheet1$");
                foreach (DataRow row1 in table1.Rows)
                {
                    // check for last record in file.
                    if (!Utilities.isNull(row1[dropship.Customer]))
                    {
                        dropshipItem.DropId = dropId;
                        // check for "afc" in customer# if it needs to be replaced when the template defines it. 
                        customerIdErrorCheck = dropshipItem.Customer = string.IsNullOrEmpty(dropship.ReplaceAFCInCustomerNo) ? row1[dropship.Customer].ToString().Trim() : Regex.Replace(row1[dropship.Customer].ToString().Trim(), "afc", dropship.ReplaceAFCInCustomerNo, RegexOptions.IgnoreCase);
                        // check for customer id, if not found then get next record.
                        if (string.IsNullOrEmpty(dropshipItem.Customer))
                        {
                            continue;
                        }
                        // invoice map, if found then append it with what's set in template.
                        if (!string.IsNullOrEmpty(dropship.Invoice))
                        {
                            dropshipItem.Invoice = row1[dropship.Invoice].ToString().Trim() + dropship.InvoiceAppend;
                        }
                        // invoice date map, if not found then use current date or use import data.
                        dropshipItem.Date = string.IsNullOrEmpty(dropship.InvoiceDate) ? DateTime.UtcNow.ToString("yyyy-MM-dd") : row1[dropship.InvoiceDate].ToString().Trim();
                        // check for item set in template, if found then default to this instead and ignore map.
                        if (!string.IsNullOrEmpty(dropship.ItemNumber))
                        {
                            dropshipItem.Item = dropship.ItemNumber;
                        }
                        else  // no item set in template, use import based on map.
                        {
                            // item map not found, throw exception.
                            if (string.IsNullOrEmpty(dropship.Item))
                            {
                                throw new Exception("Item map not found in template and no default item set. Import failed.");
                            }
                            // check for item from import add prefix if any, else if missing then get next record.
                            if (!string.IsNullOrEmpty(row1[dropship.Item].ToString()))
                            {
                                dropshipItem.Item = dropship.ItemPrefix + row1[dropship.Item].ToString().Trim();
                            }
                            else
                                continue;
                        }
                        // item description map.
                        if (!string.IsNullOrEmpty(dropship.ItemDesc))
                        {
                            dropshipItem.ItemDesc = row1[dropship.ItemDesc].ToString().Trim();
                        }
                        // UOM map.
                        if (!string.IsNullOrEmpty(dropship.UOM))
                        {
                            dropshipItem.UOM = row1[dropship.UOM].ToString().ToUpper().Trim();
                            // if uom has EA then we need to change it to EACH, since GP doesn't have EA.
                            if (string.Compare(dropshipItem.UOM, "EA", StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                dropshipItem.UOM = "EACH";
                            }
                        }
                        // qty map.
                        if (!string.IsNullOrEmpty(dropship.Quantity))
                        {
                            // check for null, if found get next record.
                            if (!Utilities.isNull(row1[dropship.Quantity]))
                            {
                                dropshipItem.Quantity = Convert.ToDecimal(row1[dropship.Quantity]);
                                // if qty is zero then get next record.
                                if (dropshipItem.Quantity == 0)
                                {
                                    continue;
                                }
                            }
                            else
                                continue;
                        }                        
                        // if item set in template, then check rate type if it's price by qty.
                        if (!string.IsNullOrEmpty(dropship.ItemNumber))
                        {
                            if (string.Compare(dropship.RateType, "PriceByQuantity", StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                foreach (DataRow row2 in dataTable2.Rows)
                                {
                                    // based on qty sold we set the price.
                                    if (dropshipItem.Quantity >= Convert.ToDecimal(row2["fromqty"]) && dropshipItem.Quantity <= Convert.ToDecimal(row2["toqty"]))
                                    {
                                        dropshipItem.Cost = Convert.ToDecimal(row2["price"]);
                                        break;
                                    }
                                }
                            }
                            else // set item cost found in GP.
                            {
                                dropshipItem.Cost = dropship.ItemCost;
                            }
                            // extended cost calculated.
                            dropshipItem.ExtCost = dropshipItem.Quantity * dropshipItem.Cost;
                        }
                        // extended cost map, use to find cost so that we always match.
                        else if (!string.IsNullOrEmpty(dropship.ExtendedCost))
                        {
                            dropshipItem.ExtCost = Convert.ToDecimal(row1[dropship.ExtendedCost]);
                            dropshipItem.Cost = dropshipItem.ExtCost / dropshipItem.Quantity;
                        }
                        // cost map, used when extended cost is not found.
                        else if (!string.IsNullOrEmpty(dropship.Cost))
                        {
                            dropshipItem.Cost = Convert.ToDecimal(row1[dropship.Cost]);
                            dropshipItem.ExtCost = dropshipItem.Quantity * dropshipItem.Cost;
                        }
                        // tax map.
                        if (!string.IsNullOrEmpty(dropship.Tax))
                        {
                            dropshipItem.Tax = Convert.ToDecimal(row1[dropship.Tax]);
                        }
                        // return map, if 1 or 0 determines if a return doc. is created in GP.
                        if (!string.IsNullOrEmpty(dropship.Return))
                        {
                            dropshipItem.ReturnFlag = Convert.ToInt32(row1[dropship.Return]);
                        }
                        // freight marker in template, check item if marker is found then 
                        if (!string.IsNullOrEmpty(dropship.FreightMarker) && dropshipItem.Item.Contains(dropship.FreightMarker))
                        {
                            dropshipItem.Item = row1[dropship.Item].ToString().Trim();
                            // canada gets taxed on freight, use tax from import data or charge 5% if not found.
                            if (dropship.CompanyId == GPConstants.CANADA)
                            {
                                dropshipItem.Tax = dropshipItem.Tax > 0 ? dropshipItem.Tax : Math.Round(dropshipItem.Cost * (decimal)0.05, 2);
                            }
                            else
                            {
                                dropshipItem.Tax = 0;
                            }
                            dropshipItem.FreightFlag = 1;
                        }
                        // vendor set in template, defaults to and ignores map.
                        if (!string.IsNullOrEmpty(dropship.VendorNumber))
                        {
                            dropshipItem.Vendor = dropship.VendorNumber;
                            // check qty and cost is greater than zero, else get next record.
                            if (dropshipItem.Quantity > 0 && dropshipItem.Cost > 0)
                            {
                                Distribution_DB.DropshipDataInsert(dropshipItem);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else  // vendor map is used.
                        {
                            // vendor map is missing, throw exception.
                            if (string.IsNullOrEmpty(dropship.Vendor))
                            {
                                throw new Exception("No Vendor ID found.  Cannot continue with import.");
                            }
                            // get code for canada or usa, there is a vendor id conversion that needs to be done.
                            string str = row1[dropship.Vendor].ToString().Split(new string[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries)[0];
                            foreach (DataRow row2 in dataTable1.Rows)
                            {
                                if (str.Contains(row2["source"].ToString()))
                                {
                                    dropshipItem.Vendor = row2["destination"].ToString().Trim();
                                    // if qty and cost > 0, insert to table else get next record.
                                    if (dropshipItem.Quantity > 0 && dropshipItem.Cost > 0)
                                    {
                                        Distribution_DB.DropshipDataInsert(dropshipItem);
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                        dropshipItem = new Distribution_Class.DropshipItem();
                    }
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.DropshipImport(): Customer: " + customerIdErrorCheck + " has an error, please check import file.");
            }
        }

        public static string DropshipCustomerCheck(Distribution_Class.Dropship drop)
        {
            try
            {
                string customerNotMatching = "";
                DataTable table1 = Distribution_DB.Dropship("import_customer", id: drop.Id);
                if (table1.Rows.Count == 0)
                {
                    throw new Exception("No import data found.");
                }
                // get gp customers based on dropship company id.
                DataTable table2 = Distribution_DB.Dropship("gp_customer", id: drop.Id);
                // compare each import customer to gp customer, no match then add to return string.
                foreach (DataRow row1 in table1.Rows)
                {
                    bool flag = false;
                    string importCustomer = row1["customer"].ToString();
                    foreach (DataRow row2 in table2.Rows)
                    {
                        // import matches to gp.
                        if (string.Compare(importCustomer, row2["customer"].ToString(), StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        customerNotMatching += customerNotMatching != "" ? ", " : "";
                        customerNotMatching += importCustomer;
                    }
                }
                return customerNotMatching;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.DropshipCustomerCheck()");
            }
        }

        public static void DropshipGPInvoice(Distribution_Class.Dropship drop, DataTable invoiceTable, string documentType)
        {
            try
            {
                DataTable dataTable = new DataTable();
                Distribution_Class.DropshipItem header = new Distribution_Class.DropshipItem();
                Distribution_Class.DropshipItem dropshipItem = new Distribution_Class.DropshipItem();
                List<Distribution_Class.DropshipItem> itemList = new List<Distribution_Class.DropshipItem>();
                foreach (DataRow row1 in invoiceTable.Rows)
                {
                    header.Customer = row1["customer"].ToString();
                    header.Invoice = row1["invoice"].ToString();
                    header.Date = row1["invoicedate"].ToString();
                    header.Total = Math.Round(Convert.ToDecimal(row1["total"]), 2);
                    if (header.Total == 0)
                    {
                        throw new Exception("Cost/Extended Cost was not found.  Cannot continue.");
                    }
                    header.Vendor = row1["vendor"].ToString();
                    header.Tax = Math.Round(Convert.ToDecimal(row1["tax"]), 2);
                    header.TaxSchedule = row1["taxSchedule"].ToString();
                    if (header.Tax > 0 && string.IsNullOrEmpty(header.TaxSchedule))
                    {
                        throw new Exception(header.Customer + " does not have a tax schedule.  Cannot continue with integration.");
                    }
                    switch (documentType)
                    {
                        case "INVOICE":
                            dataTable = Distribution_DB.Dropship("invoice_item", drop.Id, header.Invoice);
                            break;
                        case "DRTN":
                            dataTable = Distribution_DB.Dropship("invoice_item_return", drop.Id, header.Invoice);
                            break;
                    }
                    foreach (DataRow row2 in dataTable.Rows)
                    {
                        dropshipItem.Item = row2["item"].ToString();
                        dropshipItem.ItemDesc = row2["itemdesc"].ToString();
                        dropshipItem.Price = dropshipItem.Cost = Math.Round(Convert.ToDecimal(row2["cost"]), 2);
                        dropshipItem.Quantity = Convert.ToDecimal(row2["quantity"]);
                        dropshipItem.UOM = row2["uom"].ToString();
                        dropshipItem.Tax = Math.Round(Convert.ToDecimal(row2["tax"]), 2);
                        dropshipItem.FreightFlag = Convert.ToInt32(row2["freightflag"]);
                        // non-freight item gets markup or discount.
                        if (dropshipItem.FreightFlag == 0)
                        {   
                            switch (drop.RateType)
                            {
                                case "CustomerMarkup":
                                    dropshipItem.Price = Math.Round(dropshipItem.Cost * (1 + drop.Rate * (decimal)0.01), 2);
                                    dropshipItem.Tax = Math.Round(dropshipItem.Tax * (1 + drop.Rate * (decimal)0.01), 2);
                                    break;
                                case "CustomerDiscount":
                                    dropshipItem.Price = Math.Round(dropshipItem.Cost * (1 - drop.Rate * (decimal)0.01), 2);
                                    dropshipItem.Tax = Math.Round(dropshipItem.Tax * (1 - drop.Rate * (decimal)0.01), 2);
                                    break;
                            }                            
                        }
                        dropshipItem.ExtCost = Math.Round(dropshipItem.Cost * dropshipItem.Quantity, 2);
                        dropshipItem.ExtPrice = Math.Round(dropshipItem.Price * dropshipItem.Quantity, 2);
                        // freight item gets tax added.
                        if (dropshipItem.FreightFlag > 0)
                        {
                            dropshipItem.ExtCost += dropshipItem.Tax;
                            dropshipItem.ExtPrice += dropshipItem.Tax;
                        }
                        itemList.Add(dropshipItem);
                        dropshipItem = new Distribution_Class.DropshipItem();
                    }
                    // check for invoice or return type.
                    if (string.Compare(documentType, "invoice", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        GP.DropshipSalesInvoice(drop, header, itemList);
                        // check to create payables.
                        if (string.Compare(drop.CreatePayable, "yes", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            GP.DropshipPayablesInvoice(drop, header);
                        }
                    }
                    else  // return.
                    { 
                        GP.DropshipSalesReturn(drop, header, itemList);
                        // check to create payables.
                        if (string.Compare(drop.CreatePayable, "yes", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            GP.DropshipPayablesCreditMemo(drop, header);
                        }
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

        public static void DropshipGPNoInvoiceFromVendor(Distribution_Class.Dropship drop, DataTable invoiceTable, string documentType)
        {
            try
            {
                DataTable dataTable = new DataTable();
                Distribution_Class.DropshipItem header = new Distribution_Class.DropshipItem();
                Distribution_Class.DropshipItem dropshipItem = new Distribution_Class.DropshipItem();
                foreach (DataRow row1 in invoiceTable.Rows)
                {
                    header.Vendor = row1["vendor"].ToString();
                    header.Date = row1["invoicedate"].ToString();
                    header.Total = Math.Round(Convert.ToDecimal(row1["total"]), 2);
                    header.CustomerCount = Convert.ToInt32(row1["customercount"]);
                    switch (documentType)
                    {
                        case "INVOICE":
                            dataTable = Distribution_DB.Dropship("no_invoice_item", id: drop.Id, invoiceNumber: header.Invoice);
                            break;
                        case "DRTN":
                            dataTable = Distribution_DB.Dropship("no_invoice_item_return", id: drop.Id, invoiceNumber: header.Invoice);
                            break;
                    }
                    foreach (DataRow row2 in dataTable.Rows)
                    {
                        dropshipItem.Customer = row2["customer"].ToString();
                        dropshipItem.Date = row2["invoicedate"].ToString();
                        dropshipItem.Item = row2["item"].ToString();
                        dropshipItem.Price = dropshipItem.Cost = Math.Round(Convert.ToDecimal(row2["cost"]), 2);
                        dropshipItem.Quantity = Convert.ToDecimal(row2["quantity"]);
                        string rateType = drop.RateType;
                        switch (rateType)
                        {
                            case "CustomerMarkup":
                                dropshipItem.Price = Math.Round(dropshipItem.Cost * (1 + drop.Rate * 0.01M), 2);
                                break;
                            case "CustomerDiscount":
                                dropshipItem.Price = Math.Round(dropshipItem.Cost * (1 - drop.Rate * 0.01M), 2);
                                break;
                        }   
                        dropshipItem.ExtPrice = Math.Round(dropshipItem.Quantity * dropshipItem.Price, 2);
                        // create invoice.
                        if (string.Compare(documentType, "invoice", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            GP.DropshipSalesNoVendorInvoice(drop, header, dropshipItem);
                        }
                        else // return
                        {
                            GP.DropshipSalesNoVendorReturn(drop, header, dropshipItem);
                        }
                    }
                    if (string.Compare(drop.CreatePayable, "yes", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        GP.DropshipPayablesForNoVendorInvoice(drop, header, dropshipItem);
                    }
                    header = new Distribution_Class.DropshipItem();
                    dropshipItem = new Distribution_Class.DropshipItem();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.DropshipGPNoInvoiceFromVendor()");
            }
        }
        // item adjustment creates an inventory item variance doc for inventory transactions.
        //public static string ItemAdjustmentRun(string filePath)
        //{
        //    try
        //    {
        //        Distribution_Class.Item item = new Distribution_Class.Item();
        //        List<Distribution_Class.Item> itemList = new List<Distribution_Class.Item>();
        //        Distribution_DB.ItemVarianceUpdate("delete_import_data", item);
        //        // build batch id.
        //        item.Batch = "INVADJ-" + DateTime.Now.ToString("MMddyy", CultureInfo.CreateSpecificCulture("en-US"));
        //        // check for existing batch.
        //        DataTable table = Distribution_DB.ItemVariance("check_for_existing_batch", item);
        //        if (Convert.ToInt32(table.Rows[0]["recordcount"]) > 0)
        //            throw new Exception("Integration halted.   Found an existing batch: " + item.Batch + ".   The batch must be deleted in GP before continuing.");
        //        ItemAdjustmentImportFrozen(filePath);
        //        ItemAdjustmentImportDryWithLot(filePath);
        //        ItemAdjustmentImportDry(filePath);
        //        string[] categoryTypes = new string[] { "FROZ", "DRYLOT", "DRYNON" };
        //        string documentNumber = Distribution.ItemAdjustmentNextDocumentNumber();
        //        string documentDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"));
        //        int num = 1;
        //        for (int index = 0; index < categoryTypes.Length; ++index)
        //        {
        //            item.Category = categoryTypes[index];
        //            DataTable dt = Distribution_DB.ItemVariance("import_data", item);
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                item.Category = categoryTypes[index];
        //                item.DocumentNumber = documentNumber;
        //                item.Number = row["item"].ToString();
        //                item.Lot = row["lot"].ToString();
        //                item.LotDateReceived = row["lotdatereceived"].ToString();
        //                item.Location = row["location"].ToString();
        //                item.Available = Convert.ToInt32(row["available"]);
        //                item.QtyEntered = Convert.ToInt32(row["actual"]);
        //                item.Variance = Convert.ToInt32(row["Variance"]);
        //                item.UOM = row["uom"].ToString();
        //                item.Cost = Convert.ToDecimal(row["cost"]);
        //                item.LineSeq = num;
        //                ++num;
        //                itemList.Add(item);
        //                item = new Distribution_Class.Item();
        //            }
        //        }
        //        if (itemList.Count <= 0)
        //            throw new Exception("No items were found for inventory adjustment.");
        //        // create GP doc.
        //        GP.ItemVariance(itemList, documentNumber, documentDate);
        //        // update reason code
        //        Distribution_DB.ItemVarianceUpdate("reason_code", item);
        //        // clear import data.
        //        Distribution_DB.ItemVarianceUpdate("delete_import_data", item);
        //        return "Done.";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw Utilities.ErrHandler(ex);
        //    }
        //}
        // item adjustment get next doc no. to create.
        //private static string ItemAdjustmentNextDocumentNumber()
        //{
        //    try
        //    {
        //        var docNumber = "";
        //        DataTable table = Distribution_DB.ItemVariance("next_doc_number", new Distribution_Class.Item());
        //        if (table.Rows.Count > 0)
        //        {
        //            docNumber = table.Rows[0]["docnumber"].ToString();
        //        }
        //        return docNumber;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw Utilities.ErrHandler(ex, "Model.Distribution.ItemAdjustmentNextDocumentNumber()");
        //    }
        //}
        // item adjustment item lot available check.
        //private static bool ItemAdjustmentCheckLotAvailable(Distribution_Class.Item item)
        //{
        //    try
        //    {
        //        bool flag = false;
        //        if (item.Variance < 0)
        //        {
        //            DataTable table = Distribution_DB.ItemVariance("check_lot_available", item);
        //            if (table.Rows.Count > 0 && Convert.ToInt32(table.Rows[0]["recordcount"]) > 0)
        //                flag = true;
        //        }
        //        else
        //            flag = true;
        //        return flag;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw Utilities.ErrHandler(ex, "Model.Distribution.ItemAdjustmentCheckLotAvailable()");
        //    }
        //}
        // item adjustment frozen goods import.
        //private static void ItemAdjustmentImportFrozen(string filePath)
        //{
        //    try
        //    {
        //        Distribution_Class.Item item = new Distribution_Class.Item();
        //        int result = 0;
        //        DataTable table = Utilities.GetExcelData(filePath, sheetName: "Work Sheet Frozen$");
        //        foreach (DataRow row in table.Rows)
        //        {
        //            if (string.IsNullOrEmpty(row["Item_Number"].ToString()))
        //                break;
        //            item.Category = "FROZ";
        //            item.Number = row["Item_Number"].ToString().Trim();
        //            item.Lot = row["Lot_No"].ToString().Trim();
        //            item.LotDateReceived = Convert.ToDateTime(row["Lot_Received_Date"]).ToString("MM/dd/yyyy");
        //            item.Location = row["Location_Code"].ToString().Trim();
        //            item.Available = int.TryParse(row["Qty_Available"].ToString(), out result) ? Convert.ToInt32(row["Qty_Available"]) : 0;
        //            item.Variance = int.TryParse(row["Variance"].ToString(), out result) ? Convert.ToInt32(row["Variance"]) : 0;
        //            item.QtyEntered = int.TryParse(row["Actual Aailable"].ToString(), out result) ? Convert.ToInt32(row["Actual Aailable"]) : 0;
        //            item.UOM = row["BASEUOFM"].ToString().Trim();
        //            item.UnitCost = Convert.ToDecimal(row["UnitCost"]);
        //            if (item.Variance > 0)
        //            {
        //                if (ItemAdjustmentCheckLotAvailable(item))
        //                    Distribution_DB.ItemVarianceUpdate("import_data", item);
        //                else
        //                    throw new Exception("Import Frozen: Quantity available is not >= variance for the following Item: " + item.Number + ", Lot: " + item.Lot + ", Date Recd: " + item.LotDateReceived + ", Variance: " + item.Variance);
        //            }
        //            item = new Distribution_Class.Item();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw Utilities.ErrHandler(ex, "Model.Distribution.ItemAdjustmentImportFrozen()");
        //    }
        //}
        // item adjustment dry with lot import.
        //private static void ItemAdjustmentImportDryWithLot(string filePath)
        //{
        //    try
        //    {
        //        Distribution_Class.Item item = new Distribution_Class.Item();
        //        int result = 0;
        //        DataTable table = Utilities.GetExcelData(filePath, "Work Sheet Dry Lot#$");
        //        foreach (DataRow row in table.Rows)
        //        {
        //            if (string.IsNullOrEmpty(row["Item_Number"].ToString()))
        //                break;
        //            item.Category = "DRYLOT";
        //            item.Number = row["Item_Number"].ToString().Trim();
        //            item.Lot = row["Lot_No"].ToString().Trim();
        //            item.LotDateReceived = Convert.ToDateTime(row["Lot_Received_Date"]).ToString("MM/dd/yyyy");
        //            item.Location = row["WH"].ToString().Trim();
        //            item.Available = int.TryParse(row["Qty_Available"].ToString(), out result) ? Convert.ToInt32(row["Qty_Available"]) : 0;
        //            item.Variance = int.TryParse(row["Variance"].ToString(), out result) ? Convert.ToInt32(row["Variance"]) : 0;
        //            item.QtyEntered = int.TryParse(row["Actual Aailable"].ToString(), out result) ? Convert.ToInt32(row["Actual Aailable"]) : 0;
        //            item.UOM = row["BASEUOFM"].ToString().Trim();
        //            item.UnitCost = Convert.ToDecimal(row["UnitCost"]);
        //            if (item.Variance > 0)
        //            {
        //                if (ItemAdjustmentCheckLotAvailable(item))
        //                    Distribution_DB.ItemVarianceUpdate("import_data", item);
        //                else
        //                    throw new Exception("Import Dry with Lot: Quantity available is not >= variance for the following Item: " + item.Number + ", Lot: " + item.Lot + ", Date Recd: " + item.LotDateReceived + ", Variance: " + item.Variance);
        //            }
        //            item = new Distribution_Class.Item();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw Utilities.ErrHandler(ex, "Model.Distribution.ItemAdjustmentImportDryWithLot()");
        //    }
        //}
        // item adjustment dry goods import.
        //private static void ItemAdjustmentImportDry(string filePath)
        //{
        //    try
        //    {
        //        Distribution_Class.Item item = new Distribution_Class.Item();
        //        int result = 0;
        //        DataTable table = Utilities.GetExcelData(filePath, "Work Sheet Dry NON Lot#$");
        //        foreach (DataRow row in table.Rows)
        //        {
        //            if (string.IsNullOrEmpty(row["Item Number"].ToString()))
        //                break;
        //            item.Category = "DRYNON";
        //            item.Number = row["Item Number"].ToString().Trim();
        //            item.Location = row["WH"].ToString().Trim();
        //            item.Variance = int.TryParse(row["Variance"].ToString(), out result) ? Convert.ToInt32(row["Variance"]) : 0;
        //            item.UOM = row["Base UOM"].ToString().Trim();
        //            item.UnitCost = Convert.ToDecimal(row["UnitCost"]);
        //            if (item.Variance > 0)
        //                Distribution_DB.ItemVarianceUpdate("import_data", item);
        //            item = new Distribution_Class.Item();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw Utilities.ErrHandler(ex, "Model.Distribution.ItemAdjustmentImportDry()");
        //    }
        //}
        // item bin is used to set locations for picklist.
        public static object ItemBin(string filePath)
        {
            try
            {
                Distribution_Class.ItemBin itemBin = new Distribution_Class.ItemBin();
                List<Distribution_Class.ItemBin> itemBinList = new List<Distribution_Class.ItemBin>();
                DataTable dt = Distribution_DB.ItemBin("records", itemBin);
                foreach (DataRow row in dt.Rows)
                {
                    itemBin.Id = Convert.ToInt32(row["itembinid"]);
                    itemBin.Item = row["item"].ToString();
                    itemBin.Location = row["location"].ToString();
                    itemBin.BinCap = row["bincap"].ToString();
                    itemBin.Secondary = row["scnd"].ToString();
                    itemBin.Third = row["third"].ToString();
                    itemBin.ItemDesc = row["itemdesc"].ToString();
                    itemBin.Priority = row["binpriority"].ToString();
                    itemBinList.Add(itemBin);
                    itemBin = new Distribution_Class.ItemBin();
                }
                WriteItemBinFile(filePath, itemBinList);
                return itemBinList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemBin()");
            }
        }

        private static void WriteItemBinFile(string filePath, List<Distribution_Class.ItemBin> itemBinList)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item" + delim + "Item Description" + delim + "Location" + delim + "Bin Cap" + delim + "Secondary" + delim + "Third" + delim + "Priority");
                    foreach (Distribution_Class.ItemBin itemBin in itemBinList)
                    {
                        streamWriter.WriteLine(
                            itemBin.Item + delim + 
                            itemBin.ItemDesc.Replace(',', ' ') + delim + 
                            itemBin.Location.Replace(',', ' ') + delim + 
                            itemBin.BinCap + delim + 
                            itemBin.Secondary + delim + 
                            itemBin.Third + delim +
                            itemBin.Priority
                            );
                    }
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
                DataTable table = Distribution_DB.DropLabel("records", drop: drop);
                foreach (DataRow row in table.Rows)
                {
                    drop.Id = Convert.ToInt32(row["droplabelid"]);
                    drop.City = row["city"].ToString();
                    drop.State = row["state"].ToString();
                    dropList.Add(drop);
                    drop = new Distribution_Class.DropLabel();
                }
                WriteDropLabelFile(filePath, dropList);
                return dropList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemBin()");
            }
        }

        private static void WriteDropLabelFile(string filePath, List<Distribution_Class.DropLabel> dropList)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("City,State");
                    foreach (Distribution_Class.DropLabel drop in dropList)
                    {
                        streamWriter.WriteLine(
                            drop.City.Replace(',', ' ') + delim + 
                            drop.State
                            );
                    }
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
                DataTable table = Distribution_DB.InTransitBillOfLading("docids", lading);
                foreach (DataRow row in table.Rows)
                {
                    lading.DocNumber = row["docid"].ToString();
                    lading.DocDate = row["docdate"].ToString();
                    lading.FromSite = row["TRNSFLOC"].ToString();
                    lading.ToSite = row["LOCNCODE"].ToString();
                    lading.DocStatus = row["docstatus"].ToString();
                    billofLadingList.Add(lading);
                    lading = new Distribution_Class.BillofLading();
                }
                return billofLadingList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.InTransitBillOfLading()");
            }
        }

        //public static object ExternalDistributionCenterBatchRecords(string externalCenterId)
        //{
        //    try
        //    {
        //        Distribution_Class.Order order = new Distribution_Class.Order();
        //        List<Distribution_Class.Order> orderList = new List<Distribution_Class.Order>();
        //        DataTable dt = Distribution_DB.ExternalDistributionCenter("records", externalCenterId);
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            order.Batch = row["batch"].ToString();
        //            order.BatchDate = row["batchdate"].ToString();
        //            order.BatchCount = Convert.ToInt32(row["batchcount"]);
        //            orderList.Add(order);
        //            order = new Distribution_Class.Order();
        //        }
        //        return orderList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw Utilities.ErrHandler(ex, "Model.Distribution.ExternalDistributionCenterBatch()");
        //    }
        //}

        //public static object ExternalDistributionCenterBatchDetail(Distribution_Class.Order order, string filePath)
        //{
        //    try
        //    {
        //        List<Distribution_Class.Order> orderList = new List<Distribution_Class.Order>();
        //        foreach (DataRow row in (InternalDataCollectionBase)Distribution_DB.ExternalDistributionCenter("record_detail", order.Batch).Rows)
        //        {
        //            order.Batch = row["batch"].ToString();
        //            order.BatchDate = row["batchdate"].ToString();
        //            order.Number = row["OrderNumber"].ToString();
        //            order.OrderDate = row["OrderDate"].ToString();
        //            order.Storecode = row["Customer"].ToString();
        //            order.Storename = row["CustomerName"].ToString();
        //            order.StoreState = row["ST"].ToString();
        //            order.OriginalBatch = row["OriginalBatch"].ToString();
        //            order.OriginalSite = row["OriginalSite"].ToString();
        //            orderList.Add(order);
        //            order = new Distribution_Class.Order();
        //        }
        //        WriteExternalDistributionCenterBatchFile(filePath, orderList);
        //        return orderList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw Utilities.ErrHandler(ex, "Model.Distribution.ExternalDistributionCenterBatchDetail()");
        //    }
        //}

        //private static void WriteExternalDistributionCenterBatchFile(
        //  string filePath,
        //  List<Distribution_Class.Order> orderList)
        //{
        //    try
        //    {
        //        string str = ",";
        //        using (StreamWriter streamWriter = new StreamWriter(filePath, false))
        //        {
        //            streamWriter.WriteLine("Batch" + str + "Batch Date" + str + "Order No." + str + "Order Date" + str + "Store" + str + "Store Name" + str + "ST" + str + "Orig. Batch" + str + "Orig. Site");
        //            foreach (Distribution_Class.Order order in orderList)
        //                streamWriter.WriteLine(order.Batch + str + order.BatchDate + str + order.Number + str + order.OrderDate + str + order.Storecode + str + order.Storename.Replace(',', ' ') + str + order.StoreState + str + order.OriginalBatch + str + order.OriginalSite);
        //            streamWriter.Close();
        //            streamWriter.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw Utilities.ErrHandler(ex, "Model.Distribution.WriteExternalDistributionCenterBatchFile()");
        //    }
        //}

        public static void WMSTrxLogData(Distribution_Class.FormInput form, string filePath)
        {
            try
            {
                Distribution_Class.WarehouseMgmtSystem warehouseMgmtSystem = new Distribution_Class.WarehouseMgmtSystem();
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    // column header.
                    streamWriter.WriteLine("Status" + delim + "Trx Type" + delim + "User Id" + delim + "Terminal Id" + delim +
                        "Trx Date" + delim + "Doc. Number" + delim + "Item" + delim + "Lot" + delim + "Qty" + delim + "From Site" + delim +
                        "From Bin" + delim + "To Site" + delim + "To Bin");
                    // get data.
                    DataTable table = Distribution_DB.WarehouseMgmtSystem("transaction_log", form);
                    foreach (DataRow row in table.Rows)
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
                        streamWriter.WriteLine(
                            warehouseMgmtSystem.Status + delim +
                            warehouseMgmtSystem.TrxType + delim +
                            warehouseMgmtSystem.UserId + delim +
                            warehouseMgmtSystem.TerminalId + delim +
                            warehouseMgmtSystem.TrxDate + delim +
                            warehouseMgmtSystem.DocNumber + delim +
                            warehouseMgmtSystem.Item + delim +
                            warehouseMgmtSystem.Lot.Replace(',', ' ') + delim +
                            warehouseMgmtSystem.Qty + delim +
                            warehouseMgmtSystem.FromSite + delim +
                            warehouseMgmtSystem.FromBin + delim +
                            warehouseMgmtSystem.ToSite + delim +
                            warehouseMgmtSystem.ToBin
                            );
                        warehouseMgmtSystem = new Distribution_Class.WarehouseMgmtSystem();
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }               
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WMSTrxLogData()");
            }
        }

        public static void WarehousePickTicket(Distribution_Class.FormInput form, string filePath)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("BatchId" + delim + "OrderNo" + delim + "DocDate" + delim + "CustomerId" + delim + "CustomerName" + delim + 
                        "Address1" + delim + "Address2" + delim + "City" + delim + "ST" + delim + "Zip" + delim + "Phone" + delim + "Comment" + delim + 
                        "FCID" + delim + "FCPhone" + delim + "ItemNo" + delim + "ItemDesc" + delim + "UOM" + delim + "UOMQty" + delim + "LineSeq" + delim + 
                        "OrderQty" + delim + "Unit/Each" + delim + "PickQty");
                    DataTable table1 = Distribution_DB.BatchPicklist("orderpicklist_store", batchId: form.Batch);
                    foreach (DataRow row1 in table1.Rows)
                    {
                        string orderNo = row1["sopnumbe"].ToString();
                        string storePart = form.Batch + delim + 
                            orderNo + delim + 
                            row1["docdate"].ToString() + delim + 
                            row1["custnmbr"].ToString() + delim + 
                            row1["custname"].ToString().Replace(",", " ") + delim + 
                            row1["address1"].ToString().Replace(",", " ") + delim + 
                            row1["address2"].ToString().Replace(",", " ") + delim + 
                            row1["city"].ToString().Replace(",", " ") + delim + 
                            row1["state"].ToString() + delim + 
                            row1["zipcode"].ToString() + delim + 
                            Utilities.FormatPhone(row1["phnumbr1"].ToString()) + delim +
                            Utilities.CleanForCSV(row1["cmmttext"].ToString()) + delim + 
                            row1["fcid"].ToString() + delim + 
                            row1["fcphone"].ToString();
                        DataTable table2 = Distribution_DB.BatchPicklist("orderpicklist_item_ver3", orderNo: orderNo);
                        foreach (DataRow row2 in table2.Rows)
                        {
                            var item = new Distribution_Class.Item();
                            item.Number = row2["item"].ToString();
                            item.Description = row2["itemdesc"].ToString().Replace(",", " ");
                            item.UOM = row2["uom"].ToString();
                            item.UOMQty = Convert.ToInt32(row2["uomqty"]);
                            item.LineSeq = Convert.ToInt32(row2["lineseq"]);
                            int pickQty = item.Sold = Convert.ToInt32(row2["qty"]);
                            item.Category = row2["lanterpick"].ToString();
                            if (item.Category == "Each")
                            {
                                pickQty = item.Sold * item.UOMQty;
                            }
                            string itemPart = item.Number + delim + 
                                item.Description + delim + 
                                item.UOM + delim + 
                                item.UOMQty + delim + 
                                item.LineSeq + delim + 
                                item.Sold + delim + 
                                item.Category + delim + 
                                pickQty;
                            streamWriter.WriteLine(storePart + delim + itemPart);
                        }
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WarehousePickTicket()");
            }
        }


        public static void WarehouseInvoiceReconcileData(Distribution_Class.FormInput form, string filePath)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("OrderNo" + delim + "DocDate" + delim + "CustomerNo" + delim + "CustomerName" + delim + "ItemCount" + delim + "OrderQty" + delim + "PickQty");
                    DataTable table = Distribution_DB.WarehouseExternalGet("reconcile_picks", form);
                    foreach (DataRow row in table.Rows)
                        streamWriter.WriteLine(
                            row["orderno"].ToString() + delim +
                            row["docdate"].ToString() + delim +
                            row["custno"].ToString() + delim +
                            row["custname"].ToString().Replace(',', '.') + delim +
                            row["itemcount"].ToString() + delim +
                            row["orderqty"].ToString() + delim +
                            row["pickqty"]
                            );
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WarehouseInvoiceReconcileData()");
            }
        }

        public static object TunaShip()
        {
            try
            {
                Distribution_Class.TunaShip tuna = new Distribution_Class.TunaShip();
                List<Distribution_Class.TunaShip> tunaList = new List<Distribution_Class.TunaShip>();
                foreach (DataRow row in (InternalDataCollectionBase)App.GetRowSp("Distribution.uspTunaShipGet").Rows)
                {
                    tuna.StoreID = Convert.ToInt32(row[0]);
                    tuna.Storecode = (string)row[1];
                    tuna.Storename = (string)row[2];
                    tuna.Address = (string)row[3];
                    tuna.City = (string)row[4];
                    tuna.State = (string)row[5];
                    tuna.Zipcode = (string)row[6];
                    string phone = Utilities.FormatPhone((string)row[7]);
                    tuna.Phone = phone == "0" ? "" : phone;
                    tuna.Region = (string)row[8];
                    tuna.Qty = (int)row[9];
                    tuna.ModifiedOn = (string)row[10];
                    tunaList.Add(tuna);
                    tuna = new Distribution_Class.TunaShip();
                }
                return tunaList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.TunaShip()");
            }
        }

        



    }
}