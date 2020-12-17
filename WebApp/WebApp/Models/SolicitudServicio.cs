using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Models.Data;

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

		public Enums.EstadosSolicitud Estado { get; set; }
	}
}
