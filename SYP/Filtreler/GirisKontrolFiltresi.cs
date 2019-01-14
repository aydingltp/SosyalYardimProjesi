using SYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SYP.Filtreler
{
    public class GirisKontrolFiltresi : FilterAttribute, IActionFilter
    {
        DataContext db = new DataContext();
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if(filterContext.HttpContext.Session["uyeid"]!=null)
            {
                int? kullaniciId = Convert.ToInt32(filterContext.HttpContext.Session["uyeid"].ToString());
                if(kullaniciId!=null)
                {
                    var kullanici = db.Kullanicilar.FirstOrDefault(p => p.Id == kullaniciId);
                    if(kullanici==null)
                    {
                        filterContext.Controller.TempData["hata"] = "Oturum zaman aşımına uğradı.";
                        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Login" } });
                    }
                }
                else
                {
                    filterContext.Controller.TempData["hata"] = "Oturum zaman aşımına uğradı.";
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Login" } });
                }
            }
            else
            {
                if(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName=="Home"
                    && filterContext.ActionDescriptor.ActionName== "Create")
                {
                    filterContext.Controller.TempData["hata"] = "Giriş yapmanız gerekmektedir.";
                }
                else
                {
                    filterContext.Controller.TempData["hata"] = "Oturum zaman aşımına uğradı.";
                }
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Login" } });
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["uyeid"] != null)
            {
                int? kullaniciId = Convert.ToInt32(filterContext.HttpContext.Session["uyeid"].ToString());
                if (kullaniciId != null)
                {
                    var kullanici = db.Kullanicilar.FirstOrDefault(p => p.Id == kullaniciId);
                    if (kullanici == null)
                    {
                        filterContext.Controller.TempData["hata"] = "Oturum zaman aşımına uğradı.";
                        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Login" } });
                    }
                }
                else
                {
                    filterContext.Controller.TempData["hata"] = "Oturum zaman aşımına uğradı.";
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Login" } });
                }
            }
            else
            {
                if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "Home"
                    && filterContext.ActionDescriptor.ActionName == "Create")
                {
                    filterContext.Controller.TempData["hata"] = "Giriş yapmanız gerekmektedir.";
                }
                else
                {
                    filterContext.Controller.TempData["hata"] = "Giriş yapmanız gerekmektedir.";
                }
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Login" } });
            }
        }
    }
}