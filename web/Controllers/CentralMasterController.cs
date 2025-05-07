using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class CentralMasterController : Controller
    {
        // GET: CentralMaster
        public ActionResult Index()
        {
            return PartialView("_holidays");
        }

        public ActionResult part()
        {
            return View("_Partial");
        }
        public ActionResult demo()
        {
            return View();
        }
    }
}