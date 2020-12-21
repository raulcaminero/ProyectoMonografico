using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;
using WebApp.ViewModels.Requerimientos.Validations;

namespace WebApp.ViewModels.Requerimientos
{
	public class EditRequerimientoViewModel
	{
		[Required(ErrorMessage = "Debe indicar el código")]
		[MaxLength(15, ErrorMessage = "Código solo puede tener 15 caracteres")]
		[Display(Name = "Código")]
		public string Codigo { get; set; }

		[Required(ErrorMessage = "Debe elegir un tipo de servicio")]
		[Display(Name = "Aplica para")]
		public string TipoServicioDescripcion { get; set; }

		public int TipoServicioId { get; set; }


		[Required(ErrorMessage = "Debe elegir una escuela")]
		[Display(Name = "Escuela")]
		public string EscuelaNombre { get; set; }

		public int EscuelaId { get; set; }

		[Required(ErrorMessage = "Debe subir el documento")]
		[FileTypes("pdf,doc,docx")]
		[Display(Name = "Documento")]
		public IFormFile Archivo { get; set; }
	}
}
