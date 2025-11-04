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
    public class DescuentoesController : ControllerBase
    {
        private readonly ConeccionContext _context;

        public DescuentoesController(ConeccionContext context)
        {
            _context = context;
        }

        // GET: api/Descuentoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Descuento>>> GetDescuentos()
        {
            return await _context.Descuentos.ToListAsync();
        }

        // GET: api/Descuentoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Descuento>> GetDescuento(int id)
        {
            var descuento = await _context.Descuentos.FindAsync(id);

            if (descuento == null)
            {
                return NotFound();
            }

            return descuento;
        }

        // PUT: api/Descuentoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDescuento(int id, Descuento descuento)
        {
            if (id != descuento.Id)
            {
                return BadRequest();
            }

            _context.Entry(descuento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DescuentoExists(id))
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

        // POST: api/Descuentoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Descuento>> PostDescuento(Descuento descuento)
        {
            _context.Descuentos.Add(descuento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDescuento", new { id = descuento.Id }, descuento);
        }

        // DELETE: api/Descuentoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDescuento(int id)
        {
            var descuento = await _context.Descuentos.FindAsync(id);
            if (descuento == null)
            {
                return NotFound();
            }

            _context.Descuentos.Remove(descuento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DescuentoExists(int id)
        {
            return _context.Descuentos.Any(e => e.Id == id);
        }
    }
}
