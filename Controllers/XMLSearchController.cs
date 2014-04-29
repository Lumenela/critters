using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Critters.Controllers
{
    public class XMLSearchController : Controller
    {
        //
        // GET: /XMLSearch/
        public ActionResult Index(string searchString)
        {
            string path = HttpContext.Server.MapPath("~/App_Data/books.xml");
            XElement root = XElement.Load(path);
            if (String.IsNullOrEmpty(searchString))
            {
                ViewBag.XmlContainer = root;
            }
            else
            {
             /*   string text = "";
                IEnumerable<XElement> books =
                from el in root.Elements("book")
                select el;
                foreach (XElement el in books)
                    text += "\n" + el;*/
            }
            
            
            return View();
        }

    }
}
