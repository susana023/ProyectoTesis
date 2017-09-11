using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public double Subtotal { get; set; }
        public int BoxUnits { get; set; }
        public int FractionUnits { get; set; }
    }
}