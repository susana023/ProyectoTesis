using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    //[Table("PurchaseOrder")]
    public class PurchaseOrder: Document
    {
        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "Debe ingresar un proveedor")]
        public int SupplierID { get; set; }
        [Display(Name = "Correlativo de la Factura")]
        public int BillCorrelative { get; set; }
        [Display(Name = "Número de serie de la Factura")]
        public string BillSerialNumber { get; set; }
        [Display(Name = "Proveedor")]
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double Total
        {
            get
            {
                return Igv + Subtotal;
            }
        }
    }
}