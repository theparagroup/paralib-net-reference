using System;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;
using com.paralib.common;

namespace com.paralib.reference.mvc
{
    public class Global : System.Web.HttpApplication
    {
        private static ILog _logger = LogManager.GetLogger(typeof(Global));

        public override void Init()
        {
            _logger.Info(null);

            base.Init();
            
        }

        private void Paramod_Configure(object sender, EventArgs e)
        {
            _logger.Info(null);
        }

        protected void Application_OnStart(object sender, EventArgs e)
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapMvcAttributeRoutes();

            _logger.Info(null);
        }


        protected void ParaMvcModule_Configure(object sender, EventArgs e)
        {
            _logger.Info(null);
        }

        protected void ParaMvcModule_OnConfigure(object sender, EventArgs e)
        {
            _logger.Info(null);
        }

        protected void ParaMvc_Configure(object sender, EventArgs e)
        {
            _logger.Info(null);
        }

        protected void ParaMvc_OnConfigure(object sender, EventArgs e)
        {
            _logger.Info(null);
        }


        protected void Session_Start(object sender, EventArgs e)
        {
            _logger.Info(null);
        }

        protected void Session_OnStart(object sender, EventArgs e)
        {
            _logger.Info(null);
        }

        protected void SessionState_Start(object sender, EventArgs e)
        {
            _logger.Info(null);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            _logger.Info(null);
        }

        protected void Application_OnBeginRequest(object sender, EventArgs e)
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