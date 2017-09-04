using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class PurchaseOrderDetail
    {
        public int ID { get; set; }
        public int PurchaseOrderID { get; set; }
        public int productID { get; set; }
        public int BoxUnits { get; set; }
        public int FractionUnits { get; set; }
        public double Subtotal { get; set; }

        public virtual Product Product { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}