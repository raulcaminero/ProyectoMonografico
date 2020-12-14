using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Profesor
    {
        public Profesor()
        {
            Modulo = new HashSet<Modulo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string EstadoId { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual ICollection<Modulo> Modulo { get; set; }
    }
}
