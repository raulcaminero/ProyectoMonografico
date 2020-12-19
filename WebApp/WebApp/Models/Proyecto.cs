using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }
        public string Tipo { get; set; }

        [DisplayName("Solicitud")]
        public int IdSolicitud { get; set; }

        [DisplayName("Archivo")]
        public int IdArchivo { get; set; }

        [DisplayName("Estado")]
        public string EstadoId { get; set; }

        [ForeignKey("IdSolicitud")]
        public virtual SolicitudServicio Solicitud { get; set; }

        [ForeignKey("IdArchivo")]
        public virtual Archivo Archivo { get; set; }

        [ForeignKey("EstadoId")]
        public virtual Estado Estado { get; set; }
    }
}
