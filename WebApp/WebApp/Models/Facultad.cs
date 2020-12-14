using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Facultad
    {
        public Facultad()
        {
            Carrera = new HashSet<Carrera>();
            Escuela = new HashSet<Escuela>();
        }

        public int FacultadId { get; set; }
        public string FacultadCodigo { get; set; }
        public string FacultadNombre { get; set; }
        public string EstadoId { get; set; }
        public int CampusId { get; set; }

        public virtual Campus Campus { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual ICollection<Carrera> Carrera { get; set; }
        public virtual ICollection<Escuela> Escuela { get; set; }
    }
}
