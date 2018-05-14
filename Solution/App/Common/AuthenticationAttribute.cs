using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Common
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var account = filterContext.HttpContext.Session["name"];
            var appKey = CookieHelper.ReadCookie("app_key");
            if (account == null||appKey == null|| appKey == "")
            {
                filterContext.HttpContext.Response.Write(" <script type='text/javascript'> window.top.location='" + System.Configuration.ConfigurationManager.AppSettings["OverTimeUrl"] + "'; </script>");
                filterContext.Result = new EmptyResult();
                return;
            }
        }
    }
}