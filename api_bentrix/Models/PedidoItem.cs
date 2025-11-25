using api_ventrix.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class PedidoItem
{
    public int Id { get; set; }

    [ForeignKey("Pedido")]
    public int Id_Pedido { get; set; }
    [JsonIgnore]
    public Pedido Pedido { get; set; }

    [ForeignKey("Producto")]
    public int Id_Producto { get; set; }

    public Producto Producto { get; set; }

    public int Cantidad { get; set; } = 1;

    public decimal Valor { get; set; }   // Precio unitario al momento del pedido (importante mantener histórico)

    [NotMapped] 
    public decimal ValorTotal => Valor * Cantidad;

    public DateTime Fecha_Creacion { get; set; } = DateTime.Now;

    public string Codigo_Lote { get; set; } = string.Empty;

    public List<Impuesto> Impuestos_Aplicados { get; set; } = new();

    public string ImagenUrl { get; set; }

    public int Id_Negocio { get; set; }

    public static ValidationResult ValidarFechaCreacion(DateTime fechaCreacion, ValidationContext context)
    {
        return fechaCreacion > DateTime.Now
            ? new ValidationResult("La fecha de creación no puede ser en el futuro")
            : ValidationResult.Success;
    }
}
