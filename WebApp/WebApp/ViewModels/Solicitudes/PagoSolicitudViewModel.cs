using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Solicitudes
{
	public class PagoSolicitudViewModel
    {
        [Required]
        public int IdSolicitud { get; set; }

        [Required]
        public IFormFile Archivo { get; set; }
        
    }
}
