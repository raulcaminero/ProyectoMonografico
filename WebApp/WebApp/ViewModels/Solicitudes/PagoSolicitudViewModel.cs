using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels.Solicitudes
{
    public class PagoSolicitudViewModel
    {
        public int IdSolicitud { get; set; }
        public IFormFile Archivo { get; set; }
    }
}
