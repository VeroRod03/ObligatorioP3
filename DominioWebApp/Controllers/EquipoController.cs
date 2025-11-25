using DominioWebApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppClienteHttp.Auxiliares;

namespace DominioWebApp.Controllers
{
    public class EquipoController : Controller
    {
        public string URLApiEquipos { get; set; }

        public EquipoController(IConfiguration config)
        {
            URLApiEquipos = config.GetValue<string>("URLApiEquips");

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(double monto)
        {
            IEnumerable<EquipoDTO> equipos = new List<EquipoDTO>();

            try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud($"{URLApiEquipos}/EquiposFiltrados?monto={monto}", "GET", null, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    equipos = JsonConvert.DeserializeObject<IEnumerable<EquipoDTO>>(body);
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
            return View(equipos);
        }

    }
}
