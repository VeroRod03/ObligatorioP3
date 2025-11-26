using DominioWebApp.DTOs;
using DominioWebApp.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppClienteHttp.Auxiliares;

namespace DominioWebApp.Controllers
{
    [FilterAutenticado]

    public class AuditoriaController : Controller
    {
        public string URLApiAuditorias { get; set; }

        public Auditoria Controller(IConfiguration config)
        {
            URLApiAuditorias = config.GetValue<string>("URLApiAuditorias");

        }

        [FilterAdministrador]
        public IActionResult Index()
        {
            IEnumerable<AuditoriaDTO> auditorias = new List<AuditoriaDTO>();

            return View(auditorias);
        }
        [HttpPost]
        public ActionResult Index(int id)
        {
            IEnumerable<AuditoriaDTO> auditorias = new List<AuditoriaDTO>();

            try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud($"{URLApiAuditorias}/AuditoriasTipoGasto?id={id}", "GET", null, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    auditorias = JsonConvert.DeserializeObject<IEnumerable<AuditoriaDTO>>(body);
                }
                else
                {
                    ViewBag.Error = body;
                    HttpResponseMessage respuestaTipoGastos = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos, "GET", null, token);

                    string bodyTipoGastos = AuxiliarClienteHttp.ObtenerBody(respuestaTipoGastos);

                    if (respuestaTipoGastos.IsSuccessStatusCode)
                    {
                        ViewBag.TipoGastos = JsonConvert.DeserializeObject<IEnumerable<TipoGastoDTO>>(bodyTipoGastos); 
                    }
                    else 
                    {
                        ViewBag.Error = bodyTipoGastos;  
                    }                    
                }
                HttpResponseMessage respuestaTipoGastos = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos, "GET", null, token);

                string bodyTipoGastos = AuxiliarClienteHttp.ObtenerBody(respuestaTipoGastos);

                if (respuestaTipoGastos.IsSuccessStatusCode)
                {
                    ViewBag.TipoGastos = JsonConvert.DeserializeObject<IEnumerable<TipoGastoDTO>>(bodyTipoGastos); 
                }
                else 
                {
                    ViewBag.Error = bodyTipoGasto;
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado. Intente de nuevo más tarde.";
                HttpResponseMessage respuestaTipoGastos = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos, "GET", null, token);

                string bodyTipoGastos = AuxiliarClienteHttp.ObtenerBody(respuestaTipoGastos);

                if (respuestaTipoGastos.IsSuccessStatusCode)
                {
                    ViewBag.TipoGastos = JsonConvert.DeserializeObject<IEnumerable<TipoGastoDTO>>(bodyTipoGastos); 
                }
                else 
                {
                    ViewBag.Error = bodyTipoGastos;  
                }                    
            }
            return View(auditorias);
        }

    }
}