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
        public JsonResult queryFrInfo(string currentPage, string page_size, string frName)
        {     
            string method = "wavenet.fxsw.engin.legal.person.list.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("page", currentPage);
            paramDictionary.Add("page_size", page_size);
            paramDictionary.Add("s_name", frName);//法人名称
            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
    }
}