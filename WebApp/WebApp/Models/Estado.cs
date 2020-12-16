using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Estado
    {
        public Estado()
        {
            AdjuntoMaterial = new HashSet<AdjuntoMaterial>();
            Calificaciones = new HashSet<Calificaciones>();
            //Campus = new HashSet<Campus>();
            //Carrera = new HashSet<Carrera>();
            //Escuela = new HashSet<Escuela>();
            //Facultad = new HashSet<Facultad>();
            Modulo = new HashSet<Modulo>();
            Usuario = new HashSet<Usuario>();
            Servicio = new HashSet<Servicio>();
            TipoServicio = new HashSet<TipoServicio>();
        }

        public string EstadoId { get; set; }
        public string EstadoNombre { get; set; }
        public virtual ICollection<AdjuntoMaterial> AdjuntoMaterial { get; set; }
        public virtual ICollection<Calificaciones> Calificaciones { get; set; }
        //public virtual ICollection<Campus> Campus { get; set; }
        //public virtual ICollection<Carrera> Carrera { get; set; }
        //public virtual ICollection<Escuela> Escuela { get; set; }
        //public virtual ICollection<Facultad> Facultad { get; set; }
        public virtual ICollection<Modulo> Modulo { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<Servicio> Servicio { get; set; }
        public virtual ICollection<TipoServicio> TipoServicio { get; set; }
    }
}
