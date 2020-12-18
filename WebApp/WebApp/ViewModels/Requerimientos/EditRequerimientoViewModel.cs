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

		[Required(ErrorMessage = "Debe subir el documento")]
		[Display(Name = "Documento")]
		public Archivo Archivo { get; set; }
	}
}
