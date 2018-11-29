using SYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using SYP.Filtreler;

namespace SYP.Controllers
{
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            var muhtaclar = db.Muhtaclar.Include(p => p.Adres).Include(p => p.Il).Where(i=>i.AdminOnay==true).ToList();
            return View(muhtaclar);
        }

        public ActionResult List(int? id, string q)
        {
            var muhtaclar = db.Muhtaclar.Where(i=>i.AdminOnay==true).AsQueryable();

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

        [GirisKontrolFiltresi]
        [HttpGet]
        [Route("Home/Ekle")]
        public ActionResult IhtiyacSahibiEkle()
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

        [GirisKontrolFiltresi]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Home/Ekle")]
        public ActionResult IhtiyacSahibiEkle(Muhtac yenimuhtac)
        {
            if (ModelState.IsValid)
            {
                int? kullaniciId = (Int32)Session["uyeid"];
                var yeniMuhtac = new Muhtac()
                {
                    Aciklama = yenimuhtac.Aciklama,
                    Aciliyet = yenimuhtac.Aciliyet,
                    Adres = yenimuhtac.Adres,
                    Baslik = yenimuhtac.Baslik,
                    EklenmeTarihi = DateTime.Today,
                    Kullanici = db.Kullanicilar.FirstOrDefault(p => p.Id == kullaniciId),
                    MuhtacAdiSoyadi = yenimuhtac.MuhtacAdiSoyadi,
                    YardimTuru = db.YardimTurler.FirstOrDefault(p => p.Id == yenimuhtac.YardimTuru.Id),
                    Il = db.Iller.FirstOrDefault(p => p.Id == yenimuhtac.Il.Id)
                    
                };
                //yenimuhtac.AdminOnay = false; gerek var mı?
                db.Muhtaclar.Add(yeniMuhtac);
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
            return View(yenimuhtac);
        }

        [HttpGet]
        [Route("Home/Detay/{id}")]
        public ActionResult IhtiyacSahibiDetay(int? id)
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



        public ActionResult Portal()
        {
            return View();
        }




    }
}