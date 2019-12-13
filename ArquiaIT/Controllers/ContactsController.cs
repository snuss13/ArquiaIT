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
    public class ContactsController : Controller
    {
        private ArquiaEntities db = new ArquiaEntities();

        // GET: Contacts
        public ActionResult Index()
        {
            var contact = db.Contact.Include(c => c.Client);
            return View(contact.ToList());
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create(int clientID)
        {
            ViewBag.ClientID = new SelectList(db.Client.Where(x => x.Id == clientID), "Id", "Name");

            Contact contact = new Contact();
            contact.Active = true; 

            return View(contact);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientID,Name,LastName,Area,TelephoneNumber,InternNumber,email,Active")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contact.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Edit", "Clients", new { Id = contact.ClientID });
            }

            ViewBag.ClientID = new SelectList(db.Client.Where(x => x.Id == contact.ClientID), "Id", "Name");
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Client.Where(x => x.Id == contact.ClientID), "Id", "Name");
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientID,Name,LastName,Area,TelephoneNumber,InternNumber,email,Active")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Clients", new { Id = contact.ClientID });
            }
            ViewBag.ClientID = new SelectList(db.Client.Where(x => x.Id == contact.ClientID), "Id", "Name");
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contact.Find(id);
            db.Contact.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Edit", "Clients", new { Id = contact.ClientID });
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
