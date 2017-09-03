using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Return
    {
        public int ID { get; set; }
        public int SaleDocumentID { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<ReturnDetail> ReturnDetails { get; set; }

        public virtual SaleDocument SaleDocument { get; set; }
    }
}