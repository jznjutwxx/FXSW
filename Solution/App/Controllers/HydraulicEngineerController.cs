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

        public ActionResult FileManage()
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

        /// <summary>
        /// 查询工程信息
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="pageSize"></param>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <param name="Status"></param>
        /// <param name="Town"></param>
        /// <returns></returns>
        public JsonResult GetProjectData(string Page, string pageSize, string startYear, string endYear, string Status, string Town)
        {
            string method = "wavenet.fxsw.engin.list.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", Page);
            paramDictionary.Add("page_size", pageSize);
            //暂以农田水利数据为例，记得修改！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
            //!!!!!!!!!!!!!!!!!!!!
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

        /// <summary>
        /// 项目法人
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLegalPerson()
        {
            string method = "wavenet.fxsw.engin.legal.person.list.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", "1");
            paramDictionary.Add("page_size", "20");
            paramDictionary.Add("s_name", "");
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }

        /// <summary>
        /// 工程设计单位
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDesignUnit()
        {
            string method = "wavenet.fxsw.engin.unit.list.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", "1");
            paramDictionary.Add("page_size", "20");
            paramDictionary.Add("s_name", "");//名称";
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }

        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetDetail(string id)
        {
            string method = "wavenet.fxsw.engin.farm.get"; //暂以农田水利为例，记得修改！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", id);

            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }

        //public ActionResult Save(string param)
        //{

        //}

    }
}