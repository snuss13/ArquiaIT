using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FG.Models;
using System.IO;
using System.Data.Entity.Validation;

namespace FG.Controllers
{
    public class NoticiasController : Controller
    {
        private FGEntities db = new FGEntities();


        public ActionResult Index()
        {
            ViewBag.Banner = db.FGAdmins.FirstOrDefault().NoticiasBanner;
            return View(db.Noticias.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticia noticia = db.Noticias.Find(id);
            if (noticia == null)
            {
                return HttpNotFound();
            }
            return View(noticia);
        }

        [FGAuthorizeAdministratorOrColaborador]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [FGAuthorizeAdministratorOrColaborador]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Volanta,Titulo,Subtitulo,Pie,Cuerpo, UploadImagePortada, UploadImageNoticia")] Noticia noticia)
        {
            noticia.TimeStamp = DateTime.Now; 
            if (noticia.UploadImagePortada != null)
            {
                using (var reader = new BinaryReader(noticia.UploadImagePortada.InputStream))
                {
                    noticia.ImagenPortada = Convert.ToBase64String(reader.ReadBytes(noticia.UploadImagePortada.ContentLength));
                }
            }

            if (noticia.UploadImageNoticia != null)
            {
                using (var reader = new BinaryReader(noticia.UploadImageNoticia.InputStream))
                {
                    noticia.ImagenNoticia = Convert.ToBase64String(reader.ReadBytes(noticia.UploadImageNoticia.ContentLength));
                }
            }

            if (ModelState.IsValid)
            {
                db.Noticias.Add(noticia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(noticia);
        }

        [FGAuthorizeAdministratorOrColaborador]
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticia noticia = db.Noticias.Find(id);
            if (noticia == null)
            {
                return HttpNotFound();
            }
            return View(noticia);
        }

        [FGAuthorizeAdministratorOrColaborador]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Volanta,Titulo,Subtitulo,ImagenPortada, ImagenNoticia,Pie,Cuerpo, UploadImagePortada, UploadImageNoticia")] Noticia noticia)
        {
            if (noticia.UploadImagePortada != null)
            {
                using (var reader = new BinaryReader(noticia.UploadImagePortada.InputStream))
                {
                    noticia.ImagenPortada = Convert.ToBase64String(reader.ReadBytes(noticia.UploadImagePortada.ContentLength));
                }
            }

            if (noticia.UploadImageNoticia != null)
            {
                using (var reader = new BinaryReader(noticia.UploadImageNoticia.InputStream))
                {
                    noticia.ImagenNoticia = Convert.ToBase64String(reader.ReadBytes(noticia.UploadImageNoticia.ContentLength));
                }
            }

            noticia.TimeStamp = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(noticia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noticia);
        }

        [FGAuthorizeAdministratorOrColaborador]
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticia noticia = db.Noticias.Find(id);
            if (noticia == null)
            {
                return HttpNotFound();
            }
            return View(noticia);
        }

        [FGAuthorizeAdministratorOrColaborador]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Noticia noticia = db.Noticias.Find(id);
            db.Noticias.Remove(noticia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
