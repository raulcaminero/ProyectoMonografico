﻿using Microsoft.AspNetCore.Mvc;
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

namespace PerfilEstudiante.Controllers
{
    public class SolicitudesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SolicitudesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Campus
        public async Task<IActionResult> Index()
        {
            var solicitudes = await _context.SolicitudesServicios
                .Include(s => s.Usuario)
                .Include(s => s.Servicio)
                .Include(s => s.Estado)
                .ToListAsync();

            return View(solicitudes);
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
        public IActionResult Registrar(string email)
        {
            cargarListas();
            return View();
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
