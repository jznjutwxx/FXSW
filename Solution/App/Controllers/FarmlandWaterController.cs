using App.Common;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class FarmlandWaterController : Controller
    {
        //
        // GET: /FarmlandWater/
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
        public JsonResult GetData()
        {
            return Json(new { result = "ok" });
        }
        //,string town
        public JsonResult GetTabData(string page,string pagesize, string sYear, string eYear, string status, string town)
        {
            string method = "wavenet.fxsw.engin.list.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", page);
            paramDictionary.Add("page_size", pagesize);
            paramDictionary.Add("n_type", "3");
            paramDictionary.Add("n_year_begin", sYear);//开始年度
            paramDictionary.Add("n_year_end", eYear);//结束年度
            paramDictionary.Add("n_pace_status", status);//工程状态1：工前准备 10:开工 20:完工 30:完工验收 40:决算审批 50:竣工验收 60:工程完结	
            paramDictionary.Add("s_town", town);//城镇

            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        public JsonResult GetDetail(string id)
        {
            string method = "wavenet.fxsw.engin.farm.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", id);

            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        public JsonResult GetDictionary(string type)
        {
            string method = "wavenet.fxsw.dictionary.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_type", type);
            //调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        public JsonResult GetLegalPerson()
        { //string page,string pagesize,string name
            string method = "wavenet.fxsw.engin.legal.person.list.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", "1");
            paramDictionary.Add("page_size", "20");
            paramDictionary.Add("s_name", "红");//名称

            //调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        public JsonResult GetDesignUnit()
        { //string page,string pagesize,string name
            string method = "wavenet.fxsw.engin.unit.list.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", "1");
            paramDictionary.Add("page_size", "20");
            paramDictionary.Add("s_name", "网");//名称";

            //调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }


        public const string METHOD = "method";
        public const string TIMESTAMP = "timestamp";
        public const string UTC_OFFSET = "utc_offset";
        public const string FORMAT = "format";
        public const string APP_KEY = "app_key";
        public const string VERSION = "ver";
        public const string SIGN = "sign";
        public const string SIGN_METHOD = "sign_method";
        public const string SESSION = "session";
        public const string LOGIN_TOKEN = "login_token";
        public const string MAC = "mac";

        public ActionResult Add(string param)
        {
            T_ENGIN_INFO arr = new T_ENGIN_INFO();
            arr = JsonHelper.JSONToObject<T_ENGIN_INFO>(param); //转成实体对象

    IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            //系统级参数设置
            //paramDictionary.Add(APP_KEY, "1005");
            //paramDictionary.Add(SESSION, "DEEACA5342B646A7BE08740438C20810");
            //paramDictionary.Add(METHOD, "wavenet.fxsw.engin.farm.report");
            //paramDictionary.Add(FORMAT, "json");
            //paramDictionary.Add(VERSION, "1");
            //paramDictionary.Add(TIMESTAMP, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
            //paramDictionary.Add(UTC_OFFSET, "+00:00");
            //paramDictionary.Add(SIGN_METHOD, "md5");
            //paramDictionary.Add(MAC, "78:D6:F0:00:D8:97");

            var method = "wavenet.fxsw.engin.farm.report";

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
            //paramDictionary.Add("n_water_area", "65595");//新增水面积
            //paramDictionary.Add("n_draft", "1");//是否有草图 1.有 0.没有
            paramDictionary.Add("s_remark", arr.S_REMARK);

            #region
            //批复工程量
            paramDictionary.Add("n_ggsl", arr.N_GGSL);//灌区数量
            paramDictionary.Add("n_ggmj", arr.N_GGMJ);//灌区面积
            paramDictionary.Add("n_bzzs", arr.N_BZZS);//泵站座数
            paramDictionary.Add("n_bztt", arr.N_BZTT);//泵站台套
            paramDictionary.Add("n_dxqd", arr.N_DXQD);//地下渠道
            paramDictionary.Add("n_dc", arr.N_DC);//渡槽
            paramDictionary.Add("n_dxh", arr.N_DXH);//倒虹吸
            paramDictionary.Add("n_cqmq", arr.N_CQMQ);//衬砌明渠
            paramDictionary.Add("n_xfjdl", arr.N_XFJDL);//新翻建道路

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
            paramDictionary.Add("n_complete_ggsl", arr.N_WCGGSL);//灌区数量
            paramDictionary.Add("n_complete_ggmj", arr.N_WCGGMJ);//灌区面积
            paramDictionary.Add("n_complete_bzzs", arr.N_WCBZZS);//泵站座数
            paramDictionary.Add("n_complete_bztt", arr.N_WCBZTT);//泵站台套
            paramDictionary.Add("n_complete_dxqd", arr.N_WCDXQD);//地下渠道
            paramDictionary.Add("n_complete_dc", arr.N_WCDC);//渡槽
            paramDictionary.Add("n_complete_dxh", arr.N_WCDXH);//倒虹吸
            paramDictionary.Add("n_complete_cqmq", arr.N_WCCQMQ);//衬砌明渠
            paramDictionary.Add("n_complete_xfjdl", arr.N_WCXFJDL);//新翻建道路


            Dictionary<string, string> fileParams = new Dictionary<string, string>();
            //FileItem item = new FileItem(FileItemType.FileInfo, @"D:\Paul .jpg");
            //FileItem item2 = new FileItem(FileItemType.FileInfo, @"D:\timg (1).jpg");
            //FileItem item3 = new FileItem(FileItemType.FileInfo, @"D:\timg (2).jpg");
            //FileItem item4 = new FileItem(FileItemType.FileInfo, @"D:\timg (1).jpg");
            //FileItem item5 = new FileItem(FileItemType.FileInfo, @"D:\Paul .jpg");
            //FileItem item6 = new FileItem(FileItemType.FileInfo, @"D:\timg (2).jpg");
            //FileItem item7 = new FileItem(FileItemType.FileInfo, @"D:\Paul .jpg");
            //FileItem item8 = new FileItem(FileItemType.FileInfo, @"D:\timg (1).jpg");
            //FileItem item9 = new FileItem(FileItemType.FileInfo, @"D:\timg (2).jpg");
            fileParams.Add("picture_file", @"D:\Desktop\fff.png");
            fileParams.Add("picture_file2", @"D:\Desktop\fff.png");
            fileParams.Add("picture_file3", @"D:\Desktop\fff.png");
            fileParams.Add("picture_file4", @"D:\Desktop\fff.png");
            fileParams.Add("picture_file5", @"D:\Desktop\fff.png");
            fileParams.Add("picture_file6", @"D:\Desktop\fff.png");
            fileParams.Add("picture_file7", @"D:\Desktop\fff.png");

            //if (item != null)
            //{
            //    fileParams["picture_file"] = item;
            //}

            //if (item2 != null)
            //{
            //    fileParams["picture_file2"] = item2;
            //}

            //if (item3 != null)
            //{
            //    fileParams["picture_file3"] = item3;
            //}

            //if (item4 != null)
            //{
            //    fileParams["picture_file4"] = item4;
            //}

            //if (item5 != null)
            //{
            //    fileParams["picture_file5"] = item5;
            //}

            //if (item6 != null)
            //{
            //    fileParams["picture_file6"] = item6;
            //}

            ////if (item7 != null)
            ////{
            ////    fileParams["drawing_file"] = item7;
            ////}

            //if (item8 != null)
            //{
            //    fileParams["sketch_file"] = item8;
            //}
            #endregion

            // paramDictionary.Add(SIGN, GetSignature(paramDictionary, "77A8799C9E43"));

            //WebUtils webPost = new WebUtils();
            //string url = "http://localhost:5627/Router.aspx";
            // string url = "http://222.66.154.70:8079/Router.aspx";
            //string url = "http://218.1.67.12:8081/Router.aspx";
            string sss = CookieHelper.GetData(Request, method, paramDictionary, fileParams);// webPost.Post(url, Encoding.UTF8, null, paramDictionary, fileParams); 
            return Json(sss);
        }
        public static string GetSignature(IDictionary<string, string> parameters, string secret)
        {
            // 先将参数以其参数名的字典序升序进行排序

            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            // 先将参数以其参数名的字典序升序进行排序

            IEnumerator<KeyValuePair<string, string>> iterator = sortedParams.GetEnumerator();

            // 遍历排序后的字典，将所有参数按"key1value1key2value2"格式拼接在一起

            StringBuilder basestring = new StringBuilder();
            basestring.Append(secret);
            while (iterator.MoveNext())
            {
                string key = iterator.Current.Key;
                string value = iterator.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
#if DEBUG
                    Console.WriteLine("{0}={1}", key, value);
#endif

                    basestring.Append(key).Append(value);
                }
            }
            basestring.Append(secret);

#if DEBUG
            Console.WriteLine(basestring);
#endif


            // 使用MD5对待签名串求签

            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(basestring.ToString()));

            // 将MD5输出的二进制结果转换为小写的十六进制
            //return Str.Bin2Str(bytes).ToLower();

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                string hex = bytes[i].ToString("x");
                if (hex.Length == 1)
                {
                    result.Append("0");
                }
                result.Append(hex);
            }

            return result.ToString().ToUpper();
        }

        public JsonResult GetProjectData(string startYear, string endYear, string Status, string Town)
        {
            // 接口
            string method = "wavenet.fxsw.engin.list.get";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", "1");
            paramDictionary.Add("page_size", "20");
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
    }
}