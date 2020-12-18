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
    public class SolicitudServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SolicitudServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SolicitudServicios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SolicitudesServicios.Include(s => s.Estado).Include(s => s.Servicio).Include(s => s.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SolicitudServicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudServicio = await _context.SolicitudesServicios
                .Include(s => s.Estado)
                .Include(s => s.Servicio)
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitudServicio == null)
            {
                return NotFound();
            }

            return View(solicitudServicio);
        }

        // GET: SolicitudServicios/Create
        public IActionResult Create()
        {
            ViewData["IdEstado"] = new SelectList(_context.Estado, "EstadoId", "EstadoId");
            ViewData["IdServicio"] = new SelectList(_context.Servicio, "Servicio_Id", "Servicio_Codigo");
            ViewData["IdUsuario"] = new SelectList(_context.usuarios, "codigo", "EstadoId");
            return View();
        }

        // POST: SolicitudServicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,IdUsuario,IdServicio,IdEstado")] SolicitudServicio solicitudServicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solicitudServicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstado"] = new SelectList(_context.Estado, "EstadoId", "EstadoId", solicitudServicio.IdEstado);
            ViewData["IdServicio"] = new SelectList(_context.Servicio, "Servicio_Id", "Servicio_Codigo", solicitudServicio.IdServicio);
            ViewData["IdUsuario"] = new SelectList(_context.usuarios, "codigo", "EstadoId", solicitudServicio.IdUsuario);
            return View(solicitudServicio);
        }

        // GET: SolicitudServicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudServicio = await _context.SolicitudesServicios.FindAsync(id);
            if (solicitudServicio == null)
            {
                return NotFound();
            }
            ViewData["IdEstado"] = new SelectList(_context.Estado, "EstadoId", "EstadoId", solicitudServicio.IdEstado);
            ViewData["IdServicio"] = new SelectList(_context.Servicio, "Servicio_Id", "Servicio_Codigo", solicitudServicio.IdServicio);
            ViewData["IdUsuario"] = new SelectList(_context.usuarios, "codigo", "EstadoId", solicitudServicio.IdUsuario);
            return View(solicitudServicio);
        }

        // POST: SolicitudServicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,IdUsuario,IdServicio,IdEstado")] SolicitudServicio solicitudServicio)
        {
            if (id != solicitudServicio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitudServicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudServicioExists(solicitudServicio.Id))
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
            ViewData["IdEstado"] = new SelectList(_context.Estado, "EstadoId", "EstadoId", solicitudServicio.IdEstado);
            ViewData["IdServicio"] = new SelectList(_context.Servicio, "Servicio_Id", "Servicio_Codigo", solicitudServicio.IdServicio);
            ViewData["IdUsuario"] = new SelectList(_context.usuarios, "codigo", "EstadoId", solicitudServicio.IdUsuario);
            return View(solicitudServicio);
        }

        // GET: SolicitudServicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudServicio = await _context.SolicitudesServicios
                .Include(s => s.Estado)
                .Include(s => s.Servicio)
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitudServicio == null)
            {
                return NotFound();
            }

            return View(solicitudServicio);
        }

        // POST: SolicitudServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitudServicio = await _context.SolicitudesServicios.FindAsync(id);
            _context.SolicitudesServicios.Remove(solicitudServicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudServicioExists(int id)
        {
            return _context.SolicitudesServicios.Any(e => e.Id == id);
        }
    }
}
