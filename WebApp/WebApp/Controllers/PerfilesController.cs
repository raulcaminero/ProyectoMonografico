using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.ViewModels.perfil;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebApp.Controllers
{
	[Microsoft.AspNetCore.Authorization.Authorize]
	public class PerfilesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        // private readonly UserManager<Usuario> _userManager;
        // private readonly IUserService _userService;

        private readonly IHostingEnvironment _env;

        public PerfilesController(ApplicationDbContext context, IHostingEnvironment environment):base(context)
        {
            _context = context;
            _env = environment;
        }

        // GET: Perfiles
        public async Task<IActionResult> Index()
        {
            var usr = AccountController.GetCurrentUser(User, _context);

			var usuarios = await _context.usuarios
                .Where(u => u.EstadoId != "E")
                .Where(u => u.codigo != usr.codigo) // No mostrar al usuario su propio usuario
                .Include(u => u.Estado)
                .Include(u => u.Campus)
                .Include(u => u.Rol)
                .ToListAsync();

			return base.View(usuarios);
        }

        // GET: Perfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Perfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Perfil/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("codigo,primer_nombre,Email,contrasena,rol,segundo_nombre,primer_apellido,segundo_apellido,tipo_identificacion,identificacion,sexo,matricula,campus")] Models.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Perfiles/Edit/5
        public async Task<IActionResult> Edit()
        {
            cargarListas();

            var usuario = AccountController.GetCurrentUser(User, _context);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        private void cargarListas()
		{
            List<SelectListItem> sexo = new List<SelectListItem>();
            sexo.Add(new SelectListItem { Text = "Masculino", Value = "M" });
            sexo.Add(new SelectListItem { Text = "Femenino", Value = "F" });

            List<SelectListItem> nacionalidad = new List<SelectListItem>();
            nacionalidad.Add(new SelectListItem { Text = "Dominicano", Value = "Dominicano" });
            nacionalidad.Add(new SelectListItem { Text = "Extranjero", Value = "Extranjero" });

            List<SelectListItem> identificaTipo = new List<SelectListItem>();
            identificaTipo.Add(new SelectListItem { Text = "Cédula", Value = "C" });
            identificaTipo.Add(new SelectListItem { Text = "Pasaporte", Value = "P" });

            var campus = _context.Campus.ToList();
            var lstCampus = new SelectList(campus, "Id", "Nombre");

            ViewBag.SelectListGenero = sexo;
            ViewBag.SelectListNacionaliad = nacionalidad;
            ViewBag.SelectListIdentificacionTipo = identificaTipo;
            ViewBag.SelectListCampus = lstCampus;
        }

        // POST: Perfil/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Usuario usuario)
        {
            if (id != usuario.codigo)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.codigo))
                        return NotFound();

                    else throw;
                }
            }

            cargarListas();
            return View(usuario);
        }

        // GET: Perfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _context.usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.codigo == id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // POST: Perfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.usuarios.FindAsync(id);
            usuario.EstadoId = "I";
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Perfiles/Delete/5
        public async Task<IActionResult> Activate(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _context.usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.codigo == id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // POST: Perfiles/Delete/5
        [HttpPost, ActionName("Activate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateConfirmed(int id)
        {
            var usuario = await _context.usuarios.FindAsync(id);
            usuario.EstadoId = "A";
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Perfiles/Delete/5
        public async Task<IActionResult> Authorize(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _context.usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.codigo == id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // POST: Perfiles/Delete/5
        [HttpPost, ActionName("Authorize")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AuthorizeConfirmed(int id)
        {
            var usuario = await _context.usuarios.FindAsync(id);
            usuario.EstadoId = "A";

            var current = AccountController.GetCurrentUser(User, _context);
            var autorizacion = new Autorizacion()
            {
                IdUsuarioAutorizado = usuario.codigo,
                IdUsuarioQueAutoriza = current.codigo,
                Fecha = DateTime.Now
            };
            _context.Autorizaciones.Add(autorizacion);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [Route("Perfiles/CambiarClave")]
        public async Task<IActionResult> CambiarClave(VM_CambarClave model)
        {
           
            var users = await _context.usuarios.FindAsync(model.codigo);
            if (model.codigo == 0)
                return NotFound();
            
            if (users.contrasena != model.old_pass)
            {
                TempData["Msg_Error_pass"] = "Su clave anterior no es valida";
                return RedirectToAction("Edit", "Perfiles", new { @id = users.codigo });
            }
            else
            {
                users.contrasena = model.new_pass;
                _context.usuarios.Update(users);
                await _context.SaveChangesAsync();
                TempData["Msg_Success"] = "Contraseña cambiada";
                
                return RedirectToAction("Edit", "Perfiles", new { @id = users.codigo });
            }
            
            return RedirectToAction("Edit","Perfiles", new { @id=users.codigo});
        }
        public async Task<IActionResult> CargarImagen(VM_CargarImagen img)
        {
            var db = new ApplicationDbContext();
            var users = await _context.usuarios.FindAsync(img.Codigo);
            string guidImagen = null;
            if (img.Foto != null)
            {
                string FotoFichero = Path.Combine(_env.WebRootPath, "imagenes");
                guidImagen = Guid.NewGuid().ToString() + img.Foto.FileName;
                string Ruta = Path.Combine(FotoFichero, guidImagen);
                img.Foto.CopyTo(new FileStream(Ruta, FileMode.Create));
                
            }

            users.RutaFoto = guidImagen;
            _context.usuarios.Update(users);
            await _context.SaveChangesAsync();
            
            TempData["Msg_Success_img"] = "Imagen Cambiada";
            return RedirectToAction("Edit", "Perfiles", new { @id = users.codigo });
        }

        private bool UsuarioExists(int id)
        {
            return _context.usuarios.Any(e => e.codigo == id);
        }
    }
}
