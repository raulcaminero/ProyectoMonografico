//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;

//namespace WebApp.Models.Data
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext()
//        {
//        }

//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<Usuario> usuarios1 { get; set; }
//        public DbSet<Campus> Campus { get; set; }
//        public DbSet<Facultad> Facultades { get; set; }
//        public DbSet<Escuela> Escuelas { get; set; }
//        public DbSet<Carrera> Carreras { get; set; }
//        public DbSet<Requerimiento> Requerimientos { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Campus>()
//                .HasIndex(c => c.Codigo)
//                .IsUnique();
//        }
//    }

//}
