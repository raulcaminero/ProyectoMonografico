using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Data
{
    public class Persona
    {
        [Key]
        public int Codigo { get; set; }
    }
}
