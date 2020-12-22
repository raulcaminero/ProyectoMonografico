using System;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels.Solicitudes
{
	public class DocumentoEntregadoViewModel
	{
		public int IdArchivo { get; set; }

		[Display(Name = "Archivo")]
		public string NombreArchivo { get; set; }

		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
		public DateTime Fecha { get; set; }

		public TipoArchivo	Tipo { get; set; }
	}
}
