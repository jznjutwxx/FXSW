using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace App.Common
{
    public class AuthorizationParams
    {
        private string _METHOD = "wavenet.fxsw.right.signin";
        private string _TIMESTAMP = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        private string _UTC_OFFSET = "+00:00";
        private string _FORMAT = "json";
        private string _APP_KEY = "";
        private string _APP_SECRET = "";
        private string _VERSION = "1";
        private string _SIGN = "";
        private string _SIGN_METHOD = "md5";
        private string _SESSION = "";
        private string _LOGIN_TOKEN = "";
        private string _MAC = "";
        private string _URL = ConfigurationManager.AppSettings["apiUrl"].ToString();

        public string METHOD { get { return _METHOD; } set { _METHOD = value; } }
        public string TIMESTAMP { get { return _TIMESTAMP; } set { _TIMESTAMP = value; } }
        public string UTC_OFFSET { get { return _UTC_OFFSET; } set { _UTC_OFFSET = value; } }
        public string FORMAT { get { return _FORMAT; } set { _FORMAT = value; } }
        public string APP_KEY { get { return _APP_KEY; } set { _APP_KEY = value; } }
        public string APP_SECRET { get { return _APP_SECRET; } set { _APP_SECRET = value; } }
        public string VERSION { get { return _VERSION; } set { _VERSION = value; } }
        public string SIGN { get { return _SIGN; } set { _SIGN = value; } }
        public string SIGN_METHOD { get { return _SIGN_METHOD; } set { _SIGN_METHOD = value; } }
        public string SESSION { get { return _SESSION; } set { _SESSION = value; } }
        public string LOGIN_TOKEN { get { return _LOGIN_TOKEN; } set { _LOGIN_TOKEN = value; } }
        public string MAC { get { return _MAC; } set { _MAC = value; } }
        public string URL { get { return _URL; } set { _URL = value; } }
    }
}