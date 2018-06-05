using App.Common;
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
            return Json(result.Data);
        }

        public JsonResult UpFiles(HttpPostedFileBase tidalFile)
        {
            String result = "error";
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    result = InsertData("picture_file", Request.Files[i]);
                }
            }
            return Json(result);
        }

        private string InsertData(string type,HttpPostedFileBase tidalFile)
        {
            string method = "wavenet.fxsw.tide.prediction.import";
            IDictionary<string, string> dic = new Dictionary<string, string>();
            Dictionary<string, HttpPostedFileBase> fileParams = new Dictionary<string, HttpPostedFileBase>();
            fileParams.Add(type, tidalFile);
            var authorization = CookieHelper.GetData(Request, method, dic, fileParams);
            return authorization;
        }
    }
}