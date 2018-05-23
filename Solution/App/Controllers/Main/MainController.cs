using App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers.Main
{
    [Authentication]
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Project()
        {
            ViewData["username"] = "系统管理员";//Session["name"] != null ? Session["name"].ToString() : "";
            return View();
        }

        public ActionResult ProjectOutside()
        {
            ViewData["username"] = "系统管理员";//Session["name"] != null ? Session["name"].ToString() : "";
            return View();
        }

        public ActionResult Hydrology()
        {
            ViewData["username"] = "系统管理员";//Session["name"] != null ? Session["name"].ToString() : "";
            return View();
        }
    }
}