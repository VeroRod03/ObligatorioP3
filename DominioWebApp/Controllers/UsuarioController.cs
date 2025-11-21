using DominioWebApp.DTOs;
using DominioWebApp.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppClienteHttp.Auxiliares;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DominioWebApp.Controllers
{
    [FilterAutenticado]
    public class UsuarioController : Controller
    {
        public string URLApiUsuarios { get; set; }
        public string URLApiEquipos { get; set; }

        public UsuarioController(IConfiguration config)
        {
            URLApiUsuarios = config.GetValue<string>("URLApiUsuarios");
            URLApiEquipos = config.GetValue<string>("URLApiEquipos");
        }
        // GET: UsuarioController
        [FilterGerente]
        public ActionResult Index(string mensaje)
        {
            ViewBag.Error = mensaje;
            IEnumerable<UsuarioDTO> usuarios = new List<UsuarioDTO>();
            try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiUsuarios, "GET", null, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    usuarios = JsonConvert.DeserializeObject<IEnumerable<UsuarioDTO>>(body);
                }
                else
                {
                    ViewBag.Error = body;
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado. Intente de nuevo más tarde.";
            }
            return View(usuarios);
        }

        [HttpPost]
        public ActionResult Index(double monto)
        {
            IEnumerable<UsuarioDTO> usuarios = new List<UsuarioDTO>();

            try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud($"{URLApiUsuarios}/UsuariosFiltrados?monto={monto}", "GET", null, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    usuarios = JsonConvert.DeserializeObject<IEnumerable<UsuarioDTO>>(body);
                }
                else
                {
                    ViewBag.Error = body;

                    HttpResponseMessage respuestaTodos = AuxiliarClienteHttp.EnviarSolicitud(URLApiUsuarios, "GET", null, token);
                    string bodyTodos = AuxiliarClienteHttp.ObtenerBody(respuestaTodos);

                    if (respuestaTodos.IsSuccessStatusCode)
                    {
                        usuarios = JsonConvert.DeserializeObject<IEnumerable<UsuarioDTO>>(bodyTodos);
                    }
                    else
                    {
                        ViewBag.Error = bodyTodos;
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado. Intente de nuevo más tarde.";
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuestaTodos = AuxiliarClienteHttp.EnviarSolicitud(URLApiUsuarios, "GET", null, token);
                string bodyTodos = AuxiliarClienteHttp.ObtenerBody(respuestaTodos);

                if (respuestaTodos.IsSuccessStatusCode)
                {
                    usuarios = JsonConvert.DeserializeObject<IEnumerable<UsuarioDTO>>(bodyTodos);
                }
                else
                {
                    ViewBag.Error = bodyTodos;
                }
            }
            return View(usuarios);
        }

        // GET: UsuarioController/Create
        [FilterGerenteAdmin]
        public ActionResult Create(string mensaje, string error)
        {
            ViewBag.Mensaje = mensaje;
            ViewBag.Error = error;

            try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiEquipos, "GET", null, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    ViewBag.Equipos = JsonConvert.DeserializeObject<IEnumerable<EquipoDTO>>(body);
                }
                else
                {
                    ViewBag.Error = body;
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado. Intente de nuevo más tarde.";
            }
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AltaUsuarioDTO usuarioDTO)
        {
            try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiUsuarios, "POST", usuarioDTO, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Create), new { mensaje = "Usuario creado exitosamente!" });
                }
                else
                {
                    ViewBag.Error = body;
                    HttpResponseMessage respuestaEquipos = AuxiliarClienteHttp.EnviarSolicitud(URLApiEquipos, "GET", null, token);

                    string bodyEquipos = AuxiliarClienteHttp.ObtenerBody(respuestaEquipos);

                    if (respuestaEquipos.IsSuccessStatusCode)
                    {
                        ViewBag.Equipos = JsonConvert.DeserializeObject<IEnumerable<EquipoDTO>>(bodyEquipos);
                    }
                    else
                    {
                        ViewBag.Error = bodyEquipos;
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado. Intente de nuevo más tarde.";
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuestaEquipos = AuxiliarClienteHttp.EnviarSolicitud(URLApiEquipos, "GET", null, token);

                string bodyEquipos = AuxiliarClienteHttp.ObtenerBody(respuestaEquipos);

                if (respuestaEquipos.IsSuccessStatusCode)
                {
                    ViewBag.Equipos = JsonConvert.DeserializeObject<IEnumerable<EquipoDTO>>(bodyEquipos);
                }
                else
                {
                    ViewBag.Error = bodyEquipos;
                }
            }
            return View();
        }
    }
}