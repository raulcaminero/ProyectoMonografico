using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WebApp.Models.Data
{
    public class Carrera
    {
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int idEscuela { get; set; }
        public string Nombre { get; set; }
        public string Detalles { get; set; }
        public string Estado { get; set; }

    }
}
