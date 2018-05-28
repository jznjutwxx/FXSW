using App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    //工程子系统-设计单位管理
    public class DesignOrganizationMangerController : Controller
    {
        // GET: DesignOrganizationManger
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult defaultPage()
        {
            //默认页面-查
            return View();
        }
        public ActionResult addPage()
        {  //增
            return View();
        }
        public ActionResult editPage()
        {   //修改
            return View();
        }
        //查询设计单位接口
        public JsonResult queryDesignOrganization(string currentPage, string page_size, string sjdwName)
        {
            string method = "wavenet.fxsw.engin.unit.list.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", currentPage);
            paramDictionary.Add("page_size", page_size);
            paramDictionary.Add("s_name", sjdwName);//设计单位名称
            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        /// <summary>
        /// 获取当前设计单位已选工程
        /// </summary>
        /// <returns></returns>
        public JsonResult getSelectedProject(string s_town, string n_type, string n_year, string s_id, bool isEditPage,string unit_or_person)
        {
            string method = "wavenet.fxsw.engin.choice.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_town", s_town); //传空代表奉贤区
            paramDictionary.Add("n_type", n_type);//工程类型 字典KEY值
            paramDictionary.Add("n_year", n_year);
            paramDictionary.Add("unit_or_person", unit_or_person);// person--法人  //unit--设计单位
            if (isEditPage)
            { //如果是编辑页面，则需要加id
                paramDictionary.Add("s_id", s_id); //新增不需要这个字段，编辑时，id值 用于查询 当前id的设计单位 已经选择了的工程 
            }
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        /// <summary>
        /// 获取待选(未选)工程
        /// </summary>
        /// <returns></returns>
        public JsonResult getUnselectedProject(string s_town, string n_type, string n_year,string unit_or_person)
        {
            string method = "wavenet.fxsw.engin.no.choice.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_town", s_town);
            paramDictionary.Add("n_type", n_type);
            paramDictionary.Add("n_year", n_year);
            paramDictionary.Add("unit_or_person", unit_or_person);
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);

        }
        /// <summary>
        /// 新增设计单位
        /// </summary>
        /// <param name="s_name"></param>
        /// <param name="s_account"></param>
        /// <param name="s_password"></param>
        /// <param name="s_engin"></param>
        /// <returns></returns>
        public JsonResult addDesignOrganization(string s_name,string s_account,string s_password,string s_engin)
        {
            string method = "wavenet.fxsw.engin.unit.create";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_name", s_name);//设计单位名称
            paramDictionary.Add("s_account", s_account);//账号
            paramDictionary.Add("s_password", s_password);//密码
            paramDictionary.Add("s_engin", s_engin);//已选工程 id,id,id
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);

        }
        /// <summary>
        /// 修改设计单位
        /// </summary>
        /// <param name="s_name"></param>
        /// <param name="s_account"></param>
        /// <param name="s_password"></param>
        /// <param name="s_engin"></param>
        /// <returns></returns>
        public JsonResult updateDesignOrganization(string sjdwId,string sjdwmc, string sjdwzh, string sjdwpwd,bool ZHhaveChanged,bool MChaveChanged,string addProjcctIdStr,string deleteProjectIdStr)
        {
            string method = "wavenet.fxsw.engin.unit.update";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", sjdwId);//设计单位ID
            if (MChaveChanged)
            {
                paramDictionary.Add("s_name", sjdwmc);//名称
            }       
            if (ZHhaveChanged)
            {
                paramDictionary.Add("s_account", sjdwzh);//账号
            }
            paramDictionary.Add("s_password", sjdwpwd);//密码
            paramDictionary.Add("s_engin", addProjcctIdStr);//新增工程 id,id,id
            paramDictionary.Add("s_no_engin", deleteProjectIdStr);//删除原有工程 id,id,id
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);

        }
        /// <summary>
        /// 删除设计单位
        /// </summary>
        /// <param name="s_id"></param>
        /// <returns></returns>
        public JsonResult deleteDesignOrganization(string s_id)
        {
            string method = "wavenet.fxsw.engin.unit.update";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", s_id);//设计单位ID
            paramDictionary.Add("n_status", "0");//删除
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);

        }

    }
}