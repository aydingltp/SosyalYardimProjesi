using SYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using SYP.Models.viewModel;
using SYP.Filtreler;

namespace SYP.Controllers
{
    public class AdminController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Admin

        [GirisKontrolFiltresi]
        public ActionResult Index(int? id, string q)
        {
            var muhtaclar = db.Muhtaclar
                            .Include(p=>p.Kullanici)
                            .Include(p => p.Adres)
                            .Include(p => p.Il).Include(i=>i.YardimTuru)
                            .OrderBy(i=>i.AdminOnay).AsQueryable();

            if (id != null)
            {
                List<onayCheckModel> onay = new List<onayCheckModel>();
                muhtacVeOnayModel muhtacVeOnay = new muhtacVeOnayModel();
                muhtacVeOnay.Muhtacs = db.Muhtaclar.OrderByDescending(i => i.Aciliyet).ToList();
                for (int i = 0; i < muhtacVeOnay.Muhtacs.Count(); i++)
                {
                    if (muhtacVeOnay.Muhtacs[i].AdminOnay)
                    {
                        onay.Add(new onayCheckModel()
                        {
                            Id = muhtacVeOnay.Muhtacs[i].Id,
                            OnaylandiMi = true
                        });
                    }
                    else
                    {
                        onay.Add(new onayCheckModel()
                        {
                            Id = muhtacVeOnay.Muhtacs[i].Id,
                            OnaylandiMi = false
                        });
                    }
                }
                muhtacVeOnay.onayCheckModels = onay;

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

            var listeMuhtac = muhtaclar.ToList();
            return View(listeMuhtac);
        }

        [HttpPost]
        public ActionResult Kaydet(List<Muhtac> models)
        {
            for (int i = 0; i < models.Count; i++)
            {
                int id = models[i].Id;
                var muhtac = db.Muhtaclar.FirstOrDefault(p => p.Id == id);
                muhtac.AdminOnay = models[i].AdminOnay;

            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [GirisKontrolFiltresi]
        public ActionResult Kullanicilar()
        {
            var kullanicilar = db.Kullanicilar.Where(i=>i.Adminmi==false).ToList();
            return View(kullanicilar);
        }

        [GirisKontrolFiltresi]
        public ActionResult Yardimlar()
        {
            var yardimlar = db.Yardimlar.ToList();
            return View(yardimlar);
        }
         public ActionResult YardimOnayi(int id)
        {
            var yardimdetay = db.Yardimlar.Where(i => i.Id == id).FirstOrDefault();
            if (yardimdetay.Onay == false)
            {
                yardimdetay.Onay = true;
                db.SaveChanges();
            }
            else
            {
                yardimdetay.Onay = false;
                db.SaveChanges();
            }
            return RedirectToAction("Yardimlar");
        }


    }
}