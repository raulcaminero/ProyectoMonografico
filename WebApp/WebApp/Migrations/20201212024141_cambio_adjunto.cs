using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class cambio_adjunto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Salario = table.Column<decimal>(type: "decimal(13, 2)", nullable: true, defaultValueSql: "((0))"),
                    FechaIngreso = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Estado_Nombre = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Estado_Id);
                });

            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Codigo = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Contrasena = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "adjuntoMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    ruta = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adjuntoMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_adjuntoMaterialEstado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Localidad",
                columns: table => new
                {
                    Localidad_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Localidad_Nombre = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidad", x => x.Localidad_Id);
                    table.ForeignKey(
                        name: "FK_Localidad_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false),
                    Usuario_Nombre = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Rol_Id = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false, defaultValueSql: "('A')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Usuario_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Profesor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfesorEstado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoServicio",
                columns: table => new
                {
                    TipoServicio_Id = table.Column<int>(nullable: false),
                    TipoServicio_Descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false, defaultValueSql: "('A')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoServicio", x => x.TipoServicio_Id);
                    table.ForeignKey(
                        name: "FK_TipoServicio_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Campus",
                columns: table => new
                {
                    Campus_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Campus_Codigo = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    Campus_Nombre = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Localidad_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campus", x => x.Campus_Id);
                    table.ForeignKey(
                        name: "FK_Campus_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Campus_Localidad",
                        column: x => x.Localidad_Id,
                        principalTable: "Localidad",
                        principalColumn: "Localidad_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Facultad",
                columns: table => new
                {
                    Facultad_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Facultad_Codigo = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    Facultad_Nombre = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Campus_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facultad", x => x.Facultad_Id);
                    table.ForeignKey(
                        name: "FK_Facultad_Campus",
                        column: x => x.Campus_Id,
                        principalTable: "Campus",
                        principalColumn: "Campus_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facultad_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Escuela",
                columns: table => new
                {
                    Escuela_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Escuela_Codigo = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    Escuela_Nombre = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Campus_Id = table.Column<int>(nullable: false),
                    Facultad_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escuela", x => x.Escuela_Id);
                    table.ForeignKey(
                        name: "FK_Escuela_Campus",
                        column: x => x.Campus_Id,
                        principalTable: "Campus",
                        principalColumn: "Campus_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Escuela_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Escuela_Facultad",
                        column: x => x.Facultad_Id,
                        principalTable: "Facultad",
                        principalColumn: "Facultad_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carrera",
                columns: table => new
                {
                    Carrera_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Carrera_Codigo = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    Carrera_Nombre = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Campus_Id = table.Column<int>(nullable: false),
                    Facultad_Id = table.Column<int>(nullable: false),
                    Escuela_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrera", x => x.Carrera_Id);
                    table.ForeignKey(
                        name: "FK_Carrera_Campus",
                        column: x => x.Campus_Id,
                        principalTable: "Campus",
                        principalColumn: "Campus_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carrera_Escuela",
                        column: x => x.Escuela_Id,
                        principalTable: "Escuela",
                        principalColumn: "Escuela_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carrera_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carrera_Facultad",
                        column: x => x.Facultad_Id,
                        principalTable: "Facultad",
                        principalColumn: "Facultad_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    Servicio_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Servicio_Codigo = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    Servicio_Descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Servicio_FechaInicio = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "('')"),
                    Servicio_FechaCierre = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "('')"),
                    Servicio_Costo = table.Column<decimal>(type: "decimal(12, 2)", nullable: true, defaultValueSql: "((0))"),
                    Usuario_Codigo = table.Column<int>(nullable: true),
                    TipoServicio_Id = table.Column<int>(nullable: false),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "('I')"),
                    Campus_Id = table.Column<int>(nullable: false),
                    Facultad_Id = table.Column<int>(nullable: false),
                    Escuela_Id = table.Column<int>(nullable: false),
                    Carrera_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.Servicio_Id);
                    table.ForeignKey(
                        name: "FK_Servicio_Campus",
                        column: x => x.Campus_Id,
                        principalTable: "Campus",
                        principalColumn: "Campus_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicio_Carrera",
                        column: x => x.Carrera_Id,
                        principalTable: "Carrera",
                        principalColumn: "Carrera_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicio_Escuela",
                        column: x => x.Escuela_Id,
                        principalTable: "Escuela",
                        principalColumn: "Escuela_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicio_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Modulo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    descripcion = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    fecha_inicio = table.Column<DateTime>(type: "datetime", nullable: true),
                    fecha_fin = table.Column<DateTime>(type: "datetime", nullable: true),
                    id_Profesor = table.Column<int>(nullable: false),
                    imagen = table.Column<string>(unicode: false, nullable: true),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    id_adjunto = table.Column<int>(nullable: true),
                    Servicio_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuloEstado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuloAdjunto",
                        column: x => x.id_adjunto,
                        principalTable: "adjuntoMaterial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuloProfesor",
                        column: x => x.id_Profesor,
                        principalTable: "Profesor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Modulo_Servicio",
                        column: x => x.Servicio_Id,
                        principalTable: "Servicio",
                        principalColumn: "Servicio_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modulo_Id = table.Column<int>(nullable: false),
                    Estudiante_Id = table.Column<int>(nullable: false),
                    Calificacion = table.Column<int>(nullable: false),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalificacionesEstado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalificacionEstudiantes",
                        column: x => x.Estudiante_Id,
                        principalTable: "Estudiante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalficaionesModulos",
                        column: x => x.Modulo_Id,
                        principalTable: "Modulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_adjuntoMaterial_Estado_Id",
                table: "adjuntoMaterial",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_Estado_Id",
                table: "Calificaciones",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_Estudiante_Id",
                table: "Calificaciones",
                column: "Estudiante_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_Modulo_Id",
                table: "Calificaciones",
                column: "Modulo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Campus_Estado_Id",
                table: "Campus",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Campus_Localidad_Id",
                table: "Campus",
                column: "Localidad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carrera_Campus_Id",
                table: "Carrera",
                column: "Campus_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carrera_Escuela_Id",
                table: "Carrera",
                column: "Escuela_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carrera_Estado_Id",
                table: "Carrera",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carrera_Facultad_Id",
                table: "Carrera",
                column: "Facultad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Escuela_Campus_Id",
                table: "Escuela",
                column: "Campus_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Escuela_Estado_Id",
                table: "Escuela",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Escuela_Facultad_Id",
                table: "Escuela",
                column: "Facultad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Facultad_Campus_Id",
                table: "Facultad",
                column: "Campus_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Facultad_Estado_Id",
                table: "Facultad",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Localidad_Estado_Id",
                table: "Localidad",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Modulo_Estado_Id",
                table: "Modulo",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Modulo_id_adjunto",
                table: "Modulo",
                column: "id_adjunto");

            migrationBuilder.CreateIndex(
                name: "IX_Modulo_id_Profesor",
                table: "Modulo",
                column: "id_Profesor");

            migrationBuilder.CreateIndex(
                name: "IX_Modulo_Servicio_Id",
                table: "Modulo",
                column: "Servicio_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Estado_Id",
                table: "Usuario",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Profesor_Estado_Id",
                table: "Profesor",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_Campus_Id",
                table: "Servicio",
                column: "Campus_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_Carrera_Id",
                table: "Servicio",
                column: "Carrera_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_Escuela_Id",
                table: "Servicio",
                column: "Escuela_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_Estado_Id",
                table: "Servicio",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TipoServicio_Estado_Id",
                table: "TipoServicio",
                column: "Estado_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "TipoServicio");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Estudiante");

            migrationBuilder.DropTable(
                name: "Modulo");

            migrationBuilder.DropTable(
                name: "adjuntoMaterial");

            migrationBuilder.DropTable(
                name: "Profesor");

            migrationBuilder.DropTable(
                name: "Servicio");

            migrationBuilder.DropTable(
                name: "Carrera");

            migrationBuilder.DropTable(
                name: "Escuela");

            migrationBuilder.DropTable(
                name: "Facultad");

            migrationBuilder.DropTable(
                name: "Campus");

            migrationBuilder.DropTable(
                name: "Localidad");

            migrationBuilder.DropTable(
                name: "Estado");
        }
    }
}
