using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api_bentrix.Models
{
    public class ImpuestoDetalle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del impuesto es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        [RegularExpression(@"^[a-záéíóúñA-ZÁÉÍÓÚÑ0-9\s\.\-&%]+$", ErrorMessage = "El nombre contiene caracteres no permitidos")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El valor del impuesto es obligatorio")]
        [Range(0.01, 100, ErrorMessage = "El valor del impuesto debe estar entre 0.01 y 100")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "El ID del impuesto es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del impuesto debe ser mayor a 0")]
        [ForeignKey("Impuesto")]
        public int ImpuestoId { get; set; }

        [Required(ErrorMessage = "La relación con Impuesto es obligatoria")]
        public Impuesto Impuesto { get; set; }
    }
}
