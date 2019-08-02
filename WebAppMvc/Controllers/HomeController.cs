using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebAppMvc.Controllers
{
    //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public class HomeController : Controller
    {
        private static log4net.ILog Log { get; set; }

        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            log.Debug("Debug message");
            log.Warn("Warn message");
            log.Error("Error message");
            log.Fatal("Fatal message");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}