using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Critters.Controllers
{
    public class XMLSearchController : Controller
    {
        //
        // GET: /XMLSearch/
        public ActionResult Index(string searchString)
        {
            return View();
        }

    }
}
