using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BIO_Prueba_Tecnica.Models;

namespace BIO_Prueba_Tecnica.Interfaces
{
    interface IMantenimientoUsuario
    {
        bool ValidarUsuario(Login DatosLogin);
        bool CrearUsua(Usuarios DatosUsuario);
    }
}
