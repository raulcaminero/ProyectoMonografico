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
        MyDB _db = new MyDB();
        public Servicio()
        {
            var lstcampus = _db.Campus.ToList();
            var lstPersona = _db.Persona.Where(p => p.Rol_Id > 1 && p.EstadoId == "A");
            var facultadList = _db.Facultad.Where(x => x.CampusId == -1).ToList();

            this.TipoServicioList = new SelectList(_db.TipoServicio.ToList(), "TipoServicioId", "TipoServicioDescripcion");
            this.PersonaList = new SelectList(lstPersona, "Codigo", "PersonaNombre");

            this.CampusList = new SelectList(lstcampus, "CampusId", "FullName");
            this.FacultadList = new SelectList(facultadList, "FacultaId", " FacultadNombre");

            this.EscuelaList = new SelectList(new List<String>());
            this.CarreraList = new SelectList(new List<String>());
        }
        public Servicio(int p_Id)
        {
            var tmp = _db.Servicio.Where(s => s.Servicio_Id == p_Id).First();

            var lstcampus = _db.Campus.ToList();
            var lstPersona = _db.Persona.ToList();
            var facultadList = _db.Facultad.Where(f => f.CampusId == tmp.Campus_Id).ToList();
            var escuelaList = _db.Escuela.Where(e => e.FacultadId == tmp.Facultad_Id && e.CampusId == tmp.Campus_Id).ToList();
            var carreraList = _db.Carrera.Where(c => c.EscuelaId == tmp.Escuela_Id && c.FacultadId == tmp.Facultad_Id && c.CampusId == tmp.Campus_Id).ToList();

            var estadoList = _db.Estado.ToList();

            this.TipoServicioList = new SelectList(_db.TipoServicio.Where(t => t.TipoServicioId == tmp.TipoServicio_Id).ToList(), "TipoServicioId", "TipoServicioDescripcion");
            this.PersonaList = new SelectList(lstPersona, "Codigo", "PersonaNombre");

            this.CampusList = new SelectList(lstcampus, "CampusId", "FullName");
            this.FacultadList = new SelectList(facultadList, "FacultadId", "FacultadNombre");

            //this.FacultadList2 = facultadList;

            this.EscuelaList = new SelectList(escuelaList, "EscuelaId", "EscuelaNombre");
            this.CarreraList = new SelectList(carreraList, "CarreraId", "CarreraNombre");
            this.EstadoList = new SelectList(estadoList, "EstadoId", "EstadoNombre");

            this.Servicio_Id = tmp.Servicio_Id;
            this.Servicio_Codigo = tmp.Servicio_Codigo; 
            this.Servicio_Descripcion = tmp.Servicio_Descripcion;
            this.Servicio_FechaInio = tmp.Servicio_FechaInio;
            this.Servicio_FechaCierre = tmp.Servicio_FechaCierre;
            this.Servicio_Costo = tmp.Servicio_Costo;
            this.Persona_Codigo = tmp.Persona_Codigo;
            this.TipoServicio_Id = tmp.TipoServicio_Id;
            this.Estado_Id = tmp.Estado_Id;
            this.Campus_Id = tmp.Campus_Id;
            this.Facultad_Id = tmp.Facultad_Id;
            this.Escuela_Id = tmp.Escuela_Id;
            this.Carrera_Id = tmp.Carrera_Id;
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
        public DateTime? Servicio_FechaInio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha Cierre")]
        public DateTime? Servicio_FechaCierre { get; set; }

        [DisplayName("Costo")]
        public decimal? Servicio_Costo { get; set; }

        [DisplayName("Coordinador")]
        public int? Persona_Codigo { get; set; }

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
        [NotMapped]
        public SelectList TipoServicioList { get; set; }
        [NotMapped]
        public SelectList PersonaList { get; set; }
        [NotMapped]
        public SelectList EstadoList { get; set; }
        [NotMapped]
        public SelectList CampusList { get; set; }
        [NotMapped]
        public SelectList FacultadList { get; set; }
        [NotMapped]
        public SelectList EscuelaList { get; set; }
        [NotMapped]
        public SelectList CarreraList { get; set; }
        public virtual ICollection<Modulo> Modulo { get; set; }
        public virtual Campus Campus { get; set; }
        public virtual Carrera Carrera { get; set; }
        public virtual Escuela Escuela { get; set; }
        public virtual Estado Estado { get; set; }
    }
}
