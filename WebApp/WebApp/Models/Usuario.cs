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

        [Display(Name = "Nombre:")]
        public string primer_nombre { set; get; }


        [Display(Name = "Correo")]
        public string Email { set; get; }
        public string contrasena { get; set; }


        [Display(Name = "Rol")]
        public string rol { get; set; }


        [Display(Name = "Segudo Nombre")]
        public string segundo_nombre { set; get; }

        [Display(Name = "Apellido")]
        public string primer_apellido { set; get; }

        [Display(Name = "Segundo Apellido")]
        public string segundo_apellido { set; get; }
        public string tipo_identificacion { set; get; }


        [Display(Name = "Identificación")]
        public string identificacion { set; get; }

        [Display(Name = "Sexo")]
        public string sexo { set; get; }

        [Display(Name = "Matrícula")]
        public string matricula { set; get; }


        [Display(Name = "Campus")]
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
