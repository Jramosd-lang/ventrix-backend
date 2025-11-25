using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_ventrix.Migrations
{
    /// <inheritdoc />
    public partial class nuevaclasepedidoitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Pedidos_PedidoId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_PedidoId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "Productos");

            migrationBuilder.AddColumn<int>(
                name: "PedidoItemId",
                table: "Impuestos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "pedidoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Pedido = table.Column<int>(type: "int", nullable: false),
                    Id_Producto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codigo_Lote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Negocio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pedidoItems_Pedidos_Id_Pedido",
                        column: x => x.Id_Pedido,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pedidoItems_Productos_Id_Producto",
                        column: x => x.Id_Producto,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Impuestos_PedidoItemId",
                table: "Impuestos",
                column: "PedidoItemId");

            migrationBuilder.CreateIndex(
                name: "IX_pedidoItems_Id_Pedido",
                table: "pedidoItems",
                column: "Id_Pedido");

            migrationBuilder.CreateIndex(
                name: "IX_pedidoItems_Id_Producto",
                table: "pedidoItems",
                column: "Id_Producto");

            migrationBuilder.AddForeignKey(
                name: "FK_Impuestos_pedidoItems_PedidoItemId",
                table: "Impuestos",
                column: "PedidoItemId",
                principalTable: "pedidoItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Impuestos_pedidoItems_PedidoItemId",
                table: "Impuestos");

            migrationBuilder.DropTable(
                name: "pedidoItems");

            migrationBuilder.DropIndex(
                name: "IX_Impuestos_PedidoItemId",
                table: "Impuestos");

            migrationBuilder.DropColumn(
                name: "PedidoItemId",
                table: "Impuestos");

            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_PedidoId",
                table: "Productos",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Pedidos_PedidoId",
                table: "Productos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");
        }
    }
}
