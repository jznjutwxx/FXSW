using App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    //水文子系统-统计分析-水质取样记录
    public class SZQYJLController : Controller
    {
        // GET: SZQYJL
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getSzqyjl(string town,string task,string level, string time)
        {
            // 水质监测接口
            string method = "wavenet.fxsw.statistics.sampling.point.list.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();    
            paramDictionary.Add("page", "1");
            paramDictionary.Add("page_size", "9999");
            paramDictionary.Add("s_town", "奉城镇"); //"奉城镇"
            paramDictionary.Add("s_task", "重污染");   //"重污染"
            paramDictionary.Add("s_level", "村级"); //"村级"
            paramDictionary.Add("time", "2018-04");
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
    }
}