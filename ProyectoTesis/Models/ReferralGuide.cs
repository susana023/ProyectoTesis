using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    //[Table("ReferralGuide")]
    public class ReferralGuide: Document
    {
        [Display(Name = "Distribuidor")]
        public int DistributorID { get; set; }
        [Display(Name = "Documento de Venta")]
        public int SaleDocumentID { get; set; }
        [Display(Name = "Cliente")]
        public int ClientID { get; set; }

        public virtual Distributor Distributor { get; set; }
        public virtual SaleDocument SaleDocument { get; set; }

        public virtual ICollection<ReferralGuideDetail> ReferralGuideDetails { get; set; }
    }
}