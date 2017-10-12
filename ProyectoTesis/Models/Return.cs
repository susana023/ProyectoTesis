using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    //[Table("Return")]
    public class Return : Document
    {
        [Display(Name = "Documento de Venta")]
        public int SaleDocumentID { get; set; }

        public virtual ICollection<ReturnDetail> ReturnDetails { get; set; }

        public virtual SaleDocument SaleDocument { get; set; }
    }
}