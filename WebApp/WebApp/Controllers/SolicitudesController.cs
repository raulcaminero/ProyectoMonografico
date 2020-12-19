using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApp.Models;
using WebApp.ViewModels.Solicitudes;
using WebApp.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApp.Controllers;
using Microsoft.AspNetCore.Hosting;

namespace PerfilEstudiante.Controllers
{
	public class SolicitudesController : Controller
	{
		private readonly ApplicationDbContext _context;
       
        public SolicitudesController(ApplicationDbContext context)
		{
			_context = context;
			
		}

        [HttpGet]
        // GET: Campus
        public async Task<IActionResult> Index()
        {
            var solicitudes = await _context.SolicitudesServicios
                .Include(s => s.Usuario)
                //.Include(s => s.Servicio)
                .Include(s => s.Estado)
                .ToListAsync();

			return View(solicitudes);
		}

		[HttpGet]
		public async Task<IActionResult> Pago(int id)
		{
			/*var solicitudes = await _context.SolicitudesServicios
				.Include(s => s.Usuario)
				.Include(s => s.Servicio)
				.Include(s => s.Estado).FirstOrDefaultAsync(m=>m.IdEstado=="A");
			*/
			var pago = new PagoSolicitudViewModel() {

				IdSolicitud = id
			
			}; 
			return View(pago);
		}
		// Solicitud Pago
		[HttpPost]
		public async Task<IActionResult> Pago(PagoSolicitudViewModel pago)
		{
			var solicitudes = await _context.SolicitudesServicios
				.Include(s => s.Usuario)
				.Include(s => s.Servicio)
				.Include(s => s.Estado).FirstOrDefaultAsync(p=> p.IdEstado=="A");
				

			return View(solicitudes);
		}
		private void cargarListas()
		{
			var campus = _context.Campus.ToList();
			ViewBag.Campus = new SelectList(campus, "Id", "Nombre");

            var facultades = _context.Facultades.ToList();
            ViewBag.Facultades = new SelectList(facultades, "Id", "NombreFacultad");

            var tipoServicios = _context.TipoServicios.ToList();
            var lstTipos = new SelectList(tipoServicios, "TipoServicioId", "TipoServicioDescripcion");
            ViewBag.TiposServicios = lstTipos;
        }

        [HttpGet]
        public IActionResult Registrar(string email)
        {
            cargarListas();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detalles(int? id)
        {
            var estados = _context.Estado.ToList();
            ViewBag.Estados = new SelectList(estados, "EstadoId", "EstadoNombre");

            if (id == null)
                return NotFound();


            var solicitud = await _context.SolicitudesServicios
                .Include(s => s.Usuario)
                //.Include(s => s.Servicio)
                .Include(s => s.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (solicitud == null)
                return NotFound();


            return View(solicitud);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detalles([Bind("Id, IdEstado")] SolicitudServicio solicitud)
        {
            if (solicitud == null)
            {
                return NotFound();
            }

            var original = _context.SolicitudesServicios.Find(solicitud.Id);
            original.IdEstado = solicitud.IdEstado;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarSolicitudViewModel vm)
        {
            // TEMPORAL
            vm.IdServicio = _context.Servicio.First().Servicio_Id;

            if (ModelState.IsValid)
            {
                // Actualizar los campos de Usuario
                var email = User.Identity.Name;
                var usuario = _context.usuarios.FirstOrDefault(u => u.Email == email);
                if (usuario == null)
                    return NotFound();

                usuario.matricula = vm.Matricula;
                usuario.sexo = vm.Sexo;
                usuario.primer_nombre = vm.Nombre1;
                usuario.segundo_nombre = vm.Nombre2;
                usuario.primer_apellido = vm.Apellido1;
                usuario.segundo_apellido = vm.Apellido2;
                usuario.contacto = vm.Contacto;
                usuario.nacionalidad = vm.Nacionalidad;
                usuario.fecha_nacimiento = vm.FechaNacimiento;

                // Registrar la Solicitud de Inscripcion
                var solicitud = new SolicitudServicio()
                {
                    IdUsuario = usuario.codigo,
                    Fecha = DateTime.Now,
                    IdEstado = "A"
                };

                _context.SolicitudesServicios.Add(solicitud);
                _context.SaveChanges();

                // Guardar los archivos
                var archivos = new List<Archivo>();
                var ctrl = new ArchivosController(_context);

                archivos.Add(await ctrl.Cargar(vm.ArchivoFoto, "Solicitudes", $"Servicios\\{vm.IdServicio}"));
                archivos.Add(await ctrl.Cargar(vm.ArchivoCedula, "Solicitudes", $"Servicios\\{vm.IdServicio}"));
                archivos.Add(await ctrl.Cargar(vm.ArchivoKardex, "Solicitudes", $"Servicios\\{vm.IdServicio}"));
                await _context.SaveChangesAsync();

                // Guardar la relación
                foreach (var archivo in archivos)
                {
                    var archivoSolicitud = new ArchivoSolicitud()
                    {
                        IdArchivo = archivo.Id,
                        IdSolicitud = solicitud.Id
                    };
                    _context.ArchivosSolicitudes.Add(archivoSolicitud);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            cargarListas();
            return View(vm);
        }

        public async Task<List<Servicio>> GetFilteredServicios(int idCarrera = 0, int idTipoServicio = 0, bool addEmpty = false)
        {
            var servicios = new List<Servicio>();

            if (addEmpty)
                servicios.Add(new Servicio() { Servicio_Descripcion = "Seleccione un servicio" });

            if (idCarrera > 0 && idTipoServicio > 0)
            {
                servicios.AddRange(await _context.Servicio
                    .Where(s => s.Carrera_Id == idCarrera)
                    .Where(s => s.TipoServicio_Id == idTipoServicio)
                    .Where(s => s.Estado_Id != "E")
                    .ToListAsync());
            }

            return servicios;
        }


    }
}
