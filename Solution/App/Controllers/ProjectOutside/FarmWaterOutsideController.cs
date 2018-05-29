using App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class FarmWaterOutsideController : Controller
    {
        // GET: FarmWaterOutside
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
        public ActionResult FileManage()
        {
            return View();
        }
        public JsonResult GetTabData(string page, string pageSize, string sYear, string eYear, string status, string town, string NameNum)
        {
            var token = CookieHelper.ReadCookie("token");//获取当前登录的账户
            var type = CookieHelper.ReadCookie("type");

            string method = "wavenet.fxsw.engin.list.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", page);
            paramDictionary.Add("page_size", pageSize);
            paramDictionary.Add("n_type", "3");
            paramDictionary.Add("n_year_begin", sYear);//开始年度
            paramDictionary.Add("n_year_end", eYear);//结束年度
            paramDictionary.Add("n_pace_status", status);//工程状态1：工前准备 10:开工 20:完工 30:完工验收 40:决算审批 50:竣工验收 60:工程完结	
            paramDictionary.Add("s_town", town);//城镇
            paramDictionary.Add("s_name", NameNum);//工程名
            paramDictionary.Add("s_project_no", NameNum);//工程编号
            paramDictionary.Add("s_account", token);//账号"1132131"
            paramDictionary.Add("s_account_type", type);//unit  person

            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
    }
}