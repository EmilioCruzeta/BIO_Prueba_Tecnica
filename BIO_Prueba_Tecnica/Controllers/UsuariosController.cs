using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using BIO_Prueba_Tecnica.Models;

namespace BIO_Prueba_Tecnica.Controllers
{
    public class UsuariosController : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login DatosLogin)
        {

            try
            {

            if (ModelState.IsValid)
            {
                MantenimientoUsuario ValidarUsua = new MantenimientoUsuario();

               
                if (ValidarUsua.ValidarUsuario(DatosLogin) )
                {
                        TempData["clave"] = DatosLogin.usuario_Clave;
                        TempData["usuario"] = DatosLogin.usuario_Nombre;


                    return RedirectToAction("EstadoCuenta");
                }
                else
                {
                    ViewBag.login = "El usuario o la clave son incorrectos.";
                    return View(DatosLogin);
                }
               
               
            }

            else
            {
                return View(DatosLogin);
            }
           } catch(Exception ex)
            {
                throw  new Exception("Error en Login",ex);
            }

        }
        [HttpGet]
        public IActionResult CrearUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearUsuario(Usuarios DatosUsuarios)
        {
            try
            {
                ViewBag.Creado = 0;
                if (ModelState.IsValid)
                {
                    MantenimientoUsuario mCrearUsuario = new MantenimientoUsuario();

                    if (mCrearUsuario.CrearUsua(DatosUsuarios) )
                    {
                        ViewBag.Creado = 1;
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return View(DatosUsuarios);
                    }
                   

               
                }
                else
                {
                    return View(DatosUsuarios);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error Creando Usuario", ex);
            }
           
        }
        [HttpGet]
        public IActionResult RecuperarCuenta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarCuenta( CuentaEmail DatosCuentaEmail)
        {
            try
            {
                ViewBag.InvalidoMail = "";
                if (ModelState.IsValid)
                {
                    MantenimientoUsuario RecuperarDatos = new MantenimientoUsuario();
                    if (RecuperarDatos.RecuperarCredenciales(DatosCuentaEmail.Emial))
                    {
                        return RedirectToAction("Login");
                    }else
                    {
                        ViewBag.InvalidoMail = "Mail Inválido";
                        return View(DatosCuentaEmail);
                    }
                   
                }
                else
                {
                    return View(DatosCuentaEmail);
                }
               
            }
            catch(Exception ex)
            {
                throw new Exception("Error Recuperando Cuenta", ex);
            }
        }

        [HttpGet]
        public IActionResult EstadoCuenta()
        {
            try
            {

                MantenimientoUsuario mCuentas = new MantenimientoUsuario();

                return View(mCuentas.LlenarEstadoCuenta()) ;
            }
            catch(Exception ex)
            {
                throw new Exception("Error Recuperando Cuenta", ex);
            }
            
        }

        [HttpGet]
        public IActionResult EstadoCuentaDetalle(int id, string titular, DateTime fecha)
        {
            try
            {

                ViewBag.titulo = titular;
                ViewBag.fecha = fecha.ToString("d");
                ViewBag.id = id;
                


               MantenimientoUsuario mCuentas = new MantenimientoUsuario();

                return View(mCuentas.LlenarEstadoCuentaDetalle(id));

            }
            catch (Exception ex)
            {
                throw new Exception("Error en estado de cuenta detalle", ex);
            }

        }
       
        public IActionResult EnviarEstadoCuenta(string IdCuenta)
        {
            try
             {
               MantenimientoUsuario Envio = new MantenimientoUsuario();
            string clave = (string)TempData["clave"];
            string usuario =  (string)TempData["usuario"];
            Envio.EnviarEstadoCuenta(IdCuenta,clave,usuario);
            return RedirectToAction("EstadoCuenta");
            }
            catch(Exception ex)
            {
                throw new Exception("Error enviando estado de cuenta", ex);
            }
           

        }


    }
}
