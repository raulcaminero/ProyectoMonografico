﻿using System;
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
	public class AccountController : BaseController
	{
		private readonly ApplicationDbContext _context;

		public AccountController(ApplicationDbContext context) : base(context)
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
				// Determinar el estado del usuario.
				// Si es un Estudiante queda como "Activo" (A).
				// Si no queda como "En proceso" (P)

				var rolEstudiante = _context.Rol.FirstOrDefault(r => r.Descripcion == "Estudiante");
				if (rolEstudiante == null)
					throw new Exception("No se encontró el rol 'Estudiante' en la base de datos.");

				var estadoId = "A";
				if (rolEstudiante.Id != RolID)
					estadoId = "P"; // En proceso, requiere autorizacion del administrador.

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
					EstadoId = estadoId
				};

				_context.Add(usuario);
				_context.SaveChanges();

				// Enviar correo de bienvenida.
				new Helper.EmailSender().Send(email,
					titulo: "Bienvenido a  Culminare",
					cuerpo: $"Te damos la Bienvenida a Culminare, {primer_nombre}.\n\n" +
					$"Ya puedes acceder a través de este enlace: https://culminare.azurewebsites.net/");
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

			loadReqs();

			return View();
		}
		[HttpPost]
		public async Task<ActionResult> Login(string email, string password)
		{
			// var usuario = _context.usuarios1.SingleOrDefault(x => x.nombre == nombre);
			var usr = await _context.usuarios
				.Where(u => u.Email == email && u.EstadoId != "E")
				.Include(u => u.Rol)
				.FirstOrDefaultAsync();

			//var usuario = _context.usuarios1.SingleOrDefault(x => x.nombre == nombre);
			if (usr == null)
			{
				ViewBag.Login = "El usuario indicado no existe";
				loadReqs();
				return View();
			}
			else if (usr.EstadoId == "P")
			{
				ViewBag.Login = "Su usuario está siendo revisado";
				loadReqs();
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

				//GeneralPurpose.Ruta = usr.RutaFoto == null? "avatar.jpg" : usr.RutaFoto;

				return RedirectToAction("Index", "Home");
			}


			ViewBag.Login = "Contraseña incorrecta";
			loadReqs();
			return View();
		}

		[Microsoft.AspNetCore.Authorization.Authorize]
		public async Task<ActionResult> LogOff()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<ActionResult> ForgotPassword(string email)
		{
			var usr = await _context.usuarios.Where(u => u.Email == email).FirstOrDefaultAsync();
			if (usr != null)
			{
				var pass = generatePassword();
				new Helper.EmailSender().Send(email, titulo: "Contraseña temporal", cuerpo: $"Hola, {usr.primer_nombre}.\n\nTemporalmente podrás acceder con la contraseña siguiente:\n\n{pass}" +
					$"\n\nTe recomendamos que una vez ingreses vayas a tu perfil y cambies tu contraseña." +
					$"\n\nhttps://culminare.azurewebsites.net/Perfiles/Edit");

				usr.contrasena = pass;
				await _context.SaveChangesAsync();
			}

			return RedirectToAction(nameof(PasswordSent));
		}

		private string generatePassword()
		{
			Random random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

			string pass = new string(Enumerable.Repeat(chars, 10)
						  .Select(s => s[random.Next(s.Length)])
						  .ToArray());
			return pass;
		}

		public ActionResult PasswordSent()
		{
			return View();
		}

		public static Usuario GetCurrentUser(IPrincipal user, ApplicationDbContext context)
		{
			var usr = user as ClaimsPrincipal;
			var email = usr.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var currentUser = context.usuarios
				.Where(u => u.EstadoId != "I")
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

		[HttpPost]
		public async Task<IActionResult> Modal(string serviceType, string school)
		{
			if (serviceType != null && school != null)
			{
				int tipoServicioId = int.Parse(serviceType);
				int escuelaId = int.Parse(school);

				var requerimiento = await _context.Requerimientos
					.Where(r => r.Estado == EstadoRequerimiento.Activo && r.TipoServicioId == tipoServicioId && r.EscuelaId == escuelaId)
					.FirstOrDefaultAsync();

				var archivosController = new ArchivosController(_context);

				if (requerimiento != null)
					return await archivosController.Descargar(requerimiento.ArchivoId);

				ViewBag.ErrorMessage = "No existen requerimientos para esta escuela";
				loadReqs();
				return View("Login");
			}

			return RedirectToAction("Login");
		}

		private void loadReqs()
		{
			ViewBag.ServiceTypes = _context.TipoServicios.ToList();

			var schools = _context.Escuelas.ToList();
			ViewBag.Schools = new SelectList(schools, "Id", "Nombre");
		}
	}
}
