using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Common;

namespace App.Controllers
{
    [Authentication]
    //工程总览
    public class ProOverViewController : Controller
    {
        // GET: ProOverView
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData(string page, string pagesize, string sYear, string eYear, 
            string status, string towns, string stype, string proNm, string proNos, string ctFlag)
        {
            string method = "wavenet.fxsw.engin.pandect.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", page);
            paramDictionary.Add("page_size", pagesize);
            paramDictionary.Add("s_town", towns);
            
            paramDictionary.Add("engin_class", status);//工程分类 1.骨干河道 2.中小河道 3.小型农田水利 4.农村生活污水 5.其他水利工程
            paramDictionary.Add("n_year_begin", sYear);
            paramDictionary.Add("n_year_end", eYear);
            paramDictionary.Add("n_type", stype);//需要查询的表单 1.骨干河道 2.中小河道 3.小型农田水利 4.农村生活污水 5.其他水利工程

            paramDictionary.Add("engin_name", proNm);//工程名称 
            paramDictionary.Add("project_nos", proNos);//工程编号
            if(!ctFlag.Equals("2")){    //2代表不限制是否有草图条件，查询所有的工程
                paramDictionary.Add("draft", ctFlag);//0没草图 1有草图
            } 

            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }

        public JsonResult GetMapData(string sYear, string eYear, string status, string towns,
            string proNm, string proNos, string ctFlag)
        {
            string method = "wavenet.fxsw.engin.pandect.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", "1");
            paramDictionary.Add("page_size", "1000");
            paramDictionary.Add("s_town", towns);

            paramDictionary.Add("engin_class", status);//工程分类 1.骨干河道 2.中小河道 3.小型农田水利 4.农村生活污水 5.其他水利工程
            paramDictionary.Add("n_year_begin", sYear);
            paramDictionary.Add("n_year_end", eYear);
            paramDictionary.Add("n_type", status);//需要查询的表单 1.骨干河道 2.中小河道 3.小型农田水利 4.农村生活污水 5.其他水利工程

            paramDictionary.Add("engin_name", proNm);//工程名称 
            paramDictionary.Add("project_nos", proNos);//工程编号
            if(!ctFlag.Equals("2")){    //2代表不限制是否有草图条件，查询所有的工程
                paramDictionary.Add("draft", ctFlag);//0没草图 1有草图
            } 

            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
    }
}