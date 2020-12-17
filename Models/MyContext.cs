using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace PlantillaInscripcion.Models
{
    public class MyContext:DbContext
    {

        public MyContext() : base() { }
        public MyContext(DbContextOptions<MyContext> options) : base(options) 
        { 
        
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //base.OnConfiguring(optionsBuilder);
            // string c = "database=dbuniversidad;server.;trusted_connection=true";
            string c = "Data Source=JOSEPC;Initial Catalog=dbuniversidad;Integrated Security=True";
            optionsBuilder.UseSqlServer(c, op =>
             {
             });
        }

        public DbSet<DatosPersonales> DatosPersonales { get; set; }
        //public DbSet<Datos> Datos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // base.OnModelCreating(modelBuilder);
        }
    }
}
