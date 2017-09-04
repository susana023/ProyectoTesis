using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Zone
    {
        public int ID { get; set; }
        public int StockroomID { get; set; }
        public string Description { get; set; }

        public virtual Stockroom Stockroom { get; set; }

        public virtual ICollection<Movement> StockroomZoneProducts { get; set; }
    }
}