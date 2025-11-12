using Microsoft.EntityFrameworkCore;
using api_ventrix.Data;
using api_ventrix.Hubs;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ConeccionContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔥 Solo UNA política CORS, y con AllowCredentials
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// ⚠️ IMPORTANTE: CORS aquí, antes de Authorization y MapHub
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();
app.MapHub<Hub_pedidos>("/pedidosHub");

app.Run();
