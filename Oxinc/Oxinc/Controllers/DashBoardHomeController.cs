﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oxinc.Controllers
{
    public class DashBoardHomeController : Controller
    {
        // GET: DashBoardHome
        public ActionResult DashBoard()
        {
            return View();
        }
    }
}