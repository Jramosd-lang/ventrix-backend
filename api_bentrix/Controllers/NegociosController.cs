using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_ventrix.Models;
using api_ventrix.Data;

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
            var negocio = await _context.Negocios.FindAsync(id);

            if (negocio == null)
            {
                return NotFound();
            }

            return negocio;
        }

        [HttpGet("productosNegocio")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductosDeNegocio(int negocioId)
        {
            var negocio = await _context.Negocios
                .Include(n => n.Productos)
                .FirstOrDefaultAsync(n => n.id == negocioId);
            if (negocio == null)
            {
                return NotFound("Negocio no encontrado.");
            }
            return Ok(negocio.Productos);
        }

        [HttpPost("agregarProducto")]
        public async Task<ActionResult> AgregarProductoANegocio(int negocioId, Producto producto)
        {
            var negocio = await _context.Negocios.Include(n => n.Productos).FirstOrDefaultAsync(n => n.id == negocioId);

            if (negocio == null)
            {
                return NotFound("Negocio no encontrado.");
            }
            negocio.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return Ok("Producto agregado al negocio exitosamente.");
        }

        [HttpPut("editarProducto")]
        public async Task<IActionResult> EditarProducto(int negocioId, Producto producto)
        {
            // Cargamos el negocio con sus productos
            var negocio = await _context.Negocios
                .Include(n => n.Productos)
                .FirstOrDefaultAsync(n => n.id == negocioId);

            if (negocio == null)
            {
                return NotFound("Negocio no encontrado.");
            }

            // Buscamos el producto existente dentro del negocio
            var productoExistente = negocio.Productos.FirstOrDefault(p => p.Id == producto.Id);
            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado en este negocio.");
            }

   
            productoExistente.Nombre = producto.Nombre;
            productoExistente.Valor = producto.Valor;
            productoExistente.Stock = producto.Stock;
            productoExistente.Descripcion = producto.Descripcion;

    

            // Guardamos cambios en la base de datos
            await _context.SaveChangesAsync();

            return Ok("Producto actualizado correctamente.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNegocio(int id, Negocio negocio)
        {
            if (id != negocio.id)
            {
                return BadRequest();
            }

            _context.Entry(negocio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NegocioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Negocios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Negocio>> PostNegocio(Negocio negocio)
        {
            _context.Negocios.Add(negocio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNegocio", new { id = negocio.id }, negocio);
        }

        // DELETE: api/Negocios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNegocio(int id)
        {
            var negocio = await _context.Negocios.FindAsync(id);
            if (negocio == null)
            {
                return NotFound();
            }

            _context.Negocios.Remove(negocio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool NegocioExists(int id)
        {
            return _context.Negocios.Any(e => e.id == id);
        }
    }
}
