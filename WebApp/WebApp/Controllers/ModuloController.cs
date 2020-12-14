using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ramontesis.Dto;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ModuloController : Controller
    {
        private readonly MyDB _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ModuloController(MyDB context, IWebHostEnvironment _webHostEnvironment)
        {
            _context = context;
            webHostEnvironment = _webHostEnvironment;
        }

        // GET: Modulo
        public async Task<IActionResult> Index()
        {
            var cULMINARE_DBContext = _context.Modulo
                .Include(m => m.Estado)
                .Include(m => m.IdProfesorNavigation)
                .Include(m=>m.IdAdjuntoNavigation);
            return View(await cULMINARE_DBContext.ToListAsync());
        }

        // GET: Modulo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //(m => m.Tema).ThenInclude( m => m.Asignacion) .ThenInclude
            var modulo = await _context.Modulo.Include(m => m.Estado)
                .Include(m => m.IdProfesorNavigation)
                .Include(m => m.IdAdjuntoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modulo == null)
            {
                return NotFound();
            }

            return View(modulo);
        }

        public IActionResult Create()
        {
            
            ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "EstadoNombre");
            ViewData["IdProfesor"] = new SelectList(_context.Profesor, "Id", "Nombre");
            ViewData["IdAdjunto"] = new SelectList(_context.AdjuntoMaterial, "Id", "Descripcion");
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Servicio_Id", "Servicio_Descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModuloDto model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                
                Modulo modulo = new Modulo
                {
                    Titulo = model.Titulo,
                    Descripcion = model.Descripcion,
                    FechaInicio = model.FechaInicio,
                    FechaFin = model.FechaFin,
                    IdProfesor = model.IdProfesor,
                    Imagen = uniqueFileName,
                    EstadoId = model.EstadoId,
                    IdAdjunto = model.IdAdjunto,
                    ServicioId = model.ServicioId
                };

                _context.Add(modulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAdjunto"] = new SelectList(_context.AdjuntoMaterial, "Id", "Descripcion");
            ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "EstadoNombre", 
                model.Estado.EstadoNombre);
            ViewData["IdProfesor"] = new SelectList(_context.Profesor, "Id", "Nombre", 
                 model.IdProfesorNavigation.Nombre);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Servicio_Id", "Servicio_Descripcion",
                model.Servicio.Servicio_Descripcion);
            return View();
        }

        private string UploadedFile(ModuloDto model)
        {
            string FileName = null;

            if (model.Imagen != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                FileName = Guid.NewGuid().ToString() + "_" + model.Imagen.FileName;
                string filePath = Path.Combine(uploadsFolder, FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Imagen.CopyTo(fileStream);
                }
            }
            return FileName;
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modulo = await _context.Modulo.FindAsync(id);
            if (modulo == null)
            {
                return NotFound();
            }
            ViewData["IdAdjunto"] = new SelectList(_context.AdjuntoMaterial, "Id", "Descripcion", modulo.IdAdjunto);
            ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "EstadoNombre", modulo.EstadoId);
            ViewData["IdProfesor"] = new SelectList(_context.Profesor, "Id", "Nombre", modulo.IdProfesor);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Servicio_Id", "Servicio_Descripcion", modulo.ServicioId);
            return View(modulo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,FechaInicio,FechaFin,IdProfesor,Imagen,EstadoId,IdAdjunto,ServicioId")] Modulo modulo)
        {
            if (id != modulo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuloExists(modulo.Id))
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
            ViewData["IdAdjunto"] = new SelectList(_context.AdjuntoMaterial, "Id", "Descripcion", modulo.IdAdjunto);
            ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "EstadoNombre", modulo.EstadoId);
            ViewData["IdProfesor"] = new SelectList(_context.Profesor, "Id", "Nombre", modulo.IdProfesor);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Servicio_Id", "Servicio_Descripcion", modulo.ServicioId);
            return View(modulo);
        }

        // GET: Modulo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modulo = await _context.Modulo
                .Include(m => m.Estado)
                .Include(m => m.IdProfesorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modulo == null)
            {
                return NotFound();
            }

            return View(modulo);
        }

        // POST: Modulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modulo = await _context.Modulo.FindAsync(id);
            if(modulo.Imagen != null)
			{
                var imagePath = Path.Combine(webHostEnvironment.WebRootPath, "images", modulo.Imagen);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.Modulo.Remove(modulo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuloExists(int id)
        {
            return _context.Modulo.Any(e => e.Id == id);
        }
    }
}
