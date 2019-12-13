//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArquiaIT.Models.Business
{
    using System;
    using System.Collections.Generic;
    
    public partial class DebitNote
    {
        public int Id { get; set; }
        public int CategoryID { get; set; }
        public int InvoiceID { get; set; }
        public int StatusID { get; set; }
        public System.DateTime NoteDate { get; set; }
        public string NoteNumber { get; set; }
        public decimal ValueInPesos { get; set; }
        public Nullable<decimal> ValueInDollars { get; set; }
        public decimal IVA { get; set; }
        public Nullable<decimal> ChangeRate { get; set; }
        public Nullable<decimal> InvoiceTotal { get; set; }
        public Nullable<System.DateTime> PayDate { get; set; }
    
        public virtual InvoiceCategory InvoiceCategory { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual InvoiceStatus InvoiceStatus { get; set; }
    }
}
