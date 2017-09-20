using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class User
    {
        public int ID { get; set; }
        public int Dni { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? ActiveFlag { get; set; }
        public int StoreID { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual Store Store { get; set; }
        public virtual Stockroom Stockroom { get; set; }
    }
}