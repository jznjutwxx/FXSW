using App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    [Authentication]
    public class SanitarySewageController : Controller
    {
        // GET: SanitarySewage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddPage()
        {
            return View();
        }
        public ActionResult EditPage()
        {
            return View();
        }
        public ActionResult DetailPage()
        {
            return View();
        }
        public JsonResult GetData()
        {
            return Json(new { result = "ok" });
        }
        //,string town
        public JsonResult GetTabData(string page, string pagesize, string sYear, string eYear, string status, string town)
        {
            string method = "wavenet.fxsw.engin.list.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", page);
            paramDictionary.Add("page_size", pagesize);
            paramDictionary.Add("n_type", "4");
            paramDictionary.Add("n_year_begin", sYear);//开始年度
            paramDictionary.Add("n_year_end", eYear);//结束年度
            paramDictionary.Add("n_pace_status", status);//工程状态1：工前准备 10:开工 20:完工 30:完工验收 40:决算审批 50:竣工验收 60:工程完结	
            paramDictionary.Add("s_town", town);//城镇

            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        public JsonResult GetDetail(string id)
        {
            string method = "wavenet.fxsw.engin.sewage.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", id);

            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
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
        public JsonResult GetLegalPerson()
        { //string page,string pagesize,string name
            string method = "wavenet.fxsw.engin.legal.person.list.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", "1");
            paramDictionary.Add("page_size", "20");
            //   paramDictionary.Add("s_name", "红");//名称

            //调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        public JsonResult GetDesignUnit()
        { //string page,string pagesize,string name
            string method = "wavenet.fxsw.engin.unit.list.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", "1");
            paramDictionary.Add("page_size", "20");
            //  paramDictionary.Add("s_name", "网");//名称";

            //调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
    }
}