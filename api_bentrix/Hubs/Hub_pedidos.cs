using Microsoft.AspNetCore.SignalR;

namespace api_ventrix.Hubs
{
    public class Hub_pedidos : Hub
    {
        public async Task UnirseAlNegocio(string negocioId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, negocioId);
            Console.WriteLine($"✅ Cliente {Context.ConnectionId} unido al negocio {negocioId}");
            await Clients.Caller.SendAsync("UnidoAlNegocio", negocioId);
        }

        public async Task SalirDelNegocio(string negocioId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, negocioId);
            Console.WriteLine($"👋 Cliente {Context.ConnectionId} salió del negocio {negocioId}");
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"🔌 Nueva conexión: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"❌ Desconexión: {Context.ConnectionId}");
            if (exception != null)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}