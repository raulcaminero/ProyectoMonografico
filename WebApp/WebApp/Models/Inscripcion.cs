using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
	public class Inscripcion
	{
		public int Id { get; set; }

		public int IdSolicitud { get; set; }

		[ForeignKey("IdSolicitud")]
		public Solicitud Solicitud { get; set; }

		public int IdServicio { get; set; }

		[ForeignKey("IdServicio")]
		public Servicio Servicio { get; set; }

		public DateTime Fecha { get; set; }

		public Enums.EstadosInscripcion Estado { get; set; }
	}
}
