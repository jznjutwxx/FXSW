using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web;

namespace App.Common
{
    public sealed class WebUtils
    {
        private static readonly WebUtils _default = new WebUtils();
        private int _timeout = 100 * 1000;

        public static WebUtils Default { get { return _default; } }

        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        public string ContentType { get; set; }

        /// <summary>
        /// 读stream的内容，结束后不关闭stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string ReadStream(Stream stream, Encoding encode)
        {
            StringBuilder sb = new StringBuilder();

            //不调用StreamReader的Close方法，否则会关闭stream
            StreamReader readStream = new StreamReader(stream, encode);
            Char[] read = new Char[2048];
            int count = 0;
            do
            {
                count = readStream.Read(read, 0, read.Length);
                if (count > 0)
                {
                    sb.Append(read, 0, count);
                }
            }
            while (count > 0);

            return sb.ToString();
        }

        public static void CopyStream(Stream from, Stream to)
        {
            byte[] buffer = new byte[2048];
            int count = 0;
            do
            {
                count = from.Read(buffer, 0, buffer.Length);
                if (count > 0)
                {
                    to.Write(buffer, 0, count);
                }
            }
            while (count > 0);
        }

        public static string UrlEncode(string data, Encoding encode)
        {
            return HttpUtility.UrlEncode(data, encode);
        }

        public bool CheckUrl(string url)
        {
            try
            {
                HttpWebRequest request = GetRequest(url, "GET", null);
                //处理响应信息
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
            }
            catch
            {
            }

            return false;
        }

        public bool GetStream(string url, Stream stream)
        {
            HttpWebRequest request = GetRequest(url, "GET", null);

            //处理响应信息
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    
                    return false;
                }

                using (Stream receiveStream = response.GetResponseStream())
                {
                    CopyStream(receiveStream, stream);
                }
            }

            return true;
        }


        public string GetContent(string url, Encoding encode)
        {
            HttpWebRequest request = GetRequest(url, "GET", null);

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //log.Error("Access Url[0] HttpStatusCode[1]", url, response.StatusDescription);
                    return null;
                }

                using (Stream respStream = response.GetResponseStream())
                {
                    return ReadStream(respStream, encode);
                }
            }
        }

        public string Post(string url, Encoding encode, IDictionary<string, string> body)
        {
            return Post(url, encode, null, body);
        }

        public string Post(string url, Encoding encode, IDictionary<string, string> head, IDictionary<string, string> body)
        {
            return Post(url, encode, head, GetUrlBody(encode, body));
        }

        public string Post(string url, Encoding encode, string body)
        {
            return Post(url, encode, null, body);
        }

        public string Post(string url, Encoding encode, IDictionary<string, string> head, string body)
        {
#if DEBUG
            Console.WriteLine("{0}?{1}", url, body);
#endif

            return Post(url, encode, head, encode.GetBytes(body));
        }

        public string Post(string url, Encoding encode, byte[] postData)
        {
            return Post(url, encode, null, postData);
        }

        public string Post(string url, Encoding encode, IDictionary<string, string> head, byte[] postData)
        {
#if DEBUG

            DateTime dtStart = DateTime.Now;
#endif
            HttpWebRequest request = GetRequest(url, "POST", head);
            if (string.IsNullOrEmpty(request.ContentType))
            {
                request.ContentType = string.Format("application/x-www-form-urlencoded;charset={0}", encode.BodyName);
            }
            request.ContentLength = postData.Length;

            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(postData, 0, postData.Length);
            }
#if DEBUG

            DateTime dtRequest = DateTime.Now;
#endif

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    
                    return null;
                }

                using (Stream respStream = response.GetResponseStream())
                {
                    string respContent = ReadStream(respStream, encode);
#if DEBUG

                    DateTime dtResponse = DateTime.Now;
                    //Console.WriteLine("Request time={0:ss.fff} Response time={1:ss.fff}", dtRequest - dtStart, dtResponse - dtRequest);
                    //Console.WriteLine(respContent);
#endif
                    return respContent;
                }
            }
        }

        #region testPostFile
        public string Post(string url, Encoding encode, IDictionary<string, string> head, IDictionary<string, string> body, IDictionary<string, FileItem> fileParams)
        {
#if DEBUG

            DateTime dtStart = DateTime.Now;
#endif
            if (fileParams == null || fileParams.Count == 0)
            {
                return Post(url, encode, body);
            }

            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线



            HttpWebRequest request = GetRequest(url, "POST", head);

            System.IO.Stream reqStream = request.GetRequestStream();
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            if (string.IsNullOrEmpty(request.ContentType))
            {
                request.ContentType = string.Format("multipart/form-data;charset={0};boundary={1}", encode.BodyName, boundary);
            }

            StringBuilder sb = new StringBuilder();

            // 组装文本请求参数
            string textTemplate = "Content-Disposition:form-data;name=\"{0}\"\r\nContent-Type:text/plain\r\n\r\n{1}";
            IEnumerator<KeyValuePair<string, string>> textEnum = body.GetEnumerator();
            while (textEnum.MoveNext())
            {
                string textEntry = string.Format(textTemplate, textEnum.Current.Key, textEnum.Current.Value);
                byte[] itemBytes = Encoding.UTF8.GetBytes(textEntry);
                reqStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                reqStream.Write(itemBytes, 0, itemBytes.Length);


                sb.Append("\r\n--" + boundary + "\r\n");
                sb.Append(textEntry);

            }
            
            // 组装文件请求参数
            string fileTemplate = "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";
            IEnumerator<KeyValuePair<string, FileItem>> fileEnum = fileParams.GetEnumerator();
            while (fileEnum.MoveNext())
            {
                string key = fileEnum.Current.Key;
                FileItem fileItem = fileEnum.Current.Value;
                string fileEntry = string.Format(fileTemplate, key, fileItem.GetFileName(), fileItem.GetMimeType());
                byte[] itemBytes = Encoding.UTF8.GetBytes(fileEntry);
                reqStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                reqStream.Write(itemBytes, 0, itemBytes.Length);

                byte[] fileBytes = fileItem.GetContent();
                reqStream.Write(fileBytes, 0, fileBytes.Length);

                sb.Append("\r\n--" + boundary + "\r\n");
                sb.Append(fileEntry);
            }

            sb.Append("\r\n--" + boundary + "--\r\n");
            string ssddd = sb.ToString();


            reqStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            reqStream.Close();

#if DEBUG

            DateTime dtRequest = DateTime.Now;
#endif

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                using (Stream respStream = response.GetResponseStream())
                {
                    string respContent = ReadStream(respStream, encode);
#if DEBUG

                    DateTime dtResponse = DateTime.Now;
                    //Console.WriteLine("Request time={0:ss.fff} Response time={1:ss.fff}", dtRequest - dtStart, dtResponse - dtRequest);
                    Console.WriteLine(respContent);
#endif
                    return respContent;
                }
            }
        }
        #endregion

        private HttpWebRequest GetRequest(string url, string method, IDictionary<string, string> head)
        {
            HttpWebRequest webRequest = null;
            if (url.ToLower().StartsWith("https"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                webRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            }
            webRequest.Timeout = Timeout;
            webRequest.Method = method;
            webRequest.KeepAlive = true;
            webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            if (!string.IsNullOrEmpty(ContentType))
            {
                webRequest.ContentType = ContentType;
            }

            if (head != null && head.Count > 0)
            {
                foreach (string name in head.Keys)
                {
                    webRequest.Headers.Add(name, head[name]);
                }
            }
            return webRequest;
        }


        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        private string GetUrlBody(Encoding encode, IDictionary<string, string> body)
        {
            if (body != null && body.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string key in body.Keys)
                {
                    sb.AppendFormat("&{0}={1}", UrlEncode(key, encode), UrlEncode(body[key], encode));
                }

                return sb.ToString(1, sb.Length - 1);
            }
            else
            {
                return string.Empty;
            }
        }

       
    }
}
