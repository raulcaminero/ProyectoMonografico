using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.perfil
{
    public class VM_Perfil
    {
        public int codigo { set; get; }
        public string primer_nombre { set; get; }
        public string Email { set; get; }
        public int rol { get; set; }

         public string segundo_nombre { set; get; }
        public string primer_apellido { set; get; }
        public string segundo_apellido { set; get; }
        public string tipo_identificacion { set; get; }
        public string identificacion { set; get; }
        public string sexo { set; get; }
        public string matricula { set; get; }
        public int campus { set; get; }


        public string old_pass { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [DataType(DataType.Password)]
        public string new_pass { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [DataType(DataType.Password)]
        [Compare("Nueva Clave", ErrorMessage = "No coincide con su nueva clave")]
        public string confirm_pass { get; set; }
    }
}
