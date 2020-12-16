using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using WebApp.Models.Data;
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
                .Where(c => c.Estado != Estados.Eliminado)
                .ToListAsync();
            /* List<Escuela> escuelas = new List<Escuela>();

             Escuela todaEscuela = new Escuela
             {
                 Id = 0,
                 Nombre = "Todas"
             };

             escuelas.Add(todaEscuela);

             escuelas.AddRange(_context.Escuelas
                 .Where(e => e.Estado == Estados.Activo)
                 .Include(e => e.Facultad)
                 .ToList());

             Facultad todas = new Facultad
             {
                 Id = 0,
                 NombreFacultad = "Todas"
             };

             List<Facultad> facultades = new List<Facultad>();
             facultades.Add(todas);

             facultades.AddRange(_context.Facultades.Where(x => x.Estado == Estados.Activo).ToList());

             VM_IndexCarrera vm = new VM_IndexCarrera
             {
                 Carreras = carreras,
                 Escuelas = escuelas,
                 Facultades = facultades
             };

             return base.View(vm);*/

            return View(carreras);
        }

        public async Task<IActionResult> _TablaCarreras(List<Carrera> carreras = null, int idFacultad = 0, int idEscuela = 0)
        {
            if (carreras == null)
            {
                carreras = await _context.Carreras.Include(x => x.Escuela.Facultad).ToListAsync();
            }
            else if (carreras.Count == 0)
            {
                carreras = await _context.Carreras.Include(x => x.Escuela.Facultad).ToListAsync();
            }

            if (idFacultad != 0)
            {
                carreras = carreras.Where(x => x.Escuela.Facultad.Id == idFacultad).ToList();
            }

            if (idEscuela != 0)
            {
                carreras = carreras.Where(x => x.IdEscuela == idEscuela).ToList();
            }
            return View(carreras);

        }

        // GET: Carreras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var carrera = await _context.Carreras
                .Where(c => c.Estado != Estados.Eliminado)
                .Include(c => c.Escuela.Facultad)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (carrera == null)
                return NotFound();

            return View(carrera);
        }

        // GET: Carreras/Create
        public IActionResult Create()
        {
            var escuelas = _context.Escuelas
                .Where(e => e.Estado == Estados.Activo)
                .Include(e => e.Facultad)
                .ToList();

            Facultad todas = new Facultad
            {
                Id = 0,
                NombreFacultad = "Todas"
            };

            List<Facultad> facultades = new List<Facultad>();
            facultades.Add(todas);

            facultades.AddRange(_context.Facultades.Where(x => x.Estado == Estados.Activo).ToList());

            VM_CreateCarrera vm = new VM_CreateCarrera
            {
                Escuelas = escuelas,
                Facultades = facultades
            };

            return View(vm);
        }

        // POST: Carreras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEscuela,Codigo,Nombre,Detalles")] Carrera carrera)
        //public async Task<IActionResult> Edit(int id, [Bind("Id,IdEscuela,Codigo,Nombre,Detalles,Estado")] Carrera carrera)
        {
            if (ModelState.IsValid)
            {
                carrera.Estado = (Estados)1;
                _context.Add(carrera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var escuelas = _context.Escuelas
                .Where(e => e.Estado == Estados.Activo)
                .Include(e => e.Facultad)
                .ToList();

            Facultad todas = new Facultad
            {
                Id = 0,
                NombreFacultad = "Todas"
            };

            List<Facultad> facultades = new List<Facultad>();
            facultades.Add(todas);

            facultades.AddRange(_context.Facultades.Where(x => x.Estado == Estados.Activo).ToList());

            VM_CreateCarrera vm = new VM_CreateCarrera
            {
                Escuelas = escuelas,
                Facultades = facultades
            };

            return View(vm);
        }

        // GET: Carreras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var carrera = await _context.Carreras.FindAsync(id);

            if (carrera == null)
                return NotFound();
            Facultad todas = new Facultad
            {
                Id = 0,
                NombreFacultad = "Todas"
            };
            List<Facultad> facultades = new List<Facultad>();
            facultades.Add(todas);
            facultades.AddRange(_context.Facultades.Where(e => e.Estado == Estados.Activo).ToList());
            var escuelas = _context.Escuelas.Where(e => e.Estado == Estados.Activo).ToList();
            VM_CreateCarrera vm = new VM_CreateCarrera
            {
                Carrera = carrera,
                Escuelas = escuelas,
                Facultades = facultades
            };
            return View(vm);
        }

        // POST: Carreras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdEscuela,Codigo,Nombre,Detalles,Estado")] Carrera carrera)
        {
            carrera.Estado = (Estados)1;
            if (id != carrera.Id)
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


        public async Task<IActionResult> Activate(int? id)
        {
            if (id == null)
                return NotFound();

            var carrera = await _context.Carreras.Include(x => x.Escuela.Facultad)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (carrera == null)
                return NotFound();

            return View(carrera);
        }

        [HttpPost, ActionName("Activate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateConfirmed(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            carrera.Estado = Estados.Activo;
            _context.Carreras.Update(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Inactivate(int? id)
        {
            if (id == null)
                return NotFound();

            var carrera = await _context.Carreras
                .Include(c => c.Escuela.Facultad)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (carrera == null)
                return NotFound();


            return View(carrera);
        }

        // POST: Campus/Inactivate/5
        [HttpPost, ActionName("Inactivate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InactivateConfirmed(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            carrera.Estado = Estados.Inactivo;
            _context.Carreras.Update(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckExisting_Code(Carrera carrera, int id)
        {
            carrera.Nombre = carrera.Nombre == null ? "" : carrera.Nombre;
            bool existsCode = false;

            if (carrera.Id == 0)
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
                return NotFound();

            var carrera = await _context.Carreras.Include(x => x.Escuela.Facultad)
                .FirstOrDefaultAsync(m => m.Id == id);

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
            carrera.Estado = Estados.Eliminado;
            _context.Carreras.Update(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool CarreraExists(int id)
        {
            return _context.Carreras.Any(e => e.Id == id);
        }

        //GetFilteredFactultades
        public async Task<List<Facultad>> GetFilteredFactultades(int idEscuela = 0, bool addEmpty = false)
        {
            List<Facultad> facultades = new List<Facultad>();

            if (addEmpty == true)
            {
                Facultad todas = new Facultad
                {
                    Id = 0,
                    NombreFacultad = "Todas"
                };
                facultades.Add(todas);
            }

            facultades.AddRange(await _context.Facultades.Where(x => x.Estado == (Estados)1).ToListAsync());

            var Facultad = _context.Escuelas.Include(x => x.Facultad).FirstOrDefault(x => x.Id == idEscuela);

            if (idEscuela > 0)
            {
                if (Facultad != null)
                {
                    facultades = facultades.Where(x => x.Id == Facultad.Facultad.Id || x.Id == 0).ToList();

                }
            }
            return facultades;
        }
        public async Task<List<Escuela>> GetFilteredEscuelas(int idFacultad = 0, bool addEmpty = false)
        {
            List<Escuela> escuelas = new List<Escuela>();

            if (addEmpty == true)
            {
                Escuela todas = new Escuela
                {
                    Id = 0,
                    Nombre = "Todas"
                };
                escuelas.Add(todas);
            }

            escuelas.AddRange(await _context.Escuelas.Where(x => x.Estado == (Estados)1).ToListAsync());

            if (idFacultad > 0)
            {
                escuelas = escuelas.Where(x => x.IdFacultad == idFacultad || x.Id == 0).ToList();
            }
            return escuelas;
        }
    }

    //public class TablaCarrerasViewComponent : ViewComponent
    //{

    //    public IViewComponentResult Invoke()
    //    {
    //        //List<Carrera> model = await  List<Carrera>();

    //        return View();
    //    }
    //}
}
