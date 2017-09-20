using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Distributor
    {
        public int ID { get; set; }
        [Display(Name = "DNI")]
        public int Dni { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Display(Name = "Placa del Auto")]
        public string LicensePlate { get; set; }
        [Display(Name = "Activo?")]
        public bool ActiveFlag { get; set; }

        public virtual ICollection<ReferralGuide> ReferralGuides { get; set; }
    }
}