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
    public class ClientsController : Controller
    {
        private ArquiaEntities db = new ArquiaEntities();

        // GET: Clients
        public ViewResult Index() //string sortOrder,
        {

            //ViewBag.SortParm = String.IsNullOrEmpty(sortOrder) ? sortOrder : "";
            
            List<Client> clients = db.Client.ToList();

            //if ( !showAll.HasValue || showAll.Value == false)
            //{
            //    clients = clients.Where(x => x.Active  == true).ToList();
            //}

            return View(clients);
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Details/5
        public ActionResult ClientPartial(int? id)
        {
            ViewBag.ClientList = new SelectList(db.Client.Where(x => x.Active == true).OrderBy(x => x.Name).ToList(), "ID", "Name");

            if (id != null)
            {
                Client client = db.Client.Find(id);
                if (client == null)
                {
                    return HttpNotFound();
                }
                return View(client);
            }
            else
            {
                return View((new Client()));
            }
        }

        public JsonResult getClientInfo(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var cl = db.Client.Find(id);

            return Json(cl, JsonRequestBehavior.AllowGet);          
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            var client = new Client();

            client.Active = true;
           
            return View(client);
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CUIT,Address,Active")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Client.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CUIT,Address,Active")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Client.Find(id);
            //db.Client.Remove(client);
            client.Active = false;
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
