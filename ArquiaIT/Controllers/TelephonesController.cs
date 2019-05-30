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
    public class TelephonesController : Controller
    {
        private ArquiaEntities db = new ArquiaEntities();

        // GET: Telephones
        public ActionResult Index()
        {
            var telephones = db.Telephones.Include(t => t.TelephoneType);
            return View(telephones.ToList());
        }

        // GET: Telephones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telephone telephone = db.Telephones.Find(id);
            if (telephone == null)
            {
                return HttpNotFound();
            }
            return View(telephone);
        }

        // GET: Telephones/Create
        public ActionResult Create(int clientID)
        {
            ViewBag.TypeID = new SelectList(db.TelephoneTypes, "Id", "Description");
            ViewBag.ClientID = new SelectList(db.Client, "Id", "Name");
            ViewBag.ClientIDNumber = clientID;
            return View();
        }

        // POST: Telephones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientID,Number,TypeID")] Telephone telephone)
        {
            if (ModelState.IsValid)
            {
                db.Telephones.Add(telephone);
                db.SaveChanges();
                return RedirectToAction("Edit", "Clients", new { id=telephone.ClientID });
            }

            ViewBag.TypeID = new SelectList(db.TelephoneTypes, "Id", "Description", telephone.TypeID);
            ViewBag.ClientID = new SelectList(db.Client, "Id", "Name", telephone.ClientID);
            return View(telephone);
        }

        // GET: Telephones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telephone telephone = db.Telephones.Find(id);
            if (telephone == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeID = new SelectList(db.TelephoneTypes, "Id", "Description", telephone.TypeID);
            ViewBag.ClientID = new SelectList(db.Client, "Id", "Name", telephone.ClientID);
            return View(telephone);
        }

        // POST: Telephones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientID,Number,TypeID")] Telephone telephone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telephone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Clients", new { id = telephone.ClientID });
            }
            ViewBag.TypeID = new SelectList(db.TelephoneTypes, "Id", "Description", telephone.TypeID);
            ViewBag.ClientID = new SelectList(db.Client, "Id", "Name", telephone.ClientID);
            return View(telephone);
        }

        // GET: Telephones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telephone telephone = db.Telephones.Find(id);
            if (telephone == null)
            {
                return HttpNotFound();
            }
            return View(telephone);
        }

        // POST: Telephones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Telephone telephone = db.Telephones.Find(id);
            db.Telephones.Remove(telephone);
            db.SaveChanges();
            return RedirectToAction("Edit", "Clients", new { id = telephone.ClientID });
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
