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
using System.IO;

namespace ArquiaIT.Controllers
{
    public class InvoicesController : Controller
    {
        private ArquiaEntities db = new ArquiaEntities();

        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(i => i.InvoiceCategory).Include(i => i.PurchaseOrderLine).Include(i => i.InvoiceStatus);
            var lista = invoices.OrderByDescending(x => x.InvoiceNumber).ToList();
            return View(lista);
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id, string src)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.Source = src;
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create(int POLineId)
        {
            var statusId = db.InvoiceStatus1.FirstOrDefault(x => x.Description == "Facturado").Id;
            ViewBag.CategoryID = new SelectList(db.InvoiceCategories, "Id", "Description");
            ViewBag.StatusID = new SelectList(db.InvoiceStatus1, "Id", "Description", statusId);
            var inv = new Invoice();

            var poLine = db.PurchaseOrderLines.FirstOrDefault(x => x.Id == POLineId);
            if (poLine != null)
            {
                inv.ChangeRate = poLine.PurchaseOrder.ChangeRate;
                inv.ValueInDollars = poLine.ValueInDollars;
                inv.ValueInPesos = poLine.ValueInPesos.Value;
                inv.StatusID = statusId;
                inv.IVA = inv.ValueInPesos * decimal.Parse("0,21");
                inv.InvoiceTotal = inv.ValueInPesos + inv.IVA;
                inv.InvoiceDate = DateTime.Now.Date;
            }

            inv.POLineID = POLineId;
            return View(inv);
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,POLineID,CategoryID,StatusID,InvoiceDate,InvoiceNumber,ValueInPesos,ValueInDollars,IVA,ChangeRate,InvoiceTotal,PayDate")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                if (invoice.PayDate.HasValue)
                    invoice.StatusID = db.InvoiceStatus1.FirstOrDefault(x => x.Description == "Cobrado").Id;

                db.Invoices.Add(invoice);

                db.SaveChanges();

                if (invoice.ChangeRate.HasValue)
                    BusinessConfiguration.UpdateChangeRate(invoice.ChangeRate.Value);

                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.InvoiceCategories, "Id", "Description", invoice.CategoryID);
            ViewBag.StatusID = new SelectList(db.InvoiceStatus1, "Id", "Description", invoice.StatusID);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id, string src)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);

            if (invoice.InvoiceStatus.Description == "Cobrado" && !User.IsInRole("Administrator"))
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientList = new SelectList(db.Client.Where(x => x.Active == true).OrderBy(x => x.Name).ToList(), "ID", "Name");
            ViewBag.CategoryID = new SelectList(db.InvoiceCategories, "Id", "Description", invoice.CategoryID);
            ViewBag.POLineID = new SelectList(db.PurchaseOrderLines, "Id", "Description", invoice.POLineID);
            ViewBag.StatusID = new SelectList(db.InvoiceStatus1, "Id", "Description", invoice.StatusID);
            ViewBag.Source = src;
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,POLineID,CategoryID,StatusID,InvoiceDate,InvoiceNumber,ValueInPesos,ValueInDollars,IVA,ChangeRate,InvoiceTotal,PayDate")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;

                if (invoice.StatusID != db.InvoiceStatus1.FirstOrDefault(x => x.Description == "Anulado").Id)
                {
                    if (invoice.PayDate.HasValue)
                        invoice.StatusID = db.InvoiceStatus1.FirstOrDefault(x => x.Description == "Cobrado").Id;
                    else if (invoice.StatusID == db.InvoiceStatus1.FirstOrDefault(x => x.Description == "Cobrado").Id)
                        invoice.StatusID = db.InvoiceStatus1.FirstOrDefault(x => x.Description == "Facturado").Id;
                }

                db.SaveChanges();

                if (invoice.ChangeRate.HasValue)
                    BusinessConfiguration.UpdateChangeRate(invoice.ChangeRate.Value);

                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.InvoiceCategories, "Id", "Description", invoice.CategoryID);
            ViewBag.ClientList = new SelectList(db.Client.Where(x => x.Active == true).OrderBy(x => x.Name).ToList(), "ID", "Name");
            ViewBag.POLineID = new SelectList(db.PurchaseOrderLines, "Id", "Description", invoice.POLineID);
            ViewBag.StatusID = new SelectList(db.InvoiceStatus1, "Id", "Description", invoice.StatusID);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);

            if (invoice.InvoiceStatus.Description == "Cobrado")
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase FileUpload)
        {
            if (FileUpload.ContentLength > 0)
            {
                StreamReader sourceStream = new StreamReader(FileUpload.InputStream);
                InvoiceBR invBR = new InvoiceBR();

                var success = invBR.insertCSVInvoices(sourceStream);

                if (success)
                    return View("UploadSuccess");
                else
                    return View("UploadFail");
            }

            return RedirectToAction("Index", new { error = "Please upload a file..." });
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
