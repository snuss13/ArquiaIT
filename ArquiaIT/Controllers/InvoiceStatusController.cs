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
    [Authorize(Roles = "Administrator")]
    public class InvoiceStatusController : Controller
    {
        private ArquiaEntities db = new ArquiaEntities();

        // GET: InvoiceStatus
        public ActionResult Index()
        {
            return View(db.InvoiceStatus1.ToList());
        }

        // GET: InvoiceStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceStatus invoiceStatus = db.InvoiceStatus1.Find(id);
            if (invoiceStatus == null)
            {
                return HttpNotFound();
            }
            return View(invoiceStatus);
        }

        // GET: InvoiceStatus/Create
        public ActionResult Create()
        {
            var invoiceStatus = new InvoiceStatus();
            invoiceStatus.Active = true;

            return View(invoiceStatus);
        }

        // POST: InvoiceStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Active")] InvoiceStatus invoiceStatus)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceStatus1.Add(invoiceStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoiceStatus);
        }

        // GET: InvoiceStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceStatus invoiceStatus = db.InvoiceStatus1.Find(id);
            if (invoiceStatus == null)
            {
                return HttpNotFound();
            }
            return View(invoiceStatus);
        }

        // POST: InvoiceStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Active")] InvoiceStatus invoiceStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoiceStatus);
        }

        // GET: InvoiceStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceStatus invoiceStatus = db.InvoiceStatus1.Find(id);
            if (invoiceStatus == null)
            {
                return HttpNotFound();
            }
            return View(invoiceStatus);
        }

        // POST: InvoiceStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceStatus invoiceStatus = db.InvoiceStatus1.Find(id);
            db.InvoiceStatus1.Remove(invoiceStatus);
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
