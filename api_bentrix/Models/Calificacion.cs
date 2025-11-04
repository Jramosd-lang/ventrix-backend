using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_ventrix.Models
{
    public class Calificacion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El valor de la calificación es obligatorio")]
        [Range(0, 5, ErrorMessage = "La calificación debe estar entre 0 y 5")]
        public int Valor { get; set; }

        [Required(ErrorMessage = "El ID del negocio es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del negocio debe ser mayor a 0")]
        [ForeignKey("Negocio")]
        public int Id_Negocio { get; set; }

        [Required(ErrorMessage = "El ID del usuario es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario debe ser mayor a 0")]
        [ForeignKey("Usuario")]
        public int Id_Usuario { get; set; }

        [Required(ErrorMessage = "El comentario es obligatorio")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "El comentario debe tener entre 5 y 500 caracteres")]
        public string Comentario { get; set; } = string.Empty;
    }
}