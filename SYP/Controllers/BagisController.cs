using SYP.Filtreler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SYP.Controllers
{
    public class BagisController : Controller
    {


        // GET: Bagis
        [GirisKontrolFiltresi]
        public ActionResult Index()
        {
            return View();
        }
    }
}