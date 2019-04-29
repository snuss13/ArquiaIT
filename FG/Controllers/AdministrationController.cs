using FG.Helpers;
using FG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FG.Controllers
{
    public class AdministrationController : Controller
    {

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
    }
}