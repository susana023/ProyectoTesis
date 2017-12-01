using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class ReferralGuideDetail
    {
        public int ID { get; set; }
        [Display(Name = "Producto")]
        public int ProductID { get; set; }
        [Display(Name = "Cantidad de Cajas")]
        public int BoxUnits { get; set; }
        [Display(Name = "Cantidad de Fracciones")]
        public int FractionUnits { get; set; }
        [Display(Name = "Entregado?")]
        public bool DeliveredFlag { get; set; }
        [Display(Name = "Activo?")]
        public bool ActiveFlag { get; set; }
        [Display(Name = "Peso")]
        public double Weight { get; set; }

        public virtual ReferralGuide ReferralGuide { get; set; }
        public virtual Product Product { get; set; }
    }
}