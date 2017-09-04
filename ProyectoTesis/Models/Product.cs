using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string BoxDescription { get; set; }
        public string FractionDescription { get; set; }
        public double BoxPrice { get; set; }
        public double FractionPrice { get; set; }
        public bool ActiveFlag { get; set; }

        public virtual ICollection<ReferralGuideDetail> ReferralGuideDetails { get; set; }
        public virtual ICollection<Movement> StockroomProducts { get; set; }
        public virtual ICollection<SaleDocumentDetail> SaleDocumentDetails { get; set; }
        public virtual ICollection<ReturnDetail> ReturnDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; }

        public virtual SalesMargin SalesMargin { get; set; }
    }
}