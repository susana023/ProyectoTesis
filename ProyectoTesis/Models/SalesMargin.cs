using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class SalesMargin
    {
        [ForeignKey("Product")]
        public int ID { get; set; }
        [Display(Name = "Margen en el Mercado")]
        public double MarketMargin { get; set; }
        [Display(Name = "Margen en Tienda")]
        public double StoreMargin { get; set; }
        [Display(Name = "Margen en Distribución")]
        public double DistributionMargin { get; set; }

        public virtual Product Product { get; set; }
    }
}