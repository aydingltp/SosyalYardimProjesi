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
using SYP.Models.viewModel;

namespace SYP.Controllers
{
    public class MuhtacController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Muhtac
        public ActionResult Index()
        {
            var muhtaclar = db.Muhtaclar.Where(i => i.Arsivmi == false).OrderBy(i => i.YardimYapildimi).OrderBy(i => i.AdminOnay).ToList();
            return View(muhtaclar);
        }
        public ActionResult Arsivle(int id)
        {
            var muhtac = db.Muhtaclar.Where(i => i.Id == id).FirstOrDefault();
            if (muhtac.Arsivmi == false)
            {
                muhtac.Arsivmi = true;
                muhtac.YardimYapildimi = true;
                TempData["arsiveklendi"] = "Başarıyla arşive eklendi.";
                db.SaveChanges();
                return RedirectToAction("Index", "Muhtac");
            }
            else
            {
                muhtac.Arsivmi = false;
                muhtac.YardimYapildimi = false;
                TempData["arsivgeri"] = "Başarıyla arşivden geri alındı.";
                db.SaveChanges();
                return RedirectToAction("Arsiv", "Admin");
            }
        }
        public JsonResult YardimYap(string yardim, int? muhtacid)
        {
            var kullaniciid = Session["uyeid"];
            if (yardim != null)
            {
                db.Yardimlar.Add(new YardimDetay()
                {
                    KullaniciId = Convert.ToInt32(kullaniciid),
                    MuhtacId = Convert.ToInt32(muhtacid),
                    YapilanYardim = yardim,
                    Tarih = DateTime.Now
                });
                TempData["Yardimeklendi"] = "Yardımınız; ekibimiz tarafından onaylandıktan sonra kaydedilecektir.";
                db.SaveChanges();
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult YardimList(int? id)
        {
            var yardimlist = db.Yardimlar.Where(i => i.MuhtacId == id).ToList();
            return PartialView("_YorumList", yardimlist);
        }

        public JsonResult YorumYap(string yorum, int? muhtacid)
        {
            var kullaniciid = Session["uyeid"];
            if (yorum != null)
            {
                db.Yorumlar.Add(new Yorum()
                {
                    KullaniciId = Convert.ToInt32(kullaniciid),
                    MuhtacId = Convert.ToInt32(muhtacid),
                    YorumIcerik = yorum,
                    YorumTarihi = DateTime.Now
                });
                TempData["Yorumeklendi"] = "Yorumunuz kontrol edildikten sonra gözükecektir.";
                db.SaveChanges();
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult YorumSil(int? yorumid, int? muhtacid)
        {
            var uyeid = Session["uyeid"];
            var yorum = db.Yorumlar.Where(i => i.Id == yorumid).SingleOrDefault();
            var muhtac = db.Muhtaclar.Where(i => i.Id == yorum.KullaniciId).SingleOrDefault();
            if (yorumid != null && muhtacid != null)
            {
                if (yorum.KullaniciId == Convert.ToInt32(uyeid) || Convert.ToBoolean(Session["yetki"]) == true)
                {
                    db.Yorumlar.Remove(yorum);
                    db.SaveChanges();
                    TempData["Yorumsil"] = "Yorum Silindi";
                    return RedirectToAction("Details", "Muhtac", new { id = muhtacid });
                }
                else
                {
                    return HttpNotFound();
                }

            }
            else
            {
                TempData["Yorumsecmehata"] = "yorum seçiniz";
                return RedirectToAction("Details", "Muhtac", new { id = muhtac.Id });
            }
        }

        public PartialViewResult YorumList(int? id)
        {
            var yorumlist = db.Yorumlar.Where(i => i.MuhtacId == id).ToList();
            return PartialView("_YorumList", yorumlist);
        }

        public ActionResult OkunmaArttir(int muhtacid)
        {
            if (Convert.ToBoolean(Session["okundu"]) == false && Convert.ToBoolean(Session["yetki"]) == false)
            {
                var muhtac = db.Muhtaclar.Where(m => m.Id == muhtacid).SingleOrDefault();
                muhtac.Okunma += 1;
                Session["okundu"] = true;
            }
            db.SaveChanges();
            return View();
        }
        [GirisKontrolFiltresi]
        public ActionResult List(int? id, string q)
        {
            var muhtaclar = db.Muhtaclar
                        .Include(i => i.Adres).Include(i => i.Il)
                        .Where(i => i.AdminOnay == true)
                        .Where(i => i.Arsivmi == false)
                        .Where(i => i.YardimYapildimi == false).AsQueryable();
            Session["okundu"] = false;
            if (id != null)
            {
                if (id == 6)
                {
                    muhtaclar = muhtaclar.OrderByDescending(i => i.Aciliyet);
                }
                else
                {
                    muhtaclar = muhtaclar.Where(i => i.YardimTuru.Id == id);
                }
            }
            if (string.IsNullOrEmpty(q) == false)
            {
                muhtaclar = muhtaclar.Where(i => i.Baslik.Contains(q) || i.Aciklama.Contains(q));
            }

            return View(muhtaclar.ToList());
        }


        [GirisKontrolFiltresi]
        [HttpGet]
        //[Route("Home/Detay/{id}")]
        // GET: Muhtac/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var muhtac = db.Muhtaclar.Include(i => i.Il)
                                    .Include(i => i.Adres)
                                    .Include(i => i.YardimTuru)
                                    .Where(i => i.Id == id)
                                    .FirstOrDefault();

            if (muhtac == null)
            {
                return HttpNotFound();
            }
            return View(muhtac);
            // < a href = "https://www.google.com/maps/place/istanbul sultanbeyli göltepe sk. no 12" > normal map link</ a >

        }

        // GET: Muhtac/Create
        [GirisKontrolFiltresi]
        [HttpGet]
        public ActionResult Create()
        {

            List<SelectListItem> yardimturleri = (from i in db.YardimTurler.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = i.YardimTuruAdi.ToString(),
                                                      Value = i.Id.ToString()
                                                  }).ToList();
            ViewBag.YardimTurleri = yardimturleri;

            List<SelectListItem> iller = db.Iller.Select(p => new SelectListItem
            {
                Text = p.IlAdi.ToString(),
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
            var ilgelen = db.Iller.FirstOrDefault(p => p.Id == muhtac.Il.Id);
            string maplink = ilgelen.IlAdi + " " + muhtac.Adres.Ilce + " " + muhtac.Adres.AdresDetay;
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
                    Il = db.Iller.FirstOrDefault(p => p.Id == muhtac.Il.Id),
                    Okunma = 0,
                    GoogleMap = maplink
                };
                TempData["eklendi"] = "İhtiyaç Sahibi Eklendi";
                db.Muhtaclar.Add(yenimuhtac);
                db.SaveChanges();

                return RedirectToAction("Index", "Giris");
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


            return View(muhtac);
        }


        // GET: Muhtac/Edit/5
        //[Route("Giris/Edit/{id}")]
        [GirisKontrolFiltresi]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muhtac muhtac = db.Muhtaclar
                .Include(i => i.Adres)
                .Include(i => i.YardimTuru)
                .Include(i => i.Il)
                .FirstOrDefault(i => i.Id == id);
            //Muhtac muhtac = db.Muhtaclar.Find(id);
            if (muhtac == null)
            {
                return HttpNotFound();
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
            return View(muhtac);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [GirisKontrolFiltresi]
        public ActionResult Edit(Muhtac muhtac)
        {
            if (ModelState.IsValid)
            {
                var entity = db.Muhtaclar.Find(muhtac.Id);
                if (entity != null)
                {
                    entity.Baslik = muhtac.Baslik;
                    entity.Aciklama = muhtac.Aciklama;
                    entity.MuhtacAdiSoyadi = muhtac.MuhtacAdiSoyadi;
                    entity.Adres = muhtac.Adres;
                    entity.Il = db.Iller.FirstOrDefault(i => i.Id == muhtac.Il.Id);
                    entity.YardimTuru = db.YardimTurler.FirstOrDefault(i => i.Id == muhtac.YardimTuru.Id);
                    entity.Aciliyet = muhtac.Aciliyet;
                    entity.YardimYapildimi = muhtac.YardimYapildimi;
                    db.SaveChanges();
                    TempData["Duzenlendi"] = entity;
                    return RedirectToAction("Index", "Giris");
                }
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
            Muhtac muhtac = db.Muhtaclar.Include(i => i.Adres).Include(i => i.Il).FirstOrDefault(i => i.Id == id);
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
            return RedirectToAction("Index", "Giris");
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
