using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_ventrix.Models
{
    public class Reporte
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de reporte es obligatorio")]
        public TipoReporte Tipo_Reporte { get; set; }

        [Required(ErrorMessage = "El ID del negocio es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del negocio debe ser mayor a 0")]
        [ForeignKey("Negocio")]
        public int Id_Negocio { get; set; }

        [Required(ErrorMessage = "El ID del vendedor es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del vendedor debe ser mayor a 0")]
        [ForeignKey("Vendedor")]
        public int Id_Vendedor { get; set; }

        [Required(ErrorMessage = "El contenido del reporte es obligatorio")]
        [StringLength(5000, MinimumLength = 10, ErrorMessage = "El contenido debe tener entre 10 y 5000 caracteres")]
        public string Contenido { get; set; } = string.Empty;
    }
}