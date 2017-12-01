using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class SaleDocumentDetail
    {
        public int ID { get; set; }
        [Display(Name = "Documento de Venta")]
        public int SaleDocumentID { get; set; }
        [Display(Name = "Producto")]
        public int ProductID { get; set; }
        [Display(Name = "Cantidad de Cajas")]
        public int BoxUnits { get; set; }
        [Display(Name = "Cantidad de Fracciones")]
        public int FractionUnits { get; set; }
        public double Subtotal { get; set; }
        [Display(Name = "Activo?")]
        public bool ActiveFlag { get; set; }
        public virtual Product Product { get; set; }
        public virtual SaleDocument SaleDocument { get; set; }
    }
}