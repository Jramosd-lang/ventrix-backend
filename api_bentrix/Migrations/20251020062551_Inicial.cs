using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_ventrix.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<int>(type: "int", nullable: false),
                    Id_Negocio = table.Column<int>(type: "int", nullable: false),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarritosCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Direccion_Destino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor_Envio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Id_Negocio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritosCompras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Negocios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagenes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Negocios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo_Reporte = table.Column<int>(type: "int", nullable: false),
                    Id_Negocio = table.Column<int>(type: "int", nullable: false),
                    Id_Vendedor = table.Column<int>(type: "int", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Descuentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Descuento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo_Descuento = table.Column<int>(type: "int", nullable: false),
                    Valor_Min = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Valor_Max = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Valor_Aplica = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_Expiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Carrito_ComprasId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descuentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Descuentos_CarritosCompras_Carrito_ComprasId",
                        column: x => x.Carrito_ComprasId,
                        principalTable: "CarritosCompras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clave_Hasheada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero_Documento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo_Documento = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Administrador_Id_Negocio = table.Column<int>(type: "int", nullable: true),
                    Nivel_Acceso = table.Column<int>(type: "int", nullable: true),
                    VendedorId = table.Column<int>(type: "int", nullable: true),
                    Fecha_Registro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Negocio = table.Column<int>(type: "int", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detalles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Carrito_ComprasId = table.Column<int>(type: "int", nullable: true),
                    NegocioId = table.Column<int>(type: "int", nullable: true),
                    Plan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha_Inicio_Plan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fecha_Fin_Plan = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personas_CarritosCompras_Carrito_ComprasId",
                        column: x => x.Carrito_ComprasId,
                        principalTable: "CarritosCompras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Personas_Negocios_NegocioId",
                        column: x => x.NegocioId,
                        principalTable: "Negocios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Personas_Personas_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Personas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Calificacion = table.Column<int>(type: "int", nullable: false),
                    Codigo_Lote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Negocio = table.Column<int>(type: "int", nullable: false),
                    Carrito_ComprasId = table.Column<int>(type: "int", nullable: true),
                    DescuentoId = table.Column<int>(type: "int", nullable: true),
                    NegocioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_CarritosCompras_Carrito_ComprasId",
                        column: x => x.Carrito_ComprasId,
                        principalTable: "CarritosCompras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Productos_Descuentos_DescuentoId",
                        column: x => x.DescuentoId,
                        principalTable: "Descuentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Productos_Negocios_NegocioId",
                        column: x => x.NegocioId,
                        principalTable: "Negocios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MetodosPago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Proveedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompradorId = table.Column<int>(type: "int", nullable: true),
                    NegocioId = table.Column<int>(type: "int", nullable: true),
                    VendedorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodosPago", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetodosPago_Negocios_NegocioId",
                        column: x => x.NegocioId,
                        principalTable: "Negocios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MetodosPago_Personas_CompradorId",
                        column: x => x.CompradorId,
                        principalTable: "Personas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MetodosPago_Personas_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Personas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Impuestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impuestos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Impuestos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImpuestoDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImpuestoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpuestoDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImpuestoDetalle_Impuestos_ImpuestoId",
                        column: x => x.ImpuestoId,
                        principalTable: "Impuestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Descuentos_Carrito_ComprasId",
                table: "Descuentos",
                column: "Carrito_ComprasId");

            migrationBuilder.CreateIndex(
                name: "IX_ImpuestoDetalle_ImpuestoId",
                table: "ImpuestoDetalle",
                column: "ImpuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_Impuestos_ProductoId",
                table: "Impuestos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_MetodosPago_CompradorId",
                table: "MetodosPago",
                column: "CompradorId");

            migrationBuilder.CreateIndex(
                name: "IX_MetodosPago_NegocioId",
                table: "MetodosPago",
                column: "NegocioId");

            migrationBuilder.CreateIndex(
                name: "IX_MetodosPago_VendedorId",
                table: "MetodosPago",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_Carrito_ComprasId",
                table: "Personas",
                column: "Carrito_ComprasId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_NegocioId",
                table: "Personas",
                column: "NegocioId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_VendedorId",
                table: "Personas",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Carrito_ComprasId",
                table: "Productos",
                column: "Carrito_ComprasId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_DescuentoId",
                table: "Productos",
                column: "DescuentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_NegocioId",
                table: "Productos",
                column: "NegocioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "ImpuestoDetalle");

            migrationBuilder.DropTable(
                name: "MetodosPago");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "Impuestos");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Descuentos");

            migrationBuilder.DropTable(
                name: "Negocios");

            migrationBuilder.DropTable(
                name: "CarritosCompras");
        }
    }
}
