using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace intraweb_rev3.Models
{
    public class RnD
    {
        public static object MenuList()
        {
            try
            {
                List<RnD_Class.Menu> menuList = new List<RnD_Class.Menu>();
                menuList.Add(new RnD_Class.Menu()
                {
                    Id = "GPCustomerList",
                    Name = "GP Customer List"
                });
                menuList.Add(new RnD_Class.Menu()
                {
                    Id = "CustomerClassUpdate",
                    Name = "Customer Class Update"
                });
                menuList.Add(new RnD_Class.Menu()
                {
                    Id = "CustomerClassCreateDelete",
                    Name = "Customer Class Create or Delete"
                });
                menuList.Add(new RnD_Class.Menu()
                {
                    Id = "CustomerClassList",
                    Name = "Customer Class List"
                });
                menuList.Add(new RnD_Class.Menu()
                {
                    Id = "ConnectPassword",
                    Name = "Connect Password"
                });
                menuList.Add(new RnD_Class.Menu()
                {
                    Id = "ConnectClickCount",
                    Name = "Connect Click Count"
                });
                menuList.Add(new RnD_Class.Menu()
                {
                    Id = "Safeway",
                    Name = "Safeway"
                });
                menuList.Sort((Comparison<RnD_Class.Menu>)((x, y) => x.Name.CompareTo(y.Name)));
                return menuList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.MenuList()");
            }
        }

        public static object GPCustomerListGet(string filePath)
        {
            try
            {
                RnD_Class.Customer customer = new RnD_Class.Customer();
                List<RnD_Class.Customer> customerList = new List<RnD_Class.Customer>();
                foreach (DataRow row in (InternalDataCollectionBase)Ecommerce_DB.CustomerClassGet("gpCustomer").Rows)
                {
                    customer.Number = row["CUSTNMBR"].ToString().Trim();
                    customer.Name = row["CUSTNAME"].ToString().Trim();
                    customer.Class = row["CUSTCLAS"].ToString().Trim();
                    customer.Address1 = row["address1"].ToString().Trim();
                    customer.City = row["city"].ToString().Trim();
                    customer.State = row["state"].ToString().Trim();
                    customer.Zip = row["zip"].ToString().Trim();
                    customer.Country = row["country"].ToString().Trim();
                    customer.Phone = Utilities.FormatPhone(row["phone1"].ToString().Trim());
                    customer.ModifiedDate = row["modified"].ToString().Trim();
                    customerList.Add(customer);
                    customer = new RnD_Class.Customer();
                }
                RnD.WriteGPCustomerListFile(filePath, customerList);
                return customerList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.RnD.GetGPCustomerList()");
            }
        }

        private static void WriteGPCustomerListFile(string filePath, List<RnD_Class.Customer> customerList)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Storecode" + delim + "Company" + delim + "CustomerClass" + delim + "Address" + delim +
                        "City" + delim + "ST" + delim + "Zipcode" + delim + "Country" + delim + "Telephone" + delim + "Modified Date");
                    foreach (RnD_Class.Customer customer in customerList)
                    {
                        streamWriter.WriteLine(
                            customer.Number + delim +
                            customer.Name.Replace(',', '.') + delim +
                            customer.Class + delim + 
                            customer.Address1.Replace(',', '.') + delim + 
                            customer.City.Replace(',', '.') + delim + 
                            customer.State + delim + 
                            customer.Zip + delim + 
                            customer.Country.Replace(',', '.') + delim + 
                            customer.Phone + delim + 
                            customer.ModifiedDate
                            );
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.RnD.WriteGPCustomerListFile()");
            }
        }

        public static object CustomerClassDroplist()
        {
            try
            {
                RnD_Class.Option option = new RnD_Class.Option();
                List<RnD_Class.Option> optionList = new List<RnD_Class.Option>();
                DataTable table = Ecommerce_DB.CustomerClassGet("all");
                foreach (DataRow row in table.Rows)
                {
                    option.Id = row["customer_class_id"].ToString().Trim();
                    option.Name = row["customer_class"].ToString();
                    optionList.Add(option);
                    option = new RnD_Class.Option();
                }
                return (object)optionList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.RnD.CustomerClassDropList()");
            }
        }

        public static string CustomerClassRun(RnD_Class.FormInput form)
        {
            try
            {
                string action = form.Action;
                if (action != "create")
                {
                    if (action == "delete")
                        RnD_DB.CustomerClassDelete(Ecommerce.GetClassId(form.Class), form.Class);
                }
                else
                {
                    int classId = RnD_DB.CustomerClassInsert(form.Class.ToUpper());
                    DataTable table = Ecommerce_DB.ProductGet("restrictions_by_class_id", "", form.CopyFromId);
                    if (form.CopyFromId > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            int productId = Convert.ToInt32(row["objectid"]);
                            Ecommerce_DB.ProductControl("restrict", classId, productId);
                        }
                    }
                }
                return "Done";
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.RnD.CustomerClassRun()");
            }
        }

        public static string CustomerClassUpdateRun(string filePath)
        {
            try
            {
                string str1 = "";
                int classId = 0;
                foreach (DataRow row in (InternalDataCollectionBase)Utilities.GetExcelData(filePath, "sheet1$").Rows)
                {
                    string str2 = row["CustomerClass"].ToString().ToUpper().Trim();
                    string storecode = row["StoreCode"].ToString().ToUpper().Trim();
                    if (str2 != str1)
                    {
                        classId = Ecommerce.GetClassId(str2);
                        str1 = str2;
                    }
                    RnD_DB.CustomerClassUpdate(classId, str2, storecode);
                }
                return "Done";
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Ecommerce.CustomerClassExecuteUpdate()");
            }
        }

        public static object CustomerClassList(string filePath)
        {
            try
            {
                RnD_Class.CustomerClass customerClass = new RnD_Class.CustomerClass();
                List<RnD_Class.CustomerClass> customerList = new List<RnD_Class.CustomerClass>();
                DataTable table = RnD_DB.GetClass("customer_class");
                foreach (DataRow row in table.Rows)
                {
                    customerClass.Id = Convert.ToInt32(row["CustomerClassId"]);
                    customerClass.ClassId = row["ClassId"].ToString().Trim();
                    customerClass.Description = row["Description"].ToString();
                    customerClass.SpecialBrownRice = Convert.ToBoolean(row["SpecialBrownRice"]);
                    customerClass.No6Container = Convert.ToBoolean(row["No6Container"]);
                    customerClass.TunaSaku = Convert.ToBoolean(row["TunaSaku"]);
                    customerClass.TunaTatakimi = Convert.ToBoolean(row["TunaTatakimi"]);
                    customerClass.Eel = Convert.ToBoolean(row["Eel"]);
                    customerClass.RetailSeaweed = Convert.ToBoolean(row["RetailSeaweed"]);
                    customerClass.RetailGingerBottle = Convert.ToBoolean(row["RetailGingerBottle"]);
                    customerClass.RetailGingerCup = Convert.ToBoolean(row["RetailGingerCup"]);
                    customerClass.MSCTuna = Convert.ToBoolean(row["MSCTuna"]);
                    customerList.Add(customerClass);
                    customerClass = new RnD_Class.CustomerClass();
                }
                RnD.WriteCustomerClassListFile(filePath, customerList);
                return customerList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.RnD.GetCustomerClassList()");
            }
        }

        private static void WriteCustomerClassListFile(string filePath, List<RnD_Class.CustomerClass> customerList)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Customer Class, Description, Special Brown Rice, No.6 Container, Tuna Saku, Tuna Tatakimi, Eel, Retail Seaweed, Retail Ginger Bottle, Retail Ginger Cup, MSC Tuna");
                    foreach (RnD_Class.CustomerClass customer in customerList)
                    {
                        streamWriter.WriteLine(
                            customer.ClassId + delim + 
                            customer.Description.Replace(",", ";") + delim + 
                            (customer.SpecialBrownRice ? "X" : "") + delim + 
                            (customer.No6Container ? "X" : "") + delim + 
                            (customer.TunaSaku ? "X" : "") + delim + 
                            (customer.TunaTatakimi ? "X" : "") + delim + 
                            (customer.Eel ? "X" : "") + delim + 
                            (customer.RetailSeaweed ? "X" : "") + delim + 
                            (customer.RetailGingerBottle ? "X" : "") + delim + 
                            (customer.RetailGingerCup ? "X" : "") + delim + 
                            (customer.MSCTuna ? "X" : "")
                            );
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.RnD.WriteCustomerClassListFile()");
            }
        }

        public static object ConnectPasswordList(string filePath)
        {
            try
            {
                RnD_Class.ConnectUser connectUser = new RnD_Class.ConnectUser();
                List<RnD_Class.ConnectUser> userList = new List<RnD_Class.ConnectUser>();
                DataTable table = AFC.QueryRow("SELECT fr.OldId, pe.PersonId, concat(pe.FirstName, ' ', pe.LastName) as fullname," +
                    " pe.Password, pe.Email, pe.WebActiveFlag FROM Person as pe INNER JOIN Franchisee as fr on pe.PersonID = fr.PersonID" +
                    " INNER JOIN Store as st on pe.PersonId = st.PersonId where st.openflag = 1 group by pe.PersonId order by fr.oldid");
                foreach (DataRow row in table.Rows)
                {
                    connectUser.FCID = row["oldid"].ToString().Trim();
                    connectUser.Id = Convert.ToInt32(row["personid"]);
                    connectUser.Name = row["fullname"].ToString().Trim();
                    connectUser.Password = row["password"].ToString().Trim();
                    connectUser.Email = row["email"].ToString().Trim();
                    connectUser.Webactive = Convert.ToInt32(row["WebActiveFlag"]) != 0 ? "Yes" : "No";
                    userList.Add(connectUser);
                    connectUser = new RnD_Class.ConnectUser();
                }
                RnD.WriteConnectPasswordListFile(filePath, userList);
                return userList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.RnD.GetConnectPasswordList()");
            }
        }

        private static void WriteConnectPasswordListFile(string filePath, List<RnD_Class.ConnectUser> userList)
        {
            try
            {
                string delim = ",";
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("FCID, Name, Password, Email, Webactive");
                    foreach (RnD_Class.ConnectUser user in userList)
                    {
                        streamWriter.WriteLine(user.FCID + delim + 
                            user.Name.Replace(',', ' ') + delim + 
                            user.Password + delim + 
                            user.Email.Replace(',', ' ') + delim + 
                            user.Webactive
                            );
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.RnD.WriteConnectPasswordListFile()");
            }
        }

        public static void ConnectSetDefaultUserPassword(string filePath)
        {
            try
            {
                DataTable table = Utilities.GetExcelData(filePath, "sheet1$");
                foreach (DataRow row in table.Rows)
                {
                    if (!Utilities.isNull(row["fcid"]))
                    {
                        DataTable personTable = AFC.QueryRow("SELECT pe.PersonId FROM Person as pe INNER JOIN Franchisee as fr on pe.PersonID = fr.PersonID where fr.OldId like '" + row["fcid"].ToString().Trim() + "'");
                        if (personTable.Rows.Count > 0)
                        {
                            int personId = Convert.ToInt32(personTable.Rows[0]["personid"]);
                            AFC.ExecuteSql("update Person set password = '" + "afc" + personId.ToString() + "', WebActiveFlag = 1 where PersonId = " + personId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.RnD.ConnectSetDefaultUserPassword()");
            }
        }

        public static void ConnectClickCount(string filePath, RnD_Class.FormInput form)
        {
            try
            {
                string delim = ",";
                DataTable table = AFC.QueryRow("select cc.fcid, cc.videoname, date_format(cc.timestamp, '%m/%d/%Y %H:%i:%s') as dateviewed, pe.Firstname, pe.Lastname, pe.Email " +
                    "from ConnectVideoClickCount as cc inner join Franchisee as fr on cc.fcid = fr.OldId inner join Person as pe on fr.PersonID = pe.PersonID " +
                    "where cc.timestamp between '" + Convert.ToDateTime(form.Startdate).ToString("yyyy-MM-dd") + 
                    "' and '" + Convert.ToDateTime(form.Enddate).ToString("yyyy-MM-dd") + "' order by cc.timestamp");
                using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                {
                    streamWriter.WriteLine("Date, FCID, Video, Firstname, Lastname, Email");
                    foreach (DataRow row in table.Rows)
                    {
                        streamWriter.WriteLine(row["dateviewed"].ToString() + delim +
                            row["fcid"].ToString() + delim +
                            row["videoname"].ToString() + delim +
                            row["firstname"].ToString() + delim +
                            row["lastname"].ToString() + delim +
                            row["email"].ToString()
                            );
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.RnD.GetConnectClickCount()");
            }
        }

        public static void SafewayItemRecode(string filePath)
        {
            try
            {
                RnD_Class.Safeway safeway = new RnD_Class.Safeway();
                DataTable table = Utilities.GetExcelData(filePath, "sheet1$");
                foreach (DataRow row in table.Rows)
                {
                    safeway.Storecode = row["Storecode"].ToString().Trim();
                    safeway.Salesdate = Convert.ToDateTime(row["sales_date"]);
                    safeway.UPC = row["upc"].ToString().Trim();
                    safeway.Category = row["category"].ToString().Trim();
                    AFC.SafewayItemRecode(safeway);
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.RnD.SafewayItemRecode()");
            }
        }

        public static void SafewayItemInsert(string filePath)
        {
            try
            {
                RnD_Class.Safeway safeway = new RnD_Class.Safeway();
                DataTable table = Utilities.GetExcelData(filePath, "sheet1$");
                foreach (DataRow row in table.Rows)
                {
                    if (!Utilities.isNull(row["Store"]))
                    {
                        safeway.Storenumber = Convert.ToInt32(row["store"]);
                        safeway.Salesdate = Convert.ToDateTime(row["sales_date"]);
                        safeway.UPC = row["upc"].ToString().Trim();
                        safeway.Item = row["item"].ToString().Trim();
                        safeway.Quantity = Convert.ToDecimal(row["quantity"]);
                        safeway.Gross = Convert.ToDecimal(row["gross_sales"]);
                        safeway.Markdown = Convert.ToDecimal(row["markdowns"]);
                        safeway.Net = Convert.ToDecimal(row["net_sales"]);
                        safeway.Category = row["category"].ToString().Trim();
                        safeway.Storecode = row["storecode"].ToString().Trim() + (safeway.Category.ToLower() == "ttw" ? "W" : string.Empty);
                        safeway.Region = row["region"].ToString().Trim();
                        safeway.Division = row["division"].ToString().Trim();
                        safeway.Brand = RnD.SafewaySetBrand(safeway.Storecode);
                        AFC.SafewayItemInsert(safeway);
                    }
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.RnD.SafewayItemInsert()");
            }
        }

        private static string SafewaySetBrand(string storeCode)
        {
            string str = string.Empty;
            try
            {
                if (Regex.IsMatch(storeCode, "alb", RegexOptions.IgnoreCase))
                    str = "Albertson";
                else if (Regex.IsMatch(storeCode, "car", RegexOptions.IgnoreCase))
                    str = "Carr's";
                else if (Regex.IsMatch(storeCode, "jew|jks", RegexOptions.IgnoreCase))
                    str = "Jewel-Osco";
                else if (Regex.IsMatch(storeCode, "pav|vo-", RegexOptions.IgnoreCase))
                    str = "Pavilions & Vons";
                else if (Regex.IsMatch(storeCode, "ran|tom", RegexOptions.IgnoreCase))
                    str = "Randall & Tom Thumb";
                else if (Regex.IsMatch(storeCode, "saf", RegexOptions.IgnoreCase))
                    str = "Safeway";
                else if (Regex.IsMatch(storeCode, "sha", RegexOptions.IgnoreCase))
                    str = "Shaws";
                return str;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.RnD.SafewaySetBrand()");
            }
        }
    }
}