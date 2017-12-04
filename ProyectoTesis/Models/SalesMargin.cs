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
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double MarketMargin { get; set; }
        [Display(Name = "Margen en Tienda")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double StoreMargin { get; set; }
        [Display(Name = "Margen en Distribución")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double DistributionMargin { get; set; }
        [Display(Name = "Activo?")]
        public bool ActiveFlag { get; set; }

        public virtual Product Product { get; set; }
    }
}