using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models.Data
{
    public class Escuela
    {
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
    }
}
