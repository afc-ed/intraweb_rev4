using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;

namespace intraweb_rev3.Models
{
    public class Ecommerce
    {
        public static object MenuList()
        {
            try
            {
                List<Ecommerce_Class.Menu> menuList = new List<Ecommerce_Class.Menu>();
                menuList.Add(new Ecommerce_Class.Menu()
                {
                    Id = "ItemRestriction",
                    Name = "Item Restriction"
                });
                menuList.Add(new Ecommerce_Class.Menu()
                {
                    Id = "ItemByClass",
                    Name = "Item By Customer Class"
                });
                menuList.Add(new Ecommerce_Class.Menu()
                {
                    Id = "ClassByItem",
                    Name = "Customer Class By Item"
                });
                menuList.Add(new Ecommerce_Class.Menu()
                {
                    Id = "PriceList",
                    Name = "Price List"
                });
                menuList.Add(new Ecommerce_Class.Menu()
                {
                    Id = "Maintenance",
                    Name = "Maintenance"
                });
                menuList.Add(new Ecommerce_Class.Menu()
                {
                    Id = "RMAccess",
                    Name = "RM Access"
                });
                menuList.Add(new Ecommerce_Class.Menu()
                {
                    Id = "Portals",
                    Name = "Portals"
                });
                menuList.Add(new Ecommerce_Class.Menu()
                {
                    Id = "Analytics",
                    Name = "Analytics"
                });
                menuList.Add(new Ecommerce_Class.Menu()
                {
                    Id = "ItemStatus",
                    Name = "Item Status"
                });
                menuList.Sort((Comparison<Ecommerce_Class.Menu>)((x, y) => x.Name.CompareTo(y.Name)));
                return (object)menuList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.MenuList()");
            }
        }

        public static object PriceList(string filePath, Ecommerce_Class.FormInput form)
        {
            try
            {
                Ecommerce_Class.Item obj = new Ecommerce_Class.Item();
                List<Ecommerce_Class.Item> itemList = new List<Ecommerce_Class.Item>();
                foreach (DataRow row in (InternalDataCollectionBase)Ecommerce_DB.ProductGet("priceList").Rows)
                {
                    obj.Code = row["code"].ToString().Trim();
                    obj.Description = row["name"].ToString().Trim();
                    obj.Price = Convert.ToDecimal(row["price"]);
                    obj.UOM = row["uom"].ToString().Trim();
                    obj.Status = Convert.ToInt32(row["status"]) == 1 ? "yes" : "no";
                    if (form.Type.ToLower() == "active")
                    {
                        if (obj.Status == "yes")
                            itemList.Add(obj);
                    }
                    else
                        itemList.Add(obj);
                    obj = new Ecommerce_Class.Item();
                }
                WritePriceListFile(filePath, itemList);
                return (object)itemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.GetPriceList()");
            }
        }

        private static void WritePriceListFile(string filePath, List<Ecommerce_Class.Item> itemList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("ProductCode" + str + "ProductName" + str + "Price" + str + "UOM" + str + "IsActive");
                    foreach (Ecommerce_Class.Item obj in itemList)
                        streamWriter.WriteLine(obj.Code + str + obj.Description.Replace(',', '.') + str + (object)obj.Price + str + obj.UOM + str + obj.Status);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.WritePriceListFile()");
            }
        }

        public static object CustomerClassIds()
        {
            try
            {
                Ecommerce_Class.Option option = new Ecommerce_Class.Option();
                List<Ecommerce_Class.Option> optionList = new List<Ecommerce_Class.Option>();
                foreach (DataRow row in (InternalDataCollectionBase)Ecommerce_DB.CustomerClassGet("all").Rows)
                {
                    option.Id = row["customer_class_id"].ToString().Trim() + "|" + row["customer_class"].ToString().Trim();
                    option.Name = row["customer_class"].ToString().Trim();
                    optionList.Add(option);
                    option = new Ecommerce_Class.Option();
                }
                return (object)optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.GetCustomerClassIds()");
            }
        }

        public static object ProductIds()
        {
            try
            {
                Ecommerce_Class.Option option = new Ecommerce_Class.Option();
                List<Ecommerce_Class.Option> optionList = new List<Ecommerce_Class.Option>();
                foreach (DataRow row in (InternalDataCollectionBase)Ecommerce_DB.ProductGet("all_status").Rows)
                {
                    option.Id = row["productid"].ToString().Trim() + "|" + row["code"].ToString().Trim();
                    option.Name = row["code"].ToString().Trim();
                    optionList.Add(option);
                    option = new Ecommerce_Class.Option();
                }
                return (object)optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.GetProductIds()");
            }
        }

        public static object GetItemByClass(string filePath, Ecommerce_Class.FormInput form)
        {
            try
            {
                Ecommerce_Class.Item obj = new Ecommerce_Class.Item();
                List<Ecommerce_Class.Item> itemList = new List<Ecommerce_Class.Item>();
                string[] strArray = form.Class.Split('|');
                int int32 = Convert.ToInt32(strArray[0]);
                string str = strArray[1];
                DataTable dataTable = Ecommerce_DB.ProductGet("restrictions_by_class_id", classId: int32);
                foreach (DataRow row1 in (InternalDataCollectionBase)Ecommerce_DB.ProductGet("all").Rows)
                {
                    obj.Class = str;
                    obj.Id = Convert.ToInt32(row1["productid"]);
                    obj.Code = row1["code"].ToString().Trim();
                    obj.Description = row1["name"].ToString().Trim();
                    obj.IsAllowed = "Yes";
                    foreach (DataRow row2 in (InternalDataCollectionBase)dataTable.Rows)
                    {
                        if (obj.Id == (int)row2["objectid"])
                        {
                            obj.IsAllowed = "No";
                            break;
                        }
                    }
                    itemList.Add(obj);
                    obj = new Ecommerce_Class.Item();
                }
                WriteItemByClassFile(filePath, itemList);
                return (object)itemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.GetItemByClass()");
            }
        }

