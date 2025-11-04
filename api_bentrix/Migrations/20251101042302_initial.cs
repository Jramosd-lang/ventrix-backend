using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_ventrix.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
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
                    Comentario = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Negocios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UrlLogo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Imagenes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NegocioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Negocios", x => x.id);
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
                    Contenido = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendedores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Negocioid = table.Column<int>(type: "int", nullable: false),
                    Plan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fecha_Inicio_Plan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_Fin_Plan = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Vendedores", x => x.id);
                    table.ForeignKey(
                        name: "FK_Vendedores_Negocios_Negocioid",
                        column: x => x.Negocioid,
                        principalTable: "Negocios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Administradores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Negocio = table.Column<int>(type: "int", nullable: false),
                    Nivel_Acceso = table.Column<int>(type: "int", nullable: false),
                    Vendedorid = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Administradores", x => x.id);
                    table.ForeignKey(
                        name: "FK_Administradores_Negocios_Id_Negocio",
                        column: x => x.Id_Negocio,
                        principalTable: "Negocios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Administradores_Vendedores_Vendedorid",
                        column: x => x.Vendedorid,
                        principalTable: "Vendedores",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "MetodosPago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Proveedor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Negocioid = table.Column<int>(type: "int", nullable: true),
                    Vendedorid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodosPago", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetodosPago_Negocios_Negocioid",
                        column: x => x.Negocioid,
                        principalTable: "Negocios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_MetodosPago_Vendedores_Vendedorid",
                        column: x => x.Vendedorid,
                        principalTable: "Vendedores",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_negocio = table.Column<int>(type: "int", nullable: false),
                    Id_comprador = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    metodoPagoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facturas_MetodosPago_metodoPagoId",
                        column: x => x.metodoPagoId,
                        principalTable: "MetodosPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Descuentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Descuento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tipo_Descuento = table.Column<int>(type: "int", nullable: false),
                    Valor_Min = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Valor_Max = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Valor_Aplica = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_Expiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacturaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descuentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Descuentos_Facturas_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Facturas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Calificacion = table.Column<int>(type: "int", nullable: false),
                    Codigo_Lote = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Negocio = table.Column<int>(type: "int", nullable: false),
                    DescuentoId = table.Column<int>(type: "int", nullable: true),
                    FacturaId = table.Column<int>(type: "int", nullable: true),
                    Negocioid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Descuentos_DescuentoId",
                        column: x => x.DescuentoId,
                        principalTable: "Descuentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Productos_Facturas_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Facturas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Productos_Negocios_Id_Negocio",
                        column: x => x.Id_Negocio,
                        principalTable: "Negocios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Productos_Negocios_Negocioid",
                        column: x => x.Negocioid,
                        principalTable: "Negocios",
                        principalColumn: "id");
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
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
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
                name: "IX_Administradores_Id_Negocio",
                table: "Administradores",
                column: "Id_Negocio");

            migrationBuilder.CreateIndex(
                name: "IX_Administradores_Vendedorid",
                table: "Administradores",
                column: "Vendedorid");

            migrationBuilder.CreateIndex(
                name: "IX_Descuentos_FacturaId",
                table: "Descuentos",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_metodoPagoId",
                table: "Facturas",
                column: "metodoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_ImpuestoDetalle_ImpuestoId",
                table: "ImpuestoDetalle",
                column: "ImpuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_Impuestos_ProductoId",
                table: "Impuestos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_MetodosPago_Negocioid",
                table: "MetodosPago",
                column: "Negocioid");

            migrationBuilder.CreateIndex(
                name: "IX_MetodosPago_Vendedorid",
                table: "MetodosPago",
                column: "Vendedorid");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_DescuentoId",
                table: "Productos",
                column: "DescuentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_FacturaId",
                table: "Productos",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Id_Negocio",
                table: "Productos",
                column: "Id_Negocio");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Negocioid",
                table: "Productos",
                column: "Negocioid");

            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_Negocioid",
                table: "Vendedores",
                column: "Negocioid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "ImpuestoDetalle");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "Impuestos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Descuentos");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "MetodosPago");

            migrationBuilder.DropTable(
                name: "Vendedores");

            migrationBuilder.DropTable(
                name: "Negocios");
        }
    }
}
