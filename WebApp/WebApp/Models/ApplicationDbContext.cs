using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApp.Models;

namespace WebApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public  virtual DbSet<Usuario> usuarios { get; set; }
        public virtual DbSet<AdjuntoMaterial> AdjuntoMaterial { get; set; }
        public virtual DbSet<Calificaciones> Calificaciones { get; set; }
        public virtual DbSet<Campus> Campus { get; set; }
        public virtual DbSet<Facultad> Facultades { get; set; }
        public virtual DbSet<Escuela> Escuelas { get; set; }
        public virtual DbSet<Carrera> Carreras { get; set; }
        public virtual DbSet<Modulo> Modulo { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Requerimiento> Requerimientos { get; set; }
        public virtual DbSet<TipoServicio> TipoServicios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            modelBuilder.Entity<AdjuntoMaterial>(entity =>
            {
                entity.ToTable("adjuntoMaterial");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoId)
                    .IsRequired()
                    .HasColumnName("Estado_Id")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Ruta)
                    .HasColumnName("ruta")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.AdjuntoMaterial)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_adjuntoMaterialEstado");
            });

            modelBuilder.Entity<Calificaciones>(entity =>
            {
                entity.Property(e => e.EstadoId)
                    .IsRequired()
                    .HasColumnName("Estado_Id")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UsuarioCodigo).HasColumnName("UsuarioCodigo");

                entity.Property(e => e.ModuloId).HasColumnName("ModuloId");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Calificaciones)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CalificacionesEstado");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Calificaciones)
                    .HasForeignKey(p=> p.UsuarioCodigo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CalificacionEstudiantes");

                entity.HasOne(d => d.Modulo)
                    .WithMany(p => p.Calificaciones)
                    .HasForeignKey(d => d.ModuloId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CalficaionesModulos");
            });
            modelBuilder.Entity<Estado>(entity =>
            {
                entity.Property(e => e.EstadoId)
                    .HasColumnName("Estado_Id")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EstadoNombre)
                    .IsRequired()
                    .HasColumnName("Estado_Nombre")
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.Property(e => e.Titulo)
                  .HasColumnName("titulo")
                  .HasMaxLength(200)
                  .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoId)
                    .IsRequired()
                    .HasColumnName("Estado_Id")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FechaFin)
                    .HasColumnName("fecha_fin")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaInicio)
                    .HasColumnName("fecha_inicio")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdAdjunto).HasColumnName("id_adjunto");

                entity.Property(e => e.Imagen)
                    .HasColumnName("imagen")
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioCodigo).HasColumnName("UsuarioCodigo");

                entity.Property(e => e.ServicioId).HasColumnName("Servicio_Id");

                entity.Property(e => e.Titulo)
                    .HasColumnName("titulo")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Modulo)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuloEstado");

                entity.HasOne(d => d.IdAdjuntoNavigation)
                    .WithMany(p => p.Modulo)
                    .HasForeignKey(d => d.IdAdjunto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuloAdjunto");

               /* tabla intermedia profesor
                * entity.HasOne(d => d.IdProfesorNavigation)
                    .WithMany(p => p.Modulo)
                    .HasForeignKey(d => d.UsuarioCodigo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuloProfesor");
               */
                entity.HasOne(d => d.Servicio)
                    .WithMany(p => p.Modulo)
                    .HasForeignKey(d => d.ServicioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Modulo_Servicio");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.Property(e => e.Servicio_Id).HasColumnName("Servicio_Id");

                entity.Property(e => e.Campus_Id).HasColumnName("Campus_Id");

                entity.Property(e => e.Carrera_Id).HasColumnName("Carrera_Id");

                entity.Property(e => e.Escuela_Id).HasColumnName("Escuela_Id");

                entity.Property(e => e.Estado_Id)
                    .HasColumnName("Estado_Id")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('I')");

                entity.Property(e => e.Facultad_Id).HasColumnName("Facultad_Id");

                entity.Property(e => e.UsuarioCodigo).HasColumnName("UsuarioCodigo");

                entity.Property(e => e.Servicio_Codigo)
                    .IsRequired()
                    .HasColumnName("Servicio_Codigo")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Servicio_Costo)
                    .HasColumnName("Servicio_Costo")
                    .HasColumnType("decimal(12, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Servicio_Descripcion)
                    .IsRequired()
                    .HasColumnName("Servicio_Descripcion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Servicio_FechaCierre)
                    .HasColumnName("Servicio_FechaCierre")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Servicio_FechaInicio)
                    .HasColumnName("Servicio_FechaInicio")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TipoServicio_Id).HasColumnName("TipoServicio_Id");

                entity.HasOne(d => d.Campus)
                    .WithMany(p => p.Servicio)
                    .HasForeignKey(d => d.Campus_Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Servicio_Campus");

                entity.HasOne(d => d.Carrera)
                    .WithMany(p => p.Servicio)
                    .HasForeignKey(d => d.Carrera_Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Servicio_Carrera");

                entity.HasOne(d => d.Escuela)
                    .WithMany(p => p.Servicio)
                    .HasForeignKey(d => d.Escuela_Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Servicio_Escuela");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Servicio)
                    .HasForeignKey(d => d.Estado_Id)
                    .HasConstraintName("FK_Servicio_Estado");
            });
            modelBuilder.Entity<TipoServicio>(entity =>
            {
                entity.Property(e => e.TipoServicioId)
                    .HasColumnName("TipoServicio_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EstadoId)
                    .IsRequired()
                    .HasColumnName("Estado_Id")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('A')");

                entity.Property(e => e.TipoServicioDescripcion)
                    .IsRequired()
                    .HasColumnName("TipoServicio_Descripcion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.TipoServicio)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoServicio_Estado");
            });
            modelBuilder.Entity<Facultad>(entity =>
            {
                entity.Property(e => e.FacultadId).HasColumnName("Facultad_Id");

                entity.Property(e => e.CampusId).HasColumnName("Campus_Id");

                entity.Property(e => e.EstadoId)
                    .IsRequired()
                    .HasColumnName("Estado_Id")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FacultadCodigo)
                    .IsRequired()
                    .HasColumnName("Facultad_Codigo")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FacultadNombre)
                    .IsRequired()
                    .HasColumnName("Facultad_Nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Campus)
                    .WithMany(p => p.Facultad)
                    .HasForeignKey(d => d.CampusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Facultad_Campus");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Facultad)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Facultad_Estado");
            });

            modelBuilder.Entity<Escuela>(entity =>
            {
                entity.Property(e => e.EscuelaId).HasColumnName("Escuela_Id");

                entity.Property(e => e.CampusId).HasColumnName("Campus_Id");

                entity.Property(e => e.EscuelaCodigo)
                    .IsRequired()
                    .HasColumnName("Escuela_Codigo")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.EscuelaNombre)
                    .IsRequired()
                    .HasColumnName("Escuela_Nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoId)
                    .IsRequired()
                    .HasColumnName("Estado_Id")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FacultadId).HasColumnName("Facultad_Id");

                entity.HasOne(d => d.Campus)
                    .WithMany(p => p.Escuela)
                    .HasForeignKey(d => d.CampusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Escuela_Campus");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Escuela)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Escuela_Estado");

                entity.HasOne(d => d.Facultad)
                    .WithMany(p => p.Escuela)
                    .HasForeignKey(d => d.FacultadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Escuela_Facultad");
            });
            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.Property(e => e.CarreraId).HasColumnName("Carrera_Id");

                entity.Property(e => e.CampusId).HasColumnName("Campus_Id");

                entity.Property(e => e.CarreraCodigo)
                    .IsRequired()
                    .HasColumnName("Carrera_Codigo")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CarreraNombre)
                    .IsRequired()
                    .HasColumnName("Carrera_Nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EscuelaId).HasColumnName("Escuela_Id");

                entity.Property(e => e.EstadoId)
                    .IsRequired()
                    .HasColumnName("Estado_Id")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FacultadId).HasColumnName("Facultad_Id");

                entity.HasOne(d => d.Campus)
                    .WithMany(p => p.Carrera)
                    .HasForeignKey(d => d.CampusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carrera_Campus");

                entity.HasOne(d => d.Escuela)
                    .WithMany(p => p.Carrera)
                    .HasForeignKey(d => d.EscuelaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carrera_Escuela");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Carrera)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carrera_Estado");

                entity.HasOne(d => d.Facultad)
                    .WithMany(p => p.Carrera)
                    .HasForeignKey(d => d.FacultadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carrera_Facultad");
            });

            modelBuilder.Entity<Campus>(entity =>
            {
                entity.Property(e => e.CampusId).HasColumnName("Campus_Id");

                entity.Property(e => e.CampusCodigo)
                    .IsRequired()
                    .HasColumnName("Campus_Codigo")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CampusNombre)
                    .IsRequired()
                    .HasColumnName("Campus_Nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoId)
                    .IsRequired()
                    .HasColumnName("Estado_Id")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Campus)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Campus_Estado");
            });


            //OnModelCreatingPartial(modelBuilder);
        }
        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<WebApp.Dto.ListaServicio> ListaServicio { get; set; }
    }

}
