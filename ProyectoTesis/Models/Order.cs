using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Order
    {
        public int ID { get; set; }
        [Display(Name = "Cliente")]
        public int ClientID { get; set; }
        [Display(Name = "Usuario")]
        public int UserID { get; set; }
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }
        [Display(Name = "Entregado?")]
        public bool? DeliveredFlag { get; set; }
        [Display(Name = "Anulado?")]
        public bool ActiveFlag { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<SaleDocument> SaleDocuments { get; set; }

        public virtual Client Client { get; set; }
        public virtual User User { get; set; }
    }
}