﻿using App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    [Authentication]
    public class SHWSProjectStatisticsController : Controller
    {
        // GET: SHWSProjectStatistics
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            return Json(new { result = "ok" });
        }
        
        public JsonResult GetTableData(string startYear, string endYear, string qchoose)
        {
            // 接口
            string method = "wavenet.fxsw.engin.statistics.sewage.get";

            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("s_pf_or_wc", qchoose);//pf 批复 wc 完成
            paramDictionary.Add("n_year_bigen", startYear);//开始年度
            paramDictionary.Add("n_year_end", endYear);//结束年度

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            
            return Json(authorization);
        }      
    }
}