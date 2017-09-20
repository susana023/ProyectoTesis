using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoTesis.Models
{
    public enum ClientType
    {
        Mayorista, Distribucion, Tienda
    }
    public class Client
    {
        public int ID { get; set; }
        [Display(Name = "DNI")]
        public int? Dni { get; set; }
        [Display(Name = "RUC")]
        public long? Ruc { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }
        [Display(Name = "Dirección")]
        public string Address { get; set; }
        public string Email { get; set; }
        public bool ActiveFlag { get; set; }
        [Display(Name = "Tipo de Cliente")]
        public ClientType ClientType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public string FullName
        {
            get
            {
                return Name + " " + LastName;
            }
        }

    }
}