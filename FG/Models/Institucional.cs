using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FG.Models
{
    public class Institucional
    {
        [AllowHtml]
        public string Texto { get; set; }
    }

}