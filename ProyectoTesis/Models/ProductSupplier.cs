using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class ProductSupplier
    {
        public int ID { get; set; }
        [Display(Name = "Producto")]
        public int ProductID { get; set; }
        [Display(Name = "Proveedor")]
        public int SupplierID { get; set; }
        [Display(Name = "Precio de Caja")]
        public double BoxPrice { get; set; }
        [Display(Name = "Precio de Fracción")]
        public double? FractionPrice { get; set; }
        [Display(Name = "Activo?")]
        public bool ActiveFlag { get; set; }

        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}