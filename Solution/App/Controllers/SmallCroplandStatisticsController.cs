using App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    [Authentication]
    public class SmallCroplandStatisticsController : Controller
    {
        // GET: SmallCroplandStatistics
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData(string startYear, string endYear, string qchoose)
        {
            // 接口
            string method = "wavenet.fxsw.engin.statistics.farm.get";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
                paramDictionary.Add("s_pf_or_wc", "pf");//pf 批复 wc 完成
                paramDictionary.Add("n_year_bigen", "2015");//开始年度
                paramDictionary.Add("n_year_end", "2019");//结束年度

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        } 
    }
}