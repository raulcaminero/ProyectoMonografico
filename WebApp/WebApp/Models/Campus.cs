using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApp.Models.Data;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public partial class Campus
    {
        public Campus()
        {
            Carrera = new HashSet<Carrera>();
            Escuela = new HashSet<Escuela>();
            Facultad = new HashSet<Facultad>();
            Servicio = new HashSet<Servicio>();
        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe registrar el codigo del Campus")]
        [Remote(action: "CheckExistingCode", controller: "Campus", AdditionalFields = "Id")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Debe registrar el nombre del Campus")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe registrar la localidad del Campus")]
        public string Localidad { get; set; }
        [Required]
        public Estados Estado { get; set; }
        public virtual ICollection<Carrera> Carrera { get; set; }
        public virtual ICollection<Escuela> Escuela { get; set; }
        public virtual ICollection<Facultad> Facultad { get; set; }
        public virtual ICollection<Servicio> Servicio { get; set; }
    }
}
