﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Common;
using System.IO;

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

            //对接！！！！！！！！！！！！！！！
            paramDictionary.Add("n_type", "5");//1:骨干河道  2:中小河道	 3:小型农田水利	 4:农村生活污水	 5:其他水利工程	
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

        public ActionResult DelData(string id)
        {
            //对接！！！！！
            string method = "wavenet.fxsw.engin.farm.update";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            //基本信息
            paramDictionary.Add("s_id", id); //id
            paramDictionary.Add("is_delete", "1");//是否删除 1删除
            Dictionary<string, string> fileParams = new Dictionary<string, string>();
            string sss = CookieHelper.GetData(Request, method, paramDictionary, fileParams);
            return Json(sss);
        }

        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetDetail(string id)
        {
            //对接！！！！！
            string method = "wavenet.fxsw.engin.farm.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", id);

            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }

        public JsonResult UpFiles()
        {
            String TempR = "";
            string path = Request.ApplicationPath;//Server.MapPath("/upload/");
            path = Server.MapPath(path += "/upload/");
            try
            {
                if (Request.Files.Count > 0)
                {
                    var f = Request.Files[0];
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    f.SaveAs(path + f.FileName);
                }
                TempR = "OK";
            }
            catch (Exception ex)
            {
                TempR = ex.Message;
            }

            return Json(TempR);//return Json(new { result = true  });
        }

        public JsonResult DeleteFile(string file_ids)
        {
            // 接口
            string method = "wavenet.fxsw.engin.file.del";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("file_ids", file_ids);

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

        //public JsonResult UpdateProjectStatus(string param)
        //{
        //    //转成实体对象
        //    BackboneRiverwayInfo Arr = new BackboneRiverwayInfo();
        //    Arr = JsonHelper.JSONToObject<BackboneRiverwayInfo>(param);

        //    // 接口
        //    string method = "wavenet.fxsw.engin.core.update";

        //    // 接口所需传递的参数
        //    IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
        //    paramDictionary.Add("s_id", Arr.s_id); //id
        //    paramDictionary.Add("s_name", Arr.s_name);
        //    paramDictionary.Add("s_project_no", Arr.s_project_no);
        //    paramDictionary.Add("n_year", Arr.n_year);//年度
        //    paramDictionary.Add("n_pace_status", Arr.n_pace_status);//工程状态 1:工前准备,10:开工,20:完工,30:完工验收,40:决算审批,50:竣工验收,60:工程完结
        //    paramDictionary.Add("s_town", Arr.s_town);//所属镇
        //    paramDictionary.Add("s_address", Arr.s_address);//项目法人
        //    paramDictionary.Add("s_legal_person", Arr.s_legal_person);
        //    paramDictionary.Add("s_unit_design", Arr.s_unit_design);//设计单位
        //    paramDictionary.Add("s_unit_build", Arr.s_unit_build);
        //    paramDictionary.Add("s_unit_supervise", Arr.s_unit_supervise);
        //    paramDictionary.Add("n_reckon_total_amt", Arr.n_reckon_total_amt);//估计总投资
        //    paramDictionary.Add("n_water_area", Arr.n_water_area);//新增水面积
        //    paramDictionary.Add("n_draft", Arr.n_draft);//是否有草图 1.有 0.没有
        //    paramDictionary.Add("s_remark", Arr.s_remark);

        //    //批复工程量
        //    paramDictionary.Add("n_length", Arr.n_length);//长度
        //    paramDictionary.Add("n_land_area", Arr.n_land_area);//土方
        //    paramDictionary.Add("n_protect_hard", Arr.n_protect_hard);//硬质护岸
        //    paramDictionary.Add("n_protect_ecology", Arr.n_protect_ecology);//生态护岸
        //    paramDictionary.Add("n_bridge", Arr.n_bridge);//桥梁
        //    paramDictionary.Add("n_plant_bank", Arr.n_plant_bank);//岸域绿化
        //    paramDictionary.Add("n_plant_slope", Arr.n_plant_slope);//斜坡绿化
        //    paramDictionary.Add("n_plant_depth", Arr.n_plant_depth);//水深绿化
        //    paramDictionary.Add("n_river_count", Arr.n_river_count);//条段
        //    //概算投资
        //    paramDictionary.Add("n_total_invest", Arr.n_total_invest);//总投资
        //    paramDictionary.Add("n_engin_cost", Arr.n_engin_cost);//工程直接费
        //    paramDictionary.Add("n_independent_cost", Arr.n_independent_cost);//独立费用
        //    paramDictionary.Add("n_prep_cost", Arr.n_prep_cost);//预备费
        //    paramDictionary.Add("n_sight_cost", Arr.n_sight_cost);//景观等费用
        //    paramDictionary.Add("n_empty_area", Arr.n_empty_area);//腾地面积
        //    paramDictionary.Add("n_build_cost", Arr.n_build_cost);//建设用地费
        //    //资金配套组成
        //    paramDictionary.Add("n_subsidy_city", Arr.n_subsidy_city);//市补
        //    paramDictionary.Add("n_subsidy_district", Arr.n_subsidy_district);//区配套
        //    paramDictionary.Add("n_subsidy_town", Arr.n_subsidy_town);//镇配套

        //    //完成工程量
        //    paramDictionary.Add("n_complete_length", Arr.n_complete_length);//长度
        //    paramDictionary.Add("n_complete_land_area", Arr.n_complete_land_area);//土方
        //    paramDictionary.Add("n_complete_protect_hard", Arr.n_complete_protect_hard);//硬质护岸
        //    paramDictionary.Add("n_complete_protect_ecology", Arr.n_complete_protect_ecology);//生态护岸
        //    paramDictionary.Add("n_complete_bridge", Arr.n_complete_bridge);//桥梁
        //    paramDictionary.Add("n_complete_plant_bank", Arr.n_complete_plant_bank);//岸域绿化
        //    paramDictionary.Add("n_complete_plant_slope", Arr.n_complete_plant_slope);//斜坡绿化
        //    paramDictionary.Add("n_complete_plant_depth", Arr.n_complete_plant_depth);//水深绿化
        //    paramDictionary.Add("n_complete_river_count", Arr.n_complete_river_count);//条段

        //    paramDictionary.Add("remove_pic_ids", "");
        //    paramDictionary.Add("is_delete", "0");//是否删除 1删除

        //    // 调用接口
        //    string authorization = CookieHelper.GetData(Request, method, paramDictionary);
        //    return Json(authorization);
        //}

        /// <summary>
        /// 文件类型名称转换
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string GetFilesType(string Type)
        {
            switch (Type)
            {
                case "工前准备":
                    Type = "gqzb_file";
                    break;
                case "开工":
                    Type = "kg_file";
                    break;
                case "完工":
                    Type = "wg_file";
                    break;
                case "完工验收":
                    Type = "wgys_file";
                    break;
                case "决算审批":
                    Type = "jssp_file";
                    break;
                case "竣工验收":
                    Type = "jgys_file";
                    break;
                default:
                    Type = "";
                    break;
            }
            return Type;
        }

        public JsonResult SaveOneFile(string s_id, string Files, string Type)
        {
            //文件路径
            string path = Request.ApplicationPath;//Server.MapPath("/upload/");
            path = Server.MapPath(path += "/upload/" + Files);
            Type = GetFilesType(Type);
            Type = Type + "1";
            // 接口
            string method = "wavenet.fxsw.engin.file.manager.report";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", s_id);//id
            paramDictionary.Add("s_person", "小张");//上传人名

            Dictionary<string, string> fileParams = new Dictionary<string, string>();
            fileParams.Add(Type, path);

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary, fileParams);
            return Json(authorization);
        }

    }
}