using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(int sayfa = 1)
        {
            //var musteriliste = db.tblmusteri.ToList();
            var musteriliste = db.tblmusteri.Where(x => x.durum == true).ToList().ToPagedList(sayfa, 3);
            return View(musteriliste);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(tblmusteri p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            p.durum = true; // Eklenecek müşterinin durumunu true yaptık. Bunu yapma sebebimiz vt'nına eklenip, sayfada listelenmemesi.
            db.tblmusteri.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSil(tblmusteri p)
        {
            var musteribul = db.tblmusteri.Find(p.id);
            musteribul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mus = db.tblmusteri.Find(id);
            return View("MusteriGetir", mus);
        }
        public ActionResult MusteriGuncelle(tblmusteri p)
        {
            var mus = db.tblmusteri.Find(p.id);
            mus.ad = p.ad;
            mus.soyad = p.soyad;
            mus.sehir = p.sehir;
            mus.bakiye = p.bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}