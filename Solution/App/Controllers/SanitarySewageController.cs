using App.Common;
using App.Models;
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
        public ActionResult FileManage()
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
        public ActionResult Add(string param)
        {
            T_ENGIN_MAIN_SEWAGE arr = new T_ENGIN_MAIN_SEWAGE();
            arr = JsonHelper.JSONToObject<T_ENGIN_MAIN_SEWAGE>(param); //转成实体对象

            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            var method = "wavenet.fxsw.engin.sewage.report";

            #region
            //基本信息
            paramDictionary.Add("s_name", arr.S_NAME);
            paramDictionary.Add("s_project_no", arr.S_PROJECT_NO);
            paramDictionary.Add("n_year", arr.N_YEAR);//年度
            paramDictionary.Add("n_pace_status", arr.N_PACE_STATUS);//工程状态 1:工前准备,10:开工,20:完工,30:完工验收,40:决算审批,50:竣工验收
            paramDictionary.Add("s_town", arr.S_TOWN);//所属镇
            paramDictionary.Add("s_address", arr.S_ADDRESS);
            paramDictionary.Add("s_legal_person", arr.S_LEGAL_PERSON);//项目法人
            paramDictionary.Add("s_unit_design", arr.S_UNIT_DESIGN);//设计单位
            paramDictionary.Add("s_unit_build", arr.S_UNIT_BUILD);
            paramDictionary.Add("s_unit_supervise", arr.S_UNIT_SUPERVISE);
            paramDictionary.Add("n_reckon_total_amt", arr.N_RECKON_TOTAL_AMT);//估计总投资
            paramDictionary.Add("s_remark", arr.S_REMARK);

            //批复工程量
            paramDictionary.Add("n_czsl", arr.N_CZSL);//村组数量
            paramDictionary.Add("n_zhs", arr.N_ZHS);//总户数
            paramDictionary.Add("n_jdclhs", arr.N_JDCLHS);//就地处理户数
            paramDictionary.Add("n_sznghs", arr.N_SZNGHS);//市政纳管户数
            paramDictionary.Add("n_jdclz", arr.N_JDCLZ);//就地处理站
            paramDictionary.Add("n_gwcd", arr.N_GWCD);//管网长度
            paramDictionary.Add("n_xfjhfc", arr.N_XFJHFC);//新翻建化粪池

            //概算投资
            paramDictionary.Add("n_total_invest", arr.N_TOTAL_INVEST);//总投资
            paramDictionary.Add("n_engin_cost", arr.N_ENGIN_COST);//工程直接费
            paramDictionary.Add("n_independent_cost", arr.N_INDEPENDENT_COST);//独立费用
            paramDictionary.Add("n_prep_cost", arr.N_PREP_COST);//预备费
            paramDictionary.Add("n_sight_cost", arr.N_SIGHT_COST);//景观等费用
            //资金配套组成
            paramDictionary.Add("n_subsidy_city", arr.N_SUBSIDY_CITY);//市补
            paramDictionary.Add("n_subsidy_district", arr.N_SUBSIDY_DISTRICT);//区配套
            paramDictionary.Add("n_subsidy_town", arr.N_SUBSIDY_TOWN);//镇配套

            //完成工程量
            paramDictionary.Add("n_complete_czsl", arr.N_WCCZSL);//村组数量
            paramDictionary.Add("n_complete_zhs", arr.N_WCZHS);//总户数
            paramDictionary.Add("n_complete_jdclhs", arr.N_WCJDCLHS);//就地处理户数
            paramDictionary.Add("n_complete_sznghs", arr.N_WCSZNGHS);//市政纳管户数
            paramDictionary.Add("n_complete_jdclz", arr.N_WCJDCLZ);//就地处理站
            paramDictionary.Add("n_complete_gwcd", arr.N_WCGWCD);//管网长度
            paramDictionary.Add("n_complete_xfjhfc", arr.N_WCXFJHFC);//新翻建化粪池

            Dictionary<string, string> fileParams = new Dictionary<string, string>();
            fileParams.Add("picture_file", @"D:\timg (2).jpg");
            fileParams.Add("picture_file2", @"D:\Paul .jpg");
            fileParams.Add("picture_file3", @"D:\timg (1).jpg");
            fileParams.Add("picture_file4", @"D:\Paul .jpg");
            fileParams.Add("picture_file5", @"D:\timg (1).jpg");
            fileParams.Add("picture_file6", @"D:\Paul .jpg");
            fileParams.Add("picture_file7", @"D:\timg (1).jpg");
            #endregion
            
            string sss = CookieHelper.GetData(Request, method, paramDictionary, fileParams);
            return Json(sss);
        }
        public ActionResult DelData(string id)
        {
            string method = "wavenet.fxsw.engin.sewage.update";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            //基本信息
            paramDictionary.Add("s_id", id); //id
            paramDictionary.Add("is_delete", "1");//是否删除 1删除
            Dictionary<string, string> fileParams = new Dictionary<string, string>();

            string sss = CookieHelper.GetData(Request, method, paramDictionary, fileParams);
            return Json(sss);
        }

        public ActionResult Edit(string param)
        {
            T_ENGIN_MAIN_SEWAGE arr = new T_ENGIN_MAIN_SEWAGE();
            arr = JsonHelper.JSONToObject<T_ENGIN_MAIN_SEWAGE>(param); //转成实体对象

            string method = "wavenet.fxsw.engin.sewage.update";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            //基本信息
            paramDictionary.Add("s_id", arr.S_ID); //id

            #region
            //基本信息
            paramDictionary.Add("s_name", arr.S_NAME);
            paramDictionary.Add("s_project_no", arr.S_PROJECT_NO);
            paramDictionary.Add("n_year", arr.N_YEAR);//年度
            paramDictionary.Add("n_pace_status", arr.N_PACE_STATUS);//工程状态 1:工前准备,10:开工,20:完工,30:完工验收,40:决算审批,50:竣工验收
            paramDictionary.Add("s_town", arr.S_TOWN);//所属镇
            paramDictionary.Add("s_address", arr.S_ADDRESS);
            paramDictionary.Add("s_legal_person", arr.S_LEGAL_PERSON);//项目法人
            paramDictionary.Add("s_unit_design", arr.S_UNIT_DESIGN);//设计单位
            paramDictionary.Add("s_unit_build", arr.S_UNIT_BUILD);
            paramDictionary.Add("s_unit_supervise", arr.S_UNIT_SUPERVISE);
            paramDictionary.Add("n_reckon_total_amt", arr.N_RECKON_TOTAL_AMT);//估计总投资
            paramDictionary.Add("s_remark", arr.S_REMARK);

            //批复工程量
            paramDictionary.Add("n_czsl", arr.N_CZSL);//村组数量
            paramDictionary.Add("n_zhs", arr.N_ZHS);//总户数
            paramDictionary.Add("n_jdclhs", arr.N_JDCLHS);//就地处理户数
            paramDictionary.Add("n_sznghs", arr.N_SZNGHS);//市政纳管户数
            paramDictionary.Add("n_jdclz", arr.N_JDCLZ);//就地处理站
            paramDictionary.Add("n_gwcd", arr.N_GWCD);//管网长度
            paramDictionary.Add("n_xfjhfc", arr.N_XFJHFC);//新翻建化粪池

            //概算投资
            paramDictionary.Add("n_total_invest", arr.N_TOTAL_INVEST);//总投资
            paramDictionary.Add("n_engin_cost", arr.N_ENGIN_COST);//工程直接费
            paramDictionary.Add("n_independent_cost", arr.N_INDEPENDENT_COST);//独立费用
            paramDictionary.Add("n_prep_cost", arr.N_PREP_COST);//预备费
            paramDictionary.Add("n_sight_cost", arr.N_SIGHT_COST);//景观等费用
            //资金配套组成
            paramDictionary.Add("n_subsidy_city", arr.N_SUBSIDY_CITY);//市补
            paramDictionary.Add("n_subsidy_district", arr.N_SUBSIDY_DISTRICT);//区配套
            paramDictionary.Add("n_subsidy_town", arr.N_SUBSIDY_TOWN);//镇配套

            //完成工程量
            paramDictionary.Add("n_complete_czsl", arr.N_WCCZSL);//村组数量
            paramDictionary.Add("n_complete_zhs", arr.N_WCZHS);//总户数
            paramDictionary.Add("n_complete_jdclhs", arr.N_WCJDCLHS);//就地处理户数
            paramDictionary.Add("n_complete_sznghs", arr.N_WCSZNGHS);//市政纳管户数
            paramDictionary.Add("n_complete_jdclz", arr.N_WCJDCLZ);//就地处理站
            paramDictionary.Add("n_complete_gwcd", arr.N_WCGWCD);//管网长度
            paramDictionary.Add("n_complete_xfjhfc", arr.N_WCXFJHFC);//新翻建化粪池

            Dictionary<string, string> fileParams = new Dictionary<string, string>();
            fileParams.Add("picture_file", @"D:\timg (2).jpg");
            fileParams.Add("picture_file2", @"D:\Paul .jpg");
            fileParams.Add("picture_file3", @"D:\timg (1).jpg");
            fileParams.Add("picture_file4", @"D:\Paul .jpg");
            fileParams.Add("picture_file5", @"D:\timg (1).jpg");
            fileParams.Add("picture_file6", @"D:\Paul .jpg");
            fileParams.Add("picture_file7", @"D:\timg (1).jpg");
            #endregion

            string sss = CookieHelper.GetData(Request, method, paramDictionary, fileParams);
            // webPost.Post(url, Encoding.UTF8, null, paramDictionary, fileParams); 
            return Json(sss);
        }
    }
}