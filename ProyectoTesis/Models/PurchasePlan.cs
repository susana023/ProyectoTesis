using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class PurchasePlan
    {
        public int ID { get; set; }
        [Display(Name = "Fecha de Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BeginDate { get; set; }
        [IsDateAfterAttribute("BeginDate", true, ErrorMessage = "La Fecha Fin debe ser mayor a la Fecha de Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Fin")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Activo?")]
        public bool ActiveFlag { get; set; }
        [Display(Name = "Inversión")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double Investment { get; set; }

        [Display(Name = "Beneficio Total")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double TotalBenefit
        {
            get
            {
                double totalBenefit = 0.0;
                if (PurchasePlanDetails != null)
                {
                    foreach (PurchasePlanDetail detail in PurchasePlanDetails)
                    {
                        totalBenefit += detail.Benefit;
                    }
                }
                return totalBenefit;
            }
        }

        public virtual ICollection<PurchasePlanDetail> PurchasePlanDetails { get; set; }
    }
}