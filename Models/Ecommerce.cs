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
                menuList.Sort((x, y) => x.Name.CompareTo(y.Name));
                return menuList;
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
                Ecommerce_Class.Item item = new Ecommerce_Class.Item();
                List<Ecommerce_Class.Item> itemList = new List<Ecommerce_Class.Item>();
                DataTable table = Ecommerce_DB.ProductGet("priceList");
                foreach (DataRow row in table.Rows)
                {
                    item.Code = row["code"].ToString().Trim();
                    item.Description = row["name"].ToString().Trim();
                    item.Price = Convert.ToDecimal(row["price"]);
                    item.UOM = row["uom"].ToString().Trim();
                    item.Status = Convert.ToInt32(row["status"]) == 1 ? "yes" : "no";
                    if (string.Compare(form.Type, "active", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        // when active only is selected.
                        if (string.Compare(item.Status, "yes", StringComparison.OrdinalIgnoreCase) == 0)
                            itemList.Add(item);
                    }
                    else
                        itemList.Add(item);
                    item = new Ecommerce_Class.Item();
                }
                WritePriceListFile(filePath, itemList);
                return itemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.PriceList()");
            }
        }

        private static void WritePriceListFile(string filePath, List<Ecommerce_Class.Item> itemList)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("ProductCode" + delim + "ProductName" + delim + "Price" + delim + "UOM" + delim + "IsActive");
                    foreach (Ecommerce_Class.Item item in itemList)
                        streamWriter.WriteLine(
                            item.Code + delim + 
                            item.Description.Replace(',', '.') + delim + 
                            item.Price + delim + 
                            item.UOM + delim + item.Status
                            );
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
                DataTable table = Ecommerce_DB.CustomerClassGet("all");
                foreach (DataRow row in table.Rows)
                {
                    option.Id = row["customer_class_id"].ToString().Trim() + "|" + row["customer_class"].ToString().Trim();
                    option.Name = row["customer_class"].ToString().Trim();
                    optionList.Add(option);
                    option = new Ecommerce_Class.Option();
                }
                return optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.CustomerClassIds()");
            }
        }

        public static object ProductIds()
        {
            try
            {
                Ecommerce_Class.Option option = new Ecommerce_Class.Option();
                List<Ecommerce_Class.Option> optionList = new List<Ecommerce_Class.Option>();
                DataTable table = Ecommerce_DB.ProductGet("all_status");
                foreach (DataRow row in table.Rows)
                {
                    option.Id = row["productid"].ToString().Trim() + "|" + row["code"].ToString().Trim();
                    option.Name = row["code"].ToString().Trim();
                    optionList.Add(option);
                    option = new Ecommerce_Class.Option();
                }
                return optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.ProductIds()");
            }
        }

        public static object GetItemByClass(string filePath, Ecommerce_Class.FormInput form)
        {
            try
            {
                Ecommerce_Class.Item item = new Ecommerce_Class.Item();
                List<Ecommerce_Class.Item> itemList = new List<Ecommerce_Class.Item>();
                string[] classArray = form.Class.Split('|');
                int classId = Convert.ToInt32(classArray[0]);
                string className = classArray[1];
                DataTable restrictionTable = Ecommerce_DB.ProductGet("restrictions_by_class_id", classId: classId);
                DataTable productTable = Ecommerce_DB.ProductGet("all_status");
                foreach (DataRow row in productTable.Rows)
                {
                    item.Class = className;
                    item.Id = Convert.ToInt32(row["productid"]);
                    item.Code = row["code"].ToString().Trim();
                    item.Description = row["name"].ToString().Trim();
                    item.IsAllowed = "Yes";
                    item.IsActive = Convert.ToInt32(row["visible"]) != 0 ? "Yes" : "No";
                    // check if found in restriction table.
                    foreach (DataRow row2 in restrictionTable.Rows)
                    {
                        if (item.Id == (int)row2["objectid"])
                        {
                            item.IsAllowed = "No";
                            break;
                        }
                    }
                    itemList.Add(item);
                    item = new Ecommerce_Class.Item();
                }
                WriteItemByClassFile(filePath, itemList);
                return itemList;
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
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("CustomerClass" + delim + "ProductCode" + delim + "ProductName" + delim + "IsAllowed" + delim + "IsActive");
                    foreach (Ecommerce_Class.Item item in itemList)
                        streamWriter.WriteLine(
                            item.Class + delim + 
                            item.Code + delim + 
                            item.Description.Replace(',', '.') + delim + 
                            item.IsAllowed + delim +
                            item.IsActive
                            );
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
                Ecommerce_Class.Item item = new Ecommerce_Class.Item();
                string delim = ",";
                DataTable productTable = Ecommerce_DB.ProductGet("all");
                DataTable classTable = Ecommerce_DB.CustomerClassGet("all");
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    string titleLine = "Notes" + delim + "ProductCode" + delim + "ProductName";
                    foreach (DataRow row in classTable.Rows)
                    {
                        titleLine += delim + row["customer_class"].ToString();
                    }
                    streamWriter.WriteLine(titleLine);
                    foreach (DataRow row1 in productTable.Rows)
                    {
                        item.Id = Convert.ToInt32(row1["productid"]);
                        item.Code = row1["code"].ToString().Trim();
                        item.Description = row1["name"].ToString().Trim().Replace(",", " ");
                        string itemLine = delim + item.Code + delim + item.Description;
                        DataTable restrictionTable = Ecommerce_DB.ProductGet("restrictions_by_product_id", productId: item.Id);
                        foreach (DataRow row2 in classTable.Rows)
                        {
                            item.IsAllowed = "Yes";
                            // check if class id found in restriction table.
                            foreach (DataRow row3 in restrictionTable.Rows)
                            {
                                if ((int)row2["customer_class_id"] == (int)row3["customerclassid"])
                                {
                                    item.IsAllowed = "No";
                                    break;
                                }
                            }
                            itemLine += delim + item.IsAllowed;
                        }
                        streamWriter.WriteLine(itemLine);
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
                Ecommerce_Class.Item item = new Ecommerce_Class.Item();
                List<Ecommerce_Class.Item> itemList = new List<Ecommerce_Class.Item>();
                string[] productArray = form.Product.Split('|');
                int productId = Convert.ToInt32(productArray[0]);
                string productName = productArray[1];
                DataTable restrictionTable = Ecommerce_DB.ProductGet("restrictions_by_product_id", productId: productId);
                DataTable classTable = Ecommerce_DB.CustomerClassGet("all");
                foreach (DataRow row in classTable.Rows)
                {
                    item.Code = productName;
                    item.Id = Convert.ToInt32(row["customer_class_id"]);
                    item.Class = row["customer_class"].ToString().Trim();
                    item.IsAllowed = "Yes";
                    // check if found in restriction table.
                    foreach (DataRow row2 in restrictionTable.Rows)
                    {
                        if (item.Id == (int)row2["CustomerClassId"])
                        {
                            item.IsAllowed = "No";
                            break;
                        }
                    }
                    itemList.Add(item);
                    item = new Ecommerce_Class.Item();
                }
                WriteClassByItemFile(filePath, itemList);
                return itemList;
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
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("ProductCode" + delim + "CustomerClass" + delim + "IsAllowed");
                    foreach (Ecommerce_Class.Item item in itemList)
                        streamWriter.WriteLine(
                            item.Code + delim + 
                            item.Class + delim + 
                            item.IsAllowed
                            );
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
                menuList.Sort((x, y) => x.Name.CompareTo(y.Name));
                return menuList;
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
                string[] taskArray = form.Item.Split(',');                
                foreach (string task in taskArray)
                {
                    switch (task)
                    {
                        case "1":
                            FixMismatchStoreLogon();
                            break;
                        case "2":
                            Ecommerce_DB.MaintenanceUpdate("unrestrictMenuCategory");
                            break;
                        case "3":
                            UpdateMissingContact();
                            break;
                        case "4":
                            UpdateItemStatus();
                            break;
                    }
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
                Ecommerce_Class.Item item = new Ecommerce_Class.Item();
                DataTable table = Ecommerce_DB.ItemResetStatus("records", item);
                foreach (DataRow row in table.Rows)
                {
                    item.Code = row["item"].ToString().Trim();
                    item.Status = row["isactive"].ToString().Trim();
                    Ecommerce_DB.ItemStatusUpdate(item);
                    item = new Ecommerce_Class.Item();
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
                DataTable table = Ecommerce_DB.MaintenanceGet("mismatchUserLogon");
                foreach (DataRow row in table.Rows)
                {
                    Ecommerce_DB.MaintenanceUpdate("updateUserLogon", customerNo: row["customer_no"].ToString().Trim(), userId: row["g_user_id"].ToString().Trim());
                }
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
                DataTable table = Ecommerce_DB.MaintenanceGet("missingContactInGP");
                foreach (DataRow row in table.Rows)
                {
                    Ecommerce_DB.MaintenanceUpdate("updateMissingContactInGP", customerNo: row["CUSTNMBR"].ToString().Trim(), customerName: row["CUSTNAME"].ToString().Trim());
                }
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
                DataTable table = AFC.GetRegions();                
                foreach (DataRow row in table.Rows)
                {
                    option.Id = row["RegionID"].ToString().Trim();
                    option.Name = row["RegionShorten"].ToString().Trim() + " | " + row["RegionName"].ToString().Trim();
                    optionList.Add(option);
                    option = new Ecommerce_Class.Option();
                }
                return optionList;
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
                DataTable table = Ecommerce_DB.UserLoginGet("login");
                foreach (DataRow row in table.Rows)
                {
                    option.Id = row["loginname"].ToString().Trim();
                    option.Name = row["loginname"].ToString().Trim() + " | " + row["EmailAddress"].ToString().Trim();
                    optionList.Add(option);
                    option = new Ecommerce_Class.Option();
                }
                return optionList;
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
                DataTable table = AFC.QueryRow("select Storecode from Store where OpenFlag <> 0 and RegionId in (" + form.Region + ") order by Storecode");
                foreach (DataRow row in table.Rows)
                {
                    Ecommerce_DB.AddUserInput("rm_access", storecode: row["storecode"].ToString().Trim());
                }
                table.Clear();
                table = Ecommerce_DB.UserLoginGet("accessForStore");
                foreach (DataRow row in table.Rows)
                {
                    string storecode = row["u_logon_name"].ToString().Trim();
                    string userAccess = form.Type == "append" ? row["u_pref3"].ToString().Trim() : string.Empty;
                    if (!userAccess.Contains("fst"))
                        userAccess = userAccess + (!string.IsNullOrEmpty(userAccess) ? "," : string.Empty) + "fst";
                    if (!userAccess.Contains("RnD"))
                        userAccess = userAccess + (!string.IsNullOrEmpty(userAccess) ? "," : string.Empty) + "RnD";
                    string[] loginArray = form.Login.Split(',');                    
                    foreach (string login in loginArray)
                    {
                        // if login not found then add it.
                        if (!userAccess.Contains(login))
                            userAccess = userAccess + (!string.IsNullOrEmpty(userAccess) ? "," : string.Empty) + login;
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
                Ecommerce_Class.Item item = new Ecommerce_Class.Item();
                DataTable table = Utilities.GetExcelData(filePath, "sheet1$");
                foreach (DataRow row in table.Rows)
                {
                    item.Code = row["ProductCode"].ToString().Trim();
                    item.Class = row["CustomerClass"].ToString().Trim();
                    item.IsAllowed = row["IsAllowed"].ToString().Trim();
                    if (!string.IsNullOrEmpty(item.Code))
                    {
                        switch (item.IsAllowed.ToLower())
                        {
                            case "yes":
                                UnRestrictItem(item);
                                break;
                            case "no":
                                RestrictItem(item);
                                break;
                        }
                    }
                    else
                        break;
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
            int productId = 0;
            try
            {
                DataTable table = Ecommerce_DB.ProductGet("byId", code);
                if (table.Rows.Count > 0)
                    productId = Convert.ToInt32(table.Rows[0]["productid"]);
                return productId;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "DB_Ecommerce.GetProductId()");
            }
        }

        public static int GetClassId(string code)
        {
            int classId = 0;
            try
            {
                DataTable table = Ecommerce_DB.CustomerClassGet("byId", code);
                if (table.Rows.Count > 0)
                    classId = Convert.ToInt32(table.Rows[0]["Customer_Class_id"]);
                return classId;
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
                // all classes.
                if (string.Compare(item.Class, "all customer classes", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    DataTable table = Ecommerce_DB.CustomerClassGet("all");
                    foreach (DataRow row in table.Rows)
                    {
                        int classId = Convert.ToInt32(row["customer_class_id"]);
                        if (productId != 0 && classId > 0)
                            Ecommerce_DB.ProductControl("restrict", classId, productId);
                    }
                }
                else  // by class id.
                {
                    int classId = GetClassId(item.Class);
                    if (productId != 0 && classId > 0)
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
                    Ecommerce_DB.ProductControl("unrestrictAll", productId: productId);
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
                string delim = ",";
                Ecommerce_Class.Analytics analytics = new Ecommerce_Class.Analytics();
                List<Ecommerce_Class.Analytics> analyticsList = new List<Ecommerce_Class.Analytics>();
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    switch (form.Type.ToLower())
                    {
                        case "common keyword searches":
                            {
                                streamWriter.WriteLine("Term" + delim + "Count");
                                analytics.Column0 = "";
                                analytics.Column1 = "Term";
                                analytics.Column2 = "Count";
                                analyticsList.Add(analytics);
                                // reset
                                analytics = new Ecommerce_Class.Analytics();
                                DataTable table = Ecommerce_DB.Analytics("common_keyword_searches");
                                int index = 1;
                                foreach (DataRow row in table.Rows)
                                {
                                    analytics.Column0 = index++.ToString();
                                    analytics.Column1 = row["term"].ToString();
                                    analytics.Column2 = row["termcount"].ToString();
                                    // for download file
                                    streamWriter.WriteLine(
                                        analytics.Column1 + delim +
                                        analytics.Column2
                                        );
                                    analyticsList.Add(analytics);
                                    analytics = new Ecommerce_Class.Analytics();
                                }
                            }
                            break;
                        case "most popular items":
                            {
                                streamWriter.WriteLine("Product Code" + delim + "Product Name" + delim + "Total Amount");
                                analytics.Column0 = "Product Code";
                                analytics.Column1 = "Product Name";
                                analytics.Column2 = "Total Amount";
                                analyticsList.Add(analytics);
                                analytics = new Ecommerce_Class.Analytics();
                                DataTable table1 = Ecommerce_DB.Analytics("most_popular_items");
                                foreach (DataRow row in table1.Rows)
                                {
                                    analytics.Column0 = row["code"].ToString();
                                    analytics.Column1 = row["name"].ToString();
                                    analytics.Column2 = Convert.ToDecimal(row["linetotal"]).ToString("C", CultureInfo.CurrentCulture);
                                    // for download file.
                                    streamWriter.WriteLine(
                                        analytics.Column0 + delim +
                                        analytics.Column1.Replace(',', '.') + delim +
                                        Convert.ToDecimal(row["linetotal"]).ToString()
                                        );
                                    analyticsList.Add(analytics);
                                    analytics = new Ecommerce_Class.Analytics();
                                }
                            }
                            break;                    
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
                return analyticsList;
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
                Ecommerce_Class.Item item = new Ecommerce_Class.Item();
                List<Ecommerce_Class.Item> itemList = new List<Ecommerce_Class.Item>();
                DataTable table = Ecommerce_DB.ItemResetStatus("records", item);
                foreach (DataRow row in table.Rows)
                {
                    item.Code = row["item"].ToString();
                    item.Description = row["item_desc"].ToString();
                    item.IsActive = row["isactive"].ToString();
                    itemList.Add(item);
                    item = new Ecommerce_Class.Item();
                }
                WriteItemResetStatusFile(filePath, itemList);
                return itemList;
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
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("ProductCode" + delim + "ProductName" + delim + "IsActive");
                    foreach (Ecommerce_Class.Item item in itemList)
                    { 
                        streamWriter.WriteLine(item.Code + delim +
                            item.Description.Replace(',', ' ') + delim +
                            item.IsActive);
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