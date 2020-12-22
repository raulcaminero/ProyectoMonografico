using System;
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
	[Microsoft.AspNetCore.Authorization.Authorize]
    public class ModuloController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ModuloController(ApplicationDbContext context, IWebHostEnvironment _webHostEnvironment):base(context)
        {
            _context = context;
            webHostEnvironment = _webHostEnvironment;
        }

        // GET: Modulo
        public async Task<IActionResult> Index()
        {
            var usr = AccountController.GetCurrentUser(User, _context);
            var idEstudiante = usr.Rol?.Descripcion == "Estudiante" ? usr.codigo : 0;

            var modulos = _context.Modulo
                .Where(m => idEstudiante == 0 || m.Servicio.Solicitudes.Any(s => s.IdUsuario == idEstudiante)) // Si es un Estudiante, solo mostrar los modulos en los que está inscrito.
                .Include(m => m.Estado)
                .Include(m => m.IdProfesorNavigation);
            return View(await modulos.ToListAsync());
        }

        // GET: Modulo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var modulo = await _context.Modulo.Include(m => m.Estado)
                .Include(m => m.IdProfesorNavigation)
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
            ViewData["UsuarioCodigo"] = new SelectList(_context.usuarios.Where(x => x.Rol.Descripcion == "Administrador"), "codigo", "NombreCompleto");
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
                    UsuarioCodigo = model.UsuarioCodigo,
                    Imagen = uniqueFileName,
                    EstadoId = model.EstadoId,
                    ServicioId = model.ServicioId
                };

                _context.Add(modulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "EstadoNombre", 
                model.Estado.EstadoNombre);
            ViewData["UsuarioCodigo"] = new SelectList(_context.usuarios.Where(x => x.Rol.Descripcion == "Administrador"), "codigo", "NombreCompleto",
            model.IdProfesorNavigation.NombreCompleto);
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
            ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "EstadoNombre", modulo.EstadoId);
            ViewData["UsuarioCodigo"] = new SelectList(_context.usuarios.Where(x => x.Rol.Descripcion == "Administrador"), "codigo", "NombreCompleto", modulo.UsuarioCodigo);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Servicio_Id", "Servicio_Descripcion", modulo.ServicioId);
            return View(modulo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,FechaInicio,FechaFin,UsuarioCodigo,Imagen,EstadoId,ServicioId")] Modulo modulo)
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
            ViewData["EstadoId"] = new SelectList(_context.Estado, "EstadoId", "EstadoNombre", modulo.EstadoId);
            ViewData["UsuarioCodigo"] = new SelectList(_context.usuarios.Where(x => x.Rol.Descripcion == "Administrador"), "codigo", "NombreCompleto", modulo.UsuarioCodigo);
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
