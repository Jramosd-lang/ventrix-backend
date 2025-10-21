using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api_bentrix.Models
{
    public class Negocio
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del negocio es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        [RegularExpression(@"^[a-záéíóúñA-ZÁÉÍÓÚÑ0-9\s\.\-&]+$", ErrorMessage = "El nombre contiene caracteres no permitidos")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "La dirección debe tener entre 10 y 150 caracteres")]
        public string Direccion { get; set; } = string.Empty;

        [Url(ErrorMessage = "La URL del logo no es válida")]
        [StringLength(500, ErrorMessage = "La URL del logo no puede exceder 500 caracteres")]
        public string UrlLogo { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        // Relaciones
        public List<Producto> Productos { get; set; } = new List<Producto>();

        public List<string> Imagenes { get; set; } = new List<string>();

        public List<MetodoPago> Metodos_Pago { get; set; } = new List<MetodoPago>();
    }
}