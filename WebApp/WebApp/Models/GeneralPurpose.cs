using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class GeneralPurpose
    {
        private readonly ApplicationDbContext _context;
        public GeneralPurpose() { 
        
        }

        public string GetUserPicture(string usr)
        {

            var fotos = _context.usuarios.FirstOrDefault(x=> x.Email == usr);
            if (fotos == null)
            {
                return "";
            }
            else
            {
            return fotos.RutaFoto;

            }
        }
    }
}
