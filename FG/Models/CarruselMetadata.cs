using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FG.Models
{
    public class CarruselMetadata
    {
        [Required(ErrorMessage = "La Imagen es requerida")]
        public string Imagen;

    }
}
