using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_ventrix.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "No se ha asignado el id del negocio")]
        public int Id_Negocio { get; set; }

        public int? Id_Comprador { get; set; }

        [Required(ErrorMessage = "No se han agregado productos")]
        public List<Producto> Productos { get; set; } = new List<Producto>();

        [Required(ErrorMessage = "No se ha dado un valor total a pagar")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total_Pagar { get; set; }

        [Required]
        public int Metodo_Pago { get; set; }

        // ESTADO DEL PEDIDO (pendiente, pagado, enviado, cancelado, etc.)

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "Pendiente";

        [Required]
        public DateTime Fecha_Creacion { get; set; } = DateTime.Now;

        public DateTime? Fecha_Pago { get; set; }
        public DateTime? Fecha_Envio { get; set; }
        public DateTime? Fecha_Entrega { get; set; }

        [MaxLength(200)]
        public string? Direccion_Envio { get; set; }

        [MaxLength(500)]
        public string? Observaciones { get; set; }

        [ForeignKey("Id_Negocio")]
        public virtual Negocio? Negocio { get; set; }

        [ForeignKey("Id_Comprador")]
        public virtual Comprador? Comprador { get; set; }

        [MaxLength(50)]
        public string Codigo { get; set; } = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    }

    public class PedidoDTO
    {
        [Required(ErrorMessage = "No se ha asignado el id del negocio")]
        public int Id_Negocio { get; set; }
        public int? Id_Comprador { get; set; }
        [Required(ErrorMessage = "No se han agregado productos")]
        public List<int> Ids_Productos { get; set; } = new List<int>();
        public decimal Total_Pagar { get; set; }

        [Required]
        public int Metodo_Pago { get; set; }
        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "Pendiente";

        [Required]
        public DateTime Fecha_Creacion { get; set; } = DateTime.Now;

        public DateTime? Fecha_Pago { get; set; }
        public DateTime? Fecha_Envio { get; set; }
        public DateTime? Fecha_Entrega { get; set; }

        [MaxLength(200)]
        public string? Direccion_Envio { get; set; }

        [MaxLength(500)]
        public string? Observaciones { get; set; }

    }
}

