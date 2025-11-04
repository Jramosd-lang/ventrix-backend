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
    public class VendedorsController : ControllerBase
    {
        private readonly ConeccionContext _context;

        public VendedorsController(ConeccionContext context)
        {
            _context = context;
        }

        // GET: api/Vendedors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendedor>>> GetVendedores()
        {
            return await _context.Vendedores.Include(v => v.Negocio).ToListAsync();
        }

        // GET: api/Vendedors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendedor>> GetVendedor(int id)
        {
            var vendedor = await _context.Vendedores.FindAsync(id);

            if (vendedor == null)
            {
                return NotFound();
            }

            return vendedor;
        }

        public class LoginRequest
        {
            public string Usuario { get; set; }
            public string Contraseña { get; set; }
        }


        [HttpPost("login-vendedor")]
        public async Task<ActionResult<Vendedor>> loginVendedor(LoginRequest login)
        {
            var vendedor = await _context.Vendedores.Include(v => v.Negocio)
                .FirstOrDefaultAsync(v => v.Usuario == login.Usuario && v.Clave_Hasheada == login.Contraseña);
            if (vendedor == null)
            {
                return NotFound();
            }
            return vendedor;
        }

        [HttpPut("cambiar-usuario")]
        public async Task<IActionResult> cambiarUsuario(string usuario, int id)
        {
            var vendedor = await _context.Vendedores.FirstOrDefaultAsync(v => v.id == id);
            if (vendedor == null)
            {
                return NotFound();
            }

            vendedor.Usuario = usuario;
            _context.Entry(vendedor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("cambiar-contraseña")]
        public async Task<IActionResult> CambiarContraseña(string contraseña, int id)
        {
            var vendedor = await _context.Vendedores.FirstOrDefaultAsync(v => v.id == id);

            if (vendedor == null)
            {
                return NotFound();
            }

            vendedor.Clave_Hasheada = contraseña;
            _context.Entry(vendedor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();

        }


        // PUT: api/Vendedors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendedor(int id, Vendedor vendedor)
        {
            if (id != vendedor.id)
            {
                return BadRequest();
            }

            _context.Entry(vendedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendedorExists(id))
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

        // POST: api/Vendedors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vendedor>> PostVendedor(Vendedor vendedor)
        {
            _context.Vendedores.Add(vendedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendedor", new { id = vendedor.id }, vendedor);
        }

        // DELETE: api/Vendedors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendedor(int id)
        {
            var vendedor = await _context.Vendedores.FindAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            _context.Vendedores.Remove(vendedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendedorExists(int id)
        {
            return _context.Vendedores.Any(e => e.id == id);
        }
    }
}
