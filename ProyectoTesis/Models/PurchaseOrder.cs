using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class PurchaseOrder
    {
        public int ID { get; set; }
        public int SupplierID { get; set; }
        public int BillCorrelative { get; set; }
        public string BillSerialNumber { get; set; }
        public bool ActiveFlag { get; set; }
        public DateTime Date { get; set; }
        public double Igv { get; set; }
        public double Subtotal { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }
}