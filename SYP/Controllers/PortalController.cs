using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SYP.Models;

namespace SYP.Controllers
{
    public class PortalController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Portal
        public ActionResult Index()
        {
            return View(db.Portallar.ToList());
        }
        public ActionResult List()
        {
            var list = db.Portallar.ToList();
            return View(list);
        }
        // GET: Portal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portal portal = db.Portallar.Find(id);
            if (portal == null)
            {
                return HttpNotFound();
            }
            return View(portal);
        }

        // GET: Portal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Portal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Portal portal)
        {
            if (ModelState.IsValid)
            {
                portal.EklenmeTarihi = DateTime.Now;
                db.Portallar.Add(portal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(portal);
        }

        // GET: Portal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portal portal = db.Portallar.Find(id);
            if (portal == null)
            {
                return HttpNotFound();
            }
            return View(portal);
        }

        // POST: Portal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Portal portal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(portal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(portal);
        }

        // GET: Portal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portal portal = db.Portallar.Find(id);
            if (portal == null)
            {
                return HttpNotFound();
            }
            return View(portal);
        }

        // POST: Portal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Portal portal = db.Portallar.Find(id);
            db.Portallar.Remove(portal);
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
