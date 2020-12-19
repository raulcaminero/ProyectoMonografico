using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;

namespace WebApp.Helper
{
    public class SessionHelper
    {
        public static string GetValue(IPrincipal User, string property)
        {
            var r = ((ClaimsIdentity)User.Identity).FindFirst(property);
            return r == null ? "" : r.Value;
        }
        public static string GetNameIdentifier(IPrincipal User)
        {
            var r = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            return r == null ? "" : r.Value;
        }
    }
}
