using FG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FG.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vm = new FGVM();
            using(var ctx = new FG.Models.FGEntities())
            {
                vm.Carrousel = ctx.Carrusels.ToList();                
                vm.Noticias = ctx.Noticias.OrderByDescending(x => x.Id).Take(6).ToList();
                vm.Sponsors = ctx.Sponsors.ToList();
                vm.Publicidades = ctx.Publicidads.ToList();
                var fgAdmin = ctx.FGAdmins.FirstOrDefault();
                vm.Institucional = fgAdmin.Institucional;
                vm.BannerNoticias = fgAdmin.NoticiasBanner;
            }
                        
            return View(vm);
        }
    }
}