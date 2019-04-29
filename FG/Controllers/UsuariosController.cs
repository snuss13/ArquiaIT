using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FG.Models;
using FG.Helpers;

namespace FG.Controllers
{
    public class UsuariosController : Controller
    {
        const string _sessionStore = "SessionStore";
        const string _CRYPTOPWD = "FuriousTeam";
        private FGEntities db = new FGEntities();

        [FGAuthorizeAdministrator]
        public ActionResult Index()
        {
            return View(db.Usuarios.ToList());
        }
        
        [FGAuthorizeAdministrator]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [FGAuthorizeAdministrator]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Password, Rol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var u = db.Usuarios.FirstOrDefault(x => x.Nombre == usuario.Nombre);
                if (u != null)
                {
                    ViewBag.ErrorMessage = "El nombre del usuario ya existe";
                    return View();
                }
                usuario.Password = CryptoHelper.Encrypt(usuario.Password, _CRYPTOPWD);
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        [FGAuthorizeAdministrator]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [FGAuthorizeAdministrator]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Password,Rol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        [FGAuthorizeAdministrator]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [FGAuthorizeAdministrator]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string nombre, string password)
        {
            SessionStore session;

            if (Session[_sessionStore] != null)
                session = Session[_sessionStore] as SessionStore;
            else
            {
                session = new SessionStore();
                Session[_sessionStore] = session;
            }

            if (session.LoginAttempts == 3)
            {
                if (session.LastLoginAttempt.AddHours(2) > DateTime.Now)
                    return View("Error");
                else //pasaron las dos horas
                    session.LoginAttempts = 1;
            }
            else
            {
                session.LoginAttempts++;
            }
            session.LastLoginAttempt = DateTime.Now;


            using (var ctx = new FGEntities())
            {

                var usuario = ctx.Usuarios.FirstOrDefault(x=>x.Nombre == nombre);
                if(usuario == null)
                {
                    ViewBag.LogginAttempts = session.LoginAttempts.ToString();
                    return View("LoginFailed");
                }

                if (CryptoHelper.Encrypt(password, _CRYPTOPWD) != usuario.Password)
                {
                    ViewBag.LogginAttempts = session.LoginAttempts.ToString();
                    return View("LoginFailed");
                }

                session.Usuario = usuario;
                return RedirectToAction("Index", "Home");
            }
        }

        [FGAuthorizeAdministratorOrColaborador]
        public ActionResult SetNewPassword()
        {
            return View();
        }

        //session.Usuario

        [FGAuthorizeAdministratorOrColaborador]
        [HttpPost]
        public ActionResult SetNewPassword(string newPassword)
        {
            using (var ctx = new FGEntities())
            {
                var usuario = (Session[_sessionStore]  as SessionStore).Usuario;
                var u = ctx.Usuarios.FirstOrDefault(x=>x.Id == usuario.Id);
                u.Password = CryptoHelper.Encrypt(newPassword, _CRYPTOPWD);
                ctx.SaveChanges();
                return RedirectToAction("Index", "Noticias");
            }
        }

        public ActionResult LogOff()
        {
            Session[_sessionStore] = null;
            return RedirectToAction("Index", "Home");
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
