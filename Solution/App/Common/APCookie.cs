using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Common
{
    public class APCookie
    {
        public string app_key { get; set; }
        public string app_secret { get; set; }
        public string app_session { get; set; }
        public string unit_id { get; set; }
    }
}