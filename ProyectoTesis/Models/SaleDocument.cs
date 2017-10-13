using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{

    public enum DocumentType
    {
        Factura, Boleta, NotaCrédito
    }
    //[Table("SaleDocument")]
    public class SaleDocument : Document
    {
        [Display(Name = "Correlativo")]
        public int Correlative { get; set; }
        [Display(Name = "Número de Serie")]
        public int SerialNumber { get; set; }
        [Display(Name = "Pedido")]
        public int OrderID { get; set; }
        [Display(Name = "IGV")]
        public double Igv { get; set; }
        [Display(Name = "Subtotal")]
        public double Subtotal { get; set; }
        [Display(Name = "Tipo de Documento")]
        public DocumentType DocumentType { get; set; }

        public virtual Order Order { get; set; }

        public virtual ICollection<SaleDocumentDetail> SaleDocumentDetails { get; set; }
        public virtual ICollection<Return> Returns { get; set; }
        public virtual ICollection<ReferralGuide> ReferralGuides { get; set; }

        public double Total
        {
            get
            {
                return Subtotal + Igv;
            }
        }
    }
}