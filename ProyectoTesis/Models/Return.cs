using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Return
    {
        public int ID { get; set; }
        [Display(Name = "Documento de Venta")]
        public int SaleDocumentID { get; set; }
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        public virtual ICollection<ReturnDetail> ReturnDetails { get; set; }

        public virtual SaleDocument SaleDocument { get; set; }
    }
}