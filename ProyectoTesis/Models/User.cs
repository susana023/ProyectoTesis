using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{

    public enum EmployeeType
    {
        Vendedor_Tienda, Distribuidor, Vendedor_Distribución, Encargado_Almacén, Encargardo_Tienda, Gerente
    }

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
        [ForeignKey("Store")]
        public int? StoreID { get; set; }
        [Display(Name = "Tipo")]
        public EmployeeType Type { get; set; }
        

        public virtual ICollection<Order> Orders { get; set; }

        public virtual Store Store { get; set; }
        public virtual Stockroom Stockroom { get; set; }

        public string FullName
        {
            get
            {
                return Name + " " + LastName;
            }
        }
    }
}