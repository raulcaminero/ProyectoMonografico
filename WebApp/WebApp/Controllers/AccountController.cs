using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Context;
using WebApp.Models;
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
            Models.Data.Usuario usurios = new Usuario();

            usurios.Email = email;
            usurios.contrasena = password;
            usurios.rol = rol;


            usurios.primer_nombre = primer_nombre;
            usurios.segundo_nombre = segundo_nombre;
            usurios.primer_apellido = primer_apellido;
            usurios.segundo_apellido = segundo_apellido;
            usurios.tipo_identificacion = tipo_identificacion;
            usurios.identificacion = identificacion;
            usurios.sexo = sexo;
            usurios.matricula = matricula;
            usurios.campus = campus;
            _context.Add(usurios);
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
            var c = _context.usuarios1.Where(x => x.Email == email).FirstOrDefault();

            //var usuario = _context.usuarios1.SingleOrDefault(x => x.nombre == nombre);
            if (c == null)
            {
                ViewBag.Message = "Este Usuario no Existe";
                return View();

            }


            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier,email),
                new Claim(ClaimTypes.Name, c.primer_nombre)
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
