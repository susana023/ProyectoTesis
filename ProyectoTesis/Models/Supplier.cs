using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Supplier
    {
        public int ID { get; set; }
        [Display(Name = "RUC")]
        public long Ruc { get; set; }
        [Display(Name = "Razón Social")]
        public string BusinessName { get; set; }
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        [Display(Name = "Dirección")]
        public string Address { get; set; }
        [Display(Name = "Contacto")]
        public string Contact { get; set; }
    }
}