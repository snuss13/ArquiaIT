﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArquiaFin.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ArquiaEntities : DbContext
    {
        public ArquiaEntities()
            : base("name=ArquiaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ArquiaUser> ArquiaUser { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceCategory> InvoiceCategory { get; set; }
        public virtual DbSet<InvoiceStatus> InvoiceStatus { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual DbSet<PurchaseOrderLine> PurchaseOrderLine { get; set; }
        public virtual DbSet<Retentions> Retentions { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Spends> Spends { get; set; }
        public virtual DbSet<Telephone> Telephone { get; set; }
        public virtual DbSet<TelephoneType> TelephoneType { get; set; }
    }
}
