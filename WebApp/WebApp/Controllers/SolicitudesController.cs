using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApp.Models;
using WebApp.ViewModels.Solicitudes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApp.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace PerfilEstudiante.Controllers
{
	[Microsoft.AspNetCore.Authorization.Authorize]
	public class SolicitudesController : BaseController
	{
		private readonly ApplicationDbContext _context;

		public SolicitudesController(ApplicationDbContext context) : base(context)
		{
			_context = context;

		}

		// GET: Campus
		[Microsoft.AspNetCore.Authorization.Authorize(Roles = "Administrador")]
		public async Task<IActionResult> Index()
		{
			// Validar que el usuario tenga acceso
			if (!AccountController.GetUsuarioEsAdministrador(User, _context))
				return NotFound();

			var solicitudes = await _context.SolicitudesServicios
				.Include(s => s.Usuario)
				.Include(s => s.Servicio)
				.Include(s => s.Estado)
				.ToListAsync();

			return View(solicitudes);
		}

		public async Task<IActionResult> CargarDocumento(int id, TipoArchivo tipo)
		{
			var pago = new CargaDocumentoSolicitudViewModel() { IdSolicitud = id, TipoDocumento = tipo };
			return View(pago);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CargarDocumento(CargaDocumentoSolicitudViewModel pago)
		{
			if (ModelState.IsValid)
			{
				var idServicio = _context.SolicitudesServicios.Find(pago.IdSolicitud).IdServicio;
				var archivo = await new ArchivosController(_context).Cargar(pago.Archivo, "Solicitudes", $"Servicios\\{idServicio}");

				var archivoSolicitud = new ArchivoSolicitud()
				{
					IdArchivo = archivo.Id,
					IdSolicitud = pago.IdSolicitud,
					Tipo = pago.TipoDocumento,
				};

				if (pago.TipoDocumento == TipoArchivo.AnteProyecto || pago.TipoDocumento == TipoArchivo.Proyecto)
				{
					var proyecto = new Proyecto()
					{
						IdArchivo = archivo.Id,
						IdSolicitud = pago.IdSolicitud,
						EstadoId = "A",
						Tipo = Enum.GetName(typeof(TipoArchivo), pago.TipoDocumento),
					};
					_context.Proyecto.Add(proyecto);
				}

				_context.ArchivosSolicitudes.Add(archivoSolicitud);
				await _context.SaveChangesAsync();
			}

			// Regresar al detalle
			return RedirectToAction(nameof(Detalles), new { id = pago.IdSolicitud });
		}

		private void cargarListas()
		{
			var campus = _context.Campus.ToList();
			ViewBag.Campus = new SelectList(campus, "Id", "Nombre");

			var escuelas = _context.Escuelas.ToList();
			ViewBag.Escuelas = new SelectList(escuelas, "Id", "Nombre");

			var facultades = _context.Facultades.ToList();
			ViewBag.Facultades = new SelectList(facultades, "Id", "NombreFacultad");

			var tipoServicios = _context.TipoServicios.ToList();
			ViewBag.TiposServicios = new SelectList(tipoServicios, "TipoServicioId", "TipoServicioDescripcion");

			var servicios = _context.Servicio.ToList();
			ViewBag.Servicios = new SelectList(servicios, "Servicio_Id", "Servicio_Descripcion");
		}

		[HttpGet]
		public async Task<IActionResult> Detalles(int? id)
		{
			if (id == null)
				return NotFound();

			var solicitud = await _context.SolicitudesServicios
				.Include(s => s.Usuario)
				.Include(s => s.Servicio)
				.Include(s => s.Estado)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (solicitud == null)
				return NotFound();

			solicitud.DocumentosEntregados = _context.ArchivosSolicitudes
				.Where(a => a.IdSolicitud == id)
				.Include(a => a.Archivo)
				.ToList()
				.Select(a => new DocumentoEntregadoViewModel()
				{
					IdArchivo = a.IdArchivo,
					Fecha = a.Archivo.Fecha,
					NombreArchivo = a.Archivo.NombreArchivo,
					Tipo = a.Tipo

				}).ToList();

			return View(solicitud);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CambiarEstado([Bind("Id, IdEstado")] SolicitudServicio solicitud)
		{
			if (solicitud == null)
				return NotFound();

			var original = _context.SolicitudesServicios.Find(solicitud.Id);
			original.IdEstado = solicitud.IdEstado;
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Detalles), new { id = solicitud.Id });
		}

		[HttpGet]
		public async Task<IActionResult> Registrar()
		{
			var usr = AccountController.GetCurrentUser(User, _context);

			var estadosValidos = new List<string>() { "A", "N", "P" }; // Activo, Inscrito, Pendiente
			var solicitud = await _context.SolicitudesServicios
				.Where(s => s.IdUsuario == usr.codigo)
				.Where(s => estadosValidos.Contains(s.IdEstado))
				.FirstOrDefaultAsync();

			if (solicitud != null)
				return RedirectToAction(nameof(Detalles), new { id = solicitud.Id });

			var registrarSolicitudVM = new RegistrarSolicitudViewModel()
			{
				Nombre1 = usr.primer_nombre,
				Nombre2 = usr.segundo_nombre,
				Apellido1 = usr.primer_apellido,
				Apellido2 = usr.segundo_apellido,
				Identificacion = usr.identificacion,
				TipoIdentificacion = usr.tipo_identificacion,
				IdCampus = usr.IdCampus ?? 0,
				Matricula = usr.matricula,
				Contacto = usr.contacto,
				Sexo = usr.sexo,
				Nacionalidad = usr.nacionalidad,
				FechaNacimiento = usr.fecha_nacimiento
			};

			cargarListas();

			return View(registrarSolicitudVM);
		}

		[HttpPost]
		public async Task<IActionResult> Registrar(RegistrarSolicitudViewModel vm)
		{
			if (ModelState.IsValid)
			{
				// Actualizar los campos de Usuario
				var usuario = AccountController.GetCurrentUser(User, _context);
				if (usuario == null)
					return NotFound();

				usuario.matricula = vm.Matricula;
				usuario.sexo = vm.Sexo;
				usuario.primer_nombre = vm.Nombre1;
				usuario.segundo_nombre = vm.Nombre2;
				usuario.primer_apellido = vm.Apellido1;
				usuario.segundo_apellido = vm.Apellido2;
				usuario.identificacion = vm.Identificacion;
				usuario.tipo_identificacion = vm.TipoIdentificacion;
				usuario.contacto = vm.Contacto;
				usuario.nacionalidad = vm.Nacionalidad;
				usuario.fecha_nacimiento = vm.FechaNacimiento;

				// Registrar la Solicitud de Inscripcion
				var solicitud = new SolicitudServicio()
				{
					IdUsuario = usuario.codigo,
					IdServicio = vm.IdServicio,
					Fecha = DateTime.Now,
					IdEstado = "A"
				};

				await _context.SolicitudesServicios.AddAsync(solicitud);
				await _context.SaveChangesAsync();


				// Guardar los archivos

				var archivos = new List<(Archivo Archivo, TipoArchivo Tipo)>();
				var ctrl = new ArchivosController(_context);

				if (vm.ArchivoFoto != null)
					archivos.Add((await ctrl.Cargar(vm.ArchivoFoto, "Solicitudes", $"Servicios\\{vm.IdServicio}"), TipoArchivo.Foto));

				if (vm.ArchivoCedula != null)
					archivos.Add((await ctrl.Cargar(vm.ArchivoCedula, "Solicitudes", $"Servicios\\{vm.IdServicio}"), TipoArchivo.Cedula));

				if (vm.ArchivoKardex != null)
					archivos.Add((await ctrl.Cargar(vm.ArchivoKardex, "Solicitudes", $"Servicios\\{vm.IdServicio}"), TipoArchivo.Notas));

				await _context.SaveChangesAsync();

				// Guardar la relación
				foreach (var archivo in archivos)
				{
					var archivoSolicitud = new ArchivoSolicitud()
					{
						IdArchivo = archivo.Archivo.Id,
						IdSolicitud = solicitud.Id,
						Tipo = archivo.Tipo
					};
					_context.ArchivosSolicitudes.Add(archivoSolicitud);
				}
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Detalles), new { id = solicitud.Id });
			}

			var errores = ModelState.ToList();

			cargarListas();
			return View(vm);
		}

		//public async Task<JsonResult> GetFilteredServicios(int idCarrera = 0, int idTipoServicio = 0, bool addEmpty = false)
		public async Task<List<Servicio>> GetFilteredServicios(int idCarrera = 0, int idTipoServicio = 0, bool addEmpty = false)
		{
			var servicios = new List<Servicio>();

			if (addEmpty)
				servicios.Add(new Servicio() { Servicio_Descripcion = "Seleccione un servicio" });

			servicios.AddRange(await _context.Servicio.Where(x => x.Estado_Id != "E").ToListAsync());

			if (idCarrera > 0)
			{
				servicios = servicios.Where(x => x.Carrera_Id == idCarrera || x.Servicio_Id == 0).ToList();
			}

			if (idTipoServicio > 0)
			{
				servicios = servicios.Where(x => x.TipoServicio_Id == idTipoServicio || x.Servicio_Id == 0).ToList();
			}


			return servicios;
		}


	}
}
