using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApp.Models.Data
{
    public class MyDB : DbContext
    {
        public MyDB()
        {
        }

        public MyDB(DbContextOptions<MyDB> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public  virtual DbSet<Facultad> Facultades { get; set; }
        public virtual DbSet<Campus> Campus { get; set; }
        public virtual DbSet<Carrera> Carrera { get; set; }
        public virtual DbSet<Escuela> Escuela { get; set; }
        public virtual DbSet<Requerimiento> Requerimientos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=CulmineraDB;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer("Server=localhost;Database=CulmineraDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campus>()
                .HasIndex(c => c.Codigo)
                .IsUnique();
        }
    }

}
