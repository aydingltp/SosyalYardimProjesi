using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
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
        public ActionResult Create(Portal portal, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                if (Resim!=null)
                {
                    WebImage img = new WebImage(Resim.InputStream);
                    FileInfo resiminfo = new FileInfo(Resim.FileName);

                    string newresim = Guid.NewGuid().ToString() + resiminfo.Extension;
                    img.Resize(600, 350);
                    img.Save("~/Uploads/" + newresim);
                    portal.Resim = "/Uploads/" + newresim;
                }


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
        public ActionResult Edit(Portal portal, HttpPostedFileBase Resim)
        {
            var yeniportal = db.Portallar.Where(i => i.Id == portal.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (Resim != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(yeniportal.Resim)))
                    {
                        System.IO.File.Delete(Server.MapPath(yeniportal.Resim));
                    }
                    WebImage img = new WebImage(Resim.InputStream);
                    FileInfo resiminfo = new FileInfo(Resim.FileName);

                    string newresim = Guid.NewGuid().ToString() + resiminfo.Extension;
                    img.Resize(600, 350);
                    img.Save("~/Uploads/" + newresim);
                    yeniportal.Resim = "/Uploads/" + newresim;
                    yeniportal.Baslik = portal.Baslik;
                    yeniportal.Icerik = portal.Icerik;
                    db.SaveChanges();
                }
                db.Entry(yeniportal).State = EntityState.Modified;
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
        public ActionResult DeleteConfirmed(int id, FormCollection collection)
        {
            Portal portal = db.Portallar.Find(id);
            if (System.IO.File.Exists(Server.MapPath(portal.Resim)))
            {
                System.IO.File.Delete(Server.MapPath(portal.Resim));
            }
            db.Portallar.Remove(portal);
            db.SaveChanges();
            return RedirectToAction("List");
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
