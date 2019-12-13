using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ArquiaIT.BusinessRules
{
    public class InvoiceBR
    {

        public bool insertCSVInvoices(StreamReader sourceStream)
        {
            List<Models.Business.Invoice> Invoices = new List<Models.Business.Invoice>();

            
            while (!sourceStream.EndOfStream)
            {
                var fileContent = sourceStream.ReadLine();
                var lineValues = fileContent.Split(';');

                var PO = new Models.Business.PurchaseOrder();
                var Inv = new Models.Business.Invoice();

               
            }

            return true;
        }


    }
}
