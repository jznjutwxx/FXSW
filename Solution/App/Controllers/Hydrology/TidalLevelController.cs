using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers.Hydrology
{
    public class TidalLevelController : Controller
    {
        // GET: TidalLevel
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetSites()
        {
            return Json(new { result = "ok"});
        }

        public JsonResult GetData()
        {
            return Json(new { result = "ok" });
        }

        public JsonResult Upload(HttpPostedFileBase tidalFile)
        {
            var result = UpFiles(tidalFile);
            return Json(new { result = result });
        }

        public string UpFiles(HttpPostedFileBase tidalFile)
        {
            String result = "error";
            string path = System.Configuration.ConfigurationManager.AppSettings["uploadPath"] + "\\";
            string classname = RouteData.Route.GetRouteData(this.HttpContext).Values["controller"].ToString() + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            path += classname;
            try
            {
                if (Request.Files.Count > 0)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    tidalFile.SaveAs(path + tidalFile.FileName);
                }
                result = "success";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
    }
}