using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FG.Models
{
    [MetadataType(typeof(SponsorMetadata))]
    public partial class Sponsor
    {
        public HttpPostedFileBase UploadImageBarra { get; set; }
        public HttpPostedFileBase UploadImagePagina { get; set; }
    }
}