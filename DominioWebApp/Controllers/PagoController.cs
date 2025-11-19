using Dominio.Enumerations;
using Dominio.Exceptions;
using Dominio.LogicaAplicacion.CasosDeUso.CasosPago;
using Dominio.LogicaAplicacion.CasosDeUso.CasosTipoGasto;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosPago;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosUsuario;
using Dominio.LogicaAplicacion.Mappers;
using DominioWebApp.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppClienteHttp.Auxiliares;

namespace DominioWebApp.Controllers
{
    [FilterAutenticado]
    public class PagoController : Controller
    {
        public string URLApiPagos { get; set; }

        public PagoController(IConfiguration config)
        {
            URLApiPagos = config.GetValue<string>("URLApiPago");
        }

        // GET: PagoController
        [FilterGerente]
        public ActionResult Index()
        {
            return View(_obtenerPagosCU.ObtenerPagos());

        }

        [FilterGerente]
        [HttpPost]
        public ActionResult Index(int mes, int anio)
        {
            try
            {
                return View(_obtenerPagosFiltradosCU.ObtenerPagosFiltrados(mes, anio));
            }
            catch (PagoException pe)
            {
                ViewBag.Error = pe.Message;
                return View(_obtenerPagosCU.ObtenerPagos());

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(_obtenerPagosCU.ObtenerPagos());
            }


            try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud("http://localhost:5027/api/PagosFiltrados?mes={mes}&anio={anio}", "GET", null, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode) // Serie 200
                {
                    contenidos = JsonConvert.DeserializeObject<IEnumerable<ContenidoDTO>>(body); // en el body hay JSON
                }
                else // Serie 400 o 500
                {
                    ViewBag.Mesaje = body; // en el body vino el mensaje del error
                }
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Ocurrió un error inesperado. Intente de nuevo más tarde.";
            }


            return View(contenidos);
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
            ViewBag.TipoGastos = _obtenerTipoGastos.ObtenerTipoGastos();
            return View();
        }

        // POST: PagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PagoDTO pagoDto, string tipoPago)
        {
            try
            {
                pagoDto.TipoPago = tipoPago;
                pagoDto.UsuarioId = HttpContext.Session.GetInt32("usuarioId").Value;
                _altaPagoCU.AgregarPago(pagoDto);
                return RedirectToAction(nameof(AltaPago), new {mensaje = "Pago registrado exitosamente ! :)"});
            }
            catch (PagoException pe)
            {
                ViewBag.Error = pe.Message;
                ViewBag.TipoPago = tipoPago;
                ViewBag.TipoGastos = _obtenerTipoGastos.ObtenerTipoGastos();
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error inesperado.";
                ViewBag.TipoPago = tipoPago;
                ViewBag.TipoGastos = _obtenerTipoGastos.ObtenerTipoGastos();
                return View();
            }
        }

        // GET: PagoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PagoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PagoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PagoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PagoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
