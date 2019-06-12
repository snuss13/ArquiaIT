using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquiaIT.Models.Business;
using ArquiaIT.BusinessRules;

namespace ArquiaIT.Controllers
{
    [Authorize]
    public class PurchaseOrdersController : Controller
    {
        private ArquiaEntities db = new ArquiaEntities();

        // GET: PurchaseOrders
        public ActionResult Index()
        {
            var purchaseOrders = db.PurchaseOrders.Include(p => p.Client);
            return View(purchaseOrders.ToList());
        }

        // GET: PurchaseOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Client, "Id", "Name", purchaseOrder.ClientID);

            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Client, "Id", "Name");
            ViewBag.ClientList = new SelectList(db.Client.Where(x => x.Active == true).OrderBy(x => x.Name).ToList(), "ID", "Name");
            var po = new PurchaseOrder();
            po.Client = new Client();
            po.ChangeRate = BusinessConfiguration.getLastChangeRate();
            return View(po);
        }

        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientID,PONumber,Date,IsDolar,ChangeRate")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseOrders.Add(purchaseOrder);
                db.SaveChanges();

                if (purchaseOrder.ChangeRate.HasValue)
                    BusinessConfiguration.UpdateChangeRate(purchaseOrder.ChangeRate.Value);

                return RedirectToAction("Edit", new { id = purchaseOrder.Id});
            }

            ViewBag.ClientID = new SelectList(db.Client, "Id", "Name", purchaseOrder.ClientID);
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }

            ViewBag.ClientID = new SelectList(db.Client, "Id", "Name", purchaseOrder.ClientID);
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientID,PONumber,Date,IsDolar,ChangeRate")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOrder).State = EntityState.Modified;
                db.SaveChanges();

                if (purchaseOrder.ChangeRate.HasValue)
                    BusinessConfiguration.UpdateChangeRate(purchaseOrder.ChangeRate.Value);

                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Client, "Id", "Name", purchaseOrder.ClientID);
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Client, "Id", "Name", purchaseOrder.ClientID);
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            db.PurchaseOrders.Remove(purchaseOrder);
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
