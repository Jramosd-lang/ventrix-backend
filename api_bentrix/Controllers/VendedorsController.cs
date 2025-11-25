using api_ventrix.Data;
using api_ventrix.Models;
using clase1_progweb.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace api_ventrix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorsController : ControllerBase
    {
        private readonly ConeccionContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public VendedorsController(ConeccionContext context, JwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public class LoginRequest
        {
            public string Usuario { get; set; }
            public string Contraseña { get; set; }
        }

        [HttpPost("login-vendedor")]
        public async Task<ActionResult> loginVendedor(LoginRequest login)

        {
            var vendedor = await _context.Vendedores.Include(p => p.Negocio).FirstOrDefaultAsync(p => p.Usuario == login.Usuario);


            if (vendedor == null || !BCrypt.Net.BCrypt.Verify(login.Contraseña, vendedor.Clave_Hasheada))
            {
                return BadRequest("credenciales incorrectas");
            }

            var token = _jwtTokenGenerator.GenerateToken(login.Usuario, "vendedor", vendedor.Negocio.id);
            if (vendedor.Negocio == null)
            {
                // Opciones: devolver un error claro o enviar id_negocio nulo
                return BadRequest("El vendedor no tiene un negocio asociado.");
            }

            return Ok(new
            {
                id = vendedor.id,
                token = token,
                rol = "vendedor",
                id_negocio = vendedor.Negocio.id
            });

        }


        // CAMBIAR USUARIO (protegido)
        [Authorize(Roles = "vendedor")]
        [HttpPut("cambiar-usuario")]
        public async Task<IActionResult> cambiarUsuario(string usuario)
        {
            var username = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            var vendedor = await _context.Vendedores.FirstOrDefaultAsync(v => v.Usuario == username);
            if (vendedor == null)
            {
                return Unauthorized();
            }

            vendedor.Usuario = usuario;
            _context.Entry(vendedor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("usuario actualizado");
        }


        // CAMBIAR CONTRASEÑA (protegido)
        [Authorize(Roles = "vendedor")]
        [HttpPut("cambiar-contraseña")]
        public async Task<IActionResult> CambiarContraseña(string contraseña)
        {
            var username = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            var vendedor = await _context.Vendedores.FirstOrDefaultAsync(v => v.Usuario == username);

            if (vendedor == null)
            {
                return Unauthorized();
            }

            vendedor.Clave_Hasheada = BCrypt.Net.BCrypt.HashPassword(contraseña);
            _context.Entry(vendedor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("contraseña actualizada");
        }


        // REGISTRO (público)
        [HttpPost]
        public async Task<ActionResult<Vendedor>> PostVendedor(Vendedor vendedor)
        {
            var usuario_encontrado = _context.Vendedores.FirstOrDefault(p => p.Usuario == vendedor.Usuario);

            if (usuario_encontrado != null)
            {
                return BadRequest("El nombre de usuario ya está en uso.");
            }

            var contraseña_hashed = BCrypt.Net.BCrypt.HashPassword(vendedor.Clave_Hasheada);
            vendedor.Clave_Hasheada = contraseña_hashed;

            _context.Vendedores.Add(vendedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendedor", new { id = vendedor.id }, vendedor);
        }


        // DELETE (público o admin, tu decides)
        [Authorize(Roles = "vendedor")]
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
    }
}
