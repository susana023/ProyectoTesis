using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Correlative
    {
        public int ID { get; set; }
        [Display(Name = "Tienda")]
        public int StoreID { get; set; }
        [Display(Name = "Correlativo")]
        public int CorrelativeNumber { get; set; }
        [Display(Name = "Serie")]
        public string SerialNumber { get; set; }
        [Display(Name = "Tipo de documento")]
        public DocumentType DocumentType { get; set; }
        [Display(Name = "Activo?")]
        public bool ActiveFlag { get; set; }

        public virtual Store Store { get; set; }
    }
}