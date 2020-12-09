using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class DatosPersonales
    {
        public string PrimerNombre {get; set;}

        public string? SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string? SegundoApellido { get; set; }

        public string TipoIdentificacion { get; set; }
         
        public string NumIdentificacion { get; set; }

        public string Sexo { get; set; }

        public string MatriculaOCodigo { get; set; }
        
        public DateTime FechaNacimiento { get; set; }

        public string Contacto { get; set; }

        public string Nacionalidad { get; set; }

        public string Campus { get; set; }
    }
}
