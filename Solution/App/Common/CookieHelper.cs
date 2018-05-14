using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Common
{
    public static class CookieHelper
    {
        #region  获取cookie参数
        /// <summary>
        /// 获取cookie参数
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static APCookie GetCookie(HttpRequestBase Request)
        {
            APCookie apCookie = new APCookie();
            apCookie.app_key = ReadCookie("app_key");
            apCookie.app_secret = ReadCookie("app_secret");
            apCookie.app_session = ReadCookie("app_session");
            apCookie.unit_id = ReadCookie("unit_id");
            return apCookie;
        }
        #endregion

        #region 调用接口（不包含文件上传）
        /// <summary>
        /// 调用接口
        /// </summary>
        /// <param name="Request">Request</param>
        /// <param name="method">接口</param>
        /// <param name="paramDictionary">参数</param>
        /// <returns></returns>
        public static string GetData(HttpRequestBase Request,string method, IDictionary<string, string> paramDictionary)
        {
            var authorizationParams = getParams(Request, method);
            var authorization = "";
            try
            {
                 authorization = AuthorizationUtils.Authorization(authorizationParams, paramDictionary);
            }
            catch (Exception ex){ }
            return authorization;
        }
        #endregion

        #region  调用接口（包含文件上传）
        /// <summary>
        /// 调用接口
        /// </summary>
        /// <param name="Request">Request</param>
        /// <param name="method">接口</param>
        /// <param name="paramDictionary">参数</param>
        /// <param name="files">文件参数</param>
        /// <returns></returns>

        public static string GetData(HttpRequestBase Request, string method, IDictionary<string, string> paramDictionary,Dictionary<string,string> files)
        {
            var authorizationParams = getParams(Request,method);

            var authorization = "";
            try
            {
                authorization = AuthorizationUtils.Authorization(authorizationParams, paramDictionary, files);
            }
            catch (Exception ex){ }
            return authorization;
        }
        #endregion

        #region 获取服务端登录返回的验证参数
        /// <summary>
        /// 获取服务端登录返回的验证参数
        /// </summary>
        /// <param name="Request">Request</param>
        /// <param name="method">接口</param>
        /// <returns></returns>
        public static AuthorizationParams getParams(HttpRequestBase Request, string method)
        {
            APCookie apCookie = CookieHelper.GetCookie(Request);
            AuthorizationParams authorizationParams = new AuthorizationParams();
            // 必须
            authorizationParams.APP_KEY = apCookie.app_key;
            authorizationParams.SESSION = apCookie.app_session;
            authorizationParams.APP_SECRET = apCookie.app_secret;
            authorizationParams.METHOD = method;
            return authorizationParams;
        }
        #endregion

        #region 读写删cookie

        /// <summary>
        /// 写cookie
        /// </summary>
        /// <param name="strname">cookie名称</param>
        /// <param name="strvalue">cookie值</param>
        /// <param name="days">保存天数</param>
        public static void WriteCookie(string strname, string strvalue, int minutes)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strname];
            if (cookie == null)
                cookie = new HttpCookie(strname);
            cookie.Value = UrlEncode(strvalue);
            if (minutes > 0)
                cookie.Expires = DateTime.Now.AddMinutes(minutes);
            HttpContext.Current.Response.AppendCookie(cookie);
        }


        /// <summary>
        /// 读取cookie值
        /// </summary>
        /// <param name="strname"></param>
        /// <returns></returns>
        public static string ReadCookie(string strname)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strname] != null)
            {
                return UrlDecode(HttpContext.Current.Request.Cookies[strname].Value.ToString());
            }
            return "";
        }


        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="strname"></param>
        public static void DeleteCookie(string strname)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strname] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[strname];
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }
        #endregion

        #region URL处理

        /// <summary>
        /// url编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            str = str.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(str);
        }

        /// <summary>
        /// url解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            return HttpContext.Current.Server.UrlDecode(str);
        }
        #endregion
    }
}