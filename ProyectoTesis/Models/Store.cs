using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Store
    {
        public int ID { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Stockroom> Stockrooms { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}