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

namespace FG.Controllers
{
    public class PublicidadesController : Controller
    {
        private FGEntities db = new FGEntities();

        // GET: Publicidades
        public ActionResult Index()
        {
            return View(db.Publicidads.ToList());
        }

        // GET: Publicidades/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicidad publicidad = db.Publicidads.Find(id);
            if (publicidad == null)
            {
                return HttpNotFound();
            }
            return View(publicidad);
        }

        // GET: Publicidades/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Publicidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Banner,Url, UploadImage")] Publicidad publicidad)
        {
            
            if(publicidad.UploadImage != null)
            {
                using (var reader = new BinaryReader(publicidad.UploadImage.InputStream))
                {
                    publicidad.Banner = Convert.ToBase64String(reader.ReadBytes(publicidad.UploadImage.ContentLength));
                }

            }
            
            if (ModelState.IsValid)
            {
                db.Publicidads.Add(publicidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publicidad);
        }

        // GET: Publicidades/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicidad publicidad = db.Publicidads.Find(id);
            if (publicidad == null)
            {
                return HttpNotFound();
            }
            return View(publicidad);
        }

        // POST: Publicidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Banner,Url,UploadImage")] Publicidad publicidad)
        {
            
            if (publicidad.UploadImage != null)
            {
                using (var reader = new BinaryReader(publicidad.UploadImage.InputStream))
                {
                    publicidad.Banner = Convert.ToBase64String(reader.ReadBytes(publicidad.UploadImage.ContentLength));
                }

            }
            if (ModelState.IsValid)
            {
                db.Entry(publicidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publicidad);
        }

        // GET: Publicidades/Delete/5
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicidad publicidad = db.Publicidads.Find(id);
            if (publicidad == null)
            {
                return HttpNotFound();
            }
            return View(publicidad);
        }

        // POST: Publicidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Publicidad publicidad = db.Publicidads.Find(id);
            db.Publicidads.Remove(publicidad);
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
