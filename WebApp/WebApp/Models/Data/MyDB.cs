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
        public virtual DbSet<Campus> Campus { get; set; }
        public virtual DbSet<Carrera> Carrera { get; set; }
        public virtual DbSet<Escuela> Escuela { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            if (!optionsBuilder.IsConfigured)
#pragma warning restore CS1030 // #warning directive
            {
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
