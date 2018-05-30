using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class SamplingSZStatisticsController : Controller
    {
        // GET: SamplingSZStatistics
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