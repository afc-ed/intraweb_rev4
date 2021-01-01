using intraweb_rev3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace intraweb_rev3.Controllers
{
    public class RnDController : Controller
    {
        public ActionResult Index() => View();
        

        [HttpPost]
        public JsonResult LoadMenu()
        {
            try
            {
                return Json(new List<object>()
                {
                    RnD.MenuList()
                });
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

        public ActionResult GPCustomerList() => View();
       
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
                return Json(new List<object>()
                {
                    RnD.CustomerClassRun(form)
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult CustomerClassUpdate() => View();
        
        
        [HttpPost]
        public JsonResult CustomerClassDroplist()
        {
            try
            {
                return Json(new List<object>()
                {
                    RnD.CustomerClassDroplist()
                });
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

        public ActionResult CustomerClassList() => View();
       
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

        public ActionResult CustomerClassEdit() => View();
       
        [HttpPost]
        public JsonResult CustomerClassEditSave(RnD_Class.CustomerClass customer)
        {
            try
            {
                List<object> objectList = new List<object>();
                App.ExecuteSql("update CustomerClass set Description = '" + customer.Description + 
                    "', SpecialBrownRice = " + Convert.ToInt32(customer.SpecialBrownRice) + 
                    ", No6Container = " + Convert.ToInt32(customer.No6Container) + 
                    ", TunaSaku = " + Convert.ToInt32(customer.TunaSaku) + 
                    ", TunaTatakimi = " + Convert.ToInt32(customer.TunaTatakimi) + 
                    ", Eel = " + Convert.ToInt32(customer.Eel) + 
                    ", RetailSeaweed = " + Convert.ToInt32(customer.RetailSeaweed) + 
                    ", RetailGingerBottle = " + Convert.ToInt32(customer.RetailGingerBottle) + 
                    ", RetailGingerCup = " + Convert.ToInt32(customer.RetailGingerCup) + 
                    ", MSCTuna = " + Convert.ToInt32(customer.MSCTuna) + 
                    " where CustomerClassId = " + customer.Id);
                objectList.Add("Done.");
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult ConnectPassword() => View();
       
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

        public ActionResult ConnectUpdatePassword() => View();
        
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

        public ActionResult ConnectEditPassword() => View();
        
        [HttpPost]
        public JsonResult ConnectSavePassword(RnD_Class.ConnectUser user)
        {
            try
            {
                AFC.ExecuteSql("update Person set password = '" + user.Password + 
                    "', WebActiveFlag = " + (user.Webactive == "Yes" ? 1 : 0) + 
                    " where PersonId = " + user.Id);
                return Json("Done");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult ConnectClickCount() => View();
       
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

        public ActionResult Safeway() => View();
        
        [HttpPost]
        public JsonResult SafewayData()
        {
            List<object> objectList = new List<object>();
            try
            {
                string processType = Request.Form["type"].ToString();
                RnD_Class.Safeway safeway = new RnD_Class.Safeway();
                switch (processType)
                {
                    case "load_data": case "item_recode":
                        HttpPostedFileBase file = Request.Files["loadfile"];
                        string filePath = GetFilePath("Upload", file.FileName);
                        Stream inputStream = file.InputStream;
                        file.SaveAs(filePath);
                        if (processType == "load_data")
                            RnD.SafewayItemInsert(filePath);
                        if (processType == "item_recode")
                            RnD.SafewayItemRecode(filePath);
                        break;
                    case "purge_by_date":
                        safeway.Startdate = Convert.ToDateTime(Request.Form["startdate"]);
                        safeway.Enddate = Convert.ToDateTime(Request.Form["enddate"]);
                        AFC.SafewayDeleteByDate(safeway);
                        break;
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