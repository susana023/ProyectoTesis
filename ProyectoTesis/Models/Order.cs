using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public bool DeliveredFlag { get; set; }
        public bool ActiveFlag { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<SaleDocument> SaleDocuments { get; set; }

        public virtual Client Client { get; set; }
        public virtual User User { get; set; }
    }
}