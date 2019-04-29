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
    public class CarruselsController : Controller
    {
        private FGEntities db = new FGEntities();

        // GET: Carrusels
        public ActionResult Index()
        {
            

            return View(db.Carrusels.ToList());
        }

        // GET: Carrusels/Details/5
        [FGAuthorizeAdministrator]
        public ActionResult Details(int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrusel carrusel = db.Carrusels.Find(id);
            if (carrusel == null)
            {
                return HttpNotFound();
            }
            return View(carrusel);
        }

        // GET: Carrusels/Create
        public ActionResult Create()
        {
            

            return View();
        }

        // POST: Carrusels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [FGAuthorizeAdministrator]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Imagen,Leyenda,Url, UploadImage")] Carrusel carrusel)
        {
            

            if (carrusel.UploadImage != null)
            {
                using (var reader = new BinaryReader(carrusel.UploadImage.InputStream))
                {
                    carrusel.Imagen = Convert.ToBase64String(reader.ReadBytes(carrusel.UploadImage.ContentLength));
                }
            }

            if (ModelState.IsValid)
            {
                db.Carrusels.Add(carrusel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carrusel);
        }

        // GET: Carrusels/Edit/5
        public ActionResult Edit(int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrusel carrusel = db.Carrusels.Find(id);
            if (carrusel == null)
            {
                return HttpNotFound();
            }
            return View(carrusel);
        }

        // POST: Carrusels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [FGAuthorizeAdministrator]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Imagen,Leyenda,Url,  UploadImage")] Carrusel carrusel)
        {
            

            if (carrusel.UploadImage != null)
            {
                using (var reader = new BinaryReader(carrusel.UploadImage.InputStream))
                {
                    carrusel.Imagen = Convert.ToBase64String(reader.ReadBytes(carrusel.UploadImage.ContentLength));
                }
            }



            if (ModelState.IsValid)
            {
                db.Entry(carrusel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carrusel);
        }

        // GET: Carrusels/Delete/5
        [FGAuthorizeAdministrator]
        public ActionResult Delete(int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrusel carrusel = db.Carrusels.Find(id);
            if (carrusel == null)
            {
                return HttpNotFound();
            }
            return View(carrusel);
        }

        // POST: Carrusels/Delete/5
        [HttpPost, ActionName("Delete")]
        [FGAuthorizeAdministrator]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carrusel carrusel = db.Carrusels.Find(id);
            db.Carrusels.Remove(carrusel);
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
