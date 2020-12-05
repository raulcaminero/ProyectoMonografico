using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels.Carrera
{
    public class VM_CreateCarrera
    {
        [Remote(action: "CheckExistingCarrera", controller: "Carreras", AdditionalFields = "Id")]
        public WebApp.Models.Data.Carrera Carrera { get; set; }

        public List<WebApp.Models.Data.Escuelas> Escuelas { get; set; }

    }
}
