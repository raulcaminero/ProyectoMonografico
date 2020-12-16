using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Data;

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


		[Required]
		public int IdCampus { get; set; }

		[ForeignKey("IdCampus")]
		public Campus Campus { get; set; }

		public int IdUsuario { get; set; }


		public SelectList ListaCampus { get; set; }
		public SelectList ListaFacultades { get; set; }
	}
}
