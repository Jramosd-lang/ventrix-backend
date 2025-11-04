using System.Collections.Generic;

namespace api_ventrix.Models
{
    public class Impuesto
    {
        public int Id { get; set; }

        public List<ImpuestoDetalle> Impuestos { get; set; } = new List<ImpuestoDetalle>();
    }

}