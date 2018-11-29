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
    public class MuhtacController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Muhtac
        public ActionResult Index()
        {
            return View(db.Muhtaclar.ToList());
        }

        // GET: Muhtac/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muhtac muhtac = db.Muhtaclar.Find(id);
            if (muhtac == null)
            {
                return HttpNotFound();
            }
            return View(muhtac);
        }

        // GET: Muhtac/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Muhtac/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Baslik,MuhtacAdiSoyadi,Aciklama,Lat,Lng,EklenmeTarihi,Aciliyet,YardimYapildimi,AdminOnay")] Muhtac muhtac)
        {
            if (ModelState.IsValid)
            {
                db.Muhtaclar.Add(muhtac);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(muhtac);
        }

        // GET: Muhtac/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muhtac muhtac = db.Muhtaclar.Find(id);
            if (muhtac == null)
            {
                return HttpNotFound();
            }
            return View(muhtac);
        }

        // POST: Muhtac/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Baslik,MuhtacAdiSoyadi,Aciklama,Lat,Lng,EklenmeTarihi,Aciliyet,YardimYapildimi,AdminOnay")] Muhtac muhtac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(muhtac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(muhtac);
        }

        // GET: Muhtac/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muhtac muhtac = db.Muhtaclar.Find(id);
            if (muhtac == null)
            {
                return HttpNotFound();
            }
            return View(muhtac);
        }

        // POST: Muhtac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Muhtac muhtac = db.Muhtaclar.Find(id);
            db.Muhtaclar.Remove(muhtac);
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
