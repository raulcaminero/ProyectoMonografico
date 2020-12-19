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

		public DateTime Fecha { get; set; }

		public TipoArchivoSolicitud	Tipo { get; set; }
	}
}
