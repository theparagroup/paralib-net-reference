using System;
using System.Web.Mvc;
using com.paralib;
using com.paralib.reference.models;

namespace com.paralib.reference.mvc.Controllers
{
    public class EmployeeController : Controller
    {
        private static ILog _logger = Paralib.GetLogger(typeof(EmployeeController));

        [Route("employee")]
        [HttpGet]
        public ActionResult Index()
        {
            return View(new Employee() { Id = 1 });
        }

        [Route("employee")]
        [HttpPost]
        public ActionResult Index(Employee employee)
        {
            return View(employee);
        }


    }
}