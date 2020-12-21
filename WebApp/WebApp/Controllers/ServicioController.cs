using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
	[Microsoft.AspNetCore.Authorization.Authorize(Roles ="Administrador")]
    public class ServicioController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ServicioController(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        // GET: Servicio
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Servicio
                .Include(s => s.Campus)
                .Include(s => s.Carrera)
                .Include(s => s.Escuela)
                .Include(s => s.Estado)
                .Include(s => s.Facultad)
                .Include(s => s.TipoServicio)
                .Include(s => s.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Servicio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio
                .Include(s => s.Campus)
                .Include(s => s.Carrera)
                .Include(s => s.Escuela)
                .Include(s => s.Estado)
                .Include(s => s.Facultad)
                .Include(s => s.Usuario)
                .Include(s => s.TipoServicio)
                .FirstOrDefaultAsync(m => m.Servicio_Id == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // GET: Servicio/Create
        public IActionResult Create()
        {
            ViewData["Campus_Id"] = new SelectList(_context.Campus, "Id", "Nombre");
            ViewData["Carrera_Id"] = new SelectList(_context.Carreras, "Id", "Nombre");
            ViewData["Escuela_Id"] = new SelectList(_context.Escuelas, "Id", "Nombre");
            ViewData["Estado_Id"] = new SelectList(_context.Estado, "EstadoId", "EstadoNombre");
            ViewData["Facultad_Id"] = new SelectList(_context.Facultades, "Id", "NombreFacultad");
            ViewData["UsuarioCodigo"] = new SelectList(_context.usuarios.Where(x => x.Rol.Descripcion == "Administrador"), "codigo", "NombreCompleto");
            ViewData["TipoServicio_Id"] = new SelectList(_context.TipoServicios, "TipoServicioId", "TipoServicioDescripcion");
            return View();
        }

        // POST: Servicio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Servicio_Id,Servicio_Codigo,Servicio_Descripcion,Servicio_FechaInicio,Servicio_FechaCierre,Servicio_Costo,UsuarioCodigo,TipoServicio_Id,Estado_Id,Campus_Id,Facultad_Id,Escuela_Id,Carrera_Id")] Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Campus_Id"] = new SelectList(_context.Campus, "Id", "Nombre", servicio.Campus_Id);
            ViewData["Carrera_Id"] = new SelectList(_context.Carreras, "Id", "Nombre", servicio.Carrera_Id);
            ViewData["Escuela_Id"] = new SelectList(_context.Escuelas, "Id", "Nombre", servicio.Escuela_Id);
            ViewData["Estado_Id"] = new SelectList(_context.Estado, "EstadoId", "EstadoNombre", servicio.Estado_Id);
            ViewData["Facultad_Id"] = new SelectList(_context.Facultades, "Id", "NombreFacultad", servicio.Facultad_Id);
            ViewData["UsuarioCodigo"] = new SelectList(_context.usuarios.Where(x => x.Rol.Descripcion == "Administrador"), "codigo", "NombreCompleto", servicio.UsuarioCodigo);
            ViewData["TipoServicio_Id"] = new SelectList(_context.TipoServicios, "TipoServicioId", "TipoServicioDescripcion", servicio.TipoServicio_Id);
            return View(servicio);
        }

        // GET: Servicio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }
            ViewData["Campus_Id"] = new SelectList(_context.Campus, "Id", "Nombre", servicio.Campus_Id);
            ViewData["Carrera_Id"] = new SelectList(_context.Carreras, "Id", "Nombre", servicio.Carrera_Id);
            ViewData["Escuela_Id"] = new SelectList(_context.Escuelas, "Id", "Nombre", servicio.Escuela_Id);
            ViewData["Estado_Id"] = new SelectList(_context.Estado, "EstadoId", "EstadoNombre", servicio.Estado_Id);
            ViewData["Facultad_Id"] = new SelectList(_context.Facultades, "Id", "NombreFacultad", servicio.Facultad_Id);
            ViewData["UsuarioCodigo"] = new SelectList(_context.usuarios.Where(x => x.Rol.Descripcion == "Administrador"), "codigo", "NombreCompleto", servicio.UsuarioCodigo);
            ViewData["TipoServicio_Id"] = new SelectList(_context.TipoServicios, "TipoServicioId", "TipoServicioDescripcion", servicio.TipoServicio_Id);
            return View(servicio);
        }

        // POST: Servicio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Servicio_Id,Servicio_Codigo,Servicio_Descripcion,Servicio_FechaInicio,Servicio_FechaCierre,Servicio_Costo,UsuarioCodigo,TipoServicio_Id,Estado_Id,Campus_Id,Facultad_Id,Escuela_Id,Carrera_Id")] Servicio servicio)
        {
            if (id != servicio.Servicio_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(servicio.Servicio_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Campus_Id"] = new SelectList(_context.Campus, "Id", "Nombre", servicio.Campus_Id);
            ViewData["Carrera_Id"] = new SelectList(_context.Carreras, "Id", "Nombre", servicio.Carrera_Id);
            ViewData["Escuela_Id"] = new SelectList(_context.Escuelas, "Id", "Nombre", servicio.Escuela_Id);
            ViewData["Estado_Id"] = new SelectList(_context.Estado, "EstadoId", "EstadoNombre", servicio.Estado_Id);
            ViewData["Facultad_Id"] = new SelectList(_context.Facultades, "Id", "NombreFacultad", servicio.Facultad_Id);
            ViewData["UsuarioCodigo"] = new SelectList(_context.usuarios.Where(x => x.Rol.Descripcion == "Administrador"), "codigo", "NombreCompleto", servicio.UsuarioCodigo);
            ViewData["TipoServicio_Id"] = new SelectList(_context.TipoServicios, "TipoServicioId", "TipoServicioDescripcion", servicio.TipoServicio_Id);
            return View(servicio);
        }

        // GET: Servicio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio
                .Include(s => s.Campus)
                .Include(s => s.Carrera)
                .Include(s => s.Escuela)
                .Include(s => s.Estado)
                .Include(s => s.Facultad)
                .Include(s => s.Usuario)
                .Include(s => s.TipoServicio)
                .FirstOrDefaultAsync(m => m.Servicio_Id == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // POST: Servicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicio = await _context.Servicio.FindAsync(id);
            _context.Servicio.Remove(servicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicio.Any(e => e.Servicio_Id == id);
        }
    }
}
