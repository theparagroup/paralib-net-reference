using System;
using System.Web.Mvc;
using System.Web;
using com.paralib;
using com.paralib.Dal;

namespace com.paralib.reference.mvc.Controllers
{
    //TODO move all this to paramodule

    public class InfoController : Controller
    {
        private static ILog _logger = Paralib.GetLogger(typeof(InfoController));

        [Route("info")]
        public ActionResult Index()
        {
            _logger.Info("generating info");
            return View();
        }

        [Route("unload")]
        public void Unload()
        {
            using (var db = new Db("paralib"))
            {
                db.ExecuteNonQuery("delete from log");
            }

            _logger.Info("log deleted");

            _logger.Info("unloading app domain on request");
            HttpRuntime.UnloadAppDomain();
        }


    }
}