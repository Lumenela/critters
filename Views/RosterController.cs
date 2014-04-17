using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Critters.Models;

namespace Critters.Views
{
    public class RosterController : Controller
    {
        private CrittersEntities db = new CrittersEntities();

        // GET: /Roster/
        public ActionResult Index(string position, string birthcity, string birthstate, string fromYear, string toYear, string fromWeight, string toWeight, string fromHeight, string toHeight)
        {
            this.FillDropdownLists();

            var rosters = from r in db.Rosters
                         select r;

            if (!String.IsNullOrEmpty(position))
            {
                rosters = rosters.Where(r => r.position.Equals(position));
            }

            if (!String.IsNullOrEmpty(birthcity))
            {
                rosters = rosters.Where(r => r.birthcity.Equals(birthcity));
            }

            if (!String.IsNullOrEmpty(birthstate))
            {
                rosters = rosters.Where(r => r.birthstate.Equals(birthstate));
            }

            if (!String.IsNullOrEmpty(fromYear))
            {
                int year = 0;
                if (Int32.TryParse(fromYear, out year))
                {
                    year = Int32.Parse(fromYear);
                    rosters = rosters.Where(r => r.birthday.Value.Year >= year);
                }
            }

            if (!String.IsNullOrEmpty(toYear))
            {
                int year = 0;
                if (Int32.TryParse(toYear, out year))
                {
                    year = Int32.Parse(toYear);
                    rosters = rosters.Where(r => r.birthday.Value.Year <= year);
                }
            }

            if (!String.IsNullOrEmpty(fromWeight))
            {
                int weight = 0;
                if (Int32.TryParse(fromWeight, out weight))
                {
                    weight = Int32.Parse(fromWeight);
                    rosters = rosters.Where(r => r.weight >= weight);
                }
            }

            if (!String.IsNullOrEmpty(toWeight))
            {
                int weight = 0;
                if (Int32.TryParse(toWeight, out weight))
                {
                    weight = Int32.Parse(toWeight);
                    rosters = rosters.Where(r => r.weight <= weight);
                }
            }

            if (!String.IsNullOrEmpty(fromHeight))
            {
                int height = 0;
                if (Int32.TryParse(fromHeight, out height))
                {
                    height = Int32.Parse(fromHeight);
                    rosters = rosters.Where(r => r.height >= height);
                }
            }

            if (!String.IsNullOrEmpty(toHeight))
            {
                int height = 0;
                if (Int32.TryParse(toHeight, out height))
                {
                    height = Int32.Parse(toHeight);
                    rosters = rosters.Where(r => r.height <= height);
                }
            }

            return View(rosters);
        }

        private void FillDropdownLists()
        {
            var cityQuery = from r in db.Rosters
                            orderby r.birthcity
                            select r.birthcity;
            var cityList = new List<string>();
            cityList.AddRange(cityQuery.Distinct());
            ViewBag.birthcity = new SelectList(cityList);

            var stateQuery = from r in db.Rosters
                             orderby r.birthstate
                             select r.birthstate;
            var stateList = new List<string>();
            stateList.AddRange(stateQuery.Distinct());
            ViewBag.birthstate = new SelectList(stateList); 
        }

        // GET: /Roster/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roster roster = db.Rosters.Find(id);
            if (roster == null)
            {
                return HttpNotFound();
            }
            return View(roster);
        }

        // GET: /Roster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Roster/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="playerid,jersey,fname,sname,position,birthday,height,weight,birthcity,birthstate")] Roster roster)
        {
            if (ModelState.IsValid)
            {
                db.Rosters.Add(roster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roster);
        }

        // GET: /Roster/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roster roster = db.Rosters.Find(id);
            if (roster == null)
            {
                return HttpNotFound();
            }
            return View(roster);
        }

        // POST: /Roster/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="playerid,jersey,fname,sname,position,birthday,height,weight,birthcity,birthstate")] Roster roster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roster);
        }

        // GET: /Roster/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roster roster = db.Rosters.Find(id);
            if (roster == null)
            {
                return HttpNotFound();
            }
            return View(roster);
        }

        // POST: /Roster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Roster roster = db.Rosters.Find(id);
            db.Rosters.Remove(roster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
