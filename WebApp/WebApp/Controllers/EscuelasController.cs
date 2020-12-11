using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Data;

namespace WebApp.Controllers
{
    public class EscuelasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EscuelasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Escuelas
        public async Task<IActionResult> Index()
        {
            var request = await _context.Escuelas
                .Where(e => e.Estado != Estados.Eliminado)
                .Include(e => e.Facultad)
                .ToListAsync();

            return View(request);
        }

        // GET: Escuelas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var escuela = await _context.Escuelas
                .Where(e => e.Estado != Estados.Eliminado)
                .Include(e => e.Facultad)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (escuela == null)
                return NotFound();

            return View(escuela);
        }

        // GET: Escuelas/Create
        public IActionResult Create()
        {
            var facultades = _context.Facultades
                .Where(e => e.Estado != Estados.Eliminado).ToList();
            ViewBag.Facultades = new SelectList(facultades, "Id", "NombreFacultad");

            return View();
        }

        // POST: Escuelas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodigoEscuela,IdFacultad,Nombre,Estado")] Escuela escuela)
        {
            if (ModelState.IsValid)
            {
                escuela.Estado = Estados.Activo;
                _context.Add(escuela);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(escuela);
        }

        // GET: Escuelas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escuela = await _context.Escuelas.FindAsync(id);
            if (escuela == null)
            {
                return NotFound();
            }
            return View(escuela);
        }

        // POST: Escuelas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodigoEscuela,IdFacultad,Nombre,Estado")] Escuela escuela)
        {
            if (id != escuela.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escuela);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscuelaExists(escuela.Id))
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
            return View(escuela);
        }

        // GET: Escuelas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var escuela = await _context.Escuelas
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (escuela == null)
                return NotFound();

            return View(escuela);
        }

        // POST: Escuelas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var escuela = await _context.Escuelas.FindAsync(id);
            escuela.Estado = Estados.Eliminado;

            _context.Escuelas.Update(escuela);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EscuelaExists(int id)
        {
            return _context.Escuelas.Any(e => e.Id == id);
        }

        // GET: Escuelas/Inactivate/5
        public async Task<IActionResult> Inactivate(int? id)
        {
            if (id == null)
                return NotFound();

            var escuela = await _context.Escuelas
                .FirstOrDefaultAsync(m => m.Id == id);

            if (escuela == null)
                return NotFound();

            return View(escuela);
        }

        // POST: Escuelas/Inactivate/5
        [HttpPost, ActionName("Inactivate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InactivateConfirmed(int id)
        {
            var escuela = await _context.Escuelas.FindAsync(id);
            escuela.Estado = Estados.Inactivo;
            _context.Escuelas.Update(escuela);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Escuelas/Activate/5
        public async Task<IActionResult> Activate(int? id)
        {
            if (id == null)
                return NotFound();

            var escuela = await _context.Escuelas
                .FirstOrDefaultAsync(m => m.Id == id);

            if (escuela == null)
                return NotFound();

            return View(escuela);
        }

        // POST: Escuelas/Inactivate/5
        [HttpPost, ActionName("Activate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateConfirmed(int id)
        {
            var escuela = await _context.Escuelas.FindAsync(id);
            escuela.Estado = Estados.Activo;
            _context.Escuelas.Update(escuela);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckExistingCode(string codigo, int id)
        {
            bool existsCode = false;

            if (id == 0)
                existsCode = _context.Escuelas.Any(c => c.CodigoEscuela== codigo);
            else
                existsCode = _context.Escuelas.Any(c => c.CodigoEscuela == codigo && c.Id != id);

            if (existsCode)
                return Json("Ya existe una escuela con este codigo");

            return Json(true);
        }
    }
}
