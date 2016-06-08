using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.paralib.reference.mvc.Models;
using log4net;

namespace com.paralib.reference.mvc.Controllers
{
    public class HelloController : Controller
    {
        private static ILog _logger = LogManager.GetLogger(typeof(HelloController));

        [Route("hello/index")]
        public ActionResult Index()
        {
            _logger.Info("hello world");
            return View(new Hello());
        }
    }
}