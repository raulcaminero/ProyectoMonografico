using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class integtracionservicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Estado_Nombre = table.Column<string>(fixedLength: true, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Estado_Id);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
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
                name: "Campus",
                columns: table => new
                {
                    Campus_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Campus_Codigo = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    Campus_Nombre = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Localidad = table.Column<string>(nullable: true),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "TipoServicios",
                columns: table => new
                {
                    TipoServicio_Id = table.Column<int>(nullable: false),
                    TipoServicio_Descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false, defaultValueSql: "('A')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoServicios", x => x.TipoServicio_Id);
                    table.ForeignKey(
                        name: "FK_TipoServicio_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    contrasena = table.Column<string>(nullable: true),
                    primer_nombre = table.Column<string>(nullable: true),
                    segundo_nombre = table.Column<string>(nullable: true),
                    primer_apellido = table.Column<string>(nullable: true),
                    segundo_apellido = table.Column<string>(nullable: true),
                    tipo_identificacion = table.Column<string>(nullable: true),
                    identificacion = table.Column<string>(nullable: true),
                    sexo = table.Column<string>(nullable: true),
                    matricula = table.Column<string>(nullable: true),
                    campus = table.Column<int>(nullable: false),
                    RolID = table.Column<int>(nullable: false),
                    EstadoId = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.codigo);
                    table.ForeignKey(
                        name: "FK_usuarios_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_usuarios_Rol_RolID",
                        column: x => x.RolID,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facultades",
                columns: table => new
                {
                    Facultad_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Facultad_Codigo = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    Facultad_Nombre = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    NombreDecano = table.Column<string>(nullable: true),
                    Ubicación = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Campus_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facultades", x => x.Facultad_Id);
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
                    TipoServicioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requerimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requerimientos_TipoServicios_TipoServicioId",
                        column: x => x.TipoServicioId,
                        principalTable: "TipoServicios",
                        principalColumn: "TipoServicio_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Escuelas",
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
                    table.PrimaryKey("PK_Escuelas", x => x.Escuela_Id);
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
                        principalTable: "Facultades",
                        principalColumn: "Facultad_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carreras",
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
                    table.PrimaryKey("PK_Carreras", x => x.Carrera_Id);
                    table.ForeignKey(
                        name: "FK_Carrera_Campus",
                        column: x => x.Campus_Id,
                        principalTable: "Campus",
                        principalColumn: "Campus_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carrera_Escuela",
                        column: x => x.Escuela_Id,
                        principalTable: "Escuelas",
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
                        principalTable: "Facultades",
                        principalColumn: "Facultad_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ListaServicio",
                columns: table => new
                {
                    Servicio_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Servicio_Codigo = table.Column<string>(nullable: true),
                    Servicio_Descripcion = table.Column<string>(nullable: true),
                    Servicio_FechaInicio = table.Column<DateTime>(nullable: true),
                    Servicio_FechaCierre = table.Column<DateTime>(nullable: true),
                    Servicio_Costo = table.Column<decimal>(nullable: true),
                    UsuarioCodigo = table.Column<int>(nullable: true),
                    TipoServicio_Id = table.Column<int>(nullable: false),
                    TipoServicio_Nombre = table.Column<string>(nullable: true),
                    Estado_Id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Estado_Nombre = table.Column<string>(nullable: true),
                    Campus_Nombre = table.Column<string>(nullable: true),
                    Faculta_Nombre = table.Column<string>(nullable: true),
                    Escuela_Nombre = table.Column<string>(nullable: true),
                    Carrera_Nombre = table.Column<string>(nullable: true),
                    CampusId = table.Column<int>(nullable: true),
                    FacultadId = table.Column<int>(nullable: true),
                    EscuelaId = table.Column<int>(nullable: true),
                    CarreraId = table.Column<int>(nullable: true),
                    EstadoId = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false)
                  
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaServicio", x => x.Servicio_Id);
                    table.ForeignKey(
                        name: "FK_ListaServicio_Campus_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campus",
                        principalColumn: "Campus_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListaServicio_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "Carrera_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListaServicio_Escuelas_EscuelaId",
                        column: x => x.EscuelaId,
                        principalTable: "Escuelas",
                        principalColumn: "Escuela_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListaServicio_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListaServicio_Facultades_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "Facultades",
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
                    UsuarioCodigo = table.Column<int>(nullable: true),
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
                        principalTable: "Carreras",
                        principalColumn: "Carrera_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicio_Escuela",
                        column: x => x.Escuela_Id,
                        principalTable: "Escuelas",
                        principalColumn: "Escuela_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicio_Estado",
                        column: x => x.Estado_Id,
                        principalTable: "Estado",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicio_usuarios_UsuarioCodigo",
                        column: x => x.UsuarioCodigo,
                        principalTable: "usuarios",
                        principalColumn: "codigo",
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
                    UsuarioCodigo = table.Column<int>(nullable: false),
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
                        name: "FK_Modulo_Servicio",
                        column: x => x.Servicio_Id,
                        principalTable: "Servicio",
                        principalColumn: "Servicio_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuloProfesor",
                        column: x => x.UsuarioCodigo,
                        principalTable: "usuarios",
                        principalColumn: "codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calificacion = table.Column<int>(nullable: false),
                    ModuloId = table.Column<int>(nullable: false),
                    UsuarioCodigo = table.Column<int>(nullable: false),
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
                        name: "FK_CalficaionesModulos",
                        column: x => x.ModuloId,
                        principalTable: "Modulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalificacionEstudiantes",
                        column: x => x.UsuarioCodigo,
                        principalTable: "usuarios",
                        principalColumn: "codigo",
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
                name: "IX_Calificaciones_ModuloId",
                table: "Calificaciones",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_UsuarioCodigo",
                table: "Calificaciones",
                column: "UsuarioCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Campus_Estado_Id",
                table: "Campus",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_Campus_Id",
                table: "Carreras",
                column: "Campus_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_Escuela_Id",
                table: "Carreras",
                column: "Escuela_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_Estado_Id",
                table: "Carreras",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_Facultad_Id",
                table: "Carreras",
                column: "Facultad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Escuelas_Campus_Id",
                table: "Escuelas",
                column: "Campus_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Escuelas_Estado_Id",
                table: "Escuelas",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Escuelas_Facultad_Id",
                table: "Escuelas",
                column: "Facultad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Facultades_Campus_Id",
                table: "Facultades",
                column: "Campus_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Facultades_Estado_Id",
                table: "Facultades",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ListaServicio_CampusId",
                table: "ListaServicio",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaServicio_CarreraId",
                table: "ListaServicio",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaServicio_EscuelaId",
                table: "ListaServicio",
                column: "EscuelaId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaServicio_EstadoId",
                table: "ListaServicio",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaServicio_FacultadId",
                table: "ListaServicio",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_Modulo_Estado_Id",
                table: "Modulo",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Modulo_id_adjunto",
                table: "Modulo",
                column: "id_adjunto");

            migrationBuilder.CreateIndex(
                name: "IX_Modulo_Servicio_Id",
                table: "Modulo",
                column: "Servicio_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Modulo_UsuarioCodigo",
                table: "Modulo",
                column: "UsuarioCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Requerimientos_TipoServicioId",
                table: "Requerimientos",
                column: "TipoServicioId");

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
                name: "IX_Servicio_UsuarioCodigo",
                table: "Servicio",
                column: "UsuarioCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_TipoServicios_Estado_Id",
                table: "TipoServicios",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_EstadoId",
                table: "usuarios",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_RolID",
                table: "usuarios",
                column: "RolID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "ListaServicio");

            migrationBuilder.DropTable(
                name: "Requerimientos");

            migrationBuilder.DropTable(
                name: "Modulo");

            migrationBuilder.DropTable(
                name: "TipoServicios");

            migrationBuilder.DropTable(
                name: "adjuntoMaterial");

            migrationBuilder.DropTable(
                name: "Servicio");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "Escuelas");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Facultades");

            migrationBuilder.DropTable(
                name: "Campus");

            migrationBuilder.DropTable(
                name: "Estado");
        }
    }
}
