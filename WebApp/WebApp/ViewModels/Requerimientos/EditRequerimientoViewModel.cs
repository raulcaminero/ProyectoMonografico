using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels.Requerimientos
{
	public class EditRequerimientoViewModel
	{
		[Required(ErrorMessage = "Debe indicar el código")]
		[MaxLength(15, ErrorMessage = "Código solo puede tener 15 caracteres")]
		[Display(Name = "Código")]
		public string Codigo { get; set; }

		[Display(Name = "Aplica para")]
		public int TipoServicioId { get; set; }

		public int EscuelaId { get; set; }

		[Required(ErrorMessage = "Debe subir el documento")]
		[Display(Name = "Documento")]
		public IFormFile Archivo { get; set; }
	}
}
