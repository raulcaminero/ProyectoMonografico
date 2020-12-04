using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels.Requerimientos
{
	public class EditRequerimientoViewModel
	{
		[Required(ErrorMessage = "Debe indicar el codigo")]
		[MaxLength(15, ErrorMessage = "Codigo solo puede tener 15 caracteres")]
		public string Codigo { get; set; }

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
