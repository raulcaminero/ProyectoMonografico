using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Empleado
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal? Salario { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
