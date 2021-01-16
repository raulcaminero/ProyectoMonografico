using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebApp.Models
{
    public partial class Modulo
    {
        public Modulo()
        {
            Calificaciones = new HashSet<Calificaciones>();
        }

        public int Id { get; set; }

        [DisplayName("Título")]
        public string Titulo { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int UsuarioCodigo { get; set; }
        public string Imagen { get; set; }
        public string EstadoId { get; set; }
        public int ServicioId { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual Servicio Servicio { get; set; }
        public virtual ICollection<Calificaciones> Calificaciones { get; set; }
        public virtual Usuario Profesor { get; set; }
    }
}
