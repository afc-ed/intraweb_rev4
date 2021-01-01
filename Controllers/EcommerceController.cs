using intraweb_rev3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace intraweb_rev3.Controllers
{
    public class EcommerceController : Controller
    {
        public ActionResult Index() => View();
        
        [HttpPost]
        public JsonResult LoadMenu()
        {
            try
            {
                return Json(new List<object>()
                {
                    Ecommerce.MenuList()
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
                throw Utilities.ErrHandler(ex, "Controller.EcommerceController.GetFilePath()");
            }
        }

        public ActionResult PriceList() => View();

        [HttpPost]
        public JsonResult PriceListData(Ecommerce_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "PriceList" + form.Type + "_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(Ecommerce.PriceList(filePath, form));
                objectList.Add(("../Download/" + filename));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult ItemByClass() => View();

        [HttpPost]
        public JsonResult CustomerClassIds()
        {
            try
            {
                return Json(new List<object>()
                {
                  Ecommerce.CustomerClassIds()
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult ItemByClassData(Ecommerce_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename;
                if (form.Class != "all")
                {
                    filename = "ItemByCustomerClass_" + form.Class.Split('|')[1] + ".csv";
                    string filePath = GetFilePath("Download", filename);
                    objectList.Add(Ecommerce.GetItemByClass(filePath, form));
                }
                else
                {
                    filename = "AllItemsByCustomerClasses.csv";
                    Ecommerce.GetAllItemsByCustomerClasses(GetFilePath("Download", filename));
                }
                objectList.Add(("../Download/" + filename));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult ClassByItem() => View();

        [HttpPost]
        public JsonResult ProductIds()
        {
            try
            {
                return Json(new List<object>()
        {
          Ecommerce.ProductIds()
        });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult ClassByItemData(Ecommerce_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "CustomerClassByItem_" + form.Product.Split('|')[1] + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(Ecommerce.GetClassByItem(filePath, form));
                objectList.Add(("../Download/" + filename));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult Maintenance() => View();

        [HttpPost]
        public JsonResult MaintenanceMenu()
        {
            try
            {
                return Json(new List<object>()
        {
          Ecommerce.MaintenanceMenu()
        });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult MaintenanceRun(Ecommerce_Class.FormInput form)
        {
            try
            {
                return Json(new List<object>()
                {
                   Ecommerce.MaintenanceRun(form)
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult RMAccess() => View();

        [HttpPost]
        public JsonResult RMAccessDroplist()
        {
            try
            {
                return Json(new List<object>()
                {
                  Ecommerce.GetRegions(),
                  Ecommerce.GetRMLogins()
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult UpdateRMAccess(Ecommerce_Class.FormInput form)
        {
            try
            {
                return Json(new List<object>()
                {
                   Ecommerce.RMAccessUpdate(form)
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult ItemRestriction() => View();

        [HttpPost]
        public JsonResult ItemRestrictionRun()
        {
            List<object> objectList = new List<object>();
            try
            {
                HttpPostedFileBase file = Request.Files[0];
                string filePath = GetFilePath("Upload", file.FileName);
                Stream inputStream = file.InputStream;
                file.SaveAs(filePath);
                objectList.Add(Ecommerce.ItemRestrictionUpdate(filePath));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                objectList.Add(ex.Message.ToString());
                return Json(objectList);
            }
        }

        public ActionResult Portals() => View();

        public ActionResult Analytics() => View();

        [HttpPost]
        public JsonResult AnalyticsRun(Ecommerce_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = form.Type + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(Ecommerce.AnalyticsRun(form, filePath));
                objectList.Add(("../Download/" + filename));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult ItemStatus() => View();

        [HttpPost]
        public JsonResult ItemStatusData()
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "ItemStatus_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(Ecommerce.ItemResetStatus(filePath));
                objectList.Add(("../Download/" + filename));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult ItemStatusEdit() => View();

        [HttpPost]
        public JsonResult ItemStatusSave(Ecommerce_Class.Item item)
        {
            try
            {
                Ecommerce_DB.ItemResetStatusUpdate("update", item);
                return Json("Ok");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        
    }
}