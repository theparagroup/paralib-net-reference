using System;
using System.Web.Mvc;
using com.paralib.Mvc.Authentication;

namespace com.paralib.reference.mvc.Controllers
{
    public class AuthenticationController : Controller
    {
        private static ILog _logger = Paralib.GetLogger(typeof(AuthenticationController));

        [AllowAnonymous]
        [Route("login")]
        public void Login()
        {
            //Forms.Authenticate("fooz");
        }

        [Route("logout")]
        public string Logout()
        {
            Forms.DeAuthenticate();
            return "logged out.";
        }


    }
}