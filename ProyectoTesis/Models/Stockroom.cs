using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Stockroom
    {
        [ForeignKey("Manager")]
        public int ID { get; set; }
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }
        [Display(Name = "Activo?")]
        public bool ActiveFlag { get; set; }

        public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; }
        public virtual ICollection<Zone> Zones { get; set; }

        public virtual Store Store { get; set; }
        public virtual User Manager { get; set; }
    }
}