using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class @base : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Localidad = table.Column<string>(nullable: false),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carrera",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(nullable: false),
                    idEscuela = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Detalles = table.Column<string>(nullable: false),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrera", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facultades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreFacultad = table.Column<string>(maxLength: 20, nullable: false),
                    Codigo = table.Column<string>(maxLength: 8, nullable: false),
                    NombreDecano = table.Column<string>(nullable: false),
                    Ubicación = table.Column<string>(nullable: false),
                    Telefono = table.Column<string>(nullable: false),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facultades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Requerimientos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(maxLength: 15, nullable: false),
                    Titulo = table.Column<string>(maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 300, nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    TipoServicio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requerimientos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    codigo = table.Column<string>(nullable: false),
                    contrasena = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.codigo);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campus_Codigo",
                table: "Campus",
                column: "Codigo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campus");

            migrationBuilder.DropTable(
                name: "Carrera");

            migrationBuilder.DropTable(
                name: "Facultades");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Requerimientos");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
