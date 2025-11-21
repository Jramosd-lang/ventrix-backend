using api_ventrix.Data;
using api_ventrix.Models;
using clase1_progweb.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_ventrix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompradorsController : ControllerBase
    {
        private readonly ConeccionContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public CompradorsController(ConeccionContext context, JwtTokenGenerator jwt)
        {
            _context = context;
            _jwtTokenGenerator = jwt;
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
            var comprador = await _context.Compradores.FirstOrDefaultAsync(p => p.Usuario == login.Usuario);


            if (comprador == null || !BCrypt.Net.BCrypt.Verify(login.Contraseña, comprador.Clave_Hasheada))
            {
                return BadRequest("credenciales incorrectas");
            }

            var token = _jwtTokenGenerator.GenerateToken(login.Usuario, "comprador");

            return Ok(new
            {
                id = comprador.id,
                token = token,
                rol = "vendedor",
            });
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
            var comprador_encontrado = await _context.Compradores.FirstOrDefaultAsync(p => p.Usuario == comprador.Usuario);

            if (comprador_encontrado != null) return BadRequest("Este usuario ya existe");

            comprador.Clave_Hasheada = BCrypt.Net.BCrypt.HashPassword(comprador.Clave_Hasheada);

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
