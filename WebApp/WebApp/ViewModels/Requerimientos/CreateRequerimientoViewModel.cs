using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;
using WebApp.ViewModels.Requerimientos.Validations;

namespace WebApp.ViewModels.Requerimientos
{
	public class CreateRequerimientoViewModel
	{
		[Required(ErrorMessage = "Debe elegir un tipo de servicio")]
		[Display(Name = "Aplica para")]
		public int TipoServicioId { get; set; }

		[Required(ErrorMessage = "Debe elegir una escuela")]
		[Display(Name = "Escuela")]
		public int EscuelaId { get; set; }

		[Required(ErrorMessage = "Debe subir el documento")]
		[FileTypes("pdf,doc,docx")]
		[Display(Name = "Documento")]
		public IFormFile Archivo { get; set; }
	}
}
