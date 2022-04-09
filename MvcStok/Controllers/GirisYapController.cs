using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using System.Web.Security;

namespace MvcStok.Controllers
{
    public class GirisYapController : Controller
    {
        // GET: GirisYap
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(tbladmin p)
        {
            var bilgiler = db.tbladmin.FirstOrDefault(x => x.kullanici == p.kullanici && x.sifre == p.sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.kullanici,false);
                return RedirectToAction("Index","Musteri");
            }
            else
            {
                return View();
            }
            return View();
        }
    }
}