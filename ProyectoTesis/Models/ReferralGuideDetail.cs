using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class ReferralGuideDetail
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int BoxUnits { get; set; }
        public int FractionUnits { get; set; }
        public bool DeliveredFlag { get; set; }

        public virtual ReferralGuide ReferralGuide { get; set; }
        public virtual Product Product { get; set; }
    }
}