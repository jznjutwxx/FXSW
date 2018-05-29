using App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    //工程子系统-法人管理
    public class LegalPersonMangerController : Controller
    {
        // GET: LegalPersonManger
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
       /// <summary>
       /// 查询设计单位
       /// </summary>
       /// <param name="currentPage"></param>
       /// <param name="page_size"></param>
       /// <param name="frName"></param>
       /// <returns></returns>
        public JsonResult queryFrInfo(string currentPage, string pagesize, string frName)
        {     
            string method = "wavenet.fxsw.engin.legal.person.list.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", currentPage);
            paramDictionary.Add("page_size", pagesize);
            paramDictionary.Add("s_name", frName);//法人名称
            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        /// <summary>
        /// 新增法人
        /// </summary>
        /// <param name="s_name"></param>
        /// <param name="s_account"></param>
        /// <param name="s_password"></param>
        /// <param name="s_engin"></param>
        /// <returns></returns>
        public JsonResult addFrInfo(string s_name, string s_account, string s_password, string s_engin)
        {
            string method = "wavenet.fxsw.engin.legal.person.create";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_name", s_name);//法人姓名
            paramDictionary.Add("s_account", s_account);//账号
            paramDictionary.Add("s_password", s_password);//密码
            paramDictionary.Add("s_engin", s_engin);//如果选择添加工程 就传工程ID
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);

        }
        /// <summary>
        /// 获取未选工程
        /// </summary>
        /// <param name="s_town"></param>
        /// <param name="n_type"></param>
        /// <param name="n_year"></param>
        /// <param name="unit_or_person"></param>
        /// <returns></returns>
        public JsonResult getUnselectedProject(string s_town, string n_type, string n_year, string unit_or_person)
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
       /// 获取已选工程
       /// </summary>
       /// <param name="s_town"></param>
       /// <param name="n_type"></param>
       /// <param name="n_year"></param>
       /// <param name="s_id"></param>
       /// <param name="isEditPage"></param>
       /// <param name="unit_or_person"></param>
       /// <returns></returns>
        public JsonResult getSelectedProject(string s_town, string n_type, string n_year, string s_id, bool isEditPage, string unit_or_person)
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
        /// 修改法人信息
        /// </summary>
        /// <param name="frId"></param>
        /// <param name="frwmc"></param>
        /// <param name="frzh"></param>
        /// <param name="frpwd"></param>
        /// <param name="ZHhaveChanged"></param>
        /// <param name="MChaveChanged"></param>
        /// <param name="addProjcctIdStr"></param>
        /// <param name="deleteProjectIdStr"></param>
        /// <returns></returns>
        public JsonResult updateFrInfo(string frId, string frmc, string frzh, string frpwd, bool ZHhaveChanged, bool MChaveChanged, string addProjcctIdStr, string deleteProjectIdStr)
        {
            string method = "wavenet.fxsw.engin.legal.person.update";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", frId);//法人ID
            if (MChaveChanged)
            {
                paramDictionary.Add("s_name", frmc);//名称
            }
            if (ZHhaveChanged)
            {
                paramDictionary.Add("s_account", frzh);//账号
            }
            paramDictionary.Add("s_password", frpwd);//密码
            paramDictionary.Add("s_engin", addProjcctIdStr);//新增工程 id,id,id
            paramDictionary.Add("s_no_engin", deleteProjectIdStr);//删除的原有工程 id,id,id
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);

        }
        /// <summary>
        /// 删除法人
        /// </summary>
        /// <param name="s_id"></param>
        /// <returns></returns>
        public JsonResult deleteFrInfo(string s_id)
        {
            string method = "wavenet.fxsw.engin.legal.person.update";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", s_id);//法人ID
            paramDictionary.Add("n_status", "0");//删除
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);

        }
    }
}