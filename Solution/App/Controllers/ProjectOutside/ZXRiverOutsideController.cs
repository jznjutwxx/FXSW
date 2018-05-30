using App.Common;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class ZXRiverOutsideController : Controller
    {
        // GET: ZXRiverOutside
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddProject()
        {
            var token = CookieHelper.ReadCookie("token");//获取当前登录的账户
            var type = CookieHelper.ReadCookie("type");
            ViewBag.type = type;
            ViewBag.token = token;
            return View();
        }
        public ActionResult EditProject()
        {
            var token = CookieHelper.ReadCookie("token");//获取当前登录的账户
            var type = CookieHelper.ReadCookie("type");
            ViewBag.type = type;
            ViewBag.token = token;
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
        public JsonResult GetSelect(string Type)
        {
            // 接口
            string method = "wavenet.fxsw.dictionary.get";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_type", Type);

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }

        public JsonResult GetLegalPerson()
        {
            //string page,string pagesize,string name
            string method = "wavenet.fxsw.engin.legal.person.list.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", "1");
            paramDictionary.Add("page_size", "20");
            paramDictionary.Add("s_name", "");//名称

            //调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }

        public JsonResult GetDesignUnit()
        {
            //string page,string pagesize,string name
            string method = "wavenet.fxsw.engin.unit.list.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", "1");
            paramDictionary.Add("page_size", "20");
            paramDictionary.Add("s_name", "");//名称";

            //调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
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
        public JsonResult GetProjectFileData(string s_id, string s_type)
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
        public JsonResult SaveProjectData(string param, string drawingFiles, string pictureFiles)
        {
            //转成实体对象
            BackboneRiverwayInfo Arr = new BackboneRiverwayInfo();
            Arr = JsonHelper.JSONToObject<BackboneRiverwayInfo>(param);

            // 接口
            string method = "wavenet.fxsw.engin.core.report";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();

            //基本信息
            paramDictionary.Add("s_gg_or_zxx", "zxx"); //gg:骨干  zxx:中小型
            paramDictionary.Add("s_name", Arr.s_name);
            paramDictionary.Add("s_project_no", Arr.s_project_no);
            paramDictionary.Add("n_year", Arr.n_year);//年度
            paramDictionary.Add("n_pace_status", Arr.n_pace_status);//工程状态 1:工前准备,10:开工,20:完工,30:完工验收,40:决算审批,50:竣工验收
            paramDictionary.Add("s_town", Arr.s_town);//所属镇
            paramDictionary.Add("s_address", Arr.s_address);//工程地址
            paramDictionary.Add("s_legal_person", Arr.s_legal_person);//项目法人s_legal_person
            paramDictionary.Add("s_unit_design", Arr.s_unit_design);//设计单位
            paramDictionary.Add("s_unit_build", Arr.s_unit_build);
            paramDictionary.Add("s_unit_supervise", Arr.s_unit_supervise);
            paramDictionary.Add("n_reckon_total_amt", Arr.n_reckon_total_amt);//估计总投资
            paramDictionary.Add("n_water_area", Arr.n_water_area);//新增水面积
            paramDictionary.Add("n_draft", Arr.n_draft);//是否有草图 1.有 0.没有
            paramDictionary.Add("s_remark", Arr.s_remark);

            //批复工程量
            paramDictionary.Add("n_length", Arr.n_length);//长度
            paramDictionary.Add("n_land_area", Arr.n_land_area);//土方
            paramDictionary.Add("n_protect_hard", Arr.n_protect_hard);//硬质护岸
            paramDictionary.Add("n_protect_ecology", Arr.n_protect_ecology);//生态护岸
            paramDictionary.Add("n_bridge", Arr.n_bridge);//桥梁
            paramDictionary.Add("n_plant_bank", Arr.n_plant_bank);//岸域绿化
            paramDictionary.Add("n_plant_slope", Arr.n_plant_slope);//斜坡绿化
            paramDictionary.Add("n_plant_depth", Arr.n_plant_depth);//水深绿化
            paramDictionary.Add("n_river_count", Arr.n_river_count);//条段
            //概算投资
            paramDictionary.Add("n_total_invest", Arr.n_total_invest);//总投资
            paramDictionary.Add("n_engin_cost", Arr.n_engin_cost);//工程直接费
            paramDictionary.Add("n_independent_cost", Arr.n_independent_cost);//独立费用
            paramDictionary.Add("n_prep_cost", Arr.n_prep_cost);//预备费
            paramDictionary.Add("n_sight_cost", Arr.n_sight_cost);//景观等费用
            paramDictionary.Add("n_empty_area", Arr.n_empty_area);//腾地面积
            paramDictionary.Add("n_build_cost", Arr.n_build_cost);//建设用地费
            //资金配套组成
            paramDictionary.Add("n_subsidy_city", Arr.n_subsidy_city);//市补
            paramDictionary.Add("n_subsidy_district", Arr.n_subsidy_district);//区配套
            paramDictionary.Add("n_subsidy_town", Arr.n_subsidy_town);//镇配套

            //完成工程量
            paramDictionary.Add("n_complete_length", Arr.n_complete_length);//长度
            paramDictionary.Add("n_complete_land_area", Arr.n_complete_land_area);//土方
            paramDictionary.Add("n_complete_protect_hard", Arr.n_complete_protect_hard);//硬质护岸
            paramDictionary.Add("n_complete_protect_ecology", Arr.n_complete_protect_ecology);//生态护岸
            paramDictionary.Add("n_complete_bridge", Arr.n_complete_bridge);//桥梁
            paramDictionary.Add("n_complete_plant_bank", Arr.n_complete_plant_bank);//岸域绿化
            paramDictionary.Add("n_complete_plant_slope", Arr.n_complete_plant_slope);//斜坡绿化
            paramDictionary.Add("n_complete_plant_depth", Arr.n_complete_plant_depth);//水深绿化
            paramDictionary.Add("n_complete_river_count", Arr.n_complete_river_count);//条段

            //文件路径
            string path = Request.ApplicationPath;
            path = Server.MapPath(path += "/upload/");

            Dictionary<string, string> fileParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(drawingFiles))
            {
                //文件类型picture_file1 drawing_file1 sketch_file
                string[] Files = drawingFiles.Split(',');
                for (int i = 0; i < Files.Length - 1; i++)
                {
                    fileParams.Add("drawing_file" + (i + 1), path + Files[i]);
                }
            }
            if (!string.IsNullOrEmpty(pictureFiles))
            {
                //文件类型picture_file1 drawing_file1 sketch_file
                string[] Files = pictureFiles.Split(',');

                for (int i = 0; i < Files.Length - 1; i++)
                {
                    fileParams.Add("picture_file" + (i + 1), path + Files[i]);
                }
            }

            // 调用接口"{'EnginCoreReportResponse':{'result':true,'id':''}}"
            string authorization = CookieHelper.GetData(Request, method, paramDictionary, fileParams);

            return Json(authorization);
        }

    }
}