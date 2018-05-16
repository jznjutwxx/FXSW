using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Common;

namespace App.Controllers
{
    [Authentication]
    public class HydraulicEngineerController : Controller
    {
        // GET: HydraulicEngineer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 工程状态，街镇数据
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public JsonResult GetSelect(string Type)
        {
            string method = "wavenet.fxsw.dictionary.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_type", Type);
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }

        public JsonResult GetProjectData(string Page, string pageSize, string startYear, string endYear, string Status, string Town)
        {
            string method = "wavenet.fxsw.engin.list.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", Page);
            paramDictionary.Add("page_size", pageSize);
            paramDictionary.Add("n_type", "3");//1:骨干河道  2:中小河道	 3:小型农田水利	 4:农村生活污水	 5:其他水利工程	
            paramDictionary.Add("s_name", "");//工程名
            paramDictionary.Add("s_project_no", "");//项目编号
            paramDictionary.Add("n_year_begin", startYear);//开始年度
            paramDictionary.Add("n_year_end", endYear);//结束年度
            paramDictionary.Add("n_pace_status", Status);//工程状态 1：工前准备 10:开工 20:完工 30:完工验收 40:决算审批 50:竣工验收 60:工程完结	
            paramDictionary.Add("s_town", Town);//城镇
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }


    }
}