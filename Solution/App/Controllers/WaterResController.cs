using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    //水资源首页
    public class WaterResController : Controller
    {
        // GET: WaterRes
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            return Json(new { result = "ok" });
        }
    }
}