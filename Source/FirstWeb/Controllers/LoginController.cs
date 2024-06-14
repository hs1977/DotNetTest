using FirstWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: /Login/Check
        public ActionResult Check()
        {
            string user = Request["username"];
            string pwd = Request["password"];

            bool success = LoginManager.CheckLogin(user, pwd);

            ViewBag.Success = success;
            return View();
        }
    }
}