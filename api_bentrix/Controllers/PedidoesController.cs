using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using api_ventrix.Data;
using api_ventrix.Models;
using Azure.Identity;
using api_ventrix.Hubs;

namespace api_ventrix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoesController : ControllerBase
    {
        private readonly ConeccionContext _context;
        private readonly IHubContext<Hub_pedidos> _hubContext;

        public PedidoesController(ConeccionContext context, IHubContext<Hub_pedidos> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/Pedidoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        [HttpGet("negocio/{id}")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidosPorNegocio(int id)
        {
            var pedidos = await _context.Pedidos
                .Where(p => p.Id_Negocio == id)
                .OrderBy(p => p.Id) 
                .Include(p => p.Productos) 
                .Include(p => p.Comprador) 
                .ToListAsync();

            if (!pedidos.Any()) 
                return NotFound($"No se encontraron pedidos para el negocio con ID {id}");

            return Ok(pedidos);
        }


        // GET: api/Pedidoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // PUT: api/Pedidoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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

        // POST: api/Pedidoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(PedidoDTO pedido)
        {
            var negocio = await _context.Negocios.FirstOrDefaultAsync(n => n.id == pedido.Id_Negocio);
            var comprador = await _context.Compradores.FirstOrDefaultAsync(c => c.id == pedido.Id_Comprador);

            if (negocio == null) return NotFound("No se encontró negocio");
            if (pedido.Id_Comprador.HasValue && comprador == null) return NotFound("No se encontró comprador");

            var productos = await _context.Productos
                .Where(p => pedido.Ids_Productos.Contains(p.Id))
                .ToListAsync();

            var idsNoEncontrados = pedido.Ids_Productos.Except(productos.Select(p => p.Id)).ToList();
            if (idsNoEncontrados.Any()) return BadRequest($"Productos no existen: {string.Join(", ", idsNoEncontrados)}");

            var pedido_ = new Pedido
            {
                Id_Negocio = negocio.id,
                Id_Comprador = comprador?.id,
                Productos = productos,
                Total_Pagar = pedido.Total_Pagar,
                Metodo_Pago = pedido.Metodo_Pago,
                Estado = pedido.Estado ?? "Pendiente",
                Direccion_Envio = pedido.Direccion_Envio,
                Observaciones = pedido.Observaciones,
                Negocio = negocio,
                Comprador = comprador
            };

            _context.Pedidos.Add(pedido_);
            await _context.SaveChangesAsync();

            // ---- EMITIR A TODOS con el nombre que escucha el frontend ----
            await _hubContext.Clients.All.SendAsync("PedidoRecibido", pedido_);

            Console.WriteLine($"Controller: emitido PedidoRecibido para pedido id={pedido_.Id}");

            return CreatedAtAction("GetPedido", new { id = pedido_.Id }, pedido_);
        }



        // DELETE: api/Pedidoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
