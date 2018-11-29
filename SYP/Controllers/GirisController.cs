using SYP.Models;
using SYP.Models.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SYP.Filtreler;
using System.Net;

namespace SYP.Controllers
{
    public class GirisController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Giris
        [GirisKontrolFiltresi]
        public ActionResult Index()
        {
            int? kullaniciId = (Int32)Session["uyeid"];
            var muhtaclar = db.Muhtaclar.Include(p => p.Adres).Include(p => p.Il)
            .Where(b => b.Kullanici.Id == kullaniciId);
            return View(muhtaclar.ToList());
        }


        [HttpGet]
        [Route("Giris/Edit/{id}")]
        public ActionResult MuhtacDuzenle(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muhtac muhtac = db.Muhtaclar.Find(id);
            if (muhtac==null)
            {
                return HttpNotFound();
            }

            return View(muhtac);
        }
        [Route("Giris/Edit/{id}")]
        [HttpPost]
        public ActionResult MuhtacDuzenle(Muhtac muhtac)
        {
            if (ModelState.IsValid)
            {
                var entity = db.Muhtaclar.Find(muhtac.Id);
                if (entity!=null)
                {
                    entity.Baslik = muhtac.Baslik;
                    entity.Aciklama = muhtac.Aciklama;
                    entity.MuhtacAdiSoyadi = muhtac.MuhtacAdiSoyadi;
                    entity.Adres = muhtac.Adres;
                    entity.Il = muhtac.Il;
                    entity.YardimTuru = muhtac.YardimTuru;
                    entity.Aciliyet = muhtac.Aciliyet;
                    entity.YardimYapildimi = muhtac.YardimYapildimi;
                    db.SaveChanges();
                    TempData["Duzenlendi"] = entity;
                    return RedirectToAction("Index");
                }
            }

            return View(muhtac);
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(loginModel kullanici)
        {           
            if (ModelState.IsValid)
            {
                var login = db.Kullanicilar.FirstOrDefault(p => p.Cep == kullanici.Tel && p.Sifre == kullanici.Sifre);
                if (login!=null)
                {
                    Session["uyeid"] = login.Id;
                    Session["kullaniciadi"] = login.Isim;
                    Session["yetki"] = login.Adminmi;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                   TempData["hata"] = "Telefon Numarası veya Şifre Yanlış.";
                    //TempData["LoginHata"] = "Cep Telefonu veya Şifre Yanlış.";
                    //ModelState.AddModelError(string.Empty, "Cep Telefonu veya Şifre Yanlış.");
                }
            }
            return View(kullanici);

        }

        public ActionResult Logout()
        {
            Session["uyeid"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                db.Kullanicilar.Add(kullanici);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(kullanici);
        }
    }
}