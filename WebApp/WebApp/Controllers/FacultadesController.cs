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
    public class FacultadesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacultadesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Facultad
        public async Task<IActionResult> Index()
        {
            var facultad = await _context.Facultades.Where
                (x => x.EstadoId != "I").ToListAsync();
            return View(facultad);
        }

        // GET: Facultad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultad = await _context.Facultades
                .FirstOrDefaultAsync(m => m.FacultadId == id);
            if (facultad == null)
            {
                return NotFound();
            }

            return View(facultad);
        }

        // GET: Facultad/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facultad/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create
            ([Bind("FacultadId,FacultadCodigo,FacultadNombre,NombreDecano,Ubicación,Telefono,EstadoId,CampusId")] Facultad facultad)
        {
            
            if (ModelState.IsValid)
            {
                bool Existe = _context.Facultades.Any(x => x.FacultadCodigo == facultad.FacultadCodigo);
                if (Existe)
                {
                    ModelState.AddModelError("Codigokey", "El codigo ya existe");

                    return View(facultad);
                }

                
                _context.Add(facultad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            return View(facultad);
        }

        // GET: Facultad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var facultad = await _context.Facultades.FindAsync(id);
            if (facultad == null)
                return NotFound();

            return View(facultad);
        }

        // POST: Facultad/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("FacultadId,FacultadCodigo,FacultadNombre,NombreDecano,Ubicación,Telefono,EstadoId,CampusId")] Facultad facultad)
        {
            if (id != facultad.FacultadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool Existe = _context.Facultades.Any(x => x.FacultadCodigo == facultad.FacultadCodigo && x.FacultadId != facultad.FacultadId);
                if (Existe)
                {
                    ModelState.AddModelError("Codigo", "El codigo ya existe");

                    return View(facultad);
                }

                try
                {
                    _context.Update(facultad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultadExists(facultad.FacultadId))
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
            return View(facultad);
        }

        // GET: Facultad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultad = await _context.Facultades
                .FirstOrDefaultAsync(m => m.FacultadId == id);
            if (facultad == null)
            {
                return NotFound();
            }

            return View(facultad);
        }

        public async Task<IActionResult> Estado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultad = await _context.Facultades
                .FirstOrDefaultAsync(m => m.FacultadId == id);
            if (facultad == null)
            {
                return NotFound();
            }

            return View(facultad);
        }

        // POST: Facultad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facultad = await _context.Facultades.FindAsync(id);

            facultad.EstadoId = "I";

            _context.Facultades.Update(facultad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ActionName("ConfirmarEstado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEstado(int id)
        {
            var facultad = await _context.Facultades.FindAsync(id);

            if (facultad.EstadoId == "I")
            {
                facultad.EstadoId = "A";
            }
            else
            {
                facultad.EstadoId = "A";
            }

            _context.Facultades.Update(facultad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultadExists(int id)
        {
            return _context.Facultades.Any(e => e.FacultadId == id);
        }
    }
}
