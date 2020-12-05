using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Data
{
	public class Requerimiento
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(15)]
		[Display(Name = "Código")]
		public string Codigo { get; set; }

		[Required]
		[MaxLength(50)]
		[Display(Name = "Título")]
		public string Titulo { get; set; }

		[Required]
		[MaxLength(300)]
		[Display(Name = "Descripción")]
		public string Descripcion { get; set; }

		[Required]
		[Display(Name = "Fecha")]
		public DateTime FechaCreacion { get; set; }

		public EstadoRequerimiento Estado { get; set; }

		[Required]
		[Display(Name = "Aplica para")]
		public TipoServicio TipoServicio { get; set; }
	}
}
