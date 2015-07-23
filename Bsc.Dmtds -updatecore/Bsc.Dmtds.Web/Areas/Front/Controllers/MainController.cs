using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bsc.Dmtds.Web.Areas.Front.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Front/Main/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Course()
        {
            return View();
        }

        public ActionResult Video()
        {
            return View();
        }
    }
}