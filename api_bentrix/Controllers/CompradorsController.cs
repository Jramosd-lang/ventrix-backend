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
    public class CompradorsController : ControllerBase
    {
        private readonly ConeccionContext _context;

        public CompradorsController(ConeccionContext context)
        {
            _context = context;
        }

        // GET: api/Compradors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comprador>>> GetCompradores()
        {
            return await _context.Compradores.ToListAsync();
        }

        // GET: api/Compradors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comprador>> GetComprador(int id)
        {
            var comprador = await _context.Compradores.FindAsync(id);

            if (comprador == null)
            {
                return NotFound();
            }

            return comprador;
        }

        [HttpPost("login-comprador")]
        public async Task<ActionResult<Comprador>> loginComprador(LoginModel login)
        {
            var comprador = await _context.Compradores.FirstOrDefaultAsync(c => c.Usuario == login.Usuario && c.Clave_Hasheada == login.Contraseña);
            if (comprador == null)
            {
                return NotFound();
            }
            return comprador;
        }

        public class LoginModel()
        {
            public string Usuario { get; set; }
            public string Contraseña { get; set; }
        }

        // PUT: api/Compradors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComprador(int id, Comprador comprador)
        {
            if (id != comprador.id)
            {
                return BadRequest();
            }

            _context.Entry(comprador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompradorExists(id))
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

        // POST: api/Compradors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comprador>> PostComprador(Comprador comprador)
        {
            _context.Compradores.Add(comprador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComprador", new { id = comprador.id }, comprador);
        }

        // DELETE: api/Compradors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComprador(int id)
        {
            var comprador = await _context.Compradores.FindAsync(id);
            if (comprador == null)
            {
                return NotFound();
            }

            _context.Compradores.Remove(comprador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompradorExists(int id)
        {
            return _context.Compradores.Any(e => e.id == id);
        }
    }
}
