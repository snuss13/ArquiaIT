using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquiaIT.Models.Business
{
    public partial class PurchaseOrder
    {
        public decimal POTotalInPesos {
            get
            {
                return this.PurchaseOrderLines.Where(x => x.ValueInPesos.HasValue).Sum(x => x.ValueInPesos.Value);
            }
        }

        public decimal POTotalInDollars
        {
            get {
                return this.PurchaseOrderLines.Where(x => x.ValueInDollars.HasValue).Sum(x =>  x.ValueInDollars.Value);
            }
        }

    }
}