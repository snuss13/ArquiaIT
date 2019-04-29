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
    public class IntegrantesController : Controller
    {
        private FGEntities db = new FGEntities();

        // GET: Integrantes
        public ActionResult Index()
        {

            ViewBag.Banner = db.FGAdmins.FirstOrDefault().IntegrantesBanner; 
            return View(db.Integrantes.ToList());
        }

        // GET: Integrantes/Details/5
        [FGAuthorizeAdministrator]
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Integrante integrante = db.Integrantes.Find(id);
            if (integrante == null)
            {
                return HttpNotFound();
            }
            return View(integrante);
        }

        // GET: Integrantes/Create
        [FGAuthorizeAdministrator]
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Integrantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [FGAuthorizeAdministrator]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Foto, UploadImage,Nick,Email,FacebookLink,TwitterLink,TwitchTVLink,YouTubeLink,Divisiones")] Integrante integrante)
        {
            
            if (integrante.UploadImage != null)
            {
                using (var reader = new BinaryReader(integrante.UploadImage.InputStream))
                {
                    integrante.Foto = Convert.ToBase64String(reader.ReadBytes(integrante.UploadImage.ContentLength));
                }
            }

            if (ModelState.IsValid)
            {
                db.Integrantes.Add(integrante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(integrante);
        }

        // GET: Integrantes/Edit/5
        [FGAuthorizeAdministrator]
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Integrante integrante = db.Integrantes.Find(id);
            if (integrante == null)
            {
                return HttpNotFound();
            }
            return View(integrante);
        }

        // POST: Integrantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [FGAuthorizeAdministrator]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Foto,Nick,Email,FacebookLink,Foto, TwitterLink,TwitchTVLink,YouTubeLink,Divisiones,UploadImage")] Integrante integrante)
        {
            
            //BinaryReader b = new BinaryReader(integrante.UploadImage.InputStream);
            //byte[] binData = b.ReadBytes((int)integrante.UploadImage.InputStream.Length);
            //string result = System.Text.Encoding.UTF8.GetString(binData);
            

            if (integrante.UploadImage != null)
            {
                using (var reader = new BinaryReader(integrante.UploadImage.InputStream))
                {
                    integrante.Foto = Convert.ToBase64String(reader.ReadBytes(integrante.UploadImage.ContentLength));
                }
            }


            if (ModelState.IsValid)
            {
                db.Entry(integrante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(integrante);
        }

        // GET: Integrantes/Delete/5
        [FGAuthorizeAdministrator]
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Integrante integrante = db.Integrantes.Find(id);
            if (integrante == null)
            {
                return HttpNotFound();
            }
            return View(integrante);
        }

        // POST: Integrantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [FGAuthorizeAdministrator]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Integrante integrante = db.Integrantes.Find(id);
            db.Integrantes.Remove(integrante);
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
