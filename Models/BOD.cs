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
                menuList.Sort((Comparison<BOD_Class.Menu>)((x, y) => x.Name.CompareTo(y.Name)));
                return (object)menuList;
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
                BOD_Class.Item obj = new BOD_Class.Item();
                List<BOD_Class.Item> itemList = new List<BOD_Class.Item>();
                foreach (DataRow row in (InternalDataCollectionBase)BOD_DB.GetProduct("priceList").Rows)
                {
                    obj.Code = row["item"].ToString().Trim();
                    obj.Description = row["itemdesc"].ToString();
                    obj.UOM = row["uom"].ToString().Trim();
                    obj.UOMQty = Convert.ToInt32(row["uomqty"]);
                    obj.Cost = Convert.ToDecimal(row["currcost"]);
                    obj.ExtCost = obj.Cost * (Decimal)obj.UOMQty;
                    obj.Price = Convert.ToDecimal(row["price"]);
                    obj.ExtPrice = obj.Price * (Decimal)obj.UOMQty;
                    obj.Status = row["itemstatus"].ToString();
                    obj.Type = row["itemtype"].ToString();
                    obj.Category = row["category"].ToString();
                    itemList.Add(obj);
                    obj = new BOD_Class.Item();
                }
                BOD.WritePriceListFile(filePath, itemList);
                return (object)itemList;
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
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Item No" + str + "Item Description" + str + "Category" + str + "UOM" + str + "UOM Qty" + str + "Cost" + str + "Ext. Cost" + str + "Price" + str + "Ext. Price" + str + "Type" + str + "Status");
                    foreach (BOD_Class.Item obj in itemList)
                        streamWriter.WriteLine(obj.Code + str + obj.Description.Replace(',', '.') + str + obj.Category + str + obj.UOM + str + (object)obj.UOMQty + str + (object)obj.Cost + str + (object)obj.ExtCost + str + (object)obj.Price + str + (object)obj.ExtPrice + str + obj.Type + str + obj.Status);
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
                string str = ",";
                DataTable dataTable = new DataTable();
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("StoreCode" + str + "StoreName" + str + "Part" + str + "FullSvc" + str + "FC" + str + "FCID" + str + "Requestor" + str + "RequestDate" + str + "ApprovedDate" + str + "AppEffectiveStartDate" + str + "AppEffectiveEndDate" + str + "CurrentCommission" + str + "CommPercent" + str + "CommUpTo" + str + "CommCriteria" + str + "CurrentAverageSale" + str + "Region" + str + "RM" + str + "Division");
                    foreach (DataRow row in (InternalDataCollectionBase)BOD_DB.Commission("report", form.FromDate, form.Id).Rows)
                        streamWriter.WriteLine(row["StoreCode"].ToString() + str + row["StoreName"].ToString().Replace(',', '.') + str + row["Part"].ToString() + str + row["FullSvc"].ToString() + str + row["FC"].ToString() + str + row["FCID"].ToString() + str + row["Requestor"].ToString() + str + row["RequestDate"].ToString() + str + row["ApprovedDate"].ToString() + str + row["AppEffectiveStartDate"].ToString() + str + row["AppEffectiveEndDate"].ToString() + str + row["CurrentCommission"].ToString() + str + row["CommPercent"].ToString() + str + row["CommUpTo"].ToString() + str + row["CommCriteria"].ToString() + str + row["CurrentAverageSale"].ToString() + str + row["Region"].ToString() + str + row["RM"].ToString() + str + row["Division"].ToString());
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