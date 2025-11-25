using api_ventrix.Data;
using api_ventrix.Hubs;
using api_ventrix.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

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

        // ================================
        // OBTENER PEDIDOS POR NEGOCIO
        // ================================
        [Authorize(Roles = "vendedor")]
        [HttpGet("negocio")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidosPorNegocio()
        {
            var idNegocioClaim = User.FindFirst("id_negocio")?.Value;
            if (idNegocioClaim == null)
                return Unauthorized("El token no contiene id_negocio.");

            int idNegocio = int.Parse(idNegocioClaim);

            var pedidos = await _context.Pedidos
                .Where(p => p.Id_Negocio == idNegocio)
                .Include(p => p.PedidoItems)
                    .ThenInclude(i => i.Producto)
                .Include(p => p.Comprador)
                .OrderBy(p => p.Id)
                .ToListAsync();

            if (!pedidos.Any())
                return NotFound("No se encontraron pedidos para tu negocio.");

            return Ok(pedidos);
        }

        // ================================
        // OBTENER PEDIDO POR ID
        // ================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.PedidoItems)
                    .ThenInclude(i => i.Producto)
                .Include(p => p.Comprador)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound();

            return pedido;
        }

        // ================================
        // CREAR PEDIDO
        // ================================
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(PedidoDTO dto)
        {
            var negocio = await _context.Negocios.FindAsync(dto.Id_Negocio);
            if (negocio == null) return NotFound("No se encontró negocio");

            Comprador comprador = null;
            if (dto.Id_Comprador.HasValue)
            {
                comprador = await _context.Compradores.FindAsync(dto.Id_Comprador.Value);
                if (comprador == null) return NotFound("No se encontró comprador");
            }

            var productos = await _context.Productos
                .Where(p => dto.Ids_Productos.Contains(p.Id))
                .ToListAsync();

            var idsNoEncontrados = dto.Ids_Productos.Except(productos.Select(p => p.Id)).ToList();
            if (idsNoEncontrados.Any())
                return BadRequest($"Productos no existen: {string.Join(", ", idsNoEncontrados)}");

            // Crear el pedido
            var pedido = new Pedido
            {
                Id_Negocio = dto.Id_Negocio,
                Id_Comprador = dto.Id_Comprador,
                Total_Pagar = dto.Total_Pagar,
                Metodo_Pago = dto.Metodo_Pago,
                Estado = dto.Estado,
                Direccion_Envio = dto.Direccion_Envio,
                Observaciones = dto.Observaciones,
                Fecha_Creacion = dto.Fecha_Creacion
            };

            // Crear los PedidoItems
            foreach (var prod in productos)
            {
                var item = new PedidoItem
                {
                    Id_Producto = prod.Id,
                    Valor = prod.Valor, // Copiar el precio actual
                    Cantidad = 1,
                    ImagenUrl = prod.ImagenUrl,
                    Id_Negocio = dto.Id_Negocio
                };

                pedido.PedidoItems.Add(item);
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            // Emitir evento por SignalR
            await _hubContext.Clients.All.SendAsync("PedidoRecibido", pedido);

            return CreatedAtAction("GetPedido", new { id = pedido.Id }, pedido);
        }

        // ================================
        // MODIFICAR ESTADO
        // ================================
        public class CambiarEstadoDTO
        {
            public int Id_Pedido { get; set; }
            public string Nuevo_Estado { get; set; }
        }

        [Authorize(Roles = "vendedor")]
        [HttpPut("cambiar-estado")]
        public async Task<IActionResult> CambiarEstadoPedido(CambiarEstadoDTO dto)
        {
            var pedido = await _context.Pedidos.FindAsync(dto.Id_Pedido);
            if (pedido == null)
                return NotFound("Pedido no encontrado");

            pedido.Estado = dto.Nuevo_Estado;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ================================
        // ELIMINAR PEDIDO
        // ================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.PedidoItems)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound();

            _context.pedidoItems.RemoveRange(pedido.PedidoItems);
            _context.Pedidos.Remove(pedido);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
