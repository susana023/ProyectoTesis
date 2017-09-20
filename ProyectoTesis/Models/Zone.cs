using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Zone
    {
        public int ID { get; set; }
        [Display(Name = "Almacén")]
        public int StockroomID { get; set; }
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public virtual Stockroom Stockroom { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual ICollection<Movement> StockroomZoneProducts { get; set; }
    }
}