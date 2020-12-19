using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Calificaciones
    {
        public int Id { get; set; }
        public int Calificacion { get; set; }
        public int ModuloId { get; set; }
        public int UsuarioCodigo { get; set; }
        public string EstadoId { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Modulo Modulo { get; set; }
    }
}
