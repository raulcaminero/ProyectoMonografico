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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("database=dbg_8;server=.;user id=sa;password=hola00");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }

}
