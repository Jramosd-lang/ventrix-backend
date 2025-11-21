using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_ventrix.Data;
using api_ventrix.Models;
using Microsoft.AspNetCore.Authorization;

namespace api_ventrix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NegociosController : ControllerBase
    {
        private readonly ConeccionContext _context;

        public NegociosController(ConeccionContext context)
        {
            _context = context;
        }

        // GET: api/Negocios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Negocio>>> GetNegocios()
        {
            return await _context.Negocios.ToListAsync();
        }

        // GET: api/Negocios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Negocio>> GetNegocio(int id)
        {
            var negocio = await _context.Negocios
                .Include(n => n.Productos)
                .FirstOrDefaultAsync(n => n.id == id);

            if (negocio == null)
                return NotFound();

            return negocio;
        }

        // PUT: api/Negocios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNegocio(int id, Negocio negocio)
        {
            if (id != negocio.id)
                return BadRequest();

            _context.Entry(negocio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NegocioExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Negocios
        [HttpPost]
        public async Task<ActionResult<Negocio>> PostNegocio(Negocio negocio)
        {
            _context.Negocios.Add(negocio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNegocio), new { id = negocio.id }, negocio);
        }

        // DELETE: api/Negocios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNegocio(int id)
        {
            var negocio = await _context.Negocios.FindAsync(id);
            if (negocio == null)
                return NotFound();

            _context.Negocios.Remove(negocio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/Negocios/5/productos
        [Authorize(Roles = "vendedor")]
        [HttpGet("{id}/productos")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductosDeNegocio(int id)
        {
            var productos = await _context.Productos
                .Where(p => p.Id_Negocio == id)
                .ToListAsync();

            if (productos == null)
                return NotFound("Este negocio no tiene productos o no existe.");


            return Ok(productos);
        }

        // POST: api/Negocios/5/productos
        [HttpPost("{id}/productos")]
        public async Task<ActionResult> AgregarProducto(int id, Producto producto)
        {
            var negocio = await _context.Negocios.FindAsync(id);
            if (negocio == null)
                return NotFound("Negocio no encontrado.");

            producto.Id_Negocio = id;
            _context.Productos.Add(producto);

            await _context.SaveChangesAsync();
            return Ok("Producto agregado correctamente.");
        }

        // PUT: api/Negocios/5/productos/10
        [HttpPut("{negocioId}/productos/{productoId}")]
        public async Task<IActionResult> EditarProducto(int negocioId, int productoId, Producto producto)
        {
            var productoExistente = await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == productoId && p.Id_Negocio == negocioId);

            if (productoExistente == null)
                return NotFound("Producto no encontrado en este negocio.");

            productoExistente.Nombre = producto.Nombre;
            productoExistente.Valor = producto.Valor;
            productoExistente.Stock = producto.Stock;
            productoExistente.Descripcion = producto.Descripcion;

            await _context.SaveChangesAsync();
            return Ok("Producto actualizado.");
        }

        // DELETE: api/Negocios/5/productos/10

        [HttpDelete("{negocioId}/productos/{productoId}")]
        public async Task<IActionResult> DeleteProducto(int negocioId, int productoId)
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == productoId && p.Id_Negocio == negocioId);

            if (producto == null)
                return NotFound("Producto no encontrado en este negocio.");

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NegocioExists(int id)
        {
            return _context.Negocios.Any(n => n.id == id);
        }
    }
}
