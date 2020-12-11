using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApp.Models.Data;

namespace WebApp.ViewModels.Carrera
{
	public class VM_CreateCarrera
    {
        [Remote(action: "CheckExistingCarrera", controller: "Carreras", AdditionalFields = "Id")]
        public WebApp.Models.Data.Carrera Carrera { get; set; }

		[Required]
		public string Codigo { get; set; }

		[Required]
		public string Nombre { get; set; }

		[Required]
		public int IdEscuela { get; set; }
		public Escuela Escuela { get; set; }

		public List<Escuela> Escuelas { get; set; }

    }
}
