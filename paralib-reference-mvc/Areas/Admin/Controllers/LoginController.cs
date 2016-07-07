using System;
using System.Web.Mvc;
using com.paralib.Mvc.Authorization;
using com.paralib.Mvc.Authentication;
using com.paralib.reference.mvc.Areas.Admin.Models;

namespace com.paralib.reference.mvc.Areas.Admin.Controllers
{
    public class LoginController : AdminController
    {
        [Route("login")]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            ParaPrinciple principle = new ParaPrinciple("woozy", new string[] { "boss", "hbic","belly" });
            Forms.Authenticate(principle, "info");
            return View();
        }

        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Credentials credentials)
        {
            return View();
        }


        [Route("logout")]
        [AllowAnonymous]
        public void Logout()
        {
            Forms.DeAuthenticate();
            //redirect to home page
        }

        [Route("error")]
        [AllowAnonymous]
        public string Error()
        {
            return "you don't have permissions to see this page.";

        }


        [Route("test")]
        public ActionResult Test()
        {
            return View();
        }

    }
}