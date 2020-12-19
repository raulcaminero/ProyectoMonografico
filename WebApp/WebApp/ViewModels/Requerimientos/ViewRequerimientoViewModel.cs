using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels.Requerimientos
{
	public class ViewRequerimientoViewModel
	{
		public int Id { get; set; }

		[Display(Name = "Código")]
		public string Codigo { get; set; }

		[Display(Name = "Aplica para")]
		public TipoServicio TipoServicio { get; set; }

		public Escuela Escuela { get; set; }

		[Display(Name = "Documento")]
		public Archivo Archivo { get; set; }

		[Display(Name = "Fecha de creación")]
		public DateTime FechaCreacion { get; set; }

		[Display(Name = "Registrado por")]
		public Usuario Usuario { get; set; }

		public EstadoRequerimiento Estado { get; set; }

		public List<ViewRequerimientoViewModel> VersionesAnteriores { get; set; }
	}
}
