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
    public class SponsorsController : Controller
    {
        private FGEntities db = new FGEntities();

        public ActionResult Index()
        {
            ViewBag.Banner = db.FGAdmins.FirstOrDefault().SponsorsBanner;
            return View(db.Sponsors.OrderBy(x=>x.Orden).ToList());
        }


        public ActionResult SideBarView()
        {
            ViewBag.Banner = db.FGAdmins.FirstOrDefault().SponsorsBanner;
            return PartialView(db.Sponsors.OrderBy(x => x.Orden).ToList());
        }

        // GET: Sponsors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsor sponsor = db.Sponsors.Find(id);
            if (sponsor == null)
            {
                return HttpNotFound();
            }
            return View(sponsor);
        }

        // GET: Sponsors/Create
        [FGAuthorizeAdministrator]
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Sponsors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [FGAuthorizeAdministrator]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LogoPagina, LogoBarra,Titulo,Url, Descripcion, FacebookUrl, TwitterUrl, UploadImagePagina,UploadImageBarra,  Orden")] Sponsor sponsor)
        {

            if (sponsor.UploadImagePagina != null)
            {
                using (var reader = new BinaryReader(sponsor.UploadImagePagina.InputStream))
                {
                    sponsor.LogoPagina = Convert.ToBase64String(reader.ReadBytes(sponsor.UploadImagePagina.ContentLength));
                }
            }

            if (sponsor.UploadImageBarra != null)
            {
                using (var reader = new BinaryReader(sponsor.UploadImageBarra.InputStream))
                {
                    sponsor.LogoBarra = Convert.ToBase64String(reader.ReadBytes(sponsor.UploadImageBarra.ContentLength));
                }
            }
            
            if (ModelState.IsValid)
            {
                db.Sponsors.Add(sponsor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sponsor);
        }

        // GET: Sponsors/Edit/5
        [FGAuthorizeAdministrator]
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsor sponsor = db.Sponsors.Find(id);
            if (sponsor == null)
            {
                return HttpNotFound();
            }
            return View(sponsor);
        }

        // POST: Sponsors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [FGAuthorizeAdministrator]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LogoPagina, LogoBarra,Titulo,Url, Descripcion, FacebookUrl, TwitterUrl, UploadImagePagina,UploadImageBarra, Orden ")] Sponsor sponsor)
        {

            if (sponsor.UploadImagePagina != null)
            {
                using (var reader = new BinaryReader(sponsor.UploadImagePagina.InputStream))
                {
                    sponsor.LogoPagina = Convert.ToBase64String(reader.ReadBytes(sponsor.UploadImagePagina.ContentLength));
                }
            }

            if (sponsor.UploadImageBarra != null)
            {
                using (var reader = new BinaryReader(sponsor.UploadImageBarra.InputStream))
                {
                    sponsor.LogoBarra = Convert.ToBase64String(reader.ReadBytes(sponsor.UploadImageBarra.ContentLength));
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(sponsor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sponsor);
        }

        // GET: Sponsors/Delete/5
        [FGAuthorizeAdministrator]
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsor sponsor = db.Sponsors.Find(id);
            if (sponsor == null)
            {
                return HttpNotFound();
            }
            return View(sponsor);
        }

        // POST: Sponsors/Delete/5
        [HttpPost, ActionName("Delete")]
        [FGAuthorizeAdministrator]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sponsor sponsor = db.Sponsors.Find(id);
            db.Sponsors.Remove(sponsor);
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
