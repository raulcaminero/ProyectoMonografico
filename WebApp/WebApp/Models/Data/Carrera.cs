using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models.Data
{
    public class Carrera
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Es necesario introducir un código para la carrera")]
        [DisplayName("Código")]
        [Remote(action: "CheckExisting_Code", controller: "Carreras", AdditionalFields = "Id")]
        public string Codigo { get; set; }
        [DisplayName("Escuela")]
        public int idEscuela { get; set; }
        [Required(ErrorMessage = "Debe colocar un nombre para la carrera")]
        [Remote(action: "CheckExisting_Name", controller: "Carreras", AdditionalFields = "Id")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe agregar detalles sobre la carrera")]
        public string Detalles { get; set; }
        public Estados Estado { get; set; }

    }
}
