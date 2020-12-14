using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Estudiante
    {
        public Estudiante()
        {
            Calificaciones = new HashSet<Calificaciones>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        public virtual ICollection<Calificaciones> Calificaciones { get; set; }
    }
}
