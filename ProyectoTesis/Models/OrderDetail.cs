﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class OrderDetail
    {
        public int ID { get; set; }
        [Display(Name = "Pedido")]
        public int OrderID { get; set; }
        [Display(Name = "Producto")]
        public int ProductID { get; set; }
        [Display(Name = "Subtotal")]
        public double Subtotal { get; set; }
        [Display(Name = "Cantidad de Cajas")]
        public int? BoxUnits { get; set; }
        [Display(Name = "Cantidad de Fracciones")]
        public int? FractionUnits { get; set; }

        [Display(Name = "Producto")]
        public virtual Product Product { get; set; }
        [Display(Name = "Orden de Compra")]
        public virtual Order Order { get; set; }
        [Display(Name = "Activo?")]
        public bool ActiveFlag { get; set; }
    }
}