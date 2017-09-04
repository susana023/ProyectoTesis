using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class PurchaseOrderDetail
    {
        public int ID { get; set; }
        public int PurchaseOrderID { get; set; }

        public int productID { get; set; }
        [Display(Name = "Cantidad de Cajas")]
        public int BoxUnits { get; set; }
        [Display(Name = "Cantidad de Fracciones")]
        public int FractionUnits { get; set; }
        public double Subtotal { get; set; }

        [Display(Name = "Producto")]
        public virtual Product Product { get; set; }
        [Display(Name = "Orden de Compra")]
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}