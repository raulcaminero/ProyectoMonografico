using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


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
        public string IdFacultad { get; set; }

        [Required(ErrorMessage = "Debe registrar el nombre de la escuela")]
        public string Nombre { get; set; }


        public Estados Estado { get; set; }
    }
}
