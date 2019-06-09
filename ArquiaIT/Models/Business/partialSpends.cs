using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquiaIT.Models.Business
{
    public partial class Spend
    {
        public string SpendMonth {
            get {
                return this.InvoiceDate.ToString("MMMM");
            }
        }
    }
}