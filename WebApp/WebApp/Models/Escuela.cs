using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Models.Data;

namespace WebApp.Models
{
    public partial class Escuela
    {
        public Escuela()
        {
            Carrera = new HashSet<Carrera>();
            Servicio = new HashSet<Servicio>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe registrar el codigo de la escuela")]
        [Remote(action: "CheckExistingCode", controller: "Escuelas", AdditionalFields = "Id")]
        public string CodigoEscuela { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el codigo de la facultad")]
        public int IdFacultad { get; set; }

        [ForeignKey("IdFacultad")]
        public Facultad Facultad { get; set; }

        [Required(ErrorMessage = "Debe registrar el nombre de la escuela")]
        public string Nombre { get; set; }
        public Estados Estado { get; set; }
        public int EscuelaId { get; set; }
        public string EscuelaCodigo { get; set; }
        public string EscuelaNombre { get; set; }
        public int? CampusId { get; set; }
        public virtual Campus Campus { get; set; }

        public virtual ICollection<Carrera> Carrera { get; set; }
        public virtual ICollection<Servicio> Servicio { get; set; }
    }
}
