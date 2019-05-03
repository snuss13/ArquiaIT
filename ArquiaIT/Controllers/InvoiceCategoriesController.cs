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
    public class InvoiceCategoriesController : Controller
    {
        private ArquiaEntities db = new ArquiaEntities();

        // GET: InvoiceCategories
        public ActionResult Index()
        {
            return View(db.InvoiceCategories.ToList());
        }

        // GET: InvoiceCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceCategory invoiceCategory = db.InvoiceCategories.Find(id);
            if (invoiceCategory == null)
            {
                return HttpNotFound();
            }
            return View(invoiceCategory);
        }

        // GET: InvoiceCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvoiceCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Active")] InvoiceCategory invoiceCategory)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceCategories.Add(invoiceCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoiceCategory);
        }

        // GET: InvoiceCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceCategory invoiceCategory = db.InvoiceCategories.Find(id);
            if (invoiceCategory == null)
            {
                return HttpNotFound();
            }
            return View(invoiceCategory);
        }

        // POST: InvoiceCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Active")] InvoiceCategory invoiceCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoiceCategory);
        }

        // GET: InvoiceCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceCategory invoiceCategory = db.InvoiceCategories.Find(id);
            if (invoiceCategory == null)
            {
                return HttpNotFound();
            }
            return View(invoiceCategory);
        }

        // POST: InvoiceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceCategory invoiceCategory = db.InvoiceCategories.Find(id);
            db.InvoiceCategories.Remove(invoiceCategory);
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
