using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Stockroom
    {
        public int ID { get; set; }
        public int ManagerID { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; }
        public virtual ICollection<Zone> Zones { get; set; }

        public virtual Store Store { get; set; }
        public virtual User Manager { get; set; }
    }
}