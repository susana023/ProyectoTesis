using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class ExpirationTime
    {
        public int ID { get; set; }
        [Display(Name = "Tipo de Producto")]
        public ProductType ProductType { get; set; }
        [Display(Name = "Meses")]
        public int Months { get; set; }
    }
}