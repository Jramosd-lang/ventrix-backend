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
    public class ImpuestoesController : ControllerBase
    {
        private readonly ConeccionContext _context;

        public ImpuestoesController(ConeccionContext context)
        {
            _context = context;
        }

        // GET: api/Impuestoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Impuesto>>> GetImpuestos()
        {
            return await _context.Impuestos.ToListAsync();
        }

        // GET: api/Impuestoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Impuesto>> GetImpuesto(int id)
        {
            var impuesto = await _context.Impuestos.FindAsync(id);

            if (impuesto == null)
            {
                return NotFound();
            }

            return impuesto;
        }

        // PUT: api/Impuestoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImpuesto(int id, Impuesto impuesto)
        {
            if (id != impuesto.Id)
            {
                return BadRequest();
            }

            _context.Entry(impuesto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImpuestoExists(id))
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

        // POST: api/Impuestoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Impuesto>> PostImpuesto(Impuesto impuesto)
        {
            _context.Impuestos.Add(impuesto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImpuesto", new { id = impuesto.Id }, impuesto);
        }

        // DELETE: api/Impuestoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImpuesto(int id)
        {
            var impuesto = await _context.Impuestos.FindAsync(id);
            if (impuesto == null)
            {
                return NotFound();
            }

            _context.Impuestos.Remove(impuesto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImpuestoExists(int id)
        {
            return _context.Impuestos.Any(e => e.Id == id);
        }
    }
}
