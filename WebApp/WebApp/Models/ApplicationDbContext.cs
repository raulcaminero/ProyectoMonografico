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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public virtual DbSet<Usuario> usuarios { get; set; }
		public virtual DbSet<Autorizacion> Autorizaciones { get; set; }
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
		public virtual DbSet<SolicitudServicio> SolicitudesServicios { get; set; }
		public virtual DbSet<Archivo> Archivos { get; set; }
		public virtual DbSet<ArchivoSolicitud> ArchivosSolicitudes { get; set; }
		public virtual DbSet<Proyecto> Proyecto { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

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
					.HasForeignKey(p => p.UsuarioCodigo)
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

				entity.HasOne(d => d.IdProfesorNavigation)
				   .WithMany(p => p.Modulo)
				   .HasForeignKey(d => d.UsuarioCodigo)
				   .OnDelete(DeleteBehavior.ClientSetNull)
				   .HasConstraintName("FK_ModuloProfesor");

				entity.HasOne(d => d.Servicio)
					.WithMany(p => p.Modulo)
					.HasForeignKey(d => d.ServicioId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Modulo_Servicio");
			});

			modelBuilder.Entity<Usuario>(entity =>
			{
				entity.Property(e => e.EstadoId)
					.IsRequired()
					.HasColumnName("Estado_Id")
					.HasMaxLength(1)
					.IsUnicode(false)
					.IsFixedLength();

				// Requerido para evitar un error de integridad entre Usuario y SolicitudServicio
				// Error obtenido:
				// Introducing FOREIGN KEY constraint 'FK_SolicitudesServicios_usuarios_IdUsuario' on table 'SolicitudesServicios' 
				// may cause cycles or multiple cascade paths.Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other 
				// FOREIGN KEY constraints. Could not create constraint or index.See previous errors.
				entity.HasMany(c => c.Solicitudes)
				  .WithOne(e => e.Usuario)
				  .OnDelete(DeleteBehavior.NoAction);
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

				entity.HasOne(d => d.TipoServicio)
					.WithMany(p => p.Servicio)
					.HasForeignKey(d => d.TipoServicio_Id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Servicio_Tiposervicio");

				entity.HasOne(d => d.Escuela)
					.WithMany(p => p.Servicio)
					.HasForeignKey(d => d.Escuela_Id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Servicio_Escuela");

				entity.HasOne(d => d.Facultad)
					.WithMany(p => p.Servicio)
					.HasForeignKey(d => d.Facultad_Id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Servicio_Facultad");

				entity.HasOne(d => d.Estado)
					.WithMany(p => p.Servicio)
					.HasForeignKey(d => d.Estado_Id)
					.HasConstraintName("FK_Servicio_Estado");
			});

			modelBuilder.Entity<Proyecto>(entity =>
			{
				entity.Property(e => e.EstadoId)
					.HasColumnName("Estado_Id")
					.HasMaxLength(1)
					.IsUnicode(false)
					.IsFixedLength()
					.HasDefaultValueSql("('I')");
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

			modelBuilder.Entity<SolicitudServicio>(entity =>
			{
				entity.Property(e => e.IdEstado)
					.IsRequired()
					.HasColumnName("IdEstado")
					.HasMaxLength(1)
					.IsUnicode(false)
					.IsFixedLength()
					.HasDefaultValueSql("('A')");
			});

			modelBuilder.Entity<Campus>()
			   .HasIndex(c => c.Codigo)
			   .IsUnique();

			modelBuilder.Entity<ArchivoSolicitud>()
				.HasKey(x => new { x.IdArchivo, x.IdSolicitud });
		}


	}

}
