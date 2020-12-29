using intraweb_rev3.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.Mvc;

namespace intraweb_rev3.Controllers
{
    public class BODController : Controller
    {
        public ActionResult Index() => View();

        [HttpPost]
        public JsonResult LoadMenu()
        {
            try
            {
                return Json(new List<object>()
                {
                  BOD.GetMenuList()
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
                throw Utilities.ErrHandler(ex, "Controller.BODController.GetFilePath()");
            }
        }

        public ActionResult PriceList() => View();

        [HttpPost]
        public JsonResult PriceListData()
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "PriceList_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(BOD.GetPriceList(filePath));
                objectList.Add(("../Download/" + filename));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult CommissionReport() => View();

        [HttpPost]
        public JsonResult CommissionReportData(BOD_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "CommissionReport_" + DateTime.Now.ToString("MMddyy", (IFormatProvider)CultureInfo.CreateSpecificCulture("en-US")) + ".csv";
                string filePath = GetFilePath("Download", filename);
                BOD.CommissionReportData(form, filePath);
                objectList.Add(("../Download/" + filename));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

       
    }
}