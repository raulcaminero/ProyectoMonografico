using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<ActionResult> registro()
        {
            ViewBag.Roles = new SelectList(_context.Rol.ToList(), "Id", "Descripcion");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> registro(string email,
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

            // consultado en la base de datos par ver si el corro existe

            var coreo = _context.usuarios.Where(x => x.Email == email).FirstOrDefault();
            if (coreo == null)
            {


                Models.Usuario usurios = new Usuario();

                usurios.Email = email;
                usurios.contrasena = password;
                usurios.RolID = RolID;


                usurios.primer_nombre = primer_nombre;
                usurios.segundo_nombre = segundo_nombre;
                usurios.primer_apellido = primer_apellido;
                usurios.segundo_apellido = segundo_apellido;
                usurios.tipo_identificacion = tipo_identificacion;
                usurios.identificacion = identificacion;
                usurios.sexo = sexo;
                usurios.matricula = matricula;
                usurios.campus = campus;
                usurios.EstadoId = "A";
                _context.Add(usurios);
                _context.SaveChanges();

                EnviarCorreo(email, primer_nombre);// para enviar correo

            }
            else
            {
                if (coreo.Email.Equals(email))
                {

                    ViewBag.Message = "Ya existe un Usuario con este Correo, Favor Vuelva a intentarlo";
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
            var c = _context.usuarios.Where(x => x.Email == email).FirstOrDefault();
            //var usuario = _context.usuarios1.SingleOrDefault(x => x.nombre == nombre);
            if (c == null)
            {

                ViewBag.Login = "Este Usuario no Existe";
                return View();
            }
            if (c.contrasena.Equals(password))
            {
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


            ViewBag.Message = "Passwor Incorecto";
            return View();
        }
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

	}
}
