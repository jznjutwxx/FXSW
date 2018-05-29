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
            ViewData["username"] = Session["name"] != null ? Session["name"].ToString() : "";
            return View();
        }

        public ActionResult ProjectOutside()
        {
            ViewData["username"] = Session["name"] != null ? Session["name"].ToString() : "";
          
            return View();
        }

        public ActionResult Hydrology()
        {
            ViewData["username"] = Session["name"] != null ? Session["name"].ToString() : "";
            return View();
        }
    }
}