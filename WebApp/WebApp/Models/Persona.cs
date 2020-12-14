using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Persona
    {
       
        public int Codigo { get; set; }
        public string PersonaNombre { get; set; }
        public int Rol_Id { get; set; }
        public string EstadoId { get; set; }

        public virtual Estado Estado { get; set; }
    }
}
