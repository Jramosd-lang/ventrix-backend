using System.Collections.Generic;

namespace api_bentrix.Models
{
    public class Impuesto
    {
        public int Id { get; set; }

        public List<ImpuestoDetalle> Impuestos { get; set; } = new List<ImpuestoDetalle>();
    }

}