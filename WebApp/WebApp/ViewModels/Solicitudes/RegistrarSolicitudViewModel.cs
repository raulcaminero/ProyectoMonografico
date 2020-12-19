using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels.Solicitudes
{
	public class RegistrarSolicitudViewModel
	{
		[Required]
		public string Nombre1 { get; set; }

		public string Nombre2 { get; set; }

		[Required]
		public string Apellido1 { get; set; }

		public string Apellido2 { get; set; }

		[Required]
		public int TipoIdentificacion { get; set; }

		[Required]
		public string Identificacion { get; set; }

		[Required]
		public string Sexo { get; set; }

		[Required]
		public string Matricula { get; set; }

		public string Nacionalidad { get; set; }

		public string Contacto { get; set; }

		public DateTime? FechaNacimiento { get; set; }


		[Required(ErrorMessage = "Debe indicar el campus")]
		public int IdCampus { get; set; }

		[ForeignKey("IdCampus")]
		public Campus Campus { get; set; }


		[Required(ErrorMessage = "Debe indicar el plan")]
		public int IdServicio { get; set; }

		[ForeignKey("IdServicio")]
		public Servicio Servicio { get; set; }

		public IFormFile ArchivoFoto { get; set; }

		public IFormFile ArchivoCedula { get; set; }

		public IFormFile ArchivoKardex { get; set; }
		public IFormFile ArchivoPago { get; set; }
	}
}
