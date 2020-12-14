using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.ViewModels.Carrera;

namespace WebApp.Controllers
{
    public class CarrerasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarrerasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carreras
        public async Task<IActionResult> Index()
        {
			var carreras = await _context.Carreras
                .Include(c => c.Escuela.Facultad)
                .Where(c => c.EstadoId !="I" )
                .ToListAsync();

			return base.View(carreras);
        }

        // GET: Carreras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var carrera = await _context.Carreras
                .Where(c => c.EstadoId != "I")
                .Include(c => c.Escuela.Facultad)
                .FirstOrDefaultAsync(m => m.CarreraId == id);

            if (carrera == null)
                return NotFound();

            return View(carrera);
        }

        // GET: Carreras/Create
        public IActionResult Create()
        {
            /*var carreras = _context.Carreras
                .Where(e => e.EstadoId == "A")
                .Include(e => e.Facultad)
                .ToList();

            VM_CreateCarrera vm = new VM_CreateCarrera
            {
                Carrera = carreras
            };   vm
            */
            return View();
        }

        // POST: Carreras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create
            ([Bind("CarreraId,CarreraCodigo,CarreraNombre,EstadoId,CampusId,FacultadId, EscuelaId")] Carrera carrera)
        {
          

            if (ModelState.IsValid)
            {
                //carrera.EstadoId != "I";
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
                return NotFound();

            var carrera = await _context.Carreras.FindAsync(id);

            if (carrera == null)
                return NotFound();

            var escuelas = _context.Carreras.Where(e => e.EstadoId == "A");

            VM_CreateCarrera vm = new VM_CreateCarrera
            {
                Carrera = carrera
               // Escuelas = escuelas
            };
            return View(vm);
        }

        // POST: Carreras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("CarreraId,CarreraCodigo,CarreraNombre,EstadoId,CampusId,FacultadId, EscuelaId")] Carrera carrera)
        {
            if (id != carrera.CarreraId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarreraExists(carrera.CarreraId))
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


        public async Task<IActionResult> Activate(int? id)
        {
            if (id == null)
                return NotFound();

            var carrera = await _context.Carreras
                .FirstOrDefaultAsync(m => m.CarreraId == id);
            
            if (carrera == null)
                return NotFound();

            return View(carrera);
        }

        [HttpPost, ActionName("Activate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateConfirmed(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            carrera.EstadoId = "A";
            _context.Carreras.Update(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Inactivate(int? id)
		{
			if (id == null)
				return NotFound();

			var carrera = await _context.Carreras
                .Include(c => c.Escuela)
				.FirstOrDefaultAsync(c => c.CarreraId == id);
			
            if (carrera == null)
				return NotFound();
			
			VM_CreateCarrera vm = new VM_CreateCarrera
			{
				Carrera = carrera
				//Escuelas = new List<Escuela>() { carrera.Escuela }
			};
			return View(vm);
		}

        // POST: Campus/Inactivate/5
        [HttpPost, ActionName("Inactivate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InactivateConfirmed(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            carrera.EstadoId = "I";
            _context.Carreras.Update(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckExisting_Code(Carrera carrera, int id)
        {
            carrera.CarreraNombre = carrera.CarreraNombre == null ? "" : carrera.CarreraNombre;
            bool existsCode = false;

            if (id == 0)
                existsCode = _context.Carreras.Any(c => c.CarreraCodigo == carrera.CarreraCodigo);
            else
                existsCode = _context.Carreras.Any(c => c.CarreraCodigo == carrera.CarreraCodigo && c.CarreraId != carrera.CarreraId);

            if (existsCode)
                return Json("Ya existe una Carrera con este codigo");

            return Json(true);
        }


        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckExisting_Name(Carrera carrera, int id)
        {
            carrera.CarreraNombre = carrera.CarreraNombre == null ? "" : carrera.CarreraNombre;
            bool existsCode = false;

            if (carrera.CarreraId == 0)
                existsCode = _context.Carreras.Any(c => c.CarreraNombre.ToLower().Equals(carrera.CarreraNombre.ToLower()));
            else
                existsCode = _context.Carreras.Any(c => c.CarreraId != carrera.CarreraId && c.CarreraNombre.ToLower().Equals(carrera.CarreraNombre.ToLower()));

            if (existsCode)
                return Json("Ya existe una Carrera con este nombre");

            return Json(true);
        }


        // GET: Carreras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var carrera = await _context.Carreras
                .FirstOrDefaultAsync(m => m.CarreraId == id);

            if (carrera == null)
                return NotFound();

            return View(carrera);
        }

        // POST: Campus/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            carrera.EstadoId = "I";
            _context.Carreras.Update(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool CarreraExists(int id)
        {
            return _context.Carreras.Any(e => e.CarreraId == id);
        }
    }
}
