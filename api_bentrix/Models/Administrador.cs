using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_bentrix.Models
{
    public class Administrador : Persona
    {
        [Required(ErrorMessage = "El ID del negocio es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del negocio debe ser mayor a 0")]
        [ForeignKey("Negocio")]
        public int Id_Negocio { get; set; }

        [Required(ErrorMessage = "El nivel de acceso es obligatorio")]
        [Range(1, 5, ErrorMessage = "El nivel de acceso debe estar entre 1 y 5")]
        public int Nivel_Acceso { get; set; }

        // Relación con Negocio (opcional, según tu DbContext)
        public virtual Negocio Negocio { get; set; }
    }
}