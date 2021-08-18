﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
                    return RedirectToAction("CrearUsuario");
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
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Login");
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
    }
}