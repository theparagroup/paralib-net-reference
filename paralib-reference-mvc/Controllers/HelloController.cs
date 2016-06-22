using System;
using System.Web.Mvc;
using com.paralib.reference.mvc.Models;
using com.paralib;

namespace com.paralib.reference.mvc.Controllers
{
    public class HelloController : Controller
    {
        private static ILog _logger = Paralib.GetLogger(typeof(HelloController));

        [Route("hello/index")]
        public ActionResult Index()
        {
            _logger.Info("hello world");
            return View(new Hello());
        }

        [Route("hello/e")]
        [HttpGet]
        public ActionResult Employee()
        {
            return View(new Employee());
        }

        [Route("hello/e")]
        [HttpPost]
        public ActionResult Employee(Employee employee)
        {
            return View(employee);
        }


    }
}