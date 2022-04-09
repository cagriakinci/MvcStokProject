using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity; // modeli kullanacağız
namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DbMvcStokEntities db = new DbMvcStokEntities();
        // model sınıfının ismiyle bir nesne türeteceğiz ki vt tablolarına erişelim.
        public ActionResult Index()
        {
            var kategoriler = db.tblkategori.ToList();
            return View(kategoriler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(tblkategori p)
        {
            db.tblkategori.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var ktg = db.tblkategori.Find(id);
            db.tblkategori.Remove(ktg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.tblkategori.Find(id);
            return View("KategoriGetir", ktgr);
        }
        public ActionResult KategoriGuncelle(tblkategori k)
        {
            var ktg = db.tblkategori.Find(k.id); // göndermiş olduğum parametredeki k'ya göre bul.
            ktg.ad = k.ad; // soldaki değer tablodan gelen - sağdaki gönderdiğimiz değer
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}