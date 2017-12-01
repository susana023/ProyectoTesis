using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public abstract class Document
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Display(Name = "Anulado?")]
        public bool ActiveFlag { get; set; }
        public double Subtotal { get; set; }

        [Display(Name = "IGV")]
        public double Igv { get; set; }
        public ICollection<Movement> Movements { get; set; }
    }
}