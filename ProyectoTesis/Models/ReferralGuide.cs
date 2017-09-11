using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class ReferralGuide
    {
        public int ID { get; set; }
        public int DistributorID { get; set; }
        public DateTime Date { get; set; }
        public int SaleDocumentID { get; set; }
        public int ClientID { get; set; }

        public virtual Distributor Distributor { get; set; }
        public virtual SaleDocument SaleDocument { get; set; }

        public virtual ICollection<ReferralGuideDetail> ReferralGuideDetails { get; set; }
    }
}