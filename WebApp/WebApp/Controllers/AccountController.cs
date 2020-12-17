using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Data;

namespace WebApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly ApplicationDbContext _context;
		public AccountController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult> registro()
		{
			//var usr = User.Identity.Name;

			return View();
		}
		[HttpPost]
		public async Task<ActionResult> registro(string email,
												 string password,
												 int rol,
												 string primer_nombre,
												 string segundo_nombre,
												 string primer_apellido,
												 string segundo_apellido,
												 string tipo_identificacion,
												 string identificacion,
												 string sexo,
												 string matricula,
												 int campus)
		{
			var usurio = new Usuario();

			usurio.Email = email;
			usurio.contrasena = password;
			usurio.rol = rol;


			usurio.primer_nombre = primer_nombre;
			usurio.segundo_nombre = segundo_nombre;
			usurio.primer_apellido = primer_apellido;
			usurio.segundo_apellido = segundo_apellido;
			usurio.tipo_identificacion = tipo_identificacion;
			usurio.identificacion = identificacion;
			usurio.sexo = sexo;
			usurio.matricula = matricula;
			usurio.campus = campus;
			_context.Add(usurio);
			_context.SaveChanges();

			return RedirectToAction("Login");
		}
		// GET: AccountController
		[HttpGet]
		public ActionResult Login()
		{
			LogOff();
			return View();
		}
		[HttpPost]
		public async Task<ActionResult> Login(string email, string password)
		{
			// var usuario = _context.usuarios1.SingleOrDefault(x => x.nombre == nombre);
			var c = _context.usuarios1
				.Where(x => x.Email == email)
				.FirstOrDefault();

			//var usuario = _context.usuarios1.SingleOrDefault(x => x.nombre == nombre);
			if (c == null)
			{
				ViewBag.Message = "Este Usuario no Existe";
				return View();
			}

			var claims = new[] {
				new Claim(ClaimTypes.NameIdentifier,email),
				new Claim(ClaimTypes.Name, c.primer_nombre),
				new Claim(ClaimTypes.Email, c.Email)
			   };

			var identity = new ClaimsIdentity(claims, "CookieAuth");
			var principal = new ClaimsPrincipal(identity);
			await HttpContext.SignInAsync("CookieAuth", principal);

			bool u = HttpContext.User.Identity.IsAuthenticated;

			return RedirectToAction("Index", "Home");
		}


		public async Task<ActionResult> LogOff()
		{



			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}



	}
}
