using App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["username"] = "系统管理员";//Session["name"] != null ? Session["name"].ToString() : "";
            return View();
        }

        public ActionResult demo()
        {
            return View();
        }

        public JsonResult GetData()
        {
            return Json(new { result = "ok"});
        }
        
    }
}