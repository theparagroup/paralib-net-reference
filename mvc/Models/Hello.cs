﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.Models
{
   public class Hello
   {
      public string Message => "Hello, the time is " + DateTime.Now +"!";
   }
}