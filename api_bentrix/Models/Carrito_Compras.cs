using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_bentrix.Models
{
    public class Carrito_Compras
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La lista de productos es obligatoria")]
        public List<Producto> Productos { get; set; } = new List<Producto>();

        [Required(ErrorMessage = "El subtotal es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El subtotal no puede ser negativo")]
        [DataType(DataType.Currency)]
        public decimal Subtotal { get; set; }

        [Required(ErrorMessage = "El total es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El total no puede ser negativo")]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        public List<Descuento> Descuentos_Aplicados { get; set; } = new List<Descuento>();

        [Required(ErrorMessage = "La dirección de destino es obligatoria")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "La dirección debe tener entre 10 y 150 caracteres")]
        public string Direccion_Destino { get; set; } = string.Empty;

        [Required(ErrorMessage = "El valor de envío es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El valor de envío no puede ser negativo")]
        [DataType(DataType.Currency)]
        public decimal Valor_Envio { get; set; }

        [Required(ErrorMessage = "El ID del negocio es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del negocio debe ser mayor a 0")]
        [ForeignKey("Negocio")]
        public int Id_Negocio { get; set; }
    }
}