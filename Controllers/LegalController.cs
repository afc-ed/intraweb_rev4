using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using intraweb_rev3.Models;

namespace intraweb_rev3.Controllers
{
    public class LegalController : Controller
    {
        // GET: Legal
        public ActionResult Index() => View();

        [HttpPost]
        public JsonResult LoadMenu()
        {
            try
            {
                return Json(new List<object>()
                {
                  Legal.GetMenuList()
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

       





    }
}