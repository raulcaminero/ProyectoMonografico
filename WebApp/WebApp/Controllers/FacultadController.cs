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
    public class FacultadController : Controller
    {
        private readonly MyDB _context;

        public FacultadController(MyDB context)
        {
            _context = context;
        }

        // GET: Facultad
        public async Task<IActionResult> Index()
        {
            return View(await _context.Facultads.ToListAsync());
        }

        // GET: Facultad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultad = await _context.Facultads
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
            {
                return NotFound();
            }

            var facultad = await _context.Facultads.FindAsync(id);
            if (facultad == null)
            {
                return NotFound();
            }
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

            var facultad = await _context.Facultads
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
            var facultad = await _context.Facultads.FindAsync(id);
            _context.Facultads.Remove(facultad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultadExists(int id)
        {
            return _context.Facultads.Any(e => e.Id == id);
        }
    }
}
