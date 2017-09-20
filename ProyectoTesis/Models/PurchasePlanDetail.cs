﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class PurchasePlanDetail
    {
        public int ID { get; set; }
        [Display(Name = "Plan de Compra")]
        public int PurchasePlanID { get; set; }
        [Display(Name = "Producto")]
        public int ProductID { get; set; }
        [Display(Name = "Beneficio")]
        public double Benefit { get; set; }
        [Display(Name = "Cantidad de Cajas")]
        public int? BoxUnits { get; set; }
        [Display(Name = "Cantidad de Fracciones")]
        public int? FractionUnits { get; set; }
    }
}