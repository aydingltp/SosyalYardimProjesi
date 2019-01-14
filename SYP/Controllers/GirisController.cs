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
                var login = db.Kullanicilar.FirstOrDefault(p => p.Tel == kullanici.Tel && p.Sifre == kullanici.Sifre);
                if (login != null)
                {
                    Session["uyeid"] = login.Id;
                    Session["kullaniciadi"] = login.KullaniciAdi;
                    Session["yetki"] = login.Adminmi;
                    Session["okundu"] = false;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["hata"] = "Telefon Numarası veya Şifre Yanlış.";
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
            var kullanicivarmi = db.Kullanicilar.Where(i => i.Tel == kullanici.Tel).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if ( kullanicivarmi == null)
                {
                    db.Kullanicilar.Add(kullanici);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Hata = "Bu telefona sahip kullanıcı zaten var!";
                }
                
            }
            return View(kullanici);
        }
    }
}