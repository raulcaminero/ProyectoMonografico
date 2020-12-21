using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
	public class ArchivoSolicitud
	{
		public int IdArchivo { get; set; }

		[ForeignKey("IdArchivo")]
		public Archivo Archivo { get; set; }

		public int IdSolicitud { get; set; }

		[ForeignKey("IdSolicitud")]
		public SolicitudServicio Solicitud { get; set; }

		public TipoArchivo Tipo { get; set; }
	}
}
