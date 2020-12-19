using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Usuario
    {
        public Usuario()
        {
            Calificaciones = new HashSet<Calificaciones>();
            Solicitudes = new HashSet<SolicitudServicio>();
        }

        [Key]
        public int codigo { set; get; }

        [Display(Name = "Nombre:")]
        public string primer_nombre { set; get; }


        [Display(Name = "Correo")]
        public string Email { set; get; }

        public string contrasena { get; set; }


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

        [MaxLength(30)]
        public string contacto { get; set; }

        [MaxLength(50)]
        public string nacionalidad { get; set; }

        public DateTime? fecha_nacimiento { get; set; }

        public int? IdCampus { set; get; }
        [ForeignKey("IdCampus")]
		public Campus Campus { get; set; }

		public int RolID { get; set; }
        public virtual Rol Rol { get; set; }

        public string EstadoId { set; get; }
        public virtual Estado Estado { get; set; }

        [NotMapped]
        public string NombreCompleto
        {
            get { return primer_nombre.Trim() + " " + primer_apellido; }
        }

        //tabla intermedia para calificaciones, profesores en modulos, servicios y de estudiantes
        public virtual ICollection<Calificaciones> Calificaciones { get; set; }
        public virtual ICollection<Modulo> Modulo { get; set; }
        public virtual ICollection<SolicitudServicio> Solicitudes { get; set; }

        public override string ToString() => Email;
	}
}
