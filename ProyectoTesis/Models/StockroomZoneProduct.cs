using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class StockroomZoneProduct
    {
        public int ProductID { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ZoneID { get; set; }

        public virtual Product Product { get; set; }
        public virtual Zone Zone { get; set; }
    }
}