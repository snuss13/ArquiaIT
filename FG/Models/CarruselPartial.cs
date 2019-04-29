using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FG.Models
{
    [MetadataType(typeof(CarruselMetadata))]
    public partial class Carrusel
    {
        public HttpPostedFileBase UploadImage { get; set; }

    }
}