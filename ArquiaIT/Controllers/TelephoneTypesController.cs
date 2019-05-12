using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquiaIT.Models.Business;

namespace ArquiaIT.Controllers
{
    public class TelephoneTypesController : Controller
    {
        private ArquiaEntities db = new ArquiaEntities();

        // GET: TelephoneTypes
        public ActionResult Index()
        {
            return View(db.TelephoneTypes.ToList());
        }

        // GET: TelephoneTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TelephoneType telephoneType = db.TelephoneTypes.Find(id);
            if (telephoneType == null)
            {
                return HttpNotFound();
            }
            return View(telephoneType);
        }

        // GET: TelephoneTypes/Create
        public ActionResult Create()
        {
            var telephoneType = new TelephoneType();

            telephoneType.Active = true;

            return View(telephoneType);
        }

        // POST: TelephoneTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Active")] TelephoneType telephoneType)
        {
            if (ModelState.IsValid)
            {
                db.TelephoneTypes.Add(telephoneType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(telephoneType);
        }

        // GET: TelephoneTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TelephoneType telephoneType = db.TelephoneTypes.Find(id);
            if (telephoneType == null)
            {
                return HttpNotFound();
            }
            return View(telephoneType);
        }

        // POST: TelephoneTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Active")] TelephoneType telephoneType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telephoneType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(telephoneType);
        }

        // GET: TelephoneTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TelephoneType telephoneType = db.TelephoneTypes.Find(id);
            if (telephoneType == null)
            {
                return HttpNotFound();
            }
            return View(telephoneType);
        }

        // POST: TelephoneTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TelephoneType telephoneType = db.TelephoneTypes.Find(id);
            db.TelephoneTypes.Remove(telephoneType);
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
