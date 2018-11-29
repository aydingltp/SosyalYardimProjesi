using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SYP.Filtreler;
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

        public ActionResult List(int? id, string q)
        {
            var muhtaclar = db.Muhtaclar.Where(i => i.AdminOnay == true).AsQueryable();

            if (id != null)
            {
                if (id == 6)
                {
                    muhtaclar = db.Muhtaclar.Where(i => i.Aciliyet == 5).Where(i => i.AdminOnay == true);
                }
                else
                {
                    muhtaclar = db.Muhtaclar.Where(i => i.YardimTuru.Id == id).Where(i => i.AdminOnay == true);
                }
            }
            if (string.IsNullOrEmpty(q) == false)
            {
                muhtaclar = muhtaclar.Where(i => i.Baslik.Contains(q) || i.Aciklama.Contains(q));
            }

            return View(muhtaclar.Include(i => i.Adres).Include(i => i.Il).ToList());
        }



        [HttpGet]
        //[Route("Home/Detay/{id}")]
        // GET: Muhtac/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Blog blog = db.Bloglar.Find(id);
            var muhtac = db.Muhtaclar.Where(i => i.Id == id).FirstOrDefault();
            //Muhtac muhtac = db.Muhtaclar.Include(b => b.Adres).Include(b=>b.Il).FirstOrDefault(b => b.Id == id);
            //Find metodu çalışmadı onun yerine firstofdefault kullanıldı.

            if (muhtac == null)
            {
                return HttpNotFound();
            }
            //ViewBag.KategoriAdi = blog.CategoryId;
            //ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi",blog.CategoryId);
            return View(muhtac);

        }

        // GET: Muhtac/Create
        [GirisKontrolFiltresi]
        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> yardimturleri = (from i in db.YardimTurler.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = i.YardimTuruAdi,
                                                      Value = i.Id.ToString()
                                                  }).ToList();
            ViewBag.YardimTurleri = yardimturleri;

            List<SelectListItem> iller = db.Iller.Select(p => new SelectListItem
            {
                Text = p.IlAdi,
                Value = p.Id.ToString()
            }).ToList();
            ViewBag.Iller = iller;
            return View();
        }

        // POST: Muhtac/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [GirisKontrolFiltresi]
        public ActionResult Create(Muhtac muhtac)
        {

            if (ModelState.IsValid)
            {
                int? kullaniciId = (Int32)Session["uyeid"];
                var yenimuhtac = new Muhtac()
                {
                    Aciklama = muhtac.Aciklama,
                    Aciliyet = muhtac.Aciliyet,
                    Adres = muhtac.Adres,
                    Baslik = muhtac.Baslik,
                    EklenmeTarihi = DateTime.Today,
                    Kullanici = db.Kullanicilar.FirstOrDefault(p => p.Id == kullaniciId),
                    MuhtacAdiSoyadi = muhtac.MuhtacAdiSoyadi,
                    YardimTuru = db.YardimTurler.FirstOrDefault(p => p.Id == muhtac.YardimTuru.Id),
                    Il = db.Iller.FirstOrDefault(p => p.Id == muhtac.Il.Id)
                };
                //yenimuhtac.AdminOnay = false; gerek var mı?
                db.Muhtaclar.Add(yenimuhtac);
                db.SaveChanges();

                return RedirectToAction("Index");
            }


            List<SelectListItem> yardimturleri = (from i in db.YardimTurler.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = i.YardimTuruAdi,
                                                      Value = i.Id.ToString()
                                                  }).ToList();
            ViewBag.YardimTurleri = yardimturleri;

            List<SelectListItem> iller = db.Iller.Select(p => new SelectListItem
            {
                Text = p.IlAdi,
                Value = p.Id.ToString()
            }).ToList();
            ViewBag.Iller = iller;

            TempData["eklendi"] = "İhtiyaç Sahibi Eklendi";
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
