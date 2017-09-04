using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class PurchasePlanDetail
    {
        public int ID { get; set; }
        public int PurchasePlanID { get; set; }
        public int ProductID { get; set; }
        public double benefit { get; set; }
        public int BoxUnits { get; set; }
        public int FractionUnits { get; set; }
    }
}