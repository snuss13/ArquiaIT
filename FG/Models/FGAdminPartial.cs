using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FG.Models
{
    [MetadataType(typeof(FGAdminMetadata))]
    public partial class FGAdmin
    {
        public HttpPostedFileBase UploadBannerNoticias { get; set; }
        public HttpPostedFileBase UploadBannerIntegrantes { get; set; }
        public HttpPostedFileBase UploadBannerSponsors { get; set; }
    }
}