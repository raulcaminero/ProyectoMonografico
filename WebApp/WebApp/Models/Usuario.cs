using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Usuario
    {
        public Usuario()
        {
            Calificaciones = new HashSet<Calificaciones>();
        }

        [Key]
        public int codigo { set; get; }
        public string Email { set; get; }
        public string contrasena { get; set; }
        public string primer_nombre { set; get; }
        public string segundo_nombre { set; get; }
        public string primer_apellido { set; get; }
        public string segundo_apellido { set; get; }
        public string tipo_identificacion { set; get; }
        public string identificacion { set; get; }
        public string sexo { set; get; }
        public string matricula { set; get; }
        public int campus { set; get; }
        public int RolID { get; set; }
        public string EstadoId { set; get; }

        public virtual Rol Rol { get; set; }
        public virtual Estado Estado { get; set; }
        
        //tabla intermedia para calificaciones, profesores en modulos, servicios y de estudiantes
        public virtual ICollection<Calificaciones> Calificaciones { get; set; }
        public virtual ICollection<Modulo> Modulo { get; set; }

    }
}
