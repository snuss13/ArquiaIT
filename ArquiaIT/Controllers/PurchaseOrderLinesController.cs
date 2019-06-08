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
    [Authorize]
    public class PurchaseOrderLinesController : Controller
    {
        private ArquiaEntities db = new ArquiaEntities();

        // GET: PurchaseOrderLines
        public ActionResult Index()
        {
            var purchaseOrderLines = db.PurchaseOrderLines.Include(p => p.PurchaseOrder);
            return View(purchaseOrderLines.ToList());
        }

        // GET: PurchaseOrderLines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderLine purchaseOrderLine = db.PurchaseOrderLines.Find(id);
            if (purchaseOrderLine == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrderLine);
        }

        // GET: PurchaseOrderLines/Create
        public ActionResult Create(int poID)
        {
            ViewBag.POID = poID;
            return View();
        }

        // POST: PurchaseOrderLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,POID,Description,ValueInDollars,Value")] PurchaseOrderLine purchaseOrderLine)
        {
            if (ModelState.IsValid)
            {
                if (purchaseOrderLine.PurchaseOrder.IsDolar)
                {
                    purchaseOrderLine.ValueInDollars = purchaseOrderLine.Value * purchaseOrderLine.PurchaseOrder.ChangeRate;
                }
                                        
                db.PurchaseOrderLines.Add(purchaseOrderLine);
                db.SaveChanges();
                return RedirectToAction("Edit", "PurchaseOrders", new { Id = purchaseOrderLine.POID});
            }

            ViewBag.POID = new SelectList(db.PurchaseOrders, "Id", "PONumber", purchaseOrderLine.POID);
            return View(purchaseOrderLine);
        }

        // GET: PurchaseOrderLines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderLine purchaseOrderLine = db.PurchaseOrderLines.Find(id);
            if (purchaseOrderLine == null)
            {
                return HttpNotFound();
            }
            ViewBag.POID = new SelectList(db.PurchaseOrders, "Id", "PONumber", purchaseOrderLine.POID);
            return View(purchaseOrderLine);
        }

        // POST: PurchaseOrderLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,POID,Description,ValueInDollars,Value")] PurchaseOrderLine purchaseOrderLine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOrderLine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "PurchaseOrders", new { Id = purchaseOrderLine.POID });
            }
            ViewBag.POID = new SelectList(db.PurchaseOrders, "Id", "PONumber", purchaseOrderLine.POID);
            return View(purchaseOrderLine);
        }

        // GET: PurchaseOrderLines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderLine purchaseOrderLine = db.PurchaseOrderLines.Find(id);
            if (purchaseOrderLine == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrderLine);
        }

        // POST: PurchaseOrderLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrderLine purchaseOrderLine = db.PurchaseOrderLines.Find(id);
            db.PurchaseOrderLines.Remove(purchaseOrderLine);
            db.SaveChanges();
            return RedirectToAction("Edit", "PurchaseOrders", new { Id = purchaseOrderLine.POID });
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
