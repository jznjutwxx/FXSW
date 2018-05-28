using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Common;
using System.IO;
using App.Models;

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

        //public JsonResult GetProjectData(string Page, string Size, string Name, string ProjectID, string startYear, string endYear, string Status, string Town)
        public JsonResult GetProjectData(string Page, string pageSize, string startYear, string endYear, string Status, string Town, string NameNum)
        {
            // 接口
            string method = "wavenet.fxsw.engin.list.get";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", Page);
            paramDictionary.Add("page_size", pageSize);
            paramDictionary.Add("n_type", "5");//1:骨干河道  2:中小河道	 3:小型农田水利	 4:农村生活污水	 5:其他水利工程	
            paramDictionary.Add("s_name", NameNum);//工程名
            paramDictionary.Add("s_project_no", NameNum);//工程编号
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
            string method = "wavenet.fxsw.engin.other.get";

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

        public JsonResult UpdateProjectStatus(string param)
        {
            //转成实体对象
            OtherRiverwayInfo Arr = new OtherRiverwayInfo();
            Arr = JsonHelper.JSONToObject<OtherRiverwayInfo>(param);

            // 接口
            string method = "wavenet.fxsw.engin.other.update";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", Arr.s_id); //id
            paramDictionary.Add("s_name", Arr.s_name);
            paramDictionary.Add("s_project_no", Arr.s_project_no);
            paramDictionary.Add("n_year", Arr.n_year);//年度
            paramDictionary.Add("n_pace_status", Arr.n_pace_status);//工程状态 1:工前准备,10:开工,20:完工,30:完工验收,40:决算审批,50:竣工验收,60:工程完结
            paramDictionary.Add("s_town", Arr.s_town);//所属镇
            paramDictionary.Add("s_address", Arr.s_address);//项目法人
            paramDictionary.Add("s_legal_person", Arr.s_legal_person);
            paramDictionary.Add("s_unit_design", Arr.s_unit_design);//设计单位
            paramDictionary.Add("s_unit_build", Arr.s_unit_build);
            paramDictionary.Add("s_unit_supervise", Arr.s_unit_supervise);
            paramDictionary.Add("n_reckon_total_amt", Arr.n_reckon_total_amt);//估计总投资
            paramDictionary.Add("n_water_area", Arr.n_water_area);//新增水面积
            paramDictionary.Add("n_draft", Arr.n_draft);//是否有草图 1.有 0.没有
            paramDictionary.Add("s_remark", Arr.s_remark);


            //批复工程量
            paramDictionary.Add("s_content", Arr.s_content);//批复工程内容
            paramDictionary.Add("s_gkpf", Arr.s_gkpf);//工可批复
            paramDictionary.Add("s_cspf", Arr.s_cspf);//初设批复
            paramDictionary.Add("s_zjpw", Arr.s_zjpw);//资金批文

            //概算投资
            paramDictionary.Add("n_total_invest", Arr.n_total_invest);//总投资
            paramDictionary.Add("n_engin_cost", Arr.n_engin_cost);//工程直接费
            paramDictionary.Add("n_independent_cost", Arr.n_independent_cost);//独立费用
            paramDictionary.Add("n_prep_cost", Arr.n_prep_cost);//预备费
            paramDictionary.Add("n_sight_cost", Arr.n_sight_cost);//景观等费用
            paramDictionary.Add("n_build_cost", Arr.n_build_cost);//建设用地费

            //资金配套组成
            paramDictionary.Add("n_subsidy_city", Arr.n_subsidy_city);//市补
            paramDictionary.Add("n_subsidy_district", Arr.n_subsidy_district);//区配套
            paramDictionary.Add("n_subsidy_town", Arr.n_subsidy_town);//镇配套

            //完成工程量
            paramDictionary.Add("s_complete_content", Arr.s_complete_content);//完成工程量

            paramDictionary.Add("remove_pic_ids", "");
            paramDictionary.Add("is_delete", "0");//是否删除 1删除

            // 调用接口
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

        public JsonResult DeleteFiles(string filename)
        {
            String TempR = "";
            string path = Request.ApplicationPath;//Server.MapPath("/upload/");
            path = Server.MapPath(path += "/upload/" + filename);
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                TempR = "OK";
            }
            catch (Exception ex)
            {
                TempR = ex.Message;
            }

            return Json(TempR);//return Json(new { result = true  });
        }

        public JsonResult SaveProjectData(string param, string drawingFiles, string pictureFiles)
        {
            //转成实体对象
            OtherRiverwayInfo Arr = new OtherRiverwayInfo();
            Arr = JsonHelper.JSONToObject<OtherRiverwayInfo>(param);

            // 接口
            string method = "wavenet.fxsw.engin.other.report";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            //基本信息
            paramDictionary.Add("s_name", Arr.s_name);
            paramDictionary.Add("s_project_no", Arr.s_project_no);
            paramDictionary.Add("n_year", Arr.n_year);//年度
            paramDictionary.Add("n_pace_status", Arr.n_pace_status);//工程状态 1:工前准备,10:开工,20:完工,30:完工验收,40:决算审批,50:竣工验收
            paramDictionary.Add("s_town", Arr.s_town);//所属镇
            paramDictionary.Add("s_address", Arr.s_address);//项目法人s_legal_person
            paramDictionary.Add("s_legal_person", Arr.s_legal_person);//工程地址
            paramDictionary.Add("s_unit_design", Arr.s_unit_design);//设计单位
            paramDictionary.Add("s_unit_build", Arr.s_unit_build);
            paramDictionary.Add("s_unit_supervise", Arr.s_unit_supervise);
            paramDictionary.Add("n_reckon_total_amt", Arr.n_reckon_total_amt);//估计总投资
            paramDictionary.Add("n_water_area", Arr.n_water_area);//新增水面积
            paramDictionary.Add("n_draft", Arr.n_draft);//是否有草图 1.有 0.没有
            paramDictionary.Add("s_remark", Arr.s_remark);
            paramDictionary.Add("s_person", "小张");//上传图片人

            //批复工程量
            paramDictionary.Add("s_content", Arr.s_content);//批复工程内容
            paramDictionary.Add("s_gkpf", Arr.s_gkpf);//工可批复
            paramDictionary.Add("s_cspf", Arr.s_cspf);//初设批复
            paramDictionary.Add("s_zjpw", Arr.s_zjpw);//资金批文

            //概算投资
            paramDictionary.Add("n_total_invest", Arr.n_total_invest);//总投资
            paramDictionary.Add("n_engin_cost", Arr.n_engin_cost);//工程直接费
            paramDictionary.Add("n_independent_cost", Arr.n_independent_cost);//独立费用
            paramDictionary.Add("n_prep_cost", Arr.n_prep_cost);//预备费
            paramDictionary.Add("n_sight_cost", Arr.n_sight_cost);//景观等费用
            paramDictionary.Add("n_build_cost", Arr.n_build_cost);//建设用地费

            //资金配套组成
            paramDictionary.Add("n_subsidy_city", Arr.n_subsidy_city);//市补
            paramDictionary.Add("n_subsidy_district", Arr.n_subsidy_district);//区配套
            paramDictionary.Add("n_subsidy_town", Arr.n_subsidy_town);//镇配套

            //完成工程量
            paramDictionary.Add("s_complete_content", Arr.s_complete_content);//完成工程量

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

        public JsonResult UpdateProjectData(string param, string drawingFiles, string pictureFiles)
        {
            //转成实体对象
            OtherRiverwayInfo Arr = new OtherRiverwayInfo();
            Arr = JsonHelper.JSONToObject<OtherRiverwayInfo>(param);

            // 接口
            string method = "wavenet.fxsw.engin.other.update";

            // 接口所需传递的参数
            #region
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", Arr.s_id); //id
            paramDictionary.Add("s_name", Arr.s_name);
            paramDictionary.Add("s_project_no", Arr.s_project_no);
            paramDictionary.Add("n_year", Arr.n_year);//年度
            paramDictionary.Add("n_pace_status", Arr.n_pace_status);//工程状态 1:工前准备,10:开工,20:完工,30:完工验收,40:决算审批,50:竣工验收
            paramDictionary.Add("s_town", Arr.s_town);//所属镇
            paramDictionary.Add("s_address", Arr.s_address);//项目法人s_legal_person
            paramDictionary.Add("s_legal_person", Arr.s_legal_person);//工程地址
            paramDictionary.Add("s_unit_design", Arr.s_unit_design);//设计单位
            paramDictionary.Add("s_unit_build", Arr.s_unit_build);
            paramDictionary.Add("s_unit_supervise", Arr.s_unit_supervise);
            paramDictionary.Add("n_reckon_total_amt", Arr.n_reckon_total_amt);//估计总投资
            paramDictionary.Add("n_water_area", Arr.n_water_area);//新增水面积
            paramDictionary.Add("n_draft", Arr.n_draft);//是否有草图 1.有 0.没有
            paramDictionary.Add("s_remark", Arr.s_remark);
            paramDictionary.Add("s_person", "小张");//上传图片人

            //批复工程量
            paramDictionary.Add("s_content", Arr.s_content);//批复工程内容
            paramDictionary.Add("s_gkpf", Arr.s_gkpf);//工可批复
            paramDictionary.Add("s_cspf", Arr.s_cspf);//初设批复
            paramDictionary.Add("s_zjpw", Arr.s_zjpw);//资金批文

            //概算投资
            paramDictionary.Add("n_total_invest", Arr.n_total_invest);//总投资
            paramDictionary.Add("n_engin_cost", Arr.n_engin_cost);//工程直接费
            paramDictionary.Add("n_independent_cost", Arr.n_independent_cost);//独立费用
            paramDictionary.Add("n_prep_cost", Arr.n_prep_cost);//预备费
            paramDictionary.Add("n_sight_cost", Arr.n_sight_cost);//景观等费用
            paramDictionary.Add("n_build_cost", Arr.n_build_cost);//建设用地费

            //资金配套组成
            paramDictionary.Add("n_subsidy_city", Arr.n_subsidy_city);//市补
            paramDictionary.Add("n_subsidy_district", Arr.n_subsidy_district);//区配套
            paramDictionary.Add("n_subsidy_town", Arr.n_subsidy_town);//镇配套

            //完成工程量
            paramDictionary.Add("s_complete_content", Arr.s_complete_content);//完成工程量

            paramDictionary.Add("is_delete", "0");//是否删除 1删除
            paramDictionary.Add("remove_pic_ids", Arr.remove_pic_ids);
            #endregion
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
            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary, fileParams);
            return Json(authorization);
        }

        public ActionResult DeleteProjectData(string s_id)
        {
            string method = "wavenet.fxsw.engin.other.update";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            //基本信息
            paramDictionary.Add("s_id", s_id); //id
            paramDictionary.Add("is_delete", "1");//是否删除 1删除

            Dictionary<string, string> fileParams = new Dictionary<string, string>();

            string authorization = CookieHelper.GetData(Request, method, paramDictionary, fileParams);
            return Json(authorization);
        }
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

    }
}