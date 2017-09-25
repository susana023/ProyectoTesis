using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public abstract class Document
    {
        [Key]
        public int ID { get; set; }
        public ICollection<Movement> Movements { get; set; }
    }
}