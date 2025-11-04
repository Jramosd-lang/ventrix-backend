using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_ventrix.Models;
using api_ventrix.Data;

namespace api_ventrix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly ConeccionContext _context;

        public FacturasController(ConeccionContext context)
        {
            _context = context;
        }

        // GET: api/Facturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            return await _context.Facturas
                .Include(f => f.Productos)
                .Include(f => f.Descuentos_aplicados)
                .Include(f => f.metodoPago)
                .ToListAsync();
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            var factura = await _context.Facturas
                .Include(f => f.Productos)
                .Include(f => f.Descuentos_aplicados)
                .Include(f => f.metodoPago)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (factura == null)
                return NotFound();

            return factura;
        }

        // POST: api/Facturas
        [HttpPost]
        public async Task<ActionResult<Factura>> PostFactura(FacturaInputModel input)
        {
            var comprador = await _context.Compradores.FirstOrDefaultAsync(p => p.id == input.Id_comprador);
            var negocio = await _context.Negocios.FirstOrDefaultAsync(n => n.id == input.Id_negocio);

            if (comprador == null)
            {
                return BadRequest("No se asigno un comprador valido");
            }

            if (negocio == null)
            {
                return BadRequest("No se asigno un negocio valido");
            }



            var factura = new Factura
            {
                Codigo = input.Codigo,
                Id_negocio = input.Id_negocio,
                Id_comprador = input.Id_comprador,
                Fecha = input.Fecha,
                subtotal = input.subtotal,
                total = input.total,
                Descuentos_aplicados = await _context.Descuentos
                    .Where(d => input.Descuentos_aplicados.Contains(d.Id))
                    .ToListAsync(),
                Productos = await _context.Productos
                    .Where(p => input.Productos.Contains(p.Id))
                    .ToListAsync(),
                metodoPago = await _context.MetodosPago
                    .FindAsync(input.metodoPago)
            };

            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFactura), new { id = factura.Id }, factura);
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
                return NotFound();

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    // Modelo auxiliar de entrada
    public class FacturaInputModel
    {
        public string Codigo { get; set; }
        public int Id_negocio { get; set; }
        public int Id_comprador { get; set; }
        public DateTime Fecha { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }
        public List<int> Descuentos_aplicados { get; set; } = new();
        public List<int> Productos { get; set; } = new();
        public int metodoPago { get; set; }
    }
}
