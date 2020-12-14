using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Campus
    {
        public Campus()
        {
            Carrera = new HashSet<Carrera>();
            Escuela = new HashSet<Escuela>();
            Facultad = new HashSet<Facultad>();
            Servicio = new HashSet<Servicio>();
        }
        public int CampusId { get; set; }
        public string CampusCodigo { get; set; }
        public string CampusNombre { get; set; }
        public string Localidad { get; set; }
        public string EstadoId { get; set; }
        public string FullName
        {
            get { return CampusCodigo.Trim() + " " + CampusNombre; }
        }

        public virtual Estado Estado { get; set; }
        public virtual ICollection<Carrera> Carrera { get; set; }
        public virtual ICollection<Escuela> Escuela { get; set; }
        public virtual ICollection<Facultad> Facultad { get; set; }
        public virtual ICollection<Servicio> Servicio { get; set; }
    }
}
