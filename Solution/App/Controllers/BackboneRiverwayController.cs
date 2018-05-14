using App.Common;
using App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace App.Controllers
{
    //[Authentication]
    public class BackboneRiverwayController : Controller
    {
        // GET: BackboneRiverway
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddProject()
        {
            return View();
        }

        public ActionResult LookProject()
        {
            return View();
        }
        public ActionResult FileManage()
        {
            return View();
        }
        public JsonResult GetData()
        {
            return Json(new { result = "ok" });
        }
        /// <summary>
        /// 工程状态，街镇数据
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public JsonResult GetSelect(string Type)
        {
            // 接口
            string method = "wavenet.fxsw.dictionary.get";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_type", Type);

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);

            //JavaScriptSerializer js = new JavaScriptSerializer();
            //authorization = js.Serialize(authorization);

            return Json(authorization);
        }
        
        //public JsonResult GetProjectData(string Page, string Size, string Name, string ProjectID, string startYear, string endYear, string Status, string Town)
        public JsonResult GetProjectData(string Page, string pageSize, string startYear, string endYear, string Status, string Town)
        {
            // 接口
            string method = "wavenet.fxsw.engin.list.get";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", Page);
            paramDictionary.Add("page_size", pageSize);
            paramDictionary.Add("n_type", "1");//1:骨干河道  2:中小河道	 3:小型农田水利	 4:农村生活污水	 5:其他水利工程	
            paramDictionary.Add("s_name", "");//工程名
            paramDictionary.Add("s_project_no", "");//项目编号
            paramDictionary.Add("n_year_begin", startYear);//开始年度
            paramDictionary.Add("n_year_end", endYear);//结束年度
            paramDictionary.Add("n_pace_status", Status);//工程状态1：工前准备 10:开工 20:完工 30:完工验收 40:决算审批 50:竣工验收 60:工程完结	
            paramDictionary.Add("s_town", Town);//城镇

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);

            return Json(authorization);
        }

        public JsonResult GetOneProjectData(string s_id)
        {
            // 接口
            string method = "wavenet.fxsw.engin.core.get";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", s_id);//id

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            
            return Json(authorization);
        }

        public JsonResult GetProjectFileData(string s_id,string s_type)
        {
            // 接口
            string method = "wavenet.fxsw.engin.file.manager.list.get";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", s_id);//id
            paramDictionary.Add("s_type", s_type);//文件类型  工前准备

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);

            return Json(authorization);
        }

        public JsonResult SaveProjectStatus(string s_id, string Status)
        {
            //var result = JsonConvert.DeserializeObject<T_ENGIN_INFO[]>(param);

            //List<T_ENGIN_INFO> list = new List<T_ENGIN_INFO>();

            // 接口
            string method = "wavenet.fxsw.engin.core.update";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", s_id); //id
            paramDictionary.Add("n_pace_status", Status);//工程状态 1:工前准备,10:开工,20:完工,30:完工验收,40:决算审批,50:竣工验收,60:工程完结

            // 调用接口
            //string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            string authorization = "";
            return Json(authorization);
        }


        public JsonResult GetFiles()
        {
            string path = Request.ApplicationPath;//Server.MapPath("/upload/");
            if (Request.Files.Count > 0)
            {
                var f = Request.Files[0];
                path += "/upload/"+f.FileName;
                f.SaveAs(Server.MapPath(path));
            }
            return Json(new { result = true  });
        }
    }
}