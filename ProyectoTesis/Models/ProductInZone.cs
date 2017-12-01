using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class ProductInZone
    {
        public int ID { get; set; }
        [Display(Name = "Zona")]
        public int ZoneID { get; set; }
        [Display(Name = "Producto")]
        public int ProductID { get; set; }
        [Display(Name = "Fecha de expiración")]
        public DateTime ExpirationDate { get; set; }
        [Display(Name = "Cantidad de Cajas")]
        public int BoxUnits { get; set; }
        [Display(Name = "Cantidad de Fracciones")]
        public int FractionUnits { get; set; }
    }
}