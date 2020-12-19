using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class TipoServicio
    {
        public TipoServicio()
        {
            Servicio = new HashSet<Servicio>();
        }
        public int TipoServicioId { get; set; }
        public string TipoServicioDescripcion { get; set; }
        public string EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

        public virtual ICollection<Servicio> Servicio { get; set; }
    }
}
