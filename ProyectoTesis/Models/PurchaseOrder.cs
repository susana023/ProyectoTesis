using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class PurchaseOrder
    {

        public int ID { get; set; }
        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "Debe ingresar un proveedor")]
        public int SupplierID { get; set; }
        [Display(Name = "Correlativo de la Factura")]
        public int BillCorrelative { get; set; }
        [Display(Name = "Número de serie de la Factura")]
        public string BillSerialNumber { get; set; }
        [Display(Name = "Flag Activo")]
        public bool ActiveFlag { get; set; }
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Display(Name = "IGV")]
        public double Igv { get; set; }
        public double Subtotal { get; set; }
        [Display(Name = "Proveedor")]
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        public double Total
        {
            get
            {
                return Igv + Subtotal;
            }
        }
    }
}