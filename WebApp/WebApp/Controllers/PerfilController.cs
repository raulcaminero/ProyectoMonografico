using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Data;
using WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using WebApp.ViewModels.perfil;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebApp.Controllers
{
    public class PerfilController : Controller
    {
        private readonly ApplicationDbContext _context;

        // private readonly UserManager<Usuario> _userManager;
        // private readonly IUserService _userService;

        private readonly IHostingEnvironment _env;

        public PerfilController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _env = environment;

        }

        // GET: Perfil
        public async Task<IActionResult> Index()
        {
            return View(await _context.usuarios.ToListAsync());
        }

        // GET: Perfil/Details/5
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

        // GET: Perfil/Create
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

        // GET: Perfil/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<SelectListItem> sexo = new List<SelectListItem>();
            sexo.Add(new SelectListItem { Text = "Masculino", Value = "0" });
            sexo.Add(new SelectListItem { Text = "Femenino", Value = "1" });

            List<SelectListItem> nacionalidad = new List<SelectListItem>();
            nacionalidad.Add(new SelectListItem { Text = "Dominicano", Value = "0" });
            nacionalidad.Add(new SelectListItem { Text = "Extranjero", Value = "1" });

            List<SelectListItem> identificaTipo = new List<SelectListItem>();
            identificaTipo.Add(new SelectListItem { Text = "Cédula", Value = "0" });
            identificaTipo.Add(new SelectListItem { Text = "Pasaporte", Value = "1" });

            List<SelectListItem> campus = new List<SelectListItem>();
            campus.Add(new SelectListItem { Text = "SEDE", Value = "0" });
            campus.Add(new SelectListItem { Text = "Santiago", Value = "1" });

            ViewBag.SelectListGenero = sexo;
            ViewBag.SelectListNacionaliad = nacionalidad;
            ViewBag.SelectListIdentificacionTipo = identificaTipo;
            ViewBag.SelectListCampus = campus;

            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            if (usuario.sexo == "M")
            {
                ViewBag.SelectSexo = "Masculino";
            }
            else
            {
                ViewBag.SelectSexo = "Femenino";
            }
            if (usuario.identificacion == "C")
            {
                ViewBag.identificacion = "Cédula";
            }
            else
            {
                ViewBag.identificacion = "Pasaporte";
            }
/*             if (usuario.Campus == 1)//Cambiar usuario.campus por usuario.campus.id <--- ERROR AQUÍ, COMENTAR
            {
                ViewBag.campus = "SEDE";
            } 
            else
            {
                ViewBag.campus = "Santiago";
            }*/
            if (usuario.identificacion == "C")
            {
                ViewBag.nacionalidad = "Dominicano";
            }
            else
            {
                ViewBag.nacionalidad = "Extranjero";
            }
            return View(usuario);
        }

        // POST: Perfil/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("codigo,primer_nombre,Email,contrasena,rol,segundo_nombre,primer_apellido,segundo_apellido,tipo_identificacion,identificacion,sexo,matricula,campus")] Models.Usuario usuario)
        {
            List<SelectListItem> sexo = new List<SelectListItem>();
            sexo.Add(new SelectListItem { Text = "Masculino", Value = "M" });
            sexo.Add(new SelectListItem { Text = "Femenino", Value = "F" });
            var genero = sexo;
            // var usuSexo = _context.usuarios.Where(x => x.sexo = genero);

            if (id != usuario.codigo)
            {
                return NotFound();
            }

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
            return View(usuario);
        }

        // GET: Perfil/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Perfil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.usuarios.FindAsync(id);
            _context.usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]

        [Route("Perfil/CambiarClave")]
        public async Task<IActionResult> CambiarClave(VM_CambarClave model)
        {
           
            var users = await _context.usuarios.FindAsync(model.codigo);
            if (model.codigo == 0 || model.codigo == null)
            {
                return NotFound();
            }
                       
            
            if (users.contrasena != model.old_pass)
            {
                
                TempData["Msg_Error_pass"] = "Su clave anterior no es Valida";
                return RedirectToAction("Edit", "Perfil", new { @id = users.codigo });
            }
            else
            {
                users.contrasena = model.new_pass;
                _context.usuarios.Update(users);
                await _context.SaveChangesAsync();
                TempData["Msg_Success"] = "Contraseña Cambiada";
                
                return RedirectToAction("Edit", "Perfil", new { @id = users.codigo });
            }
            
            return RedirectToAction("Edit","Perfil", new { @id=users.codigo});

        }
  /*       public async Task<IActionResult> CargarImagen(VM_CargarImagen img)
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

/*             users.RutaFoto = guidImagen; */
         /*    _context.usuarios.Update(users);
            await _context.SaveChangesAsync();

            TempData["Msg_Success_img"] = "Imagen Cambiada";
            return RedirectToAction("Edit", "Perfil", new { @id = users.codigo }); */

       /*  } */ 

        private bool UsuarioExists(int id)
        {
            return _context.usuarios.Any(e => e.codigo == id);
        }
    }
}
