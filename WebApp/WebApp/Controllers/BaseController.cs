using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {

            if (User.Identity.IsAuthenticated)
            {
                var usr = User as ClaimsPrincipal;
                var email = usr.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var foto = _context.usuarios.FirstOrDefault(x => x.Email == email);
                if (string.IsNullOrWhiteSpace(foto?.RutaFoto))
                {
                    ViewBag.UserPicture = "avatar.jpg";
                }

                else
                {
                    ViewBag.UserPicture = foto.RutaFoto;
                }
            }

            else
            {
                ViewBag.UserPicture = "avatar.jpg";
            }

            base.OnActionExecuted(context);
        }
    }
}