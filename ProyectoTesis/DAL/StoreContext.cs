using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoTesis.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProyectoTesis.DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext() : base("StoreContext")
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSupplier> ProductSuppliers { get; set; }
        public DbSet<ReferralGuide> ReferralGuides { get; set; }
        public DbSet<ReferralGuideDetail> ReferralGuideDetails { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<ReturnDetail> ReturnDetails { get; set; }
        public DbSet<SaleDocument> SaleDocuments { get; set; }
        public DbSet<SaleDocumentDetail> SaleDocumentDetails { get; set; }
        public DbSet<Stockroom> Stockrooms { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<PurchasePlan> PurchasePlans { get; set; }
        public DbSet<PurchasePlanDetail> PurchasePlanDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}