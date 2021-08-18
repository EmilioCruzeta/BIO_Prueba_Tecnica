using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BIO_Prueba_Tecnica.Models
{
    public class Login
    {

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El nombre de usuario es requerida.")]
        public string usuario_Nombre { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Clave")]
        [Required(ErrorMessage = "La clave de usuario es requerida.")]
        public string usuario_Clave { get; set; }
    }
}
