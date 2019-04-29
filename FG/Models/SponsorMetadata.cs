using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FG.Models
{
    public class SponsorMetadata
    {
        [Required(ErrorMessage = "Título es requerido")]
        public string Titulo;
    }
}
