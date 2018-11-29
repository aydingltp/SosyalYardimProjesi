using SYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace SYP.Controllers
{
    public class AdminController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Admin
        public ActionResult Index()
        {

            var muhtaclar = db.Muhtaclar.Include(p => p.Adres).Include(p => p.Il).Where(i => i.AdminOnay == false);
            // Include(p => p.Adres).Include(p => p.Il)
            return View(muhtaclar.ToList());
        }
    }
}