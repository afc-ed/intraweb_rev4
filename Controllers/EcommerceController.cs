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
                return this.Json((object)new List<object>()
                {
                    Ecommerce.MenuList()
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
                throw Utilities.ErrHandler(ex, "Controller.EcommerceController.GetFilePath()");
            }
        }

        public ActionResult PriceList() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult PriceListData(Ecommerce_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "PriceList" + form.Type + "_" + Utilities.GetRandom() + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Ecommerce.PriceList(filePath, form));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult ItemByClass() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult CustomerClassIds()
        {
            try
            {
                return this.Json((object)new List<object>()
                {
                  Ecommerce.CustomerClassIds()
                });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
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
                    string filePath = this.GetFilePath("Download", filename);
                    objectList.Add(Ecommerce.GetItemByClass(filePath, form));
                }
                else
                {
                    filename = "AllItemsByCustomerClasses.csv";
                    Ecommerce.GetAllItemsByCustomerClasses(this.GetFilePath("Download", filename));
                }
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult ClassByItem() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult ProductIds()
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Ecommerce.ProductIds()
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult ClassByItemData(Ecommerce_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "CustomerClassByItem_" + form.Product.Split('|')[1] + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Ecommerce.GetClassByItem(filePath, form));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult Maintenance() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult MaintenanceMenu()
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Ecommerce.MaintenanceMenu()
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult MaintenanceRun(Ecommerce_Class.FormInput form)
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          (object) Ecommerce.MaintenanceRun(form)
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult RMAccess() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult RMAccessDroplist()
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Ecommerce.GetRegions(),
          Ecommerce.GetRMLogins()
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult UpdateRMAccess(Ecommerce_Class.FormInput form)
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          (object) Ecommerce.RMAccessUpdate(form)
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult ItemRestriction() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult ItemRestrictionRun()
        {
            List<object> objectList = new List<object>();
            try
            {
                HttpPostedFileBase file = this.Request.Files[0];
                string filePath = this.GetFilePath("Upload", file.FileName);
                Stream inputStream = file.InputStream;
                file.SaveAs(filePath);
                objectList.Add((object)Ecommerce.ItemRestrictionUpdate(filePath));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                objectList.Add((object)ex.Message.ToString());
                return this.Json((object)objectList);
            }
        }

        public ActionResult Portals() => (ActionResult)this.View();

        public ActionResult Analytics() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult AnalyticsRun(Ecommerce_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = form.Type + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Ecommerce.AnalyticsRun(form, filePath));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult ItemStatus() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult ItemStatusData()
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "ItemStatus_" + (object)Utilities.GetRandom() + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Ecommerce.ItemResetStatus(filePath));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult ItemStatusEdit() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult ItemStatusSave(Ecommerce_Class.Item item)
        {
            try
            {
                Ecommerce_DB.ItemResetStatusUpdate("update", item);
                return this.Json((object)"Ok");
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        
    }
}