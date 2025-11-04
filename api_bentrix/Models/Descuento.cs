using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace api_ventrix.Models
{
    public class Descuento
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del descuento es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        [RegularExpression(@"^[a-záéíóúñA-ZÁÉÍÓÚÑ0-9\s\.\-&%]+$", ErrorMessage = "El nombre contiene caracteres no permitidos")]
        public string Nombre_Descuento { get; set; } = string.Empty;
        [Required(ErrorMessage = "El tipo de descuento es obligatorio")]
        public TipoDescuento Tipo_Descuento { get; set; }
        [Required(ErrorMessage = "El valor mínimo es obligatorio")]
        [Range(0, 162514264337593543, ErrorMessage = "El valor mínimo no puede ser negativo")]
        [DataType(DataType.Currency)]
        public decimal Valor_Min { get; set; }
        [Required(ErrorMessage = "El valor máximo es obligatorio")]
        [Range(0, 162514264337593543, ErrorMessage = "El valor máximo no puede ser negativo")]
        [DataType(DataType.Currency)]
        [CustomValidation(typeof(Descuento), nameof(ValidarValorMax))]
        public decimal Valor_Max { get; set; }
        [Required(ErrorMessage = "El valor a aplicar es obligatorio")]
        [Range(0.01, 162514264337593543, ErrorMessage = "El valor a aplicar debe ser mayor a 0")]
        [DataType(DataType.Currency)]
        public decimal Valor_Aplica { get; set; }
        [Required(ErrorMessage = "La fecha de creación es obligatoria")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Descuento), nameof(ValidarFechaCreacion))]
        public DateTime Fecha_Creacion { get; set; }
        [Required(ErrorMessage = "La fecha de expiración es obligatoria")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Descuento), nameof(ValidarFechaExpiracion))]
        public DateTime Fecha_Expiracion { get; set; }
        public List<Producto> Productos_Aplicados { get; set; } = new List<Producto>();
        // Validación personalizada para Valor_Max
        public static ValidationResult ValidarValorMax(object valor, ValidationContext context)
        {
            if (valor is decimal valorMax)
            {
                var descuento = context.ObjectInstance as Descuento;
                if (descuento != null && valorMax <= descuento.Valor_Min)
                {
                    return new ValidationResult("El valor máximo debe ser mayor al valor mínimo");
                }
            }
            return ValidationResult.Success;
        }
        // Validación personalizada para Fecha_Creacion
        public static ValidationResult ValidarFechaCreacion(object valor, ValidationContext context)
        {
            if (valor is DateTime fechaCreacion)
            {
                if (fechaCreacion > DateTime.Now)
                {
                    return new ValidationResult("La fecha de creación no puede ser en el futuro");
                }
            }
            return ValidationResult.Success;
        }
        // Validación personalizada para Fecha_Expiracion
        public static ValidationResult ValidarFechaExpiracion(object valor, ValidationContext context)
        {
            if (valor is DateTime fechaExpiracion)
            {
                var descuento = context.ObjectInstance as Descuento;
                if (descuento != null && fechaExpiracion <= descuento.Fecha_Creacion)
                {
                    return new ValidationResult("La fecha de expiración debe ser posterior a la fecha de creación");
                }
                if (fechaExpiracion <= DateTime.Now)
                {
                    return new ValidationResult("La fecha de expiración debe ser en el futuro");
                }
            }
            return ValidationResult.Success;
        }
    }
}