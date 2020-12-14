using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Modulo
    {
        public Modulo()
        {
            Calificaciones = new HashSet<Calificaciones>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int UsuarioCodigo { get; set; }
        public string Imagen { get; set; }
        public string EstadoId { get; set; }
        public int? IdAdjunto { get; set; }
        public int ServicioId { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual AdjuntoMaterial IdAdjuntoNavigation { get; set; }
        public virtual Usuario IdProfesorNavigation { get; set; }
        public virtual Servicio Servicio { get; set; }
        public virtual ICollection<Calificaciones> Calificaciones { get; set; }
    }
}
