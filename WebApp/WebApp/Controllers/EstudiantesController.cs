using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class EstudiantesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public EstudiantesController(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.usuarios.Include(x => x.Rol).ToListAsync());
        }

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

          /*  Random rnd = new Random();
            var p = rnd.Next();
            usuario.campus = int.Parse(p.ToString());
          */
            return View(usuario);
        }

    }
}
