using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.paralib.reference.mvc.Models;

namespace com.paralib.reference.mvc.Controllers
{
   public class HelloController : Controller
   {
      [Route("hello/index")]
      public ActionResult Index()
      {
         return View(new Hello());
      }
   }
}