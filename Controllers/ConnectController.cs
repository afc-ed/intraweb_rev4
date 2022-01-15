using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using intraweb_rev3.Models;

namespace intraweb_rev3.Controllers
{
    public class ConnectController : Controller
    {
        public ActionResult Memo() => View();

        [HttpPost]
        public JsonResult MemoGrid()
        {
            try
            {
                List<object> objectList = new List<object>();
                objectList.Add(Connect.MemoGrid());
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult MemoNew() => View();

        [HttpPost]
        public JsonResult MemoCreate(Connect_Class.Memo memo)
        {
            try
            {
                List<object> objectList = new List<object>();
                objectList.Add(Connect_DB.MemoUpdate("create", memo));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }
        public ActionResult MemoDetail() => View();

        [HttpPost]
        public JsonResult MemoDetailGetData(Connect_Class.Memo memo)
        {
            try
            {
                List<object> objectList = new List<object>();
                objectList.Add(Connect.MemoDetailGetData(memo));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult MemoSave(Connect_Class.Memo memo)
        {
            try
            {
                Connect_DB.MemoUpdate("save", memo);
                return Json("Done");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult MemoDelete(Connect_Class.Memo memo)
        {
            try
            {
                Connect_DB.MemoUpdate("delete", memo);
                return Json("Done");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult Filter() => View();

        public ActionResult Region() => View();
        public ActionResult Storegroup() => View();
        public ActionResult State() => View();

        public ActionResult Preview() => View();

        [HttpPost]
        public JsonResult FilterOptions(Connect_Class.Filter filter)
        {
            try
            {
                List<object> objectList = new List<object>();
                objectList.Add(Connect.FilterOptions(filter));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult FilterUpdate(Connect_Class.Filter filter)
        {
            try
            {
                Connect_DB.FilterUpdate(filter);
                return Json("Done");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }


        public ActionResult Announcement() => View();

        [HttpPost]
        public JsonResult AnnouncementGrid()
        {
            try
            {
                List<object> objectList = new List<object>();
                objectList.Add(Connect.AnnouncementGrid());
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult AnnouncementNew() => View();

        [HttpPost]
        public JsonResult AnnouncementCreate(Connect_Class.Announcement announcement)
        {
            try
            {
                List<object> objectList = new List<object>();
                objectList.Add(Connect_DB.AnnouncementUpdate("create", announcement));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }
        public ActionResult AnnouncementDetail() => View();

        [HttpPost]
        public JsonResult AnnouncementDetailGetData(Connect_Class.Announcement announcement)
        {
            try
            {
                List<object> objectList = new List<object>();
                objectList.Add(Connect.AnnouncementDetailGetData(announcement));
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult AnnouncementSave(Connect_Class.Announcement announcement)
        {
            try
            {
                Connect_DB.AnnouncementUpdate("save", announcement);
                return Json("Done");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult AnnouncementDelete(Connect_Class.Announcement announcement)
        {
            try
            {
                Connect_DB.AnnouncementUpdate("delete", announcement);
                return Json("Done");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }




    }
}