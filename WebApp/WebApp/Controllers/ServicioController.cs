using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json;
using WebApp.Models;
using WebApp.Dto;
using WebApp.Models.Enums;

namespace WebApp.Controllers
{
    public class ServicioController : Controller
    {
        public string _draw = "";
        public string _start = "";
        public string _length = "";
        public string _sortColumn = "";
        public string _sortColumnDir = "";
        public string _valorBusqueda = "";

        public int _pagSize, _skip, _regTotal;

        ApplicationDbContext _db;
        public ServicioController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //List<ListaServicio> LstServicios = new List<ListaServicio>();


            var LstServicios = (from s in _db.Servicio
                                join es in _db.Estado on s.Estado_Id equals es.EstadoId
                                join t in _db.TipoServicios on s.TipoServicio_Id equals t.TipoServicioId
                                join c in _db.Campus on s.Campus_Id equals c.Id
                                join f in _db.Facultades on s.Facultad_Id equals f.Id
                                join e in _db.Escuelas on s.Escuela_Id equals e.Id
                                join r in _db.Carreras on s.Carrera_Id equals r.Id
                                where (s.Estado_Id != "E")
                                select new ListaServicio
                                {
                                    Servicio_Id = s.Servicio_Id,
                                    Servicio_Codigo = s.Servicio_Codigo,
                                    Servicio_Descripcion = s.Servicio_Descripcion,
                                    Servicio_FechaInicio = s.Servicio_FechaInicio,
                                    Servicio_FechaCierre = s.Servicio_FechaCierre,
                                    Estado_Id = s.Estado_Id,
                                    TipoServicio_Nombre = t.TipoServicioDescripcion,
                                    Campus = c,
                                    Escuela = e,
                                    Carrera = r,
                                    Facultad = f

                                }).ToList();

            if (LstServicios.ToList().Count < 1)
            {
                ModelState.AddModelError("NoListado", "No Hay Dados Para el Listado");
            }

            return View(LstServicios);

        }

        public IActionResult Crear()
        {
            Servicio servicio = new Servicio();

            return View(servicio);
        }

        [HttpPost]
        public IActionResult Crear(Servicio servicio, string action)
        {
            if (action == "listado")
            {

                return RedirectToAction("Listado");
            }
            if (ModelState.IsValid)
            {
                servicio.Estado_Id = "I";

                _db.Servicio.Add(servicio);
                _db.SaveChanges();

                return RedirectToAction("Index", "Servicio");
            }


            return View("Index", servicio);
            //return View();
        }

        public IActionResult Detalle(int id)
        {
            Servicio servicios = new Servicio(id);

            return View(servicios);
        }

        public ActionResult Editar(int id, string action)
        {
            Servicio servicio = new Servicio(id);
            if (action == "listado")
            {
                return RedirectToAction("Index");
            }

            return View(servicio);
        }

