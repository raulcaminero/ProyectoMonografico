using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Data
{
    public class Usuario
    {

        [Key]
        public int codigo { set; get; }
        public string primer_nombre { set; get; }
        public string Email { set; get; }
        public string contrasena { get; set; }
        public int rol { get; set; }


        public string segundo_nombre { set; get; }
        public string primer_apellido { set; get; }
        public string segundo_apellido { set; get; }
        public string tipo_identificacion { set; get; }
        public string identificacion { set; get; }
        public string sexo { set; get; }
        public string matricula { set; get; }
        public int campus { set; get; }


        public string? nacionalidad {set; get;}

        public string? contacto {get; set;}
        public DateTime? fecha_nacimiento {set; get;}
    }
}
