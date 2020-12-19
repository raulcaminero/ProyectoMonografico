using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
	public class Requerimiento
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(15)]
		[Display(Name = "Código")]
		public string Codigo { get; set; }

		[Required]
		[Display(Name = "Aplica para")]
		public int TipoServicioId { get; set; }

		[Required]
		[Display(Name = "Escuela")]
		public int EscuelaId { get; set; }

		[Required]
		[Display(Name = "Documento")]
		public int ArchivoId { get; set; }

		[Required]
		[Display(Name = "Fecha de creación")]
		public DateTime FechaCreacion { get; set; }

		[Required]
		[Display(Name = "Registrado por")]
		public int UsuarioCodigo { get; set; }

		public EstadoRequerimiento Estado { get; set; }

		[ForeignKey("TipoServicioId")]
		public TipoServicio TipoServicio { get; set; }

		[ForeignKey("EscuelaId")]
		public Escuela Escuela { get; set; }

		[ForeignKey("ArchivoId")]
		public Archivo Archivo { get; set; }

		[ForeignKey("UsuarioCodigo")]
		public Usuario Usuario { get; set; }
	}
}
