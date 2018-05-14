using App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//水文子系统-综合监测
namespace App.Controllers
{
    [Authentication]
    public class ZHJCController : Controller
    {
        // GET: ZHJC
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 自动监测点-水质监测项目接口
        /// </summary>
        /// <param name="zdzt"> 站点状态</param>
        /// <param name="twons">所选街镇</param>
        /// <returns></returns>
        public JsonResult getSZJCdata(string[] zdzt, string twons)
        {
            // 水质监测接口
            string method = "wavenet.fxsw.station.info.get";        
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            foreach (string item in zdzt)
            {
                if (item == "正常")
                {
                    paramDictionary.Add("is_normal", "1");//正常
                }
                if (item == "设备异常")
                {
                    paramDictionary.Add("is_abnormality", "1");//设备异常
                }
            }     
            paramDictionary.Add("area", twons);//所属街镇

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);

            return Json(authorization);

        }
        /// <summary>
        /// 自动监测点-其他监测项目接口(雨量、水位、流量、风速风向)
        /// </summary>
        /// <returns></returns>
        public JsonResult getOtherJCdata(string[] zdzt, string twons,string jcxm)
        {
            string method = "wavenet.fxsw.station.distribution.info.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            foreach (string item in zdzt)
            {
                if (item == "正常")
                {
                    paramDictionary.Add("is_normal", "1");//正常
                }
                if (item == "设备异常")
                {
                    paramDictionary.Add("is_abnormality", "1");//设备异常
                }
            }
            paramDictionary.Add("area", twons);//所属街镇
            paramDictionary.Add("item", jcxm);//雨量 水位 流量 风速风向
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        /// <summary>
        /// 人工取样点数据
        /// </summary>
        /// <param name="jcrw"></param>
        /// <param name="twons"></param>
        /// <returns></returns>
        public JsonResult getRgqydData(string jcrw,string twons)
        {
            string method = "wavenet.fxsw.sampling.point.list.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", "1");  //请求第几页的数据
            paramDictionary.Add("page_size", "9999"); //当前页的数据条数  9999
            //paramDictionary.Add("s_town", twons); //所选街镇
            //paramDictionary.Add("s_task", jcrw);  //监测任务
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        /// <summary>
        /// 人工取样点取样记录数据
        /// </summary>
        /// <returns></returns>
        public JsonResult getRgqyd_qyjlData(string qyid, string startTime,string endTime)
        {
            string qyjlmethod = "wavenet.fxsw.sampling.report.list.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", "1");
            paramDictionary.Add("page_size", "9999");
            paramDictionary.Add("river_part_id", qyid);
            paramDictionary.Add("time_begin", startTime);
            paramDictionary.Add("time_end", endTime);
            string authorization = CookieHelper.GetData(Request, qyjlmethod, paramDictionary);
            return Json(authorization);
        }
        /// <summary>
        /// 自动监测点-雨量、水位、流量曲线图接口
        /// </summary>
        /// <returns></returns>
        public JsonResult getZdjcdChartData()
        {
            string method = "wavenet.fxsw.ylsw.diagram.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("name", "南桥");
            paramDictionary.Add("time_begin", "2018-5-01 00:00:00");
            paramDictionary.Add("time_end", "2018-5-03 00:00:00");
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
    }
}