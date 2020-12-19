using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

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
		public async Task<ActionResult> Registro()
		{
			ViewBag.Roles = new SelectList(_context.Rol.ToList(), "Id", "Descripcion");
			ViewBag.Campus = new SelectList(_context.Campus.ToList(), "Id", "Nombre");

			return View();
		}
		[HttpPost]
		public async Task<ActionResult> Registro(string email,
												 string password,
												 int RolID,
												 string primer_nombre,
												 string segundo_nombre,
												 string primer_apellido,
												 string segundo_apellido,
												 string tipo_identificacion,
												 string identificacion,
												 string sexo,
												 string matricula,
												 int campus,
												 string EstadoId)
		{

			// consultado en la base de datos par ver si el correo existe

			var usuario = await _context.usuarios.FirstOrDefaultAsync(x => x.Email == email);
			if (usuario == null)
			{
				usuario = new Usuario()
				{
					Email = email,
					contrasena = password,
					RolID = RolID,
					primer_nombre = primer_nombre,
					segundo_nombre = segundo_nombre,
					primer_apellido = primer_apellido,
					segundo_apellido = segundo_apellido,
					tipo_identificacion = tipo_identificacion,
					identificacion = identificacion,
					sexo = sexo,
					matricula = matricula,
					IdCampus = campus,
					EstadoId = "A"
				};

				_context.Add(usuario);
				_context.SaveChanges();

				EnviarCorreo(email, primer_nombre);// para enviar correo

			}
			else
			{
				if (usuario.Email.Equals(email))
				{

					ViewBag.Message = "Ya existe un usuario con este correo, Por favor vuelva a intentarlo.";
					return View();
				}
			}

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
			var usr = await _context.usuarios
				.Where(u => u.Email == email)
				.Include(u => u.Rol)
				.FirstOrDefaultAsync();

			//var usuario = _context.usuarios1.SingleOrDefault(x => x.nombre == nombre);
			if (usr == null)
			{

				ViewBag.Login = "El usuario indicado no existe";
				return View();
			}
			if (usr.contrasena.Equals(password))
			{
				var claims = new[] {
					new Claim(ClaimTypes.NameIdentifier,email),
					new Claim(ClaimTypes.Name, usr.primer_nombre),
					new Claim(ClaimTypes.Role, usr.Rol.Descripcion)
				};
				
				var identity = new ClaimsIdentity(claims, "CookieAuth");
				var principal = new ClaimsPrincipal(identity);
				await HttpContext.SignInAsync("CookieAuth", principal);

				return RedirectToAction("Index", "Home");
			}


			ViewBag.Message = "Contraseña incorrecta";
			return View();
		}

		[Microsoft.AspNetCore.Authorization.Authorize]
		public async Task<ActionResult> LogOff()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		public string EnviarCorreo(string correo, string nombre)
		{
			var mensaje = "Cumplido";


			MailMessage Correo = new MailMessage();
			Correo.From = new MailAddress("culminare.v2@gmail.com");
			Correo.To.Add(correo);
			Correo.Subject = ("Bienvenido a  Culminare");
			Correo.Body = "Saludos, Te damos la Bienvenida a Culminare:" + "     " + nombre;
			Correo.Priority = MailPriority.Normal;

			SmtpClient ServerEmail = new SmtpClient();
			ServerEmail.Credentials = new NetworkCredential("culminare.v2@gmail.com", "UASD1538");
			ServerEmail.Host = "smtp.gmail.com";
			ServerEmail.Port = 587;
			ServerEmail.EnableSsl = true;
			try
			{
				ServerEmail.Send(Correo);
			}
			catch (Exception e)
			{
				Console.Write(e);
			}
			Correo.Dispose();
			return mensaje;
		}

		public static Usuario GetCurrentUser(IPrincipal user, ApplicationDbContext context)
		{
			var usr = user as ClaimsPrincipal;
			var email = usr.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var currentUser = context.usuarios
				.Include(u => u.Rol)
				.FirstOrDefault(u => u.Email == email);

			return currentUser;
		}

		public static bool GetUsuarioEsAdministrador(IPrincipal user, ApplicationDbContext context)
		{
			var usr = GetCurrentUser(user, context);
			var rol = usr.Rol?.Descripcion?.ToLower();
			var esAdmin = (rol == "administrador");
			return esAdmin;
		}
	}
}
