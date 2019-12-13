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
    public class SpendTypesController : Controller
    {
        private ArquiaEntities db = new ArquiaEntities();

        // GET: SpendTypes
        public ActionResult Index()
        {
            return View(db.SpendType.ToList());
        }

        // GET: SpendTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpendType spendType = db.SpendType.Find(id);
            if (spendType == null)
            {
                return HttpNotFound();
            }
            return View(spendType);
        }

        // GET: SpendTypes/Create
        public ActionResult Create()
        {
            var spendType = new SpendType();
            spendType.active = true;

            return View(spendType);
        }

        // POST: SpendTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,active")] SpendType spendType)
        {
            if (ModelState.IsValid)
            {
                db.SpendType.Add(spendType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(spendType);
        }

        // GET: SpendTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpendType spendType = db.SpendType.Find(id);
            if (spendType == null)
            {
                return HttpNotFound();
            }
            return View(spendType);
        }

        // POST: SpendTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,active")] SpendType spendType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spendType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(spendType);
        }

        // GET: SpendTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpendType spendType = db.SpendType.Find(id);
            if (spendType == null)
            {
                return HttpNotFound();
            }
            return View(spendType);
        }

        // POST: SpendTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SpendType spendType = db.SpendType.Find(id);
            db.SpendType.Remove(spendType);
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
