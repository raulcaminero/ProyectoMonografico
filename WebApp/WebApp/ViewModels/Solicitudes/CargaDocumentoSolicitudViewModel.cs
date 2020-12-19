using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels.Solicitudes
{
	public class CargaDocumentoSolicitudViewModel
    {
        [Required]
        public int IdSolicitud { get; set; }

        [Required]
        public IFormFile Archivo { get; set; }

		public TipoArchivoSolicitud TipoDocumento { get; set; }

	}
}
