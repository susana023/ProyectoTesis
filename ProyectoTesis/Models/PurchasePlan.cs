using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class PurchasePlan
    {
        public int ID { get; set; }
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }
        [Display(Name = "Inversión")]
        public double Investment { get; set; }

        public virtual ICollection<PurchasePlanDetail> PurchasePlanDetails { get; set; }
    }
}