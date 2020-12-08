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
using WebApp.Models;
using WebApp.Models.Data;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        MyDB _db;
        public AccountController( MyDB db)
        {
            _db = db;
        }

        // GET: AccountController
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login( Usuario usr  )
        {
            var result = await AutenticaUsr(usr);

            if (result)
            {
                return RedirectToAction("index", "home");
            }

            ViewBag.Error = "Credenciales incorrectas";

            return View(usr);
        }

        private async Task<bool> AutenticaUsr(Usuario usr)
        {
            if (!await ValidarUsr(usr)) return false;

            await CrearCookie(usr);
            return true;
        }
        private async Task<bool> ValidarUsr(Usuario usr)
        {
            /*
            if (usr.codigo == "carlos" && usr.contrasena == "123")
            {
                return true;
            }
            else
            {
                return false;
            }
            */

            var u = _db.Usuario.Where(x => x.codigo == usr.codigo).SingleOrDefault();
            if (u == null) return false;
            if (u.contrasena != usr.contrasena) return false;

            return true;

        }

        private async Task<bool> CrearCookie(Usuario usr)
        {
            var claims = new[]
                {
                    new Claim( ClaimTypes.Name, usr.codigo),
                    new Claim( ClaimTypes.NameIdentifier , usr.codigo)
                };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("CookieAuth", principal);

            bool u = HttpContext.User.Identity.IsAuthenticated;

            return true;
        }

    }
}
