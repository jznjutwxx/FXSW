using App.Common;
using App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{

    [Authentication]
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
            var token = CookieHelper.ReadCookie("token");//获取当前登录的账户
            var type = CookieHelper.ReadCookie("type");
            ViewBag.type = type;
            ViewBag.token = token;
            return View();
        }
        public ActionResult EditPage()
        {
            var token = CookieHelper.ReadCookie("token");//获取当前登录的账户
            var type = CookieHelper.ReadCookie("type");
            ViewBag.type = type;
            ViewBag.token = token;
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
        public JsonResult GetTabData(string page,string pageSize, string sYear, string eYear, string status, string town, string NameNum)
        {
            var token = CookieHelper.ReadCookie("token");//获取当前登录的账户
            var type = CookieHelper.ReadCookie("type");
            //调用接口
            string method = "wavenet.fxsw.engin.list.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", page);
            paramDictionary.Add("page_size", pageSize);
            paramDictionary.Add("n_type", "3");
            paramDictionary.Add("n_year_begin", sYear);//开始年度
            paramDictionary.Add("n_year_end", eYear);//结束年度
            paramDictionary.Add("n_pace_status", status);//工程状态1：工前准备 10:开工 20:完工 30:完工验收 40:决算审批 50:竣工验收 60:工程完结	
            paramDictionary.Add("s_town", town);//城镇
            paramDictionary.Add("s_name", NameNum);//工程名
            paramDictionary.Add("s_project_no", NameNum);//工程编号

            paramDictionary.Add("s_account", token);//账号"1132131"
            paramDictionary.Add("s_account_type", type);//unit  person

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
         //   paramDictionary.Add("s_name", "红");//名称

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
          //  paramDictionary.Add("s_name", "网");//名称";

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

        public ActionResult Add(string param, string drawingFiles, string pictureFiles)
        {
            T_ENGIN_INFO arr = new T_ENGIN_INFO();
            arr = JsonHelper.JSONToObject<T_ENGIN_INFO>(param); //转成实体对象

    IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
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
            paramDictionary.Add("n_draft", arr.N_DRAFT);//是否有草图 1.有 0.没有
            paramDictionary.Add("s_remark", arr.S_REMARK);
            paramDictionary.Add("s_person", "小张");//上传图片人

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
            string sss = CookieHelper.GetData(Request, method, paramDictionary, fileParams);
            return Json(sss);
        }
        public ActionResult Edit(string param, string drawingFiles, string pictureFiles)
        {
            T_ENGIN_INFO arr = new T_ENGIN_INFO();
            arr = JsonHelper.JSONToObject<T_ENGIN_INFO>(param); //转成实体对象

            string method = "wavenet.fxsw.engin.farm.update";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            //基本信息
            paramDictionary.Add("s_id", arr.S_ID); //id

            #region
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
            paramDictionary.Add("s_person", "小张");//上传图片人
            paramDictionary.Add("n_draft", arr.N_DRAFT);//是否有草图 1.有 0.没有

            //批复工程量
            paramDictionary.Add("n_ggsl", arr.N_GGSL);//灌区数量
            paramDictionary.Add("n_ggmj", arr.N_GGMJ);//灌区面积
            paramDictionary.Add("n_bzzs", arr.N_BZZS);//泵站座数
            paramDictionary.Add("n_bztt", arr.N_BZTT);//泵站台套
            paramDictionary.Add("n_dxqd", arr.N_DXQD);//地下渠道
            paramDictionary.Add("n_dc", arr.N_DC);//渡槽
            paramDictionary.Add("n_dxh", arr.N_DXH);//倒虹吸
            paramDictionary.Add("n_cqmq", arr.N_CQMQ);//衬砌明渠
            paramDictionary.Add("n_xfjdl", arr.N_WCXFJDL);//新翻建道路

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
            #endregion

            paramDictionary.Add("remove_pic_ids", arr.REMOVE_PIC_IDS);//删除的图片ID
            //paramDictionary.Add("is_delete", "0");//是否删除 1删除

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

            string sss = CookieHelper.GetData(Request, method, paramDictionary, fileParams);
            // webPost.Post(url, Encoding.UTF8, null, paramDictionary, fileParams); 
            return Json(sss);
        }
        public ActionResult DelData(string id)
        {
            string method = "wavenet.fxsw.engin.farm.update";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            //基本信息
            paramDictionary.Add("s_id",id); //id
            paramDictionary.Add("is_delete", "1");//是否删除 1删除
            #region
            //paramDictionary.Add("s_name", arr.S_NAME);
            //paramDictionary.Add("s_project_no", arr.S_PROJECT_NO);
            //paramDictionary.Add("n_year", arr.N_YEAR);//年度
            //paramDictionary.Add("n_pace_status", arr.N_PACE_STATUS);//工程状态 1:工前准备,10:开工,20:完工,30:完工验收,40:决算审批,50:竣工验收
            //paramDictionary.Add("s_town", arr.S_TOWN);//所属镇
            //paramDictionary.Add("s_address", arr.S_ADDRESS);
            //paramDictionary.Add("s_legal_person", arr.S_LEGAL_PERSON);//项目法人
            //paramDictionary.Add("s_unit_design", arr.S_UNIT_DESIGN);//设计单位
            //paramDictionary.Add("s_unit_build", arr.S_UNIT_BUILD);
            //paramDictionary.Add("s_unit_supervise", arr.S_UNIT_SUPERVISE);
            //paramDictionary.Add("n_reckon_total_amt", arr.N_RECKON_TOTAL_AMT);//估计总投资
            //paramDictionary.Add("s_remark", arr.S_REMARK);

            ////批复工程量
            //paramDictionary.Add("n_ggsl", arr.N_GGSL);//灌区数量
            //paramDictionary.Add("n_ggmj", arr.N_GGMJ);//灌区面积
            //paramDictionary.Add("n_bzzs", arr.N_BZZS);//泵站座数
            //paramDictionary.Add("n_bztt", arr.N_BZTT);//泵站台套
            //paramDictionary.Add("n_dxqd", arr.N_DXQD);//地下渠道
            //paramDictionary.Add("n_dc", arr.N_DC);//渡槽
            //paramDictionary.Add("n_dxh", arr.N_DXH);//倒虹吸
            //paramDictionary.Add("n_cqmq", arr.N_CQMQ);//衬砌明渠
            //paramDictionary.Add("n_xfjdl", arr.N_WCXFJDL);//新翻建道路

            ////概算投资
            //paramDictionary.Add("n_total_invest", arr.N_TOTAL_INVEST);//总投资
            //paramDictionary.Add("n_engin_cost", arr.N_ENGIN_COST);//工程直接费
            //paramDictionary.Add("n_independent_cost", arr.N_INDEPENDENT_COST);//独立费用
            //paramDictionary.Add("n_prep_cost", arr.N_PREP_COST);//预备费
            //paramDictionary.Add("n_sight_cost", arr.N_SIGHT_COST);//景观等费用
            ////资金配套组成
            //paramDictionary.Add("n_subsidy_city", arr.N_SUBSIDY_CITY);//市补
            //paramDictionary.Add("n_subsidy_district", arr.N_SUBSIDY_DISTRICT);//区配套
            //paramDictionary.Add("n_subsidy_town", arr.N_SUBSIDY_TOWN);//镇配套

            ////完成工程量
            //paramDictionary.Add("n_complete_ggsl", arr.N_WCGGSL);//灌区数量
            //paramDictionary.Add("n_complete_ggmj", arr.N_WCGGMJ);//灌区面积
            //paramDictionary.Add("n_complete_bzzs", arr.N_WCBZZS);//泵站座数
            //paramDictionary.Add("n_complete_bztt", arr.N_WCBZTT);//泵站台套
            //paramDictionary.Add("n_complete_dxqd", arr.N_WCDXQD);//地下渠道
            //paramDictionary.Add("n_complete_dc", arr.N_WCDC);//渡槽
            //paramDictionary.Add("n_complete_dxh", arr.N_WCDXH);//倒虹吸
            //paramDictionary.Add("n_complete_cqmq", arr.N_WCCQMQ);//衬砌明渠
            //paramDictionary.Add("n_complete_xfjdl", arr.N_WCXFJDL);//新翻建道路

            //// paramDictionary.Add("remove_pic_ids", "3156467F238740B59C6650828698B2A2,BA4D7B0736564E05B51F181F9BB6F9B9");//删除的图片ID
            //paramDictionary.Add("is_delete", "1");//是否删除 1删除

            Dictionary<string, string> fileParams = new Dictionary<string, string>();
            //fileParams.Add("picture_file", @"D:\timg (2).jpg");
            //fileParams.Add("picture_file2", @"D:\Paul .jpg");
            //fileParams.Add("picture_file3", @"D:\timg (1).jpg");
            //fileParams.Add("picture_file4", @"D:\Paul .jpg");
            //fileParams.Add("picture_file5", @"D:\timg (1).jpg");
            //fileParams.Add("picture_file6", @"D:\Paul .jpg");
            //fileParams.Add("picture_file7", @"D:\timg (1).jpg");
            #endregion

            string sss = CookieHelper.GetData(Request, method, paramDictionary, fileParams);
            // webPost.Post(url, Encoding.UTF8, null, paramDictionary, fileParams); 
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
        
        public JsonResult GetProjectFileData(string id, string s_type)
        {
            // 接口
            string method = "wavenet.fxsw.engin.file.manager.list.get";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_id", id);//id
            paramDictionary.Add("s_type", s_type);//文件类型  工前准备

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);

            return Json(authorization);
        }
        public JsonResult SaveProjectStatus(string param)
        {
            //转成实体对象
            T_ENGIN_INFO arr = new T_ENGIN_INFO();
            arr = JsonHelper.JSONToObject<T_ENGIN_INFO>(param);

            // 接口
            string method = "wavenet.fxsw.engin.farm.update";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            #region
            //基本信息   
            paramDictionary.Add("s_id", arr.S_ID); //id
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
            paramDictionary.Add("n_reckon_total_amt", arr.N_RECKON_TOTAL_AMT);
            paramDictionary.Add("s_remark", arr.S_REMARK);

            //批复工程量
            paramDictionary.Add("n_ggsl", arr.N_GGSL);//灌区数量
            paramDictionary.Add("n_ggmj", arr.N_GGMJ);//灌区面积
            paramDictionary.Add("n_bzzs", arr.N_BZZS);//泵站座数
            paramDictionary.Add("n_bztt", arr.N_BZTT);//泵站台套
            paramDictionary.Add("n_dxqd", arr.N_DXQD);//地下渠道
            paramDictionary.Add("n_dc", arr.N_DC);//渡槽
            paramDictionary.Add("n_dxh", arr.N_DXH);//倒虹吸
            paramDictionary.Add("n_cqmq", arr.N_CQMQ);//衬砌明渠
            paramDictionary.Add("n_xfjdl", arr.N_WCXFJDL);//新翻建道路

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

            // paramDictionary.Add("remove_pic_ids", "3156467F238740B59C6650828698B2A2,BA4D7B0736564E05B51F181F9BB6F9B9");//删除的图片ID
            //paramDictionary.Add("is_delete", "0");//是否删除 1删除
            
            #endregion

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
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
        public JsonResult SaveOneFile(string id, string Files, string Type)
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
            paramDictionary.Add("s_id", id);//id
            paramDictionary.Add("s_person", "小张");//上传人名

            Dictionary<string, string> fileParams = new Dictionary<string, string>();
            fileParams.Add(Type, path);

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary, fileParams);
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

        public JsonResult DeleteFilesLocal(string filename)
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