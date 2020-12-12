using Microsoft.EntityFrameworkCore;
using PerfilEstudiante.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerfilEstudiante.Models
{
    public class Datos_Solicitud: DbContext
    {
        public Datos_Solicitud()
        {

        }

        public Datos_Solicitud(DbContextOptions<Datos_Solicitud> options): base(options)
        {
           
        }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Datos_Adicionales> Datos_Adicionales { get; set; }
    }

    public class Persona
    {
        [Key]
        public int codigo { set; get; }
        public int IdDatos { get; set; }
        public string primer_nombre { set; get; }
        public string Email { set; get; }
        public string contrasena { get; set; }
        public int rol { get; set; }


        public string segundo_nombre { set; get; }
        public string primer_apellido { set; get; }
        public string segundo_apellido { set; get; }
        public string tipo_identificacion { set; get; }
        public string identificacion { set; get; }
        public string sexo { set; get; }
        public string matricula { set; get; }
        public int campus { set; get; }
    }

    public class Datos_Adicionales
    {
        [key]
        public int id { get; set; }
        public DateTime fecha_naciamientob { get; set; }
        public string celular { get; set; }
        public string nacionalidad { get; set; }

        public int personaid { set; get; }
        public Persona Persona { set; get; }
    }
}

