using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class SalesMargin
    {
        [ForeignKey("Product")]
        public int ID { get; set; }
        public double MarketMargin { get; set; }
        public double StoreMargin { get; set; }
        public double DistributionMargin { get; set; }

        public virtual Product Product { get; set; }
    }
}