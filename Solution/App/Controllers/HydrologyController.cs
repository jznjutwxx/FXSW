using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Common;
//水文首页
namespace App.Controllers
{
    public class HydrologyController : Controller
    {
        //
        // GET: /Hydrology/
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            return Json(new { result = "ok" });
        }
        //获取河道数
        public JsonResult GetHDnum()
        {
            string hdinfo = "[{'hdinfo':[{ x: 121.394, y: 30.9391, twon: '庄行镇', num: 298 },{ x: 121.47114, y: 30.9331, twon: '南桥镇', num: 290 },{ x: 121.45696, y: 30.8486, twon: '柘林镇', num: 545 },{ x: 121.56765, y: 30.98598, twon: '金汇镇', num: 899 },{ x: 121.55456, y: 30.91892, twon: '青村镇', num: 774 },{ x: 121.65979, y: 30.86712, twon: '海湾镇', num: 91 },{ x: 121.66415, y: 30.94291, twon: '奉城镇', num: 930 },{ x: 121.74158, y: 30.94672, twon: '四团镇', num: 496 }]}]";
            return Json(new { result = hdinfo });
        }
        /// <summary>
        /// 测站统计
        /// </summary>
        /// <returns></returns>
        public JsonResult getCZnum(string s_area)
        {
            string method = "wavenet.fxsw.station.statistics.distribution.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_area", s_area);                                         
            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        /// <summary>
        /// 雨量统计
        /// </summary>
        /// <returns></returns>
        public JsonResult getYLnum(string s_area)
        {
            string method = "wavenet.fxsw.statistics.yltoday.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_area", s_area);
            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);

        }
        /// <summary>
        /// 潮位预报
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public JsonResult getCWYB(string date)
        {
            string method="wavenet.fxsw.tide.prediction.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("date",date);//日期
            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);

            return Json(authorization);
        }
        /// <summary>
        /// 实时报警信息
        /// </summary>
        /// <returns></returns>
        public JsonResult getTimeInfo()
        {
            string method = "wavenet.fxsw.real.time.alarm.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);

            return Json(authorization);
        }
    }
}