using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Localidad
    {
        public Localidad()
        {
            Campus = new HashSet<Campus>();
        }

        public int LocalidadId { get; set; }
        public string LocalidadNombre { get; set; }
        public string EstadoId { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual ICollection<Campus> Campus { get; set; }
    }
}
