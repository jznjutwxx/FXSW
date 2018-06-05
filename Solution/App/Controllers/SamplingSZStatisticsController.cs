using App.Common;
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
        public JsonResult GetDictionary(string type)
        {
            string method = "wavenet.fxsw.dictionary.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_type", type);
            //调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        public JsonResult GetPointSite(string town, string task)
        {
            string method = "wavenet.fxsw.statistics.single.point.site";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_town", town);//所属镇
            paramDictionary.Add("s_task", task);//清洁水 进水 水功能 来水 劣5类 其他 重污染 黑臭
            //调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        public JsonResult GetData(string zdname, string type, string date)
        {
            // 接口
            string method = "wavenet.fxsw.statistics.single.point.chart";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("zdname", zdname);
            paramDictionary.Add("type", type);
            paramDictionary.Add("date", date);

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        public JsonResult GetLineData(string zdname, string type, string date)
        {
            // 接口
            string method = "wavenet.fxsw.statistics.single.point.chart";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("zdname", zdname);
            paramDictionary.Add("type", type);
            paramDictionary.Add("date", date);

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }

        public JsonResult GetLinedata(string zdname, string type, string date)
        {
            // 接口
            string method = "wavenet.fxsw.statistics.single.point.chart";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();

            paramDictionary.Add("name", "四平新路金路港桥");//站名
            paramDictionary.Add("type", "逐年");//逐月 逐年
                                              //if (type == "逐年")
                                              //{
                                              //    paramDictionary.Add("begin_date", "2014-01-01");//逐年 开始日期
                                              //    paramDictionary.Add("end_date", "2019-01-01");//逐年 结束日期
                                              //} else if (type=="逐月") {
                                              //    paramDictionary.Add("date", "2016-01-01");//逐月 日期
                                              //}

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
    }
}