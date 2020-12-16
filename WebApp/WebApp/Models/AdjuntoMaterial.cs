using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class AdjuntoMaterial
    {
        public AdjuntoMaterial()
        {
            Modulo = new HashSet<Modulo>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Ruta { get; set; }
        public string EstadoId { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual ICollection<Modulo> Modulo { get; set; }
    }
}
