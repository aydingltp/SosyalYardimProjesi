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

        //public ActionResult List(int? id, string q)
        //{
        //    var muhtaclar = db.Muhtaclar.Where(i=>i.AdminOnay==true).AsQueryable();

        //    if (id != null)
        //    {
        //        if (id == 6)
        //        {
        //            muhtaclar = db.Muhtaclar.Where(i => i.Aciliyet == 5).Where(i => i.AdminOnay == true);
        //        }
        //        else
        //        {
        //            muhtaclar = db.Muhtaclar.Where(i => i.YardimTuru.Id == id).Where(i => i.AdminOnay == true);
        //        }
        //    }
        //    if (string.IsNullOrEmpty(q) == false)
        //    {
        //        muhtaclar = muhtaclar.Where(i => i.Baslik.Contains(q) || i.Aciklama.Contains(q));
        //    }

        //    return View(muhtaclar.Include(i => i.Adres).Include(i => i.Il).ToList());
        //}

       


        public ActionResult Portal()
        {
            return View();
        }




    }
}