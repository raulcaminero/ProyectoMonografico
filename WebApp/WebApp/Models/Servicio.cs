using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace WebApp.Models
{
    public partial class Servicio
    {
        public Servicio()
        {
            Modulo = new HashSet<Modulo>();
        }

        [Key]
        public int Servicio_Id { get; set; }
        [DisplayName("Codigo(*)")]
        public string Servicio_Codigo { get; set; }

        [DisplayName("Descripcion(*)")]
        public string Servicio_Descripcion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Fecha Inicio")]
        public DateTime? Servicio_FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha Cierre")]
        public DateTime? Servicio_FechaCierre { get; set; }

        [DisplayName("Costo")]
        public decimal? Servicio_Costo { get; set; }

        [DisplayName("Coordinador")]
        public int? UsuarioCodigo { get; set; }

        [DisplayName("Tipo Servicio(*)")]
        public int TipoServicio_Id { get; set; }

        [DisplayName("Estado")]
        public string Estado_Id { get; set; }

        [DisplayName("Campus(*)")]
        public int Campus_Id { get; set; }

        [DisplayName("Facultad(*)")]
        public int Facultad_Id { get; set; }

        [DisplayName("Escuela(*)")]
        public int Escuela_Id { get; set; }

        [DisplayName("Carrera(*)")]
        public int Carrera_Id { get; set; }
        
        public virtual ICollection<Modulo> Modulo { get; set; }
        public virtual Campus Campus { get; set; }
        public virtual Carrera Carrera { get; set; }
        public virtual Facultad Facultad { get; set; }
        public virtual Escuela Escuela { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual TipoServicio TipoServicio { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
