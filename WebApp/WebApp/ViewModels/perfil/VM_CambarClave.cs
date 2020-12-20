using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.perfil
{
    public class VM_CambarClave
    {
                public int codigo { get; set; }
                [Required]
        public string old_pass { get; set; }
                [Required]
                public string  new_pass { get; set; }
                
               public string confirm_pass { get; set; }
    }
}
