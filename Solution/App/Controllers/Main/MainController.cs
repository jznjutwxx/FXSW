using App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers.Main
{
   
    public class MainController : Controller
    {
        // GET: Main
        #region 子系统选择
        [Authentication]
        public ActionResult Index()
        {
            return View();
        }

        [Authentication]
        public JsonResult GetMenus()
        {
            string method = "wavenet.fxsw.system.menu.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("account", CookieHelper.ReadCookie("token"));
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }

        [Authentication]
        public ActionResult Project(string id)
        {
            ViewData["username"] = Session["name"] != null ? Session["name"].ToString() : "";
            ViewData["id"] = Session["pid"] = Session["pid"] != null ? Session["pid"] : id;
            return View();
        }

        [Authentication]
        public ActionResult Hydrology(string id)
        {
            ViewData["username"] = Session["name"] != null ? Session["name"].ToString() : "";
            ViewData["id"] = Session["hid"] = Session["hid"] != null ? Session["hid"] : id;
            return View();
        }

        [Authentication]
        public JsonResult GetSubMenus(string id)
        {
            string method = "wavenet.fxsw.menu.get";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("is_gc", "0");
            paramDictionary.Add("account", CookieHelper.ReadCookie("token"));
            paramDictionary.Add("id", id);
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        }
        #endregion

        #region 工程对外
        [ProjectAuthentication]
        public ActionResult ProjectOutside()
        {
            ViewData["username"] = Session["name"] != null ? Session["name"].ToString() : "";
            return View();
        }

        [ProjectAuthentication]
        public JsonResult GetOutsideMenus()
        {
            // 接口
            string method = "wavenet.fxsw.menu.get";
            // 接口所需传递的参数
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("is_gc", "1");

            // 调用接口
            string authorization = CookieHelper.GetData(Request, method, paramDictionary);
            return Json(authorization);
        } 
        #endregion
    }
}