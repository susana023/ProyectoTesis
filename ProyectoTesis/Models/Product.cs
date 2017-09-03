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

        public virtual ReferralGuideDetail ReferralGuideDetail { get; set; }
        public virtual ProductSupplier ProductSupplier { get; set; }
        public virtual StockroomZoneProduct StockroomProduct { get; set; }
        public virtual SaleDocumentDetail SaleDocumentDetail { get; set; }
        public virtual ReturnDetail ReturnDetail { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
    }
}