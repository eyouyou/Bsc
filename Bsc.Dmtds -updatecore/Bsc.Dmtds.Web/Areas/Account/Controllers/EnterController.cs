using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bsc.Dmtds.Web.Areas.Account.Controllers
{
    [Authorize]
    public class EnterController : Controller
    {
        // GET: Account/Enter
        public ActionResult Index()
        {
            return View();
        }
    }
}