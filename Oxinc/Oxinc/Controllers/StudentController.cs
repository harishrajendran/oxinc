using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oxinc.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult StudentView()
        {
            return PartialView();
        }

        public ActionResult StudentBulkView()
        {
            return PartialView();
        }
    }
}