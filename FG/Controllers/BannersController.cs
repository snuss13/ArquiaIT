using FG.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FG.Controllers
{
    public class BannersController : Controller
    {
        // GET: Banners
        [FGAuthorizeAdministrator]
        public ActionResult Index()
        {
            FGAdmin fgAdmin = null;
            using (var ctx = new FGEntities())
            {
                fgAdmin = ctx.FGAdmins.FirstOrDefault();
            }

            return View(fgAdmin);
        }
        [HttpPost]
        [FGAuthorizeAdministrator]
        public ActionResult SaveBanners([Bind(Include = "Id, BannerNoticias, BannerIntegrantes, BannerSponsors,UploadBannerNoticias, UploadBannerIntegrantes, UploadBannerSponsors")] FGAdmin fgAdmin)
        {
            if (fgAdmin.UploadBannerNoticias != null)
            {
                using (var reader = new BinaryReader(fgAdmin.UploadBannerNoticias.InputStream))
                {
                    fgAdmin.NoticiasBanner = Convert.ToBase64String(reader.ReadBytes(fgAdmin.UploadBannerNoticias.ContentLength));
                }
            }

            if (fgAdmin.UploadBannerSponsors != null)
            {
                using (var reader = new BinaryReader(fgAdmin.UploadBannerSponsors.InputStream))
                {
                    fgAdmin.SponsorsBanner = Convert.ToBase64String(reader.ReadBytes(fgAdmin.UploadBannerSponsors.ContentLength));
                }
            }

            if (fgAdmin.UploadBannerIntegrantes != null)
            {
                using (var reader = new BinaryReader(fgAdmin.UploadBannerIntegrantes.InputStream))
                {
                    fgAdmin.IntegrantesBanner = Convert.ToBase64String(reader.ReadBytes(fgAdmin.UploadBannerIntegrantes.ContentLength));
                }
            }

            using( var ctx = new FGEntities())
            {
                var entity = ctx.FGAdmins.First();
                entity.NoticiasBanner = fgAdmin.NoticiasBanner;
                entity.SponsorsBanner = fgAdmin.SponsorsBanner;
                entity.IntegrantesBanner = fgAdmin.IntegrantesBanner;
                ctx.SaveChanges();
            }

            return View("BannersSaved");
        }


    }
}