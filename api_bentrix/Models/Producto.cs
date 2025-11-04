using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace api_ventrix.Models
{
    public class Producto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        [RegularExpression(@"^[a-záéíóúñA-ZÁÉÍÓÚÑ0-9\s\.\-&]+$", ErrorMessage = "El nombre contiene caracteres no permitidos")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El valor del producto es obligatorio")]
        [Range(0.01,162514264337593543, ErrorMessage = "El valor debe ser mayor a 0")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "La fecha de creación es obligatoria")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Producto), nameof(ValidarFechaCreacion))]
        public DateTime Fecha_Creacion { get; set; }
        [Required(ErrorMessage = "El stock es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "El estado es obligatorio")]
        public bool Estado { get; set; }
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string Descripcion { get; set; } = string.Empty;
        [Required(ErrorMessage = "La calificación es obligatoria")]
        [Range(0, 5, ErrorMessage = "La calificación debe estar entre 0 y 5")]
        public int Calificacion { get; set; }
        [Required(ErrorMessage = "El código de lote es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El código de lote debe tener entre 3 y 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9\-_]+$", ErrorMessage = "El código de lote solo puede contener letras, números, guiones y guiones bajos")]
        public string Codigo_Lote { get; set; } = string.Empty;
        // Relaciones
        public List<Impuesto> Impuestos_Aplicados { get; set; } = new List<Impuesto>();

        [Required(ErrorMessage = "La imagen es obligatoria")]
        [Url]
        public string ImagenUrl { get; set; }
        [Required(ErrorMessage = "El ID del negocio es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del negocio debe ser mayor a 0")]
        [ForeignKey("Negocio")]
        public int Id_Negocio { get; set; }
        // Validación personalizada para la fecha de creación
        public static ValidationResult ValidarFechaCreacion(DateTime fechaCreacion, ValidationContext context)
        {
            if (fechaCreacion > DateTime.Now)
            {
                return new ValidationResult("La fecha de creación no puede ser en el futuro");
            }
            return ValidationResult.Success;
        }
    }
}