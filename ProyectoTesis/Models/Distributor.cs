using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Distributor
    {
        public int ID { get; set; }
        public int Dni { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string LicensePlate { get; set; }
        public bool ActiveFlag { get; set; }

        public virtual ICollection<ReferralGuide> ReferralGuides { get; set; }
    }
}