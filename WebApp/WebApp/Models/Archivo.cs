using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
	public partial class Archivo
    {
        public int Id { get; set; }

        /// <summary>
        /// Indica el proceso o funcionalidad del sistema que guardar el archivo.
        /// Ejemplo: Requerimientos, Perfil, Ante-Proyecto, etc.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Modulo { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Ruta { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string NombreArchivo { get; set; }

        [Required]
        [MaxLength(25)]
        public string Extension{ get; set; }

    }
}
