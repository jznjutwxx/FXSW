﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    //工程子系统-设计单位管理
    public class DesignOrganizationMangerController : Controller
    {
        // GET: DesignOrganizationManger
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult defaultPage ()
        { 
            //默认页面-查
            return View();
        }
        public ActionResult addPage()
        {  //增
            return View();
        }
        public ActionResult editPage()
        {   //修改
            return View();
        }   
    }
}