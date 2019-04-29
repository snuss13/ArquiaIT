using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FG.Models;

namespace FG.Controllers
{
    public class InstitucionalController : Controller
    {
        private FGEntities db = new FGEntities();


        public ActionResult Index()
        {
            ViewBag.Institucional = db.FGAdmins.First().Institucional;
            return PartialView();
        }

        
        [FGAuthorizeAdministrator]
        public ActionResult Edit()
        {
            var institucional = new Institucional();
            institucional.Texto = db.FGAdmins.First().Institucional;
            return View(institucional);
        }

        // POST: Institucional/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [FGAuthorizeAdministrator]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Texto")] Institucional institucional)
        {
            var fgAdmin = db.FGAdmins.First();
            fgAdmin.Institucional = institucional.Texto;

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(institucional);
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
