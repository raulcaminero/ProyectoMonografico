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
    public class CalificacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalificacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Calificaciones
        public async Task<IActionResult> Index()
        {
            var ApplicationDbContext = _context.Calificaciones.Include(c => c.Estado).Include(c => c.Estudiante).Include(c => c.Modulo);
            return View(await ApplicationDbContext.ToListAsync());
        }

        // GET: Calificaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificaciones = await _context.Calificaciones
                .Include(c => c.Estado)
                .Include(c => c.Estudiante)
                .Include(c => c.Modulo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calificaciones == null)
            {
                return NotFound();
            }

            return View(calificaciones);
        }

        // GET: Calificaciones/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "EstadoId");
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "Nombre");
            ViewData["ModuloId"] = new SelectList(_context.Modulo, "Id", "Titulo");
            return View();
        }

        // POST: Calificaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModuloId,EstudianteId,Calificacion,EstadoId")] Calificaciones calificaciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calificaciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "EstadoId", calificaciones.EstadoId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "Nombre", calificaciones.EstudianteId);
            ViewData["ModuloId"] = new SelectList(_context.Modulo, "Id", "Titulo", calificaciones.ModuloId);
            return View(calificaciones);
        }

        // GET: Calificaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificaciones = await _context.Calificaciones.FindAsync(id);
            if (calificaciones == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "EstadoNombre", calificaciones.EstadoId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "Nombre", calificaciones.EstudianteId);
            ViewData["ModuloId"] = new SelectList(_context.Modulo, "Id", "Titulo", calificaciones.ModuloId);
            return View(calificaciones);
        }

        // POST: Calificaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModuloId,EstudianteId,Calificacion,EstadoId")] Calificaciones calificaciones)
        {
            if (id != calificaciones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calificaciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalificacionesExists(calificaciones.Id))
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
            ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "EstadoNombre", calificaciones.EstadoId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "Id", "Nombre", calificaciones.EstudianteId);
            ViewData["ModuloId"] = new SelectList(_context.Modulo, "Id", "Titulo", calificaciones.ModuloId);
            return View(calificaciones);
        }

        // GET: Calificaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificaciones = await _context.Calificaciones
                .Include(c => c.Estado)
                .Include(c => c.Estudiante)
                .Include(c => c.Modulo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calificaciones == null)
            {
                return NotFound();
            }

            return View(calificaciones);
        }

        // POST: Calificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calificaciones = await _context.Calificaciones.FindAsync(id);
            _context.Calificaciones.Remove(calificaciones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalificacionesExists(int id)
        {
            return _context.Calificaciones.Any(e => e.Id == id);
        }
    }
}
