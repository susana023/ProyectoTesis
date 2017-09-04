using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class ProductSupplier
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int SupplierID { get; set; }
        public double BoxPrice { get; set; }
        public double FractionPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}