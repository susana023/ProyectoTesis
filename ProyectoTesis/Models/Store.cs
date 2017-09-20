using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Store
    {
        public int ID { get; set; }
        [Display(Name = "Dirección")]
        public string Address { get; set; }
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Display(Name = "Activa?")]
        public bool ActiveFlag { get; set; }

        public virtual ICollection<Stockroom> Stockrooms { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}