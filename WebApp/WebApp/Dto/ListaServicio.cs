using WebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebApp.Dto
{
    public class ListaServicio
    {
        [Key]
        public int Servicio_Id { get; set; }
        public string Servicio_Codigo { get; set; }
        public string Servicio_Descripcion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Servicio_FechaInio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Servicio_FechaCierre { get; set; }
        public decimal? Servicio_Costo { get; set; }
        public int? Persona_Codigo { get; set; }
        public int TipoServicio_Id { get; set; }
        public string TipoServicio_Nombre { get; set; }
        public String Estado_Id { get; set; }
        public string Estado_Nombre { get; set; }
        public string Campus_Nombre { get; set; }
        public string Faculta_Nombre { get; set; }
        public string Escuela_Nombre { get; set; }
        public string Carrera_Nombre { get; set; }

        public Campus Campus { get; set; }
        public Facultad Facultad { get; set; }
        public Escuela Escuela { get; set; }
        public Carrera Carrera { get; set; }
        public Estado Estado { get; set; }
    }
}
