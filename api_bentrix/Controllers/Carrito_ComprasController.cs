using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_bentrix.Models;
using api_ventrix.Data;

namespace api_ventrix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Carrito_ComprasController : ControllerBase
    {
        private readonly ConeccionContext _context;

        public Carrito_ComprasController(ConeccionContext context)
        {
            _context = context;
        }

        // GET: api/Carrito_Compras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrito_Compras>>> GetCarritosCompras()
        {
            return await _context.CarritosCompras.ToListAsync();
        }

        // GET: api/Carrito_Compras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carrito_Compras>> GetCarrito_Compras(int id)
        {
            var carrito_Compras = await _context.CarritosCompras.FindAsync(id);

            if (carrito_Compras == null)
            {
                return NotFound();
            }

            return carrito_Compras;
        }

        // PUT: api/Carrito_Compras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrito_Compras(int id, Carrito_Compras carrito_Compras)
        {
            if (id != carrito_Compras.Id)
            {
                return BadRequest();
            }

            _context.Entry(carrito_Compras).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Carrito_ComprasExists(id))
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

        // POST: api/Carrito_Compras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carrito_Compras>> PostCarrito_Compras(Carrito_Compras carrito_Compras)
        {
            _context.CarritosCompras.Add(carrito_Compras);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrito_Compras", new { id = carrito_Compras.Id }, carrito_Compras);
        }

        // DELETE: api/Carrito_Compras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrito_Compras(int id)
        {
            var carrito_Compras = await _context.CarritosCompras.FindAsync(id);
            if (carrito_Compras == null)
            {
                return NotFound();
            }

            _context.CarritosCompras.Remove(carrito_Compras);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Carrito_ComprasExists(int id)
        {
            return _context.CarritosCompras.Any(e => e.Id == id);
        }
    }
}
