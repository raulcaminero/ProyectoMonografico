using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
	public class SolicitudServicio
	{
		public int Id { get; set; }

		public DateTime Fecha { get; set; }

		public int IdUsuario { get; set; }

		[ForeignKey("IdUsuario")]
		public Usuario Usuario { get; set; }

		public int IdServicio { get; set; }

		[ForeignKey("IdServicio")]
		public Servicio Servicio { get; set; }

		public string IdEstado { get; set; }

		[ForeignKey("IdEstado")]
		public Estado Estado { get; set; }

		[NotMapped]
		public List<ViewModels.Solicitudes.DocumentoEntregadoViewModel> DocumentosEntregados { get; set; }
	}
}