        [HttpPost]
        public ActionResult Editar(Servicio data)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(data).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(data);
        }

        public ActionResult Eliminar(int id, string action)
        {
            var ServicioData = _db.Servicio.Where(x => x.Servicio_Id == id).FirstOrDefault();
            if (ServicioData != null)
            {

                ServicioData.Estado_Id = "E";

                _db.Entry(ServicioData).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return RedirectToAction("Index");

            //Servicio servicio = new Servicio(id);
            if (action == "listado")
            {
                return RedirectToAction("Index");
            }

            //return View(servicio);
        }


        //------------------------------------------------
        [HttpPost]
        public IActionResult ServicioJson()
        {
            //List<ListaServicio> LstServicios = new List<ListaServicio>();

            var _draw = HttpContext.Request.Query["draw"].FirstOrDefault();
            var _start = HttpContext.Request.Query["start"].FirstOrDefault();
            var _length = HttpContext.Request.Query["length"].FirstOrDefault();
            var _sortColumn = HttpContext.Request.Query["columns[" + Request.Query["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var _sortColumnDir = HttpContext.Request.Query["order[0][dir]"].FirstOrDefault();
            var _valorBusqueda = HttpContext.Request.Query["search[value]"].FirstOrDefault();

            _pagSize = _length != null ? Convert.ToInt32(_length) : 5;
            _skip = _start != null ? Convert.ToInt32(_start) : 0;
            _regTotal = 0;
            _sortColumn = _sortColumn != null ? _sortColumn : "";
            _sortColumnDir = _sortColumnDir != null ? _sortColumnDir : "";
            _valorBusqueda = _valorBusqueda != null ? _valorBusqueda : "";

            //Ordenar Por defecto
            if (_sortColumn == "" || _sortColumnDir == "")
            {
                _sortColumnDir = "asc";
                _sortColumn = "Servicio_Codigo";
            }

            var LstServicios = (from s in _db.Servicio
                                join es in _db.Estado on s.Estado_Id equals es.EstadoId
                                join t in _db.TipoServicios on s.TipoServicio_Id equals t.TipoServicioId
                                join c in _db.Campus on s.Campus_Id equals c.Id
                                join f in _db.Facultades on s.Facultad_Id equals f.Id
                                join e in _db.Escuelas on s.Escuela_Id equals e.Id
                                join r in _db.Carreras on s.Carrera_Id equals r.Id
                                select new ListaServicio
                                {
                                    Servicio_Id = s.Servicio_Id,
                                    Servicio_Codigo = s.Servicio_Codigo,
                                    Servicio_Descripcion = s.Servicio_Descripcion,
                                    Servicio_FechaInicio = s.Servicio_FechaInicio,
                                    Servicio_FechaCierre = s.Servicio_FechaCierre,
                                    TipoServicio_Nombre = t.TipoServicioDescripcion,
                                    Facultad = f,
                                    Escuela = e,
                                    Carrera = r,
                                    Campus = c,

                                }).ToList();

            _regTotal = LstServicios.Count();

            LstServicios = LstServicios.Skip(_skip).Take(_pagSize).ToList();

            return Json(new { draw = _draw, recordsFiltered = _regTotal, recordsTotal = _regTotal, data = LstServicios });

        }
        [HttpGet]
        public ActionResult GetFacultades(string campusId)
        {
            if (!string.IsNullOrWhiteSpace(campusId))  // && campusId.Length == 3
            {
                IEnumerable<SelectListItem> facultades = _db.Facultades.AsNoTracking()
                 .Where(n => n.CampusId == int.Parse(campusId) && n.Estado == Estados.Activo)
                 .OrderBy(n => n.NombreFacultad)
                     .Select(n =>
                     new SelectListItem
                     {
                         Value = n.Id.ToString(),
                         Text = n.NombreFacultad
                     }).ToList();

                //var repo = new RegionsRepository();
                //IEnumerable<SelectListItem> facultades = repo.Getfacultades(campusId);
                //return Json(facultades, Newtonsoft.Json.JsonSerializerSettings());

                return Json(facultades);
            }

            return null;
        }

        [HttpGet]
        public ActionResult GetEscuelas(string campusId, int facultadId)
        {
            if (!string.IsNullOrWhiteSpace(campusId) && facultadId > 0)  // && campusId.Length == 3
            {
                IEnumerable<SelectListItem> datos = _db.Escuelas.AsNoTracking()
                 .Where(n => n.CampusId == int.Parse(campusId)
                        && n.IdFacultad == facultadId && n.Estado == Estados.Activo)
                 .OrderBy(n => n.Nombre)
                     .Select(n =>
                     new SelectListItem
                     {
                         Value = n.EscuelaId.ToString(),
                         Text = n.Nombre
                     }).ToList();

                //var repo = new EscuelasRepository();
                //IEnumerable<SelectListItem> escuelas = repo.Getfacultades(campusId, facultadId);
                //return Json(escuelas, Newtonsoft.Json.JsonSerializerSettings());

                return Json(datos);
            }

            return null;
        }

        [HttpGet]
        public ActionResult GetCarreras(string campusId, int facultadId, int escuelaId)
        {
            if (!string.IsNullOrWhiteSpace(campusId) && facultadId > 0 && escuelaId > 0)  // && campusId.Length == 3
            {
                IEnumerable<SelectListItem> datos = _db.Carreras.AsNoTracking()
                 .Where(n => n.CampusId == int.Parse(campusId) &&
                             n.FacultadId == facultadId &&
                             n.IdEscuela == escuelaId &&
                             n.Estado == Estados.Activo)
                 .OrderBy(n => n.Nombre)
                     .Select(n =>
                     new SelectListItem
                     {
                         Value = n.Id.ToString(),
                         Text = n.Nombre
                     }).ToList();

                //var repo = new CarrerasRepository();
                //IEnumerable<SelectListItem> carreras = repo.Getfacultades(campusId, facultadId, escuelaId);
                //return Json(carreras, Newtonsoft.Json.JsonSerializerSettings());

                return Json(datos);
            }

            return null;
        }
    }
}