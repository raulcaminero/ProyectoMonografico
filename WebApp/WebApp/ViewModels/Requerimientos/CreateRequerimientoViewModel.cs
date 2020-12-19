using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels.Requerimientos
{
	public class CreateRequerimientoViewModel
	{
		[Display(Name = "Aplica para")]
		public TipoServicio TipoServicio { get; set; }

		public Escuela Escuela { get; set; }

		[Required(ErrorMessage = "Debe subir el documento")]
		[Display(Name = "Documento")]
		public Archivo Archivo { get; set; }
	}
}
