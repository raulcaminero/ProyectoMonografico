using Microsoft.AspNetCore.Http;
using WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ramontesis.Dto
{
	public class ModuloDto
    {

        public ModuloDto()
        {
            Calificaciones = new HashSet<Calificaciones>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int IdProfesor { get; set; }
        public IFormFile Imagen { get; set; }
        public string EstadoId { get; set; }
        public int IdAdjunto { get; set; }
        public int ServicioId { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual AdjuntoMaterial IdAdjuntoNavigation { get; set; }
        public virtual Profesor IdProfesorNavigation { get; set; }
        public virtual Servicio Servicio { get; set; }
        public virtual ICollection<Calificaciones> Calificaciones { get; set; }


    }
}
