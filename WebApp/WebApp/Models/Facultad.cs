using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public partial class Facultad
    {
        public Facultad()
        {
            Carrera = new HashSet<Carrera>();
            Escuela = new HashSet<Escuela>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe Introducir el nombre de la facultad")]
        [StringLength(20)]
        public string NombreFacultad { get; set; }

        [Required(ErrorMessage = "Debe Introducir el Codigo de la facultad")]
        [StringLength(8, ErrorMessage = "No puede tener mas de 8 digitos")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Debe Introducir el nombre del decano de la facultad")]
        public string NombreDecano { get; set; }

        [Required(ErrorMessage = "Debe Introducir el Ubicacion de la Facultad en la Torre Administrativa")]
        public string Ubicación { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        public Estados Estado { get; set; }

        public int? CampusId { get; set; }

        public virtual Campus Campus { get; set; }
        public virtual ICollection<Carrera> Carrera { get; set; }
        public virtual ICollection<Escuela> Escuela { get; set; }
    }
}
