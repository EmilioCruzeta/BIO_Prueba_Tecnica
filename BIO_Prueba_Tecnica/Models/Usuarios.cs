using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BIO_Prueba_Tecnica.Models
{
    public class Usuarios
    {
        //public Usuarios()
        //{
        //    usuario_FechaMod = DateTime.Now;
        //}

        [Display(Name ="Usuario")]
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string usuario_Nombre { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Clave")]
        [MinLength(8, ErrorMessage = "La clave de usuario debe tener al menos 8 caracteres")]
        [Required(ErrorMessage = "La clave de usuario es requerido.")]
        public string usuario_Clave { get; set; }

        [Display(Name = "FechaCrea")]
        public DateTime usuario_FechaCrea { get; set; }

        [Display(Name = "FechaMod")]
        public DateTime usuario_FechaMod { get; set; }

        [Display(Name = "FechaNac")]
        [Required(ErrorMessage = "La fecha de nacimiento es requerido.")]
        public DateTime usuario_FechaNac { get; set; }

        [Display(Name ="Correo")]
        [EmailAddress(ErrorMessage = "Debe ingresar un mail válido.")]
        public string usuario_Email { get; set; }

        [Display(Name = "Genero")]
        [RegularExpression("[MmFf]", ErrorMessage = "Solo puede ingresar una M o F.")]
        public string usuario_Genero { get; set; }

        [Display(Name = "Edad")]
        [Range(18, 120, ErrorMessage = "Su edad debe de ser mayor o igual 18 años.")]
        public int usuario_Edad { get; set; }
    }
}
