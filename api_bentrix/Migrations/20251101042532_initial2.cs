using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_ventrix.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Compradorid",
                table: "MetodosPago",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Compradores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha_Registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Detalles = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Clave_Hasheada = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Numero_Documento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Tipo_Documento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compradores", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MetodosPago_Compradorid",
                table: "MetodosPago",
                column: "Compradorid");

            migrationBuilder.AddForeignKey(
                name: "FK_MetodosPago_Compradores_Compradorid",
                table: "MetodosPago",
                column: "Compradorid",
                principalTable: "Compradores",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MetodosPago_Compradores_Compradorid",
                table: "MetodosPago");

            migrationBuilder.DropTable(
                name: "Compradores");

            migrationBuilder.DropIndex(
                name: "IX_MetodosPago_Compradorid",
                table: "MetodosPago");

            migrationBuilder.DropColumn(
                name: "Compradorid",
                table: "MetodosPago");
        }
    }
}
