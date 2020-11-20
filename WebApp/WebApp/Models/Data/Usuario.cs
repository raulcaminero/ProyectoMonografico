using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Data
{
    public class Usuario
    {

        [Key]
        public string codigo { get; set; }
        public string contrasena { get; set; }
    }
}
