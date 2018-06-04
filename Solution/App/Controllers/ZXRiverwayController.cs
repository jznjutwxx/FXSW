using App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    [Authentication]
    public class ZXRiverwayController : Controller
    {
        // GET: ZXRiverway
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddProject()
        {
            return View();
        }
        public JsonResult GetData()
        {
            return Json(new { result = "ok" });
        }

        //public JsonResult GetProjectData(string Page, string Size, string Name, string ProjectID, string startYear, string endYear, string Status, string Town)
        public JsonResult GetProjectData(string Page, string pageSize, string startYear, string endYear, string Status, string Town, string NameNum)
        {
            var token = CookieHelper.ReadCookie("token");//获取当前登录的账户
            var type = CookieHelper.ReadCookie("type");
            // 接口
            string method = "wavenet.fxsw.engin.list.get";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", Page);
            paramDictionary.Add("page_size", pageSize);
            paramDictionary.Add("n_type", "2");//1:骨干河道  2:中小河道	 3:小型农田水利	 4:农村生活污水	 5:其他水利工程	
            paramDictionary.Add("s_name", NameNum);//工程名
            paramDictionary.Add("s_project_no", NameNum);//项目编号
            paramDictionary.Add("n_year_begin", startYear);//开始年度
            paramDictionary.Add("n_year_end", endYear);//结束年度
            paramDictionary.Add("n_pace_status", Status);//工程状态1：工前准备 10:开工 20:完工 30:完工验收 40:决算审批 50:竣工验收 60:工程完结	
            paramDictionary.Add("s_town", Town);//城镇

            paramDictionary.Add("s_account", token);//账号"1132131"
            paramDictionary.Add("s_account_type", type);//unit  person
            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);

            return Json(authorization);
        }

    }
}