using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Carrera
    {
        public Carrera()
        {
            Servicio = new HashSet<Servicio>();
        }

        public int CarreraId { get; set; }
        public string CarreraCodigo { get; set; }
        public string CarreraNombre { get; set; }
        public string EstadoId { get; set; }
        public int CampusId { get; set; }
        public int FacultadId { get; set; }
        public int EscuelaId { get; set; }
        public virtual Campus Campus { get; set; }
        public virtual Escuela Escuela { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Facultad Facultad { get; set; }
        public virtual ICollection<Servicio> Servicio { get; set; }
    }
}
