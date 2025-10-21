using System.ComponentModel.DataAnnotations;

namespace api_bentrix.Models
{
    public class MetodoPago
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del método de pago es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        [RegularExpression(@"^[a-záéíóúñA-ZÁÉÍÓÚÑ0-9\s\.\-&]+$", ErrorMessage = "El nombre contiene caracteres no permitidos")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de pago es obligatorio")]
        public TipoPago Tipo { get; set; }

        [Required(ErrorMessage = "El proveedor es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El proveedor debe tener entre 3 y 100 caracteres")]
        [RegularExpression(@"^[a-záéíóúñA-ZÁÉÍÓÚÑ0-9\s\.\-&]+$", ErrorMessage = "El proveedor contiene caracteres no permitidos")]
        public string Proveedor { get; set; } = string.Empty;
    }
}