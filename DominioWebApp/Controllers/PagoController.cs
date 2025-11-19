using Dominio.DominioWebApp.DTOs;
using Dominio.Enumerations;
using Dominio.Exceptions;
using Dominio.DominioWebApp.CasosDeUso.CasosPago;
using Dominio.DominioWebApp.CasosDeUso.CasosTipoGasto;
using Dominio.DominioWebApp.DTOs;
using Dominio.DominioWebApp.InterfacesDeCasosDeUso.CasosPago;
using Dominio.DominioWebApp.InterfacesDeCasosDeUso.CasosTipoGasto;
using Dominio.DominioWebApp.InterfacesDeCasosDeUso.CasosUsuario;
using Dominio.DominioWebApp.Mappers;
using DominioWebApp.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAppClienteHttp.Auxiliares;

namespace DominioWebApp.Controllers
{
    [FilterAutenticado]
    public class PagoController : Controller
    {
        public string URLApiPagos { get; set; }
        public string URLApiTipoGastos { get; set; }

        public PagoController(IConfiguration config)
        {
            URLApiPagos = config.GetValue<string>("URLApiPagos");
            URLApiTipoGastos = config.GetValue<string>("URLApiTipoGastos");

        }

        // GET: PagoController
        [FilterGerente]
        public ActionResult Index()
        {
            IEnumerable<PagoDTO> pagos = new List<PagoDTO>();

            try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiPagos, "GET", null, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode) // Serie 200
                {
                    pagos = JsonConvert.DeserializeObject<IEnumerable<PagoDTO>>(body); // en el body hay JSON
                }
                else // Serie 400 o 500
                {
                    ViewBag.Error = body; // en el body vino el mensaje del error
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado. Intente de nuevo más tarde.";
            }
            return View(pagos);
        }

        [FilterGerente]
        [HttpPost]
        public ActionResult Index(int mes, int anio)
        {
            IEnumerable<PagoDTO> pagos = new List<PagoDTO>();

            try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud($"{URLApiPagos}/PagosFiltrados?mes={mes}&anio={anio}", "GET", null, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode) // Serie 200
                {
                    pagos = JsonConvert.DeserializeObject<IEnumerable<PagoDTO>>(body); // en el body hay JSON
                }
                else // Serie 400 o 500
                {
                    ViewBag.Error = body; // en el body vino el mensaje del error

                    respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiPagos, "GET", null, token);
                    string bodyTodos = AuxiliarClienteHttp.ObtenerBody(respuesta);
                    pagos = JsonConvert.DeserializeObject<IEnumerable<PagoDTO>>(bodyTodos); // en el body hay JSON
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado. Intente de nuevo más tarde.";
            }
            return View(pagos);
        }

        public IActionResult AltaPago(string mensaje)
        {
            ViewBag.Mensaje = mensaje;
            return View();
        }

        // GET: PagoController/Create
        public ActionResult Create(string tipoPago)
        {
            ViewBag.TipoPago = tipoPago;

            try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos, "GET", null, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    ViewBag.TipoGastos = JsonConvert.DeserializeObject<IEnumerable<PagoDTO>>(body); 
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

        // POST: PagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PagoDTO pagoDto, string tipoPago)
        {
            try
            {
                string token = HttpContext.Session.GetString("token");
                pagoDto.TipoPago = tipoPago;
                pagoDto.UsuarioId = HttpContext.Session.GetInt32("usuarioId").Value;
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos, "POST", pagoDto, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(AltaPago), new {mensaje = "Pago registrado exitosamente ! :)"});
                }
                else 
                {
                    ViewBag.Error = body;  
                    ViewBag.TipoPago = tipoPago;
                    HttpResponseMessage respuestaTipoGastos = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos, "GET", null, token);

                    string bodyTipoGastos = AuxiliarClienteHttp.ObtenerBody(respuestaTipoGastos);

                    if (respuestaTipoGastos.IsSuccessStatusCode)
                    {
                        ViewBag.TipoGastos = JsonConvert.DeserializeObject<IEnumerable<PagoDTO>>(bodyTipoGastos); 
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
                ViewBag.TipoPago = tipoPago;
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos, "GET", null, token);

                string bodyTipoGastos = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    ViewBag.TipoGastos = JsonConvert.DeserializeObject<IEnumerable<PagoDTO>>(bodyTipoGastos); 
                }
                else 
                {
                    ViewBag.Error = bodyTipoGastos;  
                }     
            }
            return View();
        }
    }
}
