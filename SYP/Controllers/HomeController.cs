using SYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using SYP.Filtreler;
using SYP.Models.viewModel;

namespace SYP.Controllers
{
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            //List<Kullanici> kullanicilar = db.Kullanicilar.ToList();*****
            var muhtaclar = db.Muhtaclar.Include(p => p.Adres).Include(p => p.Il).Where(i=>i.AdminOnay==true).ToList();
            return View(muhtaclar);
        }

            
        public ActionResult Portal()
        {
            return View();
        }

        public PartialViewResult bagisIstatistik()
        {
            var yardimsayisi = db.Muhtaclar.Where(i => i.AdminOnay == true);

            bagisSayilariPartialModel models = new bagisSayilariPartialModel()
            {
                yapilacakyardim = yardimsayisi.Count(i => i.YardimYapildimi == false),
                yapilanyardim = yardimsayisi.Count(i => i.YardimYapildimi == true)
            };


            return PartialView("bagisIstatistik",models);
        }
        public PartialViewResult bagisSayilari()
        {
            var muhtaclar = db.Muhtaclar.Where(i => i.AdminOnay == true);
            bagisYapPartialModel models = new bagisYapPartialModel()
            {
                Acil = muhtaclar.Count(p => p.Aciliyet == 5),
                Egitim = muhtaclar.Count(p => p.YardimTuru.Id ==1 ),
                Gida = muhtaclar.Count(p => p.YardimTuru.Id==4),
                Giyim = muhtaclar.Count(p => p.YardimTuru.Id == 5),
                Maddi = muhtaclar.Count(p => p.YardimTuru.Id == 2),
                Saglik = muhtaclar.Count(p => p.YardimTuru.Id == 3)
            };
            return PartialView("bagisSayilari", models);
        }


    }
}