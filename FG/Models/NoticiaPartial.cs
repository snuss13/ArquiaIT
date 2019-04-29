using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FG.Models
{
    [MetadataType(typeof(NoticiaMetadata))]
    public partial class Noticia
    {
        public HttpPostedFileBase UploadImagePortada { get; set; }
        public HttpPostedFileBase UploadImageNoticia { get; set; }

        public string CuerpoCorto
        {
            get
            {
                if (this.Cuerpo.Length > 255)
                    return this.Cuerpo.Substring(0, 255) + "...";
                else return this.Cuerpo;
            }
        }
    }
}