using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_ventrix.Migrations
{
    /// <inheritdoc />
    public partial class pedidofix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Negocio = table.Column<int>(type: "int", nullable: false),
                    Id_Comprador = table.Column<int>(type: "int", nullable: true),
                    Total_Pagar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Metodo_Pago = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_Pago = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fecha_Envio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fecha_Entrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Direccion_Envio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Compradores_Id_Comprador",
                        column: x => x.Id_Comprador,
                        principalTable: "Compradores",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Pedidos_Negocios_Id_Negocio",
                        column: x => x.Id_Negocio,
                        principalTable: "Negocios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_PedidoId",
                table: "Productos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Id_Comprador",
                table: "Pedidos",
                column: "Id_Comprador");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Id_Negocio",
                table: "Pedidos",
                column: "Id_Negocio");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Pedidos_PedidoId",
                table: "Productos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Pedidos_PedidoId",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_PedidoId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "Productos");
        }
    }
}
