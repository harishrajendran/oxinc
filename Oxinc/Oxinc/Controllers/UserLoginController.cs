using Dapper;
using MySql.Data.MySqlClient;
using Oxinc.Models;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oxinc.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: UserLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserLogin Model)
        { 
            return View();
        }
       
        [HttpPost]
        public ActionResult Authorize(UserLogin userlogin)
        {                
            string query = "select id,CAST(AES_DECRYPT( password , '9524477366')AS CHAR) as password,loginname from user where loginname = '" + userlogin.loginname.ToLower()+"'";
            var ValidUsers=Repository.executequery(query);
            string validpassword=ValidUsers.Select(x => x.password).FirstOrDefault();
            if (!ValidUsers.Any()|| validpassword != userlogin.password)
            {
                userlogin.loginerrormessage = "wrong user name or password";
                return View("Index", userlogin);
            }
            else
            {
               
                Session["UserId"] = ValidUsers.Select(x => x.Id).FirstOrDefault();
                return RedirectToAction("DashBoard", "DashBoardHome");
            }
            
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "UserLogin");
        }
    }
}