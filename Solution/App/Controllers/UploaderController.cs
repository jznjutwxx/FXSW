using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class UploaderController : Controller
    {
        // GET: Uploader
        public void WebUploaderController()
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            var applicationPath = VirtualPathUtility.ToAbsolute("~") == "/" ? "" : VirtualPathUtility.ToAbsolute("~");
            urlPath = applicationPath + "/WebUpload/" + date;
        }
        static string urlPath = string.Empty;
        public ActionResult Process(string id, string name, string type, string lastModifiedDate, int size, HttpPostedFileBase file)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string filePathName = string.Empty;

            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "WebUpload\\" + date);
            if (Request.Files.Count == 0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" });
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            file.SaveAs(Path.Combine(localPath, filePathName));

            return Json(new
            {
                jsonrpc = "2.0",
                id = "id",
                filePath = urlPath + "/" + filePathName
            });
        }


        public ActionResult Delete(string filePathName)
        {
            if (string.IsNullOrEmpty(filePathName))
            {
                return Content("no");
            }
            //检查路径
            //if (!filePathName.StartsWith(urlPath) ||
            //    filePathName.Substring(6, filePathName.Length - 7).Contains("../"))
            //{
            //    return Content("no!");
            //}
            string localFilePathName = this.Server.MapPath(filePathName);

            try
            {
                System.IO.File.Delete(localFilePathName);
                return Content("ok");
            }
            catch
            {
                return Content("no");
            }
        }
        private static Dictionary<Guid, Tuple<string, string>> DicPathId = new Dictionary<Guid, Tuple<string, string>>();

        public ActionResult GetPathId(string filePathName, string fileName)
        {
            if (string.IsNullOrEmpty(filePathName))
            {
                return Content("no");
            }
            //检查路径
            //if (!filePathName.StartsWith(urlPath) ||
            //    filePathName.Substring(6, filePathName.Length - 7).Contains("../"))
            //{
            //    return Content("no!");
            //}

            //string localFilePathName = this.Server.MapPath(filePathName);//this.Server.MapPath("~/" + filePathName.Replace("../", ""));
            string localFilePathName = filePathName;
            try
            {
                Guid id = Guid.NewGuid();
                Tuple<string, string> fileinfo = new Tuple<string, string>(localFilePathName, fileName);
                DicPathId.Add(id, fileinfo);
                return Content(id.ToString("N"));
            }
            catch
            {
                return Content("no");
            }
        }

        //下载的url 不能使用ajax
        [ActionName("Download")]
        public ActionResult Download(Guid pathid)
        {
            Tuple<string, string> fileinfo = DicPathId[pathid];
            string path = fileinfo.Item1;
            string filename = fileinfo.Item2;
            DicPathId.Remove(pathid);
            return File(path, "application/octet-stream", HttpUtility.UrlEncode(Path.GetFileName(filename)));
        }
    }
}