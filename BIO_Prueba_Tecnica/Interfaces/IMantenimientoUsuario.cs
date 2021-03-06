using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BIO_Prueba_Tecnica.Models;

namespace BIO_Prueba_Tecnica.Interfaces
{
    interface IMantenimientoUsuario
    {
        int ValidarUsuario(Login DatosLogin);
        bool CrearUsua(Usuarios DatosUsuario);
        List<EstadoCuenta> LlenarEstadoCuenta();
        List<EstadoCuenta> LlenarEstadoCuentaDetalle(int id);
        bool RecuperarCredenciales(string email);
        bool ValidarMail(string mail);
        void EnviarEstadoCuenta(string idCuenta, string clave, string usuario);
    }
}
