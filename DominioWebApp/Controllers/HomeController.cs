using Dominio.Exceptions;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosUsuario;
using DominioWebApp.Filters;
using DominioWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DominioWebApp.Controllers;

public class HomeController : Controller
{
    private ILogin _loginCU;
    public HomeController(ILogin loginCU)
    {
        _loginCU = loginCU;
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
        try
        {
            UsuarioDTO logueado = _loginCU.Login(email, contra);
            HttpContext.Session.SetInt32("usuarioId", logueado.Id);
            HttpContext.Session.SetString("usuarioRol", logueado.Rol.ToString());
            return RedirectToAction("Index");
        }
        catch (UsuarioException ue)
        {
            ViewBag.Error = ue.Message;
            return View();
        }
        catch (Exception e)
        {
            ViewBag.Error = "Error inesperado.";
            return View();
        }
    }
    
}
