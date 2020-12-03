using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Data
{
    public class Campus
    {
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
    }
}
