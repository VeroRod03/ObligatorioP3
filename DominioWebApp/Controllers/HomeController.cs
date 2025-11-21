using DominioWebApp.DTOs;
using DominioWebApp.DTOs;
using DominioWebApp.Filters;
using DominioWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WebAppClienteHttp.Auxiliares;

namespace DominioWebApp.Controllers;

public class HomeController : Controller
{
    public string URLApiLogin { get; set; }

    public HomeController(IConfiguration config)
    {
        URLApiLogin = config.GetValue<string>("URLApiLogin");
    }
    [FilterAutenticado]
    public IActionResult Index(string mensaje)
    {
        ViewBag.Error = mensaje;
        return View();
    }
    public IActionResult Login(string mensaje)
    {
        ViewBag.Error = mensaje;
        return View();
    }
    
    [HttpPost]
    public IActionResult Login(string email, string contra)
    {
        UsuarioDTO usuario = null;
        try
        {
            UsuarioLoginDTO logueado = new UsuarioLoginDTO();
            logueado.Email = email;
            logueado.Contra = contra;
            
            HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiLogin, "POST", logueado);

            string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

            if (respuesta.IsSuccessStatusCode) // Serie 200
            {
                usuario = JsonConvert.DeserializeObject<UsuarioDTO>(body); // en el body hay JSON
                HttpContext.Session.SetString("usuarioRol", usuario.Rol);
                HttpContext.Session.SetInt32("usuarioId", usuario.Id);
                HttpContext.Session.SetString("token", usuario.Token);
                return RedirectToAction("Index");
            }
            else // Serie 400 o 500
            {
                //para poder ver solo el mensaje de error
                //var errorObj = JsonConvert.DeserializeObject<dynamic>(body);
                ViewBag.Error = body; // en el body vino el mensaje del error
                return View();
            }
        }
        catch (Exception ex)
        {
            ViewBag.Error = "Sucedio un error inesperado , " + ex.Message;
            return View();
        }
    }

    [FilterAutenticado]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

}
