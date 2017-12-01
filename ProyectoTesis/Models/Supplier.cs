using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Supplier
    {
        public int ID { get; set; }
        [Display(Name = "RUC")]
        [Required(ErrorMessage = "Debe ingresar el RUC")]
        [MinValue(10000000000, ErrorMessage = "El RUC debe tener 11 dígitos")]
        [MaxValue(99999999999, ErrorMessage = "El RUC debe tener 11 dígitos")]
        public long Ruc { get; set; }
        [Display(Name = "Razón Social")]
        [Required(ErrorMessage = "Debe ingresar la Razón Social")]
        public string BusinessName { get; set; }
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Debe ingresar un teléfono")]
        [Phone(ErrorMessage ="Ingrese un teléfono válido")]
        public string Phone { get; set; }
        [Display(Name = "Correo Electrónico")]
        [EmailAddress(ErrorMessage = "Correo Electrónico inválido")]
        public string Email { get; set; }
        [Display(Name = "Dirección")]
        public string Address { get; set; }
        [Display(Name = "Contacto")]
        public string Contact { get; set; }
        [Display(Name = "Activo?")]
        public bool ActiveFlag { get; set; }
        public class MinValue : ValidationAttribute
        {
            private readonly long _minValue;

            public MinValue(long minValue)
            {
                _minValue = minValue;
            }

            public override bool IsValid(object value)
            {
                return (long)value >= _minValue;
            }
        }

        public class MaxValue : ValidationAttribute
        {
            private readonly long _maxValue;

            public MaxValue(long maxValue)
            {
                _maxValue = maxValue;
            }

            public override bool IsValid(object value)
            {
                return (long)value <= _maxValue;
            }
        }
    }
}