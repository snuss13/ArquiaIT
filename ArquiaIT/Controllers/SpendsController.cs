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
    public class SpendsController : Controller
    {
        private ArquiaEntities db = new ArquiaEntities();

        // GET: Spends
        public ActionResult Index()
        {
            var spend = db.Spend;
            return View(spend.ToList());
        }
        
        // GET: Spends
        public ActionResult InvoiceSpends(int invoiceID)
        {
            var spend = db.Spend.Where(x => x.InvoiceID == invoiceID).Include(s => s.Invoice);
            return View(spend.ToList());
        }

        // GET: Spends/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spend spend = db.Spend.Find(id);
            if (spend == null)
            {
                return HttpNotFound();
            }
            return View(spend);
        }

        // GET: Spends/Create
        public ActionResult Spends(string value, string otherCharges)
        {
            var spend = new Spend();

            decimal invoiceValue = 0;
            decimal.TryParse(value, out invoiceValue);

            decimal otherTaxes = 0;
            decimal.TryParse(otherCharges, out otherTaxes);

            spend.InvoiceValue = invoiceValue;
            spend.OtherTaxes = otherTaxes;
            spend.IVA = spend.InvoiceValue * decimal.Parse("0,21");
            spend.IIBB_ARBA = spend.InvoiceValue * decimal.Parse("0,05");
            spend.ARCIBA = spend.InvoiceValue * decimal.Parse("0,035");
            spend.InvoiceTotal = spend.InvoiceValue + spend.IVA + spend.IIBB_ARBA.Value + spend.ARCIBA.Value + spend.OtherTaxes.Value;

            return PartialView(spend);
        }

        // GET: Spends/Create
        public ActionResult Create()
        {
            //ViewBag.InvoiceID = new SelectList(db.Invoices, "Id", "InvoiceNumber");
            var spend = new Spend();

            spend.InvoiceDate = DateTime.Now.Date;
            spend.InvoiceID = null;
            
            return View(spend);
        }

        // POST: Spends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InvoiceID,Vendor,Description,InvoiceNumber,InvoiceDate,POValueInDollars,InvoiveValueInDollars,InvoiceValue,IVA,IIBB_ARBA,ARCIBA,InvoiceTotal,PayDate,OtherTaxes")] Spend spend)
        {
            if (ModelState.IsValid)
            {
                db.Spend.Add(spend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceID = new SelectList(db.Invoices, "Id", "InvoiceNumber", spend.InvoiceID);
            return View(spend);
        }

        // GET: Spends/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spend spend = db.Spend.Find(id);
            if (spend == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "Id", "InvoiceNumber", spend.InvoiceID);
            return View(spend);
        }

        // POST: Spends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InvoiceID,Vendor,Description,InvoiceNumber,InvoiceDate,POValueInDollars,InvoiveValueInDollars,InvoiceValue,IVA,IIBB_ARBA,ARCIBA,InvoiceTotal,PayDate,OtherTaxes")] Spend spend)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spend).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "Id", "InvoiceNumber", spend.InvoiceID);
            return View(spend);
        }

        // GET: Spends/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spend spend = db.Spend.Find(id);
            if (spend == null)
            {
                return HttpNotFound();
            }
            return View(spend);
        }

        // POST: Spends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spend spend = db.Spend.Find(id);
            db.Spend.Remove(spend);
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
