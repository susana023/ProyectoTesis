﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class SaleDocumentDetail
    {
        public int ID { get; set; }
        public int SaleDocumentID { get; set; }
        public int ProductID { get; set; }
        public int BoxUnits { get; set; }
        public int FractionUnits { get; set; }
        public double Subtotal { get; set; }

        public virtual Product Product { get; set; }
        public virtual SaleDocument SaleDocument { get; set; }
    }
}