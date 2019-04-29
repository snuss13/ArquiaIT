using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FG.Models
{
    public class FGVM
    {
        public List<Carrusel> Carrousel { get; set; }
        public List<Publicidad> Publicidades { get; set; }
        public List<Noticia> Noticias { get; set; }
        public List<Sponsor> Sponsors { get; set; }
        public string Institucional { get; set; }
        public string BannerNoticias { get; set; }
    }
}