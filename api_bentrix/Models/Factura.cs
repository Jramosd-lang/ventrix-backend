using api_ventrix.Models;
using Microsoft.Identity.Client;

namespace api_ventrix.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int Id_negocio { get; set; }
        public int Id_comprador { get; set; }
        public DateTime Fecha {  get; set; }
        public Decimal subtotal { get; set; }
        public Decimal total { get; set; }
        public List<Descuento> Descuentos_aplicados { get; set; }
        public List<Producto> Productos {  get; set; }
        public MetodoPago metodoPago{ get; set; }

    }
}
