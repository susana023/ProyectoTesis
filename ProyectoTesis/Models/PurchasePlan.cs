using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class PurchasePlan
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public double Investment { get; set; }

        public virtual ICollection<PurchasePlanDetail> PurchasePlanDetails { get; set; }
    }
}