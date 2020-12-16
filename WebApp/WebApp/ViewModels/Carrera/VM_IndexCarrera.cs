using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Data;
namespace WebApp.ViewModels.Carrera
{
    public class VM_IndexCarrera
    {
        public IEnumerable<WebApp.Models.Data.Carrera> Carreras { get; set; }
        public List<Escuela> Escuelas { get; set; }
        public List<Facultad> Facultades { get; set; }

    }
}
