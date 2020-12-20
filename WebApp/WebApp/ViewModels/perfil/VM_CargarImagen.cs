using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebApp.ViewModels.perfil
{
    public class VM_CargarImagen

    {   public int Codigo { set; get; }
        public IFormFile Foto { set; get; }
    }
}
