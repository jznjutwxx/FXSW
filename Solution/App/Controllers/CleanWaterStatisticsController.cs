using App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class CleanWaterStatisticsController : Controller
    {
        // GET: CleanWaterStatistics
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetData(string t)
        {
            return Json(new { result = "ok" });
        }
        
        public JsonResult GetBarData(string choose, string month, string year)
        {
            // 接口
            string method = "";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("choose", choose);
            paramDictionary.Add("month", month);
            paramDictionary.Add("year", year);

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);

            return Json(authorization);
        }

        public JsonResult GetPieData(string choose, string month, string year)
        {
            // 接口
            string method = "";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("choose", choose);
            paramDictionary.Add("month", month);
            paramDictionary.Add("year", year);

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);

            return Json(authorization);
        }

        public JsonResult GetTableData(string choose, string month, string year)
        {
            // 接口
            string method = "";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("choose", choose);
            paramDictionary.Add("month", month);
            paramDictionary.Add("year", year);

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);

            return Json(authorization);
        }
    }
}