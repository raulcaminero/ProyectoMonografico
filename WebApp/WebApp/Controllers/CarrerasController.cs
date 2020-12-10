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
    public class CarrerasController : Controller
    {
        private readonly MyDB _context;

        public CarrerasController(MyDB context)
        {
            _context = context;
        }

        // GET: Carreras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Carreras.ToListAsync());
        }

        // GET: Carreras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carreras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrera == null)
            {
                return NotFound();
            }

            return View(carrera);
        }

        // GET: Carreras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carreras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,idEscuela,Nombre,Detalles,Estado")] Carrera carrera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carrera);
        }

        // GET: Carreras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carreras.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }
            List<Escuelas> escuelas = new List<Escuelas>();
            escuelas.Add(new Escuelas { Id = 1, Nombre = "Ciencias" });
            escuelas.Add(new Escuelas { Id = 2, Nombre = "Economía" });
            escuelas.Add(new Escuelas { Id = 3, Nombre = "Humanidades" });

            VM_CreateCarrera vm = new VM_CreateCarrera
            {
                Carrera = carrera,
                Escuelas = escuelas
            };
            return View(vm);
        }

        public async Task<IActionResult> Activate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carreras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrera == null)
            {
                return NotFound();
            }

            return View(carrera);
        }

        public async Task<IActionResult> Inactivate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carreras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrera == null)
            {
                return NotFound();
            }

            return View(carrera);
        }

        // POST: Campus/Inactivate/5
        [HttpPost, ActionName("Inactivate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InactivateConfirmed(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            carrera.Estado = 0;
            _context.Carreras.Update(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Campus/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            carrera.Estado = (Estados)(-1);
            _context.Carreras.Update(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Activate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateConfirmed(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            carrera.Estado = (Estados)1;
            _context.Carreras.Update(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Carreras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,idEscuela,Nombre,Detalles,Estado")] Carrera carrera)
        {
            if (id != carrera.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarreraExists(carrera.Id))
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
            return View(carrera);
        }


        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckExisting_Code(Carrera carrera, int id)
        {
            carrera.Nombre = carrera.Nombre == null ? "" : carrera.Nombre;
            bool existsCode = false;

            if (id == 0)
                existsCode = _context.Carreras.Any(c => c.Codigo == carrera.Codigo);
            else
                existsCode = _context.Carreras.Any(c => c.Codigo == carrera.Codigo && c.Id != carrera.Id);

            if (existsCode)
                return Json("Ya existe una Carrera con este codigo");

            return Json(true);
        }


        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckExisting_Name(Carrera carrera, int id)
        {
            carrera.Nombre = carrera.Nombre == null ? "" : carrera.Nombre;
            bool existsCode = false;

            if (carrera.Id == 0)
                existsCode = _context.Carreras.Any(c => c.Nombre.ToLower().Equals(carrera.Nombre.ToLower()));
            else
                existsCode = _context.Carreras.Any(c => c.Id != carrera.Id && c.Nombre.ToLower().Equals(carrera.Nombre.ToLower()));

            if (existsCode)
                return Json("Ya existe una Carrera con este nombre");

            return Json(true);
        }

        // GET: Carreras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Carreras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrera == null)
            {
                return NotFound();
            }

            return View(carrera);
        }

        // POST: Carreras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrera = await _context.Carrera.FindAsync(id);
            _context.Carrera.Remove(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarreraExists(int id)
        {
            return _context.Carreras.Any(e => e.Id == id);
        }
    }
}
