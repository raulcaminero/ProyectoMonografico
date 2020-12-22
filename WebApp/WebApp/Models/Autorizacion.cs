using System;

namespace WebApp.Models
{
	public class Autorizacion
	{
		public int Id { get; set; }

		public DateTime Fecha { get; set; }

		public int IdUsuarioAutorizado { get; set; }

		public int IdUsuarioQueAutoriza { get; set; }

	}
}
