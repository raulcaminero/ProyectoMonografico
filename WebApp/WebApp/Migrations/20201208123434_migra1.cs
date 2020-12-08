using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantillaInscripcion.Migrations
{
    public partial class migra1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatosPersonales",
                columns: table => new
                {
                    IdPersona = table.Column<int>(nullable: false),
                    PrimerNombre = table.Column<string>(nullable: true),
                    SegundoNombre = table.Column<string>(nullable: true),
                    PrimerApellido = table.Column<string>(nullable: true),
                    SegundoApellido = table.Column<string>(nullable: true),
                    TipoIdentificacion = table.Column<string>(nullable: true),
                    NumIdentificacion = table.Column<string>(nullable: true),
                    Sexo = table.Column<string>(nullable: true),
                    MatriculaOCodigo = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    Contacto = table.Column<string>(nullable: true),
                    Nacionalidad = table.Column<string>(nullable: true),
                    Campus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosPersonales", x => x.IdPersona);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatosPersonales");
        }
    }
}
