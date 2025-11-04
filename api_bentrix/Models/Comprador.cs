using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_ventrix.Models
{
    public class Comprador : Persona
    {
       

        [Required(ErrorMessage = "La fecha de registro es obligatoria")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Comprador), nameof(ValidarFechaRegistro))]
        public DateTime Fecha_Registro { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "La dirección debe tener entre 10 y 150 caracteres")]
        public string Direccion { get; set; } = string.Empty;

        public List<MetodoPago> Metodos_De_Pago { get; set; } = new List<MetodoPago>();

        [StringLength(500, ErrorMessage = "Los detalles no pueden exceder 500 caracteres")]
        public string Detalles { get; set; } = string.Empty;

        // Validación personalizada para la fecha de registro
        public static ValidationResult ValidarFechaRegistro(DateTime fechaRegistro, ValidationContext context)
        {
            if (fechaRegistro > DateTime.Now)
            {
                return new ValidationResult("La fecha de registro no puede ser en el futuro");
            }
            return ValidationResult.Success;
        }
    }
}