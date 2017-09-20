using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public enum MovementType
    {
        Compra, Pérdida, Hallazgo, Despacho
    }

    public class Movement
    {
        public int ID { get; set; }
        [Display(Name = "Producto")]
        public int ProductID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Vencimiento")]
        public DateTime ExpirationDate { get; set; }
        [Display(Name = "Zona")]
        public int ZoneID { get; set; }
        [Display(Name = "Tipo de Movimiento")]
        public MovementType MovementType { get; set; }
        [Display(Name = "Cantidad de Cajas")]
        public int BoxUnits { get; set; }
        [Display(Name = "Cantidad de Fracciones")]
        public int FractionUnits { get; set; }
        [Display(Name = "Documento de Referencia")]
        public int DocumentID { get; set; }

        [Display(Name = "Producto")]
        public virtual Product Product { get; set; }
        [Display(Name = "Zona")]
        public virtual Zone Zone { get; set; }
    }
}