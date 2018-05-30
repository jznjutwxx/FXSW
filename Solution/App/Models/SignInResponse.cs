using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class SignInResponse
    {
        public SignInAuthorizationFXSWRightResponse SignInAuthorizationFXSWRightResponse { get; set; }
    }

    public class SignInAuthorizationFXSWRightResponse
    {
        public string app_key { get; set; }
        public string app_secret { get; set; }
        public string app_session { get; set; }
        public string unit_id { get; set; }
        public string unit_name { get; set; }
        public string token { get; set; }
        public string role { get; set; }
    }

    public class EnginSignInResponse
    {
        public EnginLoginResponse enginLoginResponse { get; set; }
    }

    public class EnginLoginResponse
    {
        public string result { get; set; }
        public string name { get; set; }
    }
}