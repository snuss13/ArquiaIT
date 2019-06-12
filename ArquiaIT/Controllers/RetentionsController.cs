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
    public class RetentionsController : Controller
    {
        private ArquiaEntities db = new ArquiaEntities();

        // GET: Retentions
        public ActionResult Index()
        {
            var retention = db.Retention.Include(r => r.Invoice);
            return View(retention.ToList());
        }

        // GET: Retentions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retention retention = db.Retention.Find(id);
            if (retention == null)
            {
                return HttpNotFound();
            }
            return View(retention);
        }

        // GET: Retentions/Create
        public ActionResult Create(int invoiceID)
        {
            Retention ret = new Retention();
            Invoice inv = db.Invoices.FirstOrDefault(x => x.Id == invoiceID);

            ret.InvoiceID = invoiceID;
            if (inv != null)
            {
                ret.Ganancias = inv.ValueInPesos * decimal.Parse("0,02");

                ret.IIBB = inv.ValueInPesos * decimal.Parse("0,03");
                ret.IIBBSpends = inv.ValueInPesos * decimal.Parse("0,01");
            }

            return View(ret);
        }

        // POST: Retentions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InvoiceID,Ganancias,IVA,SUSS,IIBB,IIBBSpends,Others")] Retention retention)
        {
            if (ModelState.IsValid)
            {
                db.Retention.Add(retention);
                db.SaveChanges();
                return RedirectToAction("Index", "Invoices");
            }

            ViewBag.InvoiceID = new SelectList(db.Invoices, "Id", "InvoiceNumber", retention.InvoiceID);
            return View(retention);
        }

        // GET: Retentions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retention retention = db.Retention.Find(id);
            if (retention == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "Id", "InvoiceNumber", retention.InvoiceID);
            return View(retention);
        }

        // POST: Retentions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InvoiceID,Ganancias,IVA,SUSS,IIBB,IIBBSpends,Others")] Retention retention)
        {
            if (ModelState.IsValid)
            {
                db.Entry(retention).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "Id", "InvoiceNumber", retention.InvoiceID);
            return View(retention);
        }

        // GET: Retentions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retention retention = db.Retention.Find(id);
            if (retention == null)
            {
                return HttpNotFound();
            }
            return View(retention);
        }

        // POST: Retentions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Retention retention = db.Retention.Find(id);
            db.Retention.Remove(retention);
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
