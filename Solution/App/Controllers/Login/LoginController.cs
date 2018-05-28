using App.Common;
using App.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace App.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProjectOutside()
        {
            return View();
        }

        public ActionResult LegalPerson()
        {
            return View();
        }

        public ActionResult OverTime()
        {
            Session.Abandon();
            return View();
        }
        public ActionResult ProjectOverTime()
        {
            Session.Abandon();
            return View();
        }

        public JsonResult checkLogin(string name,string pwd,string type="")
        {
            var result = "error";
            var username = "";
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("userNo", name.Trim());
            paramDictionary.Add("password", pwd.Trim());

            AuthorizationParams ap = new AuthorizationParams();
            ap.TIMESTAMP = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            ap.URL = ConfigurationManager.AppSettings["loginUrl"].ToString();

            var authorization = AuthorizationUtils.LoginAuthorization(ap, paramDictionary);

            if (authorization.Contains("账号或密码错误"))
            {
                result = "error";
            }
            else
            {
                SignInResponse signInResponse = JsonToObject<SignInResponse>(authorization);
                if (signInResponse != null && signInResponse.SignInAuthorizationFXSWRightResponse.app_key!=null && signInResponse.SignInAuthorizationFXSWRightResponse.app_key != "")
                {
                    result = "sucess";
                    username = signInResponse.SignInAuthorizationFXSWRightResponse.unit_name;
                    Session["name"] = signInResponse.SignInAuthorizationFXSWRightResponse.unit_name;
                    CookieHelper.WriteCookie("app_key", signInResponse.SignInAuthorizationFXSWRightResponse.app_key, 0);
                    CookieHelper.WriteCookie("app_secret", signInResponse.SignInAuthorizationFXSWRightResponse.app_secret, 0);
                    CookieHelper.WriteCookie("app_session", signInResponse.SignInAuthorizationFXSWRightResponse.app_session, 0);
                    CookieHelper.WriteCookie("unit_id", signInResponse.SignInAuthorizationFXSWRightResponse.unit_id, 0);
                    CookieHelper.WriteCookie("unit_name", signInResponse.SignInAuthorizationFXSWRightResponse.unit_name, 0);
                    CookieHelper.WriteCookie("token", signInResponse.SignInAuthorizationFXSWRightResponse.token, 0);
                    CookieHelper.WriteCookie("role", signInResponse.SignInAuthorizationFXSWRightResponse.role, 0);
                    CookieHelper.WriteCookie("type", type, 0);
                }
            }
            return Json(new { name=name, result=result });
        }

        public JsonResult Logout()
        {
            Session.Abandon();
            CookieHelper.DeleteCookie("app_key");
            CookieHelper.DeleteCookie("app_secret");
            CookieHelper.DeleteCookie("app_session");
            CookieHelper.DeleteCookie("unit_id");
            CookieHelper.DeleteCookie("unit_name");
            CookieHelper.DeleteCookie("token");
            CookieHelper.DeleteCookie("role");
            CookieHelper.DeleteCookie("type");
            return Json(new { result = "sucess" });
        }

        /// <summary>
        /// Json转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string jsonText)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            T obj = js.Deserialize<T>(jsonText);
            return obj;
        }


    }
}