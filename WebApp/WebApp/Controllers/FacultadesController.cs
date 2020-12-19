using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
//using WebApp.Models.Data;
using WebApp.Models.Enums;

namespace WebApp.Controllers
{
	[Microsoft.AspNetCore.Authorization.Authorize]
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
            var facultad = await _context.Facultades.Where(x => x.Estado != Estados.Eliminado).ToListAsync();
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
                .FirstOrDefaultAsync(m => m.Id == id);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreFacultad,Codigo,NombreDecano,Ubicación,Telefono,Estado")] Facultad facultad)
        {
            
            if (ModelState.IsValid)
            {
                bool Existe = _context.Facultades.Any(x => x.Codigo == facultad.Codigo);
                if (Existe)
                {
                    ModelState.AddModelError("Codigokey", "El codigo ya existe");

                    return View(facultad);
                }

                facultad.Estado = (Estados)1;
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreFacultad,Codigo,NombreDecano,Ubicación,Telefono,Estado")] Facultad facultad)
        {
            if (id != facultad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool Existe = _context.Facultades.Any(x => x.Codigo == facultad.Codigo && x.Id != facultad.Id);
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
                    if (!FacultadExists(facultad.Id))
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
                .FirstOrDefaultAsync(m => m.Id == id);
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
                .FirstOrDefaultAsync(m => m.Id == id);
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

            facultad.Estado = Estados.Eliminado;

            _context.Facultades.Update(facultad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ActionName("ConfirmarEstado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEstado(int id)
        {
            var facultad = await _context.Facultades.FindAsync(id);

            if (facultad.Estado == Estados.Activo)
            {
                facultad.Estado = Estados.Inactivo;
            }
            else
            {
                facultad.Estado = Estados.Activo;
            }

            _context.Facultades.Update(facultad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultadExists(int id)
        {
            return _context.Facultades.Any(e => e.Id == id);
        }
    }
}
