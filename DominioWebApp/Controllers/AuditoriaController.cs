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
        public string URLApiTipoGastos { get; set; }


        public AuditoriaController(IConfiguration config)
        {
            URLApiAuditorias = config.GetValue<string>("URLApiAuditorias");
            URLApiTipoGastos = config.GetValue<string>("URLApiTipoGastos");

        }

        [FilterAdministrador]
        public IActionResult Index()
        {
            IEnumerable<AuditoriaDTO> auditorias = new List<AuditoriaDTO>();
            try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos, "GET", null, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    ViewBag.TipoGastos = JsonConvert.DeserializeObject<IEnumerable<TipoGastoDTO>>(body);
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
            return View(auditorias);
        }
        [HttpPost]
        public ActionResult Index(int TipoGastoId)
        {
            IEnumerable<AuditoriaDTO> auditorias = new List<AuditoriaDTO>();

            try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud($"{URLApiAuditorias}/AuditoriasTipoGasto?idTipoGasto={TipoGastoId}", "GET", null, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    auditorias = JsonConvert.DeserializeObject<IEnumerable<AuditoriaDTO>>(body);
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
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado. Intente de nuevo más tarde.";
                string token = HttpContext.Session.GetString("token");
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