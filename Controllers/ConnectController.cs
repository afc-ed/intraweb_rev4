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

        public ActionResult Filter() => View();

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
    }
}