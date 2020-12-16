using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Data;

namespace WebApp.Models
{
	public class Solicitud
	{
		public int Id { get; set; }

		public int IdUsuario { get; set; }

		[ForeignKey("IdUsuario")]
		public Usuario Usuario { get; set; }

		public DateTime Fecha { get; set; }

		public Enums.EstadosSolicitud Estado { get; set; }
	}
}
