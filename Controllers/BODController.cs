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
        public ActionResult Index() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult LoadMenu()
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          BOD.GetMenuList()
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        private string GetFilePath(string dir, string filename)
        {
            try
            {
                string str = this.Server.MapPath("~/" + dir);
                Utilities.DeleteOldFiles(str);
                return Path.Combine(str, filename);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Controller.BODController.GetFilePath()");
            }
        }

        public ActionResult PriceList() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult PriceListData()
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "PriceList_" + (object)Utilities.GetRandom() + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(BOD.GetPriceList(filePath));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult CommissionReport() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult CommissionReportData(BOD_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "CommissionReport_" + DateTime.Now.ToString("MMddyy", (IFormatProvider)CultureInfo.CreateSpecificCulture("en-US")) + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                BOD.CommissionReportData(form, filePath);
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

       
    }
}