using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace App.Common
{
    public class AuthorizationUtils
    {
        #region 属性
        private const string METHOD = "method";
        private const string TIMESTAMP = "timestamp";
        private const string UTC_OFFSET = "utc_offset";
        private const string FORMAT = "format";
        private const string APP_KEY = "app_key";
        private const string VERSION = "ver";
        private const string SIGN = "sign";
        private const string SIGN_METHOD = "sign_method";
        private const string SESSION = "session";
        private const string LOGIN_TOKEN = "login_token";
        private const string MAC = "mac";
        #endregion

        #region 调用服务端
        /// <summary>
        /// 调用服务端（不包含文件上传）
        /// </summary>
        /// <param name="authorizationParams">服务端参数</param>
        /// <param name="dic">业务参数</param>
        /// <returns></returns>
        public static string Authorization(AuthorizationParams authorizationParams, IDictionary<string, string> dic)
        {
            // 基本参数
            var paramDictionary = getParams(authorizationParams, dic);
            WebUtils webPost = new WebUtils();
            string url = authorizationParams.URL;
            string sss = WebUtils.Default.Post(url, Encoding.UTF8, paramDictionary);
            return sss;
        }
        /// <summary>
        /// 调用服务端（包含文件上传）
        /// </summary>
        /// <param name="authorizationParams">服务端参数</param>
        /// <param name="dic">业务参数</param>
        /// <param name="files">文件</param>
        /// <returns></returns>
        public static string Authorization(AuthorizationParams authorizationParams, IDictionary<string, string> dic, Dictionary<string, string> files)
        {
            // 基本参数
            var paramDictionary = getParams(authorizationParams,dic);

            // 文件参数
            IDictionary<string, FileItem> fileParams = new Dictionary<string, FileItem>();
            foreach (string file in files.Keys)
            {
                FileItem item = new FileItem(FileItemType.FileInfo, files[file]);
                fileParams[file] = item;
            }

            WebUtils webPost = new WebUtils();
            string url = authorizationParams.URL;
            string sss = webPost.Post(url, Encoding.UTF8, null, paramDictionary, fileParams);
            return sss;
        }

        /// <summary>
        /// 获取服务端登录返回的参数，以及各式话业务参数
        /// </summary>
        /// <param name="authorizationParams">服务端登录返回的参数</param>
        /// <param name="dic">业务参数</param>
        /// <returns></returns>
        public static IDictionary<string, string> getParams(AuthorizationParams authorizationParams, IDictionary<string, string> dic)
        {
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            //系统级参数设置 
            paramDictionary.Add(APP_KEY, authorizationParams.APP_KEY);
            paramDictionary.Add(SESSION, authorizationParams.SESSION);
            paramDictionary.Add(METHOD, authorizationParams.METHOD);
            paramDictionary.Add(FORMAT, authorizationParams.FORMAT);
            paramDictionary.Add(VERSION, authorizationParams.VERSION);
            paramDictionary.Add(TIMESTAMP, authorizationParams.TIMESTAMP);
            paramDictionary.Add(UTC_OFFSET, authorizationParams.UTC_OFFSET);
            paramDictionary.Add(SIGN_METHOD, authorizationParams.SIGN_METHOD);

            foreach (KeyValuePair<string, string> item in dic)
            {
                paramDictionary.Add(item.Key, item.Value);
            }
            paramDictionary.Add(SIGN, GetSignature(paramDictionary, authorizationParams.APP_SECRET));
            return paramDictionary;
        }
        #endregion

        #region 登录调用
        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="authorizationParams">服务端参数</param>
        /// <param name="dic">用户名&密码</param>
        /// <returns></returns>
        public static string LoginAuthorization(AuthorizationParams authorizationParams, IDictionary<string, string> dic)
        {
            IDictionary<string, string> paramDictionary = new Dictionary<string, string>();
            //系统级参数设置 
            paramDictionary.Add(METHOD, authorizationParams.METHOD);
            paramDictionary.Add(FORMAT, authorizationParams.FORMAT);
            paramDictionary.Add(VERSION, authorizationParams.VERSION);
            paramDictionary.Add(TIMESTAMP, authorizationParams.TIMESTAMP);
            paramDictionary.Add(UTC_OFFSET, authorizationParams.UTC_OFFSET);

            foreach (KeyValuePair<string, string> item in dic)
            {
                paramDictionary.Add(item.Key, item.Value);
            }

            WebUtils webPost = new WebUtils();
            string url = authorizationParams.URL;
            string sss = WebUtils.Default.Post(url, Encoding.UTF8, paramDictionary);
            return sss;
        }
        #endregion

        #region 加密算法
        /// <summary>
        /// 加密算法
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static string GetSignature(IDictionary<string, string> parameters, string secret)
        {
            // 先将参数以其参数名的字典序升序进行排序

            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            // 先将参数以其参数名的字典序升序进行排序

            IEnumerator<KeyValuePair<string, string>> iterator = sortedParams.GetEnumerator();

            // 遍历排序后的字典，将所有参数按"key1value1key2value2"格式拼接在一起

            StringBuilder basestring = new StringBuilder();
            basestring.Append(secret);
            while (iterator.MoveNext())
            {
                string key = iterator.Current.Key;
                string value = iterator.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
#if DEBUG
                    Console.WriteLine("{0}={1}", key, value);
#endif

                    basestring.Append(key).Append(value);
                }
            }
            basestring.Append(secret);

#if DEBUG
            Console.WriteLine(basestring);
#endif


            // 使用MD5对待签名串求签

            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(basestring.ToString()));

            // 将MD5输出的二进制结果转换为小写的十六进制
            //return Str.Bin2Str(bytes).ToLower();

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                string hex = bytes[i].ToString("x");
                if (hex.Length == 1)
                {
                    result.Append("0");
                }
                result.Append(hex);
            }

            return result.ToString().ToUpper();
        }
        #endregion
    }
}