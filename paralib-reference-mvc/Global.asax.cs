using System;
using System.Web.Mvc;
using System.Web.Routing;
using com.paralib;
using com.paralib.Configuration;

namespace com.paralib.reference.mvc
{
    public class Global : System.Web.HttpApplication
    {
        private static ILog _logger = Paralib.GetLogger(typeof(Global));


        static Global()
        {
            _logger.Info("static .ctor");
            Paralib.Configure += Paralib_Configure;
        }

        private static void Paralib_Configure(Settings settings)
        {
            _logger.Info(null);

            //programatically override paralib settings here

        }

        public override void Init()
        {
            _logger.Info(null);
            base.Init();
        }

        protected void Application_OnStart(object sender, EventArgs e)
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapMvcAttributeRoutes();

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

        protected void Application_AuthorizeRequest(object sender, EventArgs e)
        {
            _logger.Info(null);

        }

        protected void Application_PostMapRequestHandler(object sender, EventArgs e)
        {
            _logger.Info(Context.CurrentHandler?.GetType().Name);
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