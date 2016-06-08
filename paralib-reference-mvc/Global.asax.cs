﻿using System;
using com.paralib.common;
using log4net;
using System.Web.Mvc;
using System.Web.Routing;

namespace com.paralib.reference.mvc
{
    public class Global : System.Web.HttpApplication
    {
        private static ILog _logger = LogManager.GetLogger(typeof(Global));

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapMvcAttributeRoutes();

            Configuration.Configure("mvc");

            _logger.Info(null);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            _logger.Info(null);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            _logger.Info(null);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            _logger.Info(null);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            _logger.Fatal(ex.Message);
        }

        protected void Session_End(object sender, EventArgs e)
        {
            _logger.Info(null);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            _logger.Info(null);
        }
    }
}