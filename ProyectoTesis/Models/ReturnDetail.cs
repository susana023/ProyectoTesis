using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class ReturnDetail
    {
        public int ID { get; set; }
        public int ReturnID { get; set; }
        public int ProductID { get; set; }
        public int BoxUnits { get; set; }
        public int FractionUnits { get; set; }
        public string Reason { get; set; }

        public virtual Product Product { get; set; }
        public virtual Return Return { get; set; }
    }
}