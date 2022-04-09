using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index(string p)
        {
            //var urunler = db.tblurunler.Where(x => x.durum == true).ToList();
            // Arama özelliği
            var urunler = db.tblurunler.Where(x=>x.durum == true);
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.ad.Contains(p) && x.durum == true);
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktg = (from x in db.tblkategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()
                                        }
                                            ).ToList();
            ViewBag.dgr = ktg;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(tblurunler p)
        {
            var ktgr = db.tblkategori.Where(x => x.id == p.tblkategori.id).FirstOrDefault();
            p.tblkategori = ktgr;
            db.tblurunler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kat = (from x in db.tblkategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()
                                        }).ToList();
            var ktgr = db.tblurunler.Find(id);
            ViewBag.urunkategori = kat;
            return View("UrunGetir", ktgr);
        }
        public ActionResult UrunGuncelle(tblurunler p)
        {
            var urun = db.tblurunler.Find(p.id);
            urun.marka = p.marka;
            urun.satisfiyat = p.satisfiyat;
            urun.stok = p.stok;
            urun.alisfiyat = p.alisfiyat;
            urun.ad = p.ad;
            var ktg = db.tblkategori.Where(x => x.id == p.tblkategori.id).FirstOrDefault();
            urun.kategori = ktg.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(tblurunler p)
        {
            var urunbul = db.tblurunler.Find(p.id);
            urunbul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}