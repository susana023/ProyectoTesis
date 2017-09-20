using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class User
    {
        public int ID { get; set; }
        [Display(Name = "DNI")]
        public int Dni { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }
        [Display(Name = "Usuario del Sistema")]
        public string Username { get; set; }
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Display(Name = "Activo?")]
        public bool? ActiveFlag { get; set; }
        [Display(Name = "Tienda")]
        public int? StoreID { get; set; }
        [Display(Name = "Tipo")]
        public string Type { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual Store Store { get; set; }
        public virtual Stockroom Stockroom { get; set; }
    }
}