using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class ReturnDetail
    {
        public int ID { get; set; }
        [Display(Name = "Devolución")]
        public int ReturnID { get; set; }
        [Display(Name = "Producto")]
        public int ProductID { get; set; }
        [Display(Name = "Cantidad de Cajas")]
        public int BoxUnits { get; set; }
        [Display(Name = "Cantidad de Fracciones")]
        public int FractionUnits { get; set; }
        [Display(Name = "Motivo")]
        public string Reason { get; set; }

        public virtual Product Product { get; set; }
        public virtual Return Return { get; set; }
    }
}