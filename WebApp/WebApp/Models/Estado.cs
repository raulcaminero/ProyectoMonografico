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
            Modulo = new HashSet<Modulo>();
            Usuario = new HashSet<Usuario>();
            Servicio = new HashSet<Servicio>();
            TipoServicio = new HashSet<TipoServicio>();
        }

        public string EstadoId { get; set; }
        public string EstadoNombre { get; set; }
        public virtual ICollection<AdjuntoMaterial> AdjuntoMaterial { get; set; }
        public virtual ICollection<Calificaciones> Calificaciones { get; set; }
        public virtual ICollection<Modulo> Modulo { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<Servicio> Servicio { get; set; }
        public virtual ICollection<TipoServicio> TipoServicio { get; set; }
    }
}
