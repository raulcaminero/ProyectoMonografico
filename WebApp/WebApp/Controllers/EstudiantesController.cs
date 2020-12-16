using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebApp.Models.Data;

namespace WebApp.Controllers
{

    [Authorize]
    public class EstudiantesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstudiantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.usuarios1.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios1
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
