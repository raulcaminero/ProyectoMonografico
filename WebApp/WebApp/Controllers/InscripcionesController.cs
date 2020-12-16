using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ViewModels.Solicitudes;
using WebApp.Models.Data;
using WebApp.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PerfilEstudiante.Controllers
{
	public class InscripcionesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public InscripcionesController(ApplicationDbContext context)
		{
			_context = context;
		}

		private void cargarListas()
		{
			var campus = _context.Campus.ToList();
			ViewBag.Campus = new SelectList(campus, "Id", "Nombre");

			var facultades = _context.Facultades.ToList();
			ViewBag.Facultades = new SelectList(facultades, "Id", "NombreFacultad");

			var escuelas = _context.Escuelas.ToList();
			ViewBag.Escuelas = new SelectList(escuelas, "Id", "Nombre");

			var carreras = _context.Carreras.ToList();
			ViewBag.Carreras = new SelectList(carreras, "Id", "Nombre");
		}

		[HttpGet]
		public ActionResult Solicitar(string email)
		{
			cargarListas();
			return View();
		}

		[HttpPost]
		public ActionResult Solicitar(RegistrarSolicitudViewModel vm)
		{
			if (ModelState.IsValid)
			{
				// Actualizar los campos de Usuario
				var usuario = _context.usuarios1.Find(vm.IdUsuario);
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
				var solicitud = new Solicitud()
				{
					IdUsuario = vm.IdUsuario,
					Fecha = DateTime.Now,
					Estado = EstadosSolicitud.Pendiente
				};

				_context.Solicitudes.Add(solicitud);
				_context.SaveChanges();

				// ESTO ES TEMPORAL, DESCONOZCO A DONDE DEBE REDIRECCIONAR UNA VEZ EL ESTUDIANTE SOLICITA LA INSCRIPCION.
				return RedirectToAction("Escuelas", "Index");
			}

			cargarListas();
			return View(vm);
		}
	}
}
