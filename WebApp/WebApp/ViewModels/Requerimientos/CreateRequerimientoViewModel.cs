using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels.Requerimientos
{
	public class CreateRequerimientoViewModel
	{
		[Required(ErrorMessage = "Debe indicar el titulo")]
		[MaxLength(50, ErrorMessage = "El titulo tiene un maximo de 50 caracteres")]
		[Display(Name = "Título")]
		public string Titulo { get; set; }

		[Required(ErrorMessage = "Debe indicar la descripcion")]
		[MaxLength(300, ErrorMessage = "La descripcion tiene un maximo de 300 caracteres")]
		[Display(Name = "Descripción")]
		public string Descripcion { get; set; }

		[Display(Name = "Aplica para")]
		public TipoServicio TipoServicio { get; set; }

	}
}