        private static void WriteItemByClassFile(string filePath, List<Ecommerce_Class.Item> itemList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("CustomerClass" + str + "ProductCode" + str + "ProductName" + str + "IsAllowed");
                    foreach (Ecommerce_Class.Item obj in itemList)
                        streamWriter.WriteLine(obj.Class + str + obj.Code + str + obj.Description.Replace(',', '.') + str + obj.IsAllowed);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.WriteItemByClassFile()");
            }
        }

        public static void GetAllItemsByCustomerClasses(string filePath)
        {
            try
            {
                Ecommerce_Class.Item obj = new Ecommerce_Class.Item();
                string str1 = ",";
                DataTable dataTable1 = new DataTable();
                DataTable dataTable2 = Ecommerce_DB.ProductGet("all");
                DataTable dataTable3 = Ecommerce_DB.CustomerClassGet("all");
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    string str2 = "Notes" + str1 + "ProductCode" + str1 + "ProductName";
                    foreach (DataRow row in (InternalDataCollectionBase)dataTable3.Rows)
                        str2 = str2 + str1 + row["customer_class"].ToString();
                    streamWriter.WriteLine(str2);
                    foreach (DataRow row1 in (InternalDataCollectionBase)dataTable2.Rows)
                    {
                        obj.Id = Convert.ToInt32(row1["productid"]);
                        obj.Code = row1["code"].ToString().Trim();
                        obj.Description = row1["name"].ToString().Trim().Replace(",", " ");
                        string str3 = str1 + obj.Code + str1 + obj.Description;
                        DataTable dataTable4 = Ecommerce_DB.ProductGet("restrictions_by_product_id", productId: obj.Id);
                        foreach (DataRow row2 in (InternalDataCollectionBase)dataTable3.Rows)
                        {
                            obj.IsAllowed = "Yes";
                            foreach (DataRow row3 in (InternalDataCollectionBase)dataTable4.Rows)
                            {
                                if ((int)row2["customer_class_id"] == (int)row3["customerclassid"])
                                {
                                    obj.IsAllowed = "No";
                                    break;
                                }
                            }
                            str3 = str3 + str1 + obj.IsAllowed;
                        }
                        streamWriter.WriteLine(str3);
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.GetAllItemsByCustomerClasses()");
            }
        }

        public static object GetClassByItem(string filePath, Ecommerce_Class.FormInput form)
        {
            try
            {
                Ecommerce_Class.Item obj = new Ecommerce_Class.Item();
                List<Ecommerce_Class.Item> itemList = new List<Ecommerce_Class.Item>();
                string[] strArray = form.Product.Split('|');
                int int32 = Convert.ToInt32(strArray[0]);
                string str = strArray[1];
                DataTable dataTable = Ecommerce_DB.ProductGet("restrictions_by_product_id", productId: int32);
                foreach (DataRow row1 in (InternalDataCollectionBase)Ecommerce_DB.CustomerClassGet("all").Rows)
                {
                    obj.Code = str;
                    obj.Id = Convert.ToInt32(row1["customer_class_id"]);
                    obj.Class = row1["customer_class"].ToString().Trim();
                    obj.IsAllowed = "Yes";
                    foreach (DataRow row2 in (InternalDataCollectionBase)dataTable.Rows)
                    {
                        if (obj.Id == (int)row2["CustomerClassId"])
                        {
                            obj.IsAllowed = "No";
                            break;
                        }
                    }
                    itemList.Add(obj);
                    obj = new Ecommerce_Class.Item();
                }
                WriteClassByItemFile(filePath, itemList);
                return (object)itemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.GetItemByClass()");
            }
        }

        private static void WriteClassByItemFile(string filePath, List<Ecommerce_Class.Item> itemList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("ProductCode" + str + "CustomerClass" + str + "IsAllowed");
                    foreach (Ecommerce_Class.Item obj in itemList)
                        streamWriter.WriteLine(obj.Code + str + obj.Class + str + obj.IsAllowed);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.WriteClassByItemFile()");
            }
        }

        public static object MaintenanceMenu()
        {
            try
            {
                List<Ecommerce_Class.Menu> menuList = new List<Ecommerce_Class.Menu>();
                menuList.Add(new Ecommerce_Class.Menu()
                {
                    Id = "1",
                    Name = "Mismatch Logon."
                });
                menuList.Add(new Ecommerce_Class.Menu()
                {
                    Id = "2",
                    Name = "Missing Menu Category for Store."
                });
                menuList.Add(new Ecommerce_Class.Menu()
                {
                    Id = "3",
                    Name = "Missing Contact in GP."
                });
                menuList.Add(new Ecommerce_Class.Menu()
                {
                    Id = "4",
                    Name = "Item Status."
                });
                menuList.Sort((Comparison<Ecommerce_Class.Menu>)((x, y) => x.Name.CompareTo(y.Name)));
                return (object)menuList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.GetMaintenanceMenu()");
            }
        }

        public static string MaintenanceRun(Ecommerce_Class.FormInput form)
        {
            try
            {
                string str1 = form.Item;
                char[] chArray = new char[1] { ',' };
                foreach (string str2 in str1.Split(chArray))
                {
                    if (str2 == "1")
                    {
                        if (str2 == "2")
                        {
                            if (str2 == "3")
                            {
                                if (str2 == "4")
                                    UpdateItemStatus();
                            }
                            else
                                UpdateMissingContact();
                        }
                        else
                            Ecommerce_DB.MaintenanceUpdate("unrestrictMenuCategory");
                    }
                    else
                        FixMismatchStoreLogon();
                }
                return "Done";
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.ExecuteMaintenance()");
            }
        }

        private static void UpdateItemStatus()
        {
            try
            {
                Ecommerce_Class.Item obj = new Ecommerce_Class.Item();
                foreach (DataRow row in (InternalDataCollectionBase)Ecommerce_DB.ItemResetStatus("records", obj).Rows)
                {
                    obj.Code = row["item"].ToString().Trim();
                    obj.Status = row["isactive"].ToString().Trim();
                    Ecommerce_DB.ItemStatusUpdate(obj);
                    obj = new Ecommerce_Class.Item();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.UpdateItemStatus()");
            }
        }

        private static void FixMismatchStoreLogon()
        {
            try
            {
                foreach (DataRow row in (InternalDataCollectionBase)Ecommerce_DB.MaintenanceGet("mismatchUserLogon").Rows)
                    Ecommerce_DB.MaintenanceUpdate("updateUserLogon", row["customer_no"].ToString().Trim(), row["g_user_id"].ToString().Trim());
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.EcomFixMismatchStoreLogon()");
            }
        }

        private static void UpdateMissingContact()
        {
            try
            {
                foreach (DataRow row in (InternalDataCollectionBase)Ecommerce_DB.MaintenanceGet("missingContactInGP").Rows)
                    Ecommerce_DB.MaintenanceUpdate("updateMissingContactInGP", row["CUSTNMBR"].ToString().Trim(), customerName: row["CUSTNAME"].ToString().Trim());
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.UpdateMissingContact()");
            }
        }

        public static object GetRegions()
        {
            try
            {
                Ecommerce_Class.Option option = new Ecommerce_Class.Option();
                List<Ecommerce_Class.Option> optionList = new List<Ecommerce_Class.Option>();
                foreach (DataRow row in (InternalDataCollectionBase)AFC.GetRegions().Rows)
                {
                    option.Id = row["RegionID"].ToString().Trim();
                    option.Name = row["RegionShorten"].ToString().Trim() + " | " + row["RegionName"].ToString().Trim();
                    optionList.Add(option);
                    option = new Ecommerce_Class.Option();
                }
                return (object)optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.GetRegions()");
            }
        }

        public static object GetRMLogins()
        {
            try
            {
                Ecommerce_Class.Option option = new Ecommerce_Class.Option();
                List<Ecommerce_Class.Option> optionList = new List<Ecommerce_Class.Option>();
                foreach (DataRow row in (InternalDataCollectionBase)Ecommerce_DB.UserLoginGet("login").Rows)
                {
                    option.Id = row["loginname"].ToString().Trim();
                    option.Name = row["loginname"].ToString().Trim() + " | " + row["EmailAddress"].ToString().Trim();
                    optionList.Add(option);
                    option = new Ecommerce_Class.Option();
                }
                return (object)optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.GetRMLogins()");
            }
        }

        public static string RMAccessUpdate(Ecommerce_Class.FormInput form)
        {
            try
            {
                
                Ecommerce_DB.ExecuteSql("delete from APP.dbo.RMAccess");
                DataTable dataTable = AFC.QueryRow("select Storecode from Store where OpenFlag <> 0 and RegionId in (" + form.Region + ") order by Storecode");
                foreach (DataRow row in dataTable.Rows)
                {
                    Ecommerce_DB.AddUserInput("rm_access", storecode: row["storecode"].ToString().Trim());
                }
                dataTable.Clear();
                dataTable = Ecommerce_DB.UserLoginGet("accessForStore");
                foreach (DataRow row in dataTable.Rows)
                {
                    string storecode = row["u_logon_name"].ToString().Trim();
                    string userAccess = form.Type == "append" ? row["u_pref3"].ToString().Trim() : string.Empty;
                    if (!userAccess.Contains("fst"))
                        userAccess = userAccess + (!string.IsNullOrEmpty(userAccess) ? "," : string.Empty) + "fst";
                    if (!userAccess.Contains("RnD"))
                        userAccess = userAccess + (!string.IsNullOrEmpty(userAccess) ? "," : string.Empty) + "RnD";
                    string login = form.Login;
                    char[] chArray = new char[1] { ',' };
                    foreach (string str1 in login.Split(chArray))
                    {
                        if (!userAccess.Contains(str1))
                            userAccess = userAccess + (!string.IsNullOrEmpty(userAccess) ? "," : string.Empty) + str1;
                    }
                    Ecommerce_DB.UserLoginUpdate(userAccess, storecode);
                    
                }
                return "Done";
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.RMAccessUpdate()");
            }
        }

        public static string ItemRestrictionUpdate(string filePath)
        {
            try
            {
                Ecommerce_Class.Item obj = new Ecommerce_Class.Item();
                DataTable table = Utilities.GetExcelData(filePath, "sheet1$");
                foreach (DataRow row in table.Rows)
                {
                    obj.Code = row["ProductCode"].ToString().Trim();
                    obj.Class = row["CustomerClass"].ToString().Trim();
                    obj.IsAllowed = row["IsAllowed"].ToString().Trim();
                    if (!string.IsNullOrEmpty(obj.Code))
                    {
                        string lower = obj.IsAllowed.ToLower();
                        if (lower == "no")
                        {
                            if (lower == "yes")
                                UnRestrictItem(obj);
                        }
                        else
                            RestrictItem(obj);
                    }
                }
                return "Done.";
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.ItemRestrictionUpdate()");
            }
        }

        private static int GetProductId(string code)
        {
            int num = 0;
            try
            {
                DataTable dataTable = Ecommerce_DB.ProductGet("byId", code);
                if (dataTable.Rows.Count > 0)
                    num = Convert.ToInt32(dataTable.Rows[0]["productid"]);
                return num;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "DB_Ecommerce.GetProductId()");
            }
        }

        public static int GetClassId(string code)
        {
            int num = 0;
            try
            {
                DataTable dataTable = Ecommerce_DB.CustomerClassGet("byId", code);
                if (dataTable.Rows.Count > 0)
                    num = Convert.ToInt32(dataTable.Rows[0]["Customer_Class_id"]);
                return num;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "DB_Ecommerce.GetClassId()");
            }
        }

        private static void RestrictItem(Ecommerce_Class.Item item)
        {
            try
            {
                int productId = GetProductId(item.Code);
                if (item.Class.ToLower() == "all customer classes")
                {
                    DataTable table = Ecommerce_DB.CustomerClassGet("all");
                    foreach (DataRow row in table.Rows)
                    {
                        int int32 = Convert.ToInt32(row["customer_class_id"]);
                        if (productId != 0 && (uint)int32 > 0)
                            Ecommerce_DB.ProductControl("restrict", int32, productId);
                    }
                }
                else
                {
                    int classId = GetClassId(item.Class);
                    if (productId != 0 && (uint)classId > 0)
                        Ecommerce_DB.ProductControl("restrict", classId, productId);
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.RestrictItem()");
            }
        }

        private static void UnRestrictItem(Ecommerce_Class.Item item)
        {
            try
            {
                int productId = GetProductId(item.Code);
                if (item.Class.ToLower() == "all customer classes")
                {
                    Ecommerce_DB.ProductControl("unrestrictAll", 0, productId);
                }
                else
                {
                    int classId = GetClassId(item.Class);
                    if (productId != 0 && classId > 0)
                        Ecommerce_DB.ProductControl("unrestrict", classId, productId);
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.UnRestrictItem()");
            }
        }

        public static object AnalyticsRun(Ecommerce_Class.FormInput form, string filePath)
        {
            try
            {
                string str = ",";
                Ecommerce_Class.Analytics analytics1 = new Ecommerce_Class.Analytics();
                List<Ecommerce_Class.Analytics> analyticsList = new List<Ecommerce_Class.Analytics>();
                DataTable dataTable1 = new DataTable();
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    string lower = form.Type.ToLower();
                    if (lower == "most popular items")
                    {
                        if (lower == "common keyword searches")
                        {
                            streamWriter.WriteLine("Term" + str + "Count");
                            analytics1.Column0 = "";
                            analytics1.Column1 = "Term";
                            analytics1.Column2 = "Count";
                            analyticsList.Add(analytics1);
                            Ecommerce_Class.Analytics analytics2 = new Ecommerce_Class.Analytics();
                            DataTable dataTable2 = Ecommerce_DB.Analytics("common_keyword_searches");
                            int num = 1;
                            foreach (DataRow row in dataTable2.Rows)
                            {
                                analytics2.Column0 = num++.ToString();
                                analytics2.Column1 = row["term"].ToString();
                                analytics2.Column2 = row["termcount"].ToString();
                                streamWriter.WriteLine(row["term"].ToString() + str + row["termcount"].ToString());
                                analyticsList.Add(analytics2);
                                analytics2 = new Ecommerce_Class.Analytics();
                            }
                        }
                    }
                    else
                    {
                        streamWriter.WriteLine("Product Code" + str + "Product Name" + str + "Total Amount");
                        analytics1.Column0 = "Product Code";
                        analytics1.Column1 = "Product Name";
                        analytics1.Column2 = "Total Amount";
                        analyticsList.Add(analytics1);
                        Ecommerce_Class.Analytics analytics2 = new Ecommerce_Class.Analytics();
                        foreach (DataRow row in (InternalDataCollectionBase)Ecommerce_DB.Analytics("most_popular_items").Rows)
                        {
                            analytics2.Column0 = row["code"].ToString();
                            analytics2.Column1 = row["name"].ToString();
                            analytics2.Column2 = Convert.ToDecimal(row["linetotal"]).ToString("C", (IFormatProvider)CultureInfo.CurrentCulture);
                            streamWriter.WriteLine(row["code"].ToString() + str + row["name"].ToString().Replace(',', '.') + str + Convert.ToDecimal(row["linetotal"]).ToString());
                            analyticsList.Add(analytics2);
                            analytics2 = new Ecommerce_Class.Analytics();
                        }
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
                return (object)analyticsList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.AnalyticsRun()");
            }
        }

        public static object ItemResetStatus(string filePath)
        {
            try
            {
                Ecommerce_Class.Item obj = new Ecommerce_Class.Item();
                List<Ecommerce_Class.Item> itemList = new List<Ecommerce_Class.Item>();
                DataTable table = Ecommerce_DB.ItemResetStatus("records", obj);
                foreach (DataRow row in table.Rows)
                {
                    obj.Code = row["item"].ToString();
                    obj.Description = row["item_desc"].ToString();
                    obj.IsActive = row["isactive"].ToString();
                    itemList.Add(obj);
                    obj = new Ecommerce_Class.Item();
                }
                WriteItemResetStatusFile(filePath, itemList);
                return (object)itemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.ItemResetStatus()");
            }
        }

        private static void WriteItemResetStatusFile(string filePath, List<Ecommerce_Class.Item> itemList)
        {
            try
            {
                string str = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("ProductCode" + str + "ProductName" + str + "IsActive");
                    foreach (Ecommerce_Class.Item obj in itemList)
                    { 
                        streamWriter.WriteLine(obj.Code + str +
                            obj.Description.Replace(',', ' ') + str +
                            obj.IsActive);
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution.WriteItemResetStatusFile()");
            }
        }
    }
}