using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class SaleDocument
    {
        public int ID { get; set; }
        public int Correlative { get; set; }
        public int SerialNumber { get; set; }
        public int OrderID { get; set; }
        public double Igv { get; set; }
        public double Subtotal { get; set; }
        public bool ActiveFlag { get; set; }
        
        public virtual Order Order { get; set; }

        public virtual ICollection<SaleDocumentDetail> SaleDocumentDetails { get; set; }
        public virtual ICollection<Return> Returns { get; set; }
        public virtual ICollection<ReferralGuide> ReferralGuides { get; set; }
    }
}