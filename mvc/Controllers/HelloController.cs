﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.Controllers
{
   public class HelloController : Controller
   {
      [Route("hello/index")]
      public ActionResult Index()
      {
         return View();
      }
   }
}