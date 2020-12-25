﻿using intraweb_rev3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace intraweb_rev3.Controllers
{
    public class RnDController : Controller
    {
        // GET: RnD
        public ActionResult Index() => View();
        

        [HttpPost]
        public JsonResult LoadMenu()
        {
            try
            {
                //List<object> objectList = new List<object>();
                //objectList.Add(RnD.MenuList());
                return Json(RnD.MenuList());            
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        private string GetFilePath(string dir, string filename)
        {
            try
            {
                string str = Server.MapPath("~/" + dir);
                Utilities.DeleteOldFiles(str);
                return Path.Combine(str, filename);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Controller.RnDController.GetFilePath()");
            }
        }

        public ActionResult GPCustomerList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GPCustomerListData()
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "GPCustomerList_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(RnD.GPCustomerListGet(filePath));
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult CustomerClassCreateDelete()
        {
            return View();
        }        

        [HttpPost]
        public JsonResult CustomerClassCreateDeleteRun(RnD_Class.FormInput form)
        {
            try
            {
                return Json(RnD.CustomerClassRun(form));
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult CustomerClassUpdate()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult CustomerClassDroplist()
        {
            try
            {
                return Json(RnD.CustomerClassDroplist());
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult CustomerClassUpdateRun()
        {
            List<object> objectList = new List<object>();
            try
            {
                HttpPostedFileBase file = Request.Files[0];
                string filePath = GetFilePath("Upload", file.FileName);
                Stream inputStream = file.InputStream;
                file.SaveAs(filePath);
                objectList.Add(RnD.CustomerClassUpdateRun(filePath));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                objectList.Add(ex.Message.ToString());
                return Json(objectList);
            }
        }

        public ActionResult CustomerClassList()
        {
            return View();
        }
       
        [HttpPost]
        public JsonResult CustomerClassListData()
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "CustomerClassList_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(RnD.CustomerClassList(filePath));
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult CustomerClassEdit()
        {
            return View();
        }       

        [HttpPost]
        public JsonResult CustomerClassEditSave(RnD_Class.CustomerClass customer)
        {
            try
            {
                List<object> objectList = new List<object>();
                App.ExecuteSql("update CustomerClass set Description = '" + customer.Description + "', SpecialBrownRice = " + (object)Convert.ToInt32(customer.SpecialBrownRice) + ", No6Container = " + (object)Convert.ToInt32(customer.No6Container) + ", TunaSaku = " + (object)Convert.ToInt32(customer.TunaSaku) + ", TunaTatakimi = " + (object)Convert.ToInt32(customer.TunaTatakimi) + ", Eel = " + (object)Convert.ToInt32(customer.Eel) + ", RetailSeaweed = " + (object)Convert.ToInt32(customer.RetailSeaweed) + ", RetailGingerBottle = " + (object)Convert.ToInt32(customer.RetailGingerBottle) + ", RetailGingerCup = " + (object)Convert.ToInt32(customer.RetailGingerCup) + ", MSCTuna = " + (object)Convert.ToInt32(customer.MSCTuna) + " where CustomerClassId = " + (object)customer.Id);
                objectList.Add("Done.");
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult ConnectPassword()
        {
            return View();
        }
       
        [HttpPost]
        public JsonResult ConnectPasswordRecords()
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "ConnectPassword_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(RnD.ConnectPasswordList(filePath));
                objectList.Add(("../Download/" + filename));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult ConnectUpdatePassword()
        {
            return View();
        }
       
        [HttpPost]
        public JsonResult ConnectPasswordUpdate()
        {
            List<object> objectList = new List<object>();
            try
            {
                HttpPostedFileBase file = Request.Files["loadfile"];
                string filePath = GetFilePath("Upload", file.FileName);
                Stream inputStream = file.InputStream;
                file.SaveAs(filePath);
                RnD.ConnectSetDefaultUserPassword(filePath);
                objectList.Add("Done");
                return Json(objectList);
            }
            catch (Exception ex)
            {
                objectList.Add(ex.Message.ToString());
                return Json(objectList);
            }
        }

        public ActionResult ConnectEditPassword()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult ConnectSavePassword(RnD_Class.ConnectUser user)
        {
            try
            {
                //List<object> objectList = new List<object>();
                AFC.ExecuteSql("update Person set password = '" + user.Password + "', WebActiveFlag = " + (user.Webactive == "Yes" ? 1 : 0) + " where PersonId = " + user.Id);
                //objectList.Add("Done.");
                return Json("Done");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult ConnectClickCount()
        {
            return View();
        }
     
        [HttpPost]
        public JsonResult ConnectClickCountData(RnD_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "ConnectClickCount_" + Utilities.GetRandom() + ".csv";
                RnD.ConnectClickCount(GetFilePath("Download", filename), form);
                objectList.Add(("../Download/" + filename));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult Safeway()
        {
            return View();
        }
       
        [HttpPost]
        public JsonResult SafewayData()
        {
            List<object> objectList = new List<object>();
            try
            {
                string str1 = Request.Form["type"].ToString();
                RnD_Class.Safeway safeway = new RnD_Class.Safeway();
                string str2 = str1;
                if (!(str2 == "load_data") && !(str2 == "item_recode"))
                {
                    if (str2 == "purge_by_date")
                    {
                        safeway.Startdate = Convert.ToDateTime(Request.Form["startdate"]);
                        safeway.Enddate = Convert.ToDateTime(Request.Form["enddate"]);
                        AFC.SafewayDeleteByDate(safeway);
                    }
                }
                else
                {
                    HttpPostedFileBase file = Request.Files["loadfile"];
                    string filePath = this.GetFilePath("Upload", file.FileName);
                    Stream inputStream = file.InputStream;
                    file.SaveAs(filePath);
                    if (str1 == "load_data")
                        RnD.SafewayItemInsert(filePath);
                    if (str1 == "item_recode")
                        RnD.SafewayItemRecode(filePath);
                }
                objectList.Add("Done");
                return Json(objectList);
            }
            catch (Exception ex)
            {
                objectList.Add(ex.Message.ToString());
                return Json(objectList);
            }
        }



    }
}