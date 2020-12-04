using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels.Requerimientos
{
	public class ViewRequerimientoViewModel
	{
		public int Id { get; set; }

		public string Codigo { get; set; }

		[Display(Name = "Título")]
		public string Titulo { get; set; }

		[Display(Name = "Descripción")]
		public string Descripcion { get; set; }

		[Display(Name = "Fecha")]
		public DateTime FechaCreacion { get; set; }

		[Display(Name = "Aplica para")]
		public TipoServicio TipoServicio { get; set; }

		public EstadoRequerimiento Estado { get; set; }

		public List<ViewRequerimientoViewModel> VersionesAnteriores { get; set; }
	}
}
