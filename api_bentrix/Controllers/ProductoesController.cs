using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_ventrix.Models;
using api_ventrix.Data;
using Microsoft.AspNetCore.Authorization;

namespace api_ventrix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ConeccionContext _context;

        public ProductosController(ConeccionContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound("Producto no encontrado.");

            return Ok(producto);
        }

        // POST: api/Productos
        [Authorize(Roles = "vendedor")]
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        // PUT: api/Productos/5
        [Authorize(Roles = "vendedor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
                return BadRequest("El ID del producto no coincide.");

            var productoExistente = await _context.Productos.FindAsync(id);
            if (productoExistente == null)
                return NotFound("Producto no encontrado.");

            productoExistente.Nombre = producto.Nombre;
            productoExistente.Valor = producto.Valor;
            productoExistente.Stock = producto.Stock;
            productoExistente.Descripcion = producto.Descripcion;

            await _context.SaveChangesAsync();
            return Ok("Producto actualizado correctamente.");
        }

        // DELETE: api/Productos/5
        [Authorize(Roles = "vendedor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound("Producto no encontrado.");

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok("Producto eliminado correctamente.");
        }
    }
}
