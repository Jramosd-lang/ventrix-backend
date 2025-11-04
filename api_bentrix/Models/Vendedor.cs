using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api_ventrix.Models
{
    public class Vendedor : Persona
    {
        public Negocio Negocio { get; set; }

        [Required(ErrorMessage = "El plan es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El plan debe tener entre 3 y 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9\s-]+$", ErrorMessage = "El plan solo puede contener letras, números y guiones")]
        public string Plan { get; set; } = string.Empty;

        public List<Administrador> Administradores { get; set; } = new List<Administrador>();

        [Required(ErrorMessage = "La fecha de inicio del plan es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime Fecha_Inicio_Plan { get; set; }

        [Required(ErrorMessage = "La fecha de fin del plan es obligatoria")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Vendedor), nameof(ValidarFechaFin), ErrorMessage = "La fecha de fin debe ser mayor a la fecha de inicio")]
        public DateTime Fecha_Fin_Plan { get; set; }

        public List<MetodoPago> Metodos_De_Pago { get; set; } = new List<MetodoPago>();

        // Validación personalizada para la fecha de fin
        public static ValidationResult ValidarFechaFin(DateTime fechaFin, ValidationContext context)
        {
            var vendedor = context.ObjectInstance as Vendedor;
            if (vendedor != null && fechaFin <= vendedor.Fecha_Inicio_Plan)
            {
                return new ValidationResult("La fecha de fin del plan debe ser posterior a la fecha de inicio");
            }
            return ValidationResult.Success;
        }
    }
}