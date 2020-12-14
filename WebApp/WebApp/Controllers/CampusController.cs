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
    public class CampusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CampusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Campus
        public async Task<IActionResult> Index()
        {
            //await _context.Campus.Where(c => c.EstadoId != "I")
            return View();
        }

        // GET: Campus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campus
                .FirstOrDefaultAsync(m => m.CampusId == id);
            if (campus == null)
            {
                return NotFound();
            }

            return View(campus);
        }

        // GET: Campus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Campus/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CampusId,CampusCodigo,CampusNombre,Localidad,EstadoID")] Campus campus)
        {
            if (ModelState.IsValid)
            {
               // campus.EstadoId = "A";
                _context.Add(campus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campus);
        }

        // GET: Campus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campus.FindAsync(id);
            if (campus == null)
            {
                return NotFound();
            }
            return View(campus);
        }

        // POST: Campus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CampusId,CampusCodigo,CampusNombre,Localidad,EstadoID")] Campus campus)
        {
            if (id != campus.CampusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampusExists(campus.CampusId))
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
            return View(campus);
        }

        // GET: Campus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campus
                .FirstOrDefaultAsync(m => m.CampusId == id);
            if (campus == null)
            {
                return NotFound();
            }

            return View(campus);
        }

        // POST: Campus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campus = await _context.Campus.FindAsync(id);
            campus.EstadoId = "I";
            _context.Campus.Update(campus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Campus/Inactivate/5
        public async Task<IActionResult> Inactivate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campus
                .FirstOrDefaultAsync(m => m.CampusId == id);
            if (campus == null)
            {
                return NotFound();
            }

            return View(campus);
        }

        // POST: Campus/Inactivate/5
        [HttpPost, ActionName("Inactivate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InactivateConfirmed(int id)
        {
            var campus = await _context.Campus.FindAsync(id);
            campus.EstadoId = "I";
            _context.Campus.Update(campus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Campus/Activate/5
        public async Task<IActionResult> Activate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campus
                .FirstOrDefaultAsync(m => m.CampusId == id);
            if (campus == null)
            {
                return NotFound();
            }

            return View(campus);
        }

        // POST: Campus/Inactivate/5
        [HttpPost, ActionName("Activate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateConfirmed(int id)
        {
            var campus = await _context.Campus.FindAsync(id);
            campus.EstadoId = "A";
            _context.Campus.Update(campus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampusExists(int id)
        {
            return _context.Campus.Any(e => e.CampusId == id);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckExistingCode (string codigo, int id)
        {
            bool existsCode = false;

            if (id == 0)
                existsCode = _context.Campus.Any(c => c.CampusCodigo == codigo);
            else
                existsCode = _context.Campus.Any(c => c.CampusCodigo == codigo && c.CampusId != id);
           
            if (existsCode)
                return Json("Ya existe un Campus con este codigo");

            return Json(true);
        }
    }
}
