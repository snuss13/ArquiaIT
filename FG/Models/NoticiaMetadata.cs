using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FG.Models
{
    public class NoticiaMetadata
    {
        [Required(ErrorMessage = "Título es requerido")]
        public string Titulo { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Cuerpo es requerido")]
        public string Cuerpo { get; set; }

    }
}