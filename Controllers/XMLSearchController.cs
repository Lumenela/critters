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
                IEnumerable<IEnumerable<XElement>> booksFound =
                    from book in root.Elements()
                    where (from p in book.Descendants()
                           where p.Value.Contains(searchString)
                           select p).Any()
                    select book.DescendantsAndSelf().Where(p => p.Value.Contains(searchString)).ToArray();


                string text = "";
                foreach (IEnumerable<XElement> el in booksFound)
                {
                    foreach (XElement l in el)
                    {
                        string pathToElement = l.Name + ">" + l.Value;
                        XElement parent = l.Parent;
                        while (parent != null)
                        {
                            pathToElement = parent.Name + ">" + pathToElement;
                            parent = parent.Parent;
                        }
                        text += pathToElement + "\n";
                    }
                }
                ViewBag.XmlContainer = text;
            }
            
            
            return View();
        }

    }
}
