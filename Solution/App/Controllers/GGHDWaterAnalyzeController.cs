using App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class GGHDWaterAnalyzeController : Controller
    {
        // GET: GGHDWaterAnalyze
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetData(string t)
        {
            return Json(new { result = "ok" });
        }
        /// <summary>
        /// 工程状态，街镇数据
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public JsonResult GetTownOrStatus(string Type)
        {
            // 接口

            string method = "wavenet.fxsw.dictionary.get";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_type", Type);

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);

            //JavaScriptSerializer js = new JavaScriptSerializer();
            //authorization = js.Serialize(authorization);

            return Json(authorization);
        }

        public JsonResult GetBarData(string town, string grade, string choose, string month, string year)
        {
            // 接口
            string method = "";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("choose", town);
            paramDictionary.Add("month", grade);
            paramDictionary.Add("year", choose);
            paramDictionary.Add("year", month);
            paramDictionary.Add("year", year);

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);

            return Json(authorization);
        }

        public JsonResult GetTableData(string town, string grade, string choose, string month, string year)
        {
            // 接口
            string method = "";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("choose", town);
            paramDictionary.Add("month", grade);
            paramDictionary.Add("year", choose);
            paramDictionary.Add("year", month);
            paramDictionary.Add("year", year);

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);

            return Json(authorization);
        }
    }
}