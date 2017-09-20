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
        [Display(Name = "Orden de Compra")]
        public int PurchaseOrderID { get; set; }
        [Display(Name = "Producto")]
        public int productID { get; set; }
        [Display(Name = "Cantidad de Cajas")]
        public int? BoxUnits { get; set; }
        [Display(Name = "Cantidad de Fracciones")]
        public int? FractionUnits { get; set; }
        public double Subtotal { get; set; }        
        [Display(Name = "Fecha de vencimiento del lote")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BatchExpirationDay { get; set; }
        public int ZoneID { get; set; }

        [Display(Name = "Producto")]
        public virtual Product Product { get; set; }
        [Display(Name = "Orden de Compra")]
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Zone Zone { get; set; }
    }
}