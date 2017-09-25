using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public enum ProductType
    {
        Caramelo, Chupete, Chocolate, Galleta, Panetón, Chicle
    }
    public class Product
    {
        public int ID { get; set; }
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Display(Name = "Descripción de la Caja")]
        public string BoxDescription { get; set; }
        [Display(Name = "Descripción de la Fracción")]
        public string FractionDescription { get; set; }
        [Display(Name = "Precio de Caja")]
        public double BoxPrice { get; set; }
        [Display(Name = "Precio de Fracción")]
        public double FractionPrice { get; set; }
        [Display(Name = "Cantidad de Fracciones")]
        public int FractionUnits { get; set; }
        [Display(Name = "Stock Lógico")]
        public double LogicalStock { get; set; }
        [Display(Name = "Stock Físico")]
        public double PhysicalStock { get; set; }
        [Display(Name = "Activo?")]
        public bool ActiveFlag { get; set; }
        [Display(Name = "Stock Mínimo")]
        public double MinStock { get; set; }
        [Display(Name = "Stock Máximo")]
        public double MaxStock { get; set; }
        [Display(Name = "Tipo de Producto")]
        public ProductType ProductType { get; set; }
            
        

        public virtual ICollection<ReferralGuideDetail> ReferralGuideDetails { get; set; }
        public virtual ICollection<Movement> StockroomProducts { get; set; }
        public virtual ICollection<SaleDocumentDetail> SaleDocumentDetails { get; set; }
        public virtual ICollection<ReturnDetail> ReturnDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; }

        public virtual SalesMargin SalesMargin { get; set; }
    }
}