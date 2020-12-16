using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public partial class Carrera
    {
        public Carrera()
        {
            Servicio = new HashSet<Servicio>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Es necesario introducir un código para la carrera")]
        [DisplayName("Código")]
        [Remote(action: "CheckExisting_Code", controller: "Carreras", AdditionalFields = "Id")]
        public string Codigo { get; set; }

        [DisplayName("Escuela")]
        public int IdEscuela { get; set; }

        [ForeignKey("IdEscuela")]
        public Escuela Escuela { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre para la carrera")]
        [Remote(action: "CheckExisting_Name", controller: "Carreras", AdditionalFields = "Id")]
        public string Nombre { get; set; }

        public Estados Estado { get; set; }
        public int? CampusId { get; set; }
        public int? FacultadId { get; set; }
        public virtual Campus Campus { get; set; }
        public virtual Facultad Facultad { get; set; }
        public virtual ICollection<Servicio> Servicio { get; set; }
    }
}
