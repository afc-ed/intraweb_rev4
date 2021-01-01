using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace intraweb_rev3.Models
{
    public class BOD
    {
        public static object GetMenuList()
        {
            try
            {
                List<BOD_Class.Menu> menuList = new List<BOD_Class.Menu>();
                menuList.Add(new BOD_Class.Menu()
                {
                    Id = "PriceList",
                    Name = "Price List"
                });
                menuList.Add(new BOD_Class.Menu()
                {
                    Id = "CommissionReport",
                    Name = "Commission Report"
                });
                menuList.Sort((x, y) => x.Name.CompareTo(y.Name));
                return menuList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.BOD.MenuList()");
            }
        }

        public static object GetPriceList(string filePath)
        {
            try
            {
                BOD_Class.Item item = new BOD_Class.Item();
                List<BOD_Class.Item> itemList = new List<BOD_Class.Item>();
                DataTable table = BOD_DB.GetProduct("priceList");
                foreach (DataRow row in table.Rows)
                {
                    item.Code = row["item"].ToString().Trim();
                    item.Description = row["itemdesc"].ToString();
                    item.UOM = row["uom"].ToString().Trim();
                    item.UOMQty = Convert.ToInt32(row["uomqty"]);
                    item.Cost = Convert.ToDecimal(row["currcost"]);
                    item.ExtCost = item.Cost * (Decimal)item.UOMQty;
                    item.Price = Convert.ToDecimal(row["price"]);
                    item.ExtPrice = item.Price * (Decimal)item.UOMQty;
                    item.Status = row["itemstatus"].ToString();
                    item.Type = row["itemtype"].ToString();
                    item.Category = row["category"].ToString();
                    itemList.Add(item);
                    item = new BOD_Class.Item();
                }
                WritePriceListFile(filePath, itemList);
                return itemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.BOD.GetPriceList()");
            }
        }

        private static void WritePriceListFile(string filePath, List<BOD_Class.Item> itemList)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item No" + delim + "Item Description" + delim + "Category" + delim + "UOM" + delim + 
                        "UOM Qty" + delim + "Cost" + delim + "Ext. Cost" + delim + "Price" + delim + "Ext. Price" + delim + 
                        "Type" + delim + "Status");
                    foreach (BOD_Class.Item item in itemList)
                        streamWriter.WriteLine(
                            item.Code + delim + 
                            item.Description.Replace(',', '.') + delim + 
                            item.Category + delim + 
                            item.UOM + delim + 
                            item.UOMQty + delim + 
                            item.Cost + delim + 
                            item.ExtCost + delim + 
                            item.Price + delim + 
                            item.ExtPrice + delim + 
                            item.Type + delim +
                            item.Status
                            );
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.BOD.WritePriceListFile()");
            }
        }

        public static void CommissionReportData(BOD_Class.FormInput form, string filePath)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("StoreCode" + delim + "StoreName" + delim + "Part" + delim + "FullSvc" + delim + "FC" + delim + 
                        "FCID" + delim + "Requestor" + delim + "RequestDate" + delim + "ApprovedDate" + delim + "AppEffectiveStartDate" + delim +
                        "AppEffectiveEndDate" + delim + "CurrentCommission" + delim + "CommPercent" + delim + "CommUpTo" + delim + "CommCriteria" + delim + 
                        "CurrentAverageSale" + delim + "Region" + delim + "RM" + delim + "Division");
                    DataTable table = BOD_DB.Commission("report", form.FromDate, form.Id);
                    foreach (DataRow row in table.Rows)
                        streamWriter.WriteLine(
                            row["StoreCode"].ToString() + delim + 
                            row["StoreName"].ToString().Replace(',', '.') + delim +
                            row["Part"].ToString() + delim + 
                            row["FullSvc"].ToString() + delim + 
                            row["FC"].ToString() + delim +
                            row["FCID"].ToString() + delim + 
                            row["Requestor"].ToString() + delim +
                            row["RequestDate"].ToString() + delim + 
                            row["ApprovedDate"].ToString() + delim + 
                            row["AppEffectiveStartDate"].ToString() + delim +
                            row["AppEffectiveEndDate"].ToString() + delim + 
                            row["CurrentCommission"].ToString() + delim +
                            row["CommPercent"].ToString() + delim +
                            row["CommUpTo"].ToString() + delim +
                            row["CommCriteria"].ToString() + delim +
                            row["CurrentAverageSale"].ToString() + delim +
                            row["Region"].ToString() + delim +
                            row["RM"].ToString() + delim +
                            row["Division"].ToString()
                            );
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.BOD.CommissionReportData()");
            }
        }




    }
}