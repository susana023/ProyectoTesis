using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public enum MovementType
    {
        E, S, H, D
    }

    public class Movement
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ZoneID { get; set; }
        public MovementType MovementType { get; set; }
        public int BoxUnits { get; set; }
        public int FractionUnits { get; set; }
        public int DocumentID { get; set; }

        public virtual Product Product { get; set; }
        public virtual Zone Zone { get; set; }
    }
}