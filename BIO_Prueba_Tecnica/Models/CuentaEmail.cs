using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BIO_Prueba_Tecnica.Models
{
    public class CuentaEmail
    {
        [Display(Name = "Emial")]
        [Required(ErrorMessage = "La email es requerido.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un email válido.")]
        public  string Emial { get; set; }
    }
}
