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

namespace DominioWebApp.Controllers
{
    public class PagoController : Controller
    {
        private IAltaPago _altaPagoCU;
        private IObtenerTipoGastos _obtenerTipoGastos;
        private IGetById _obtenerTipoGastoPorId;
        private IObtenerUsuarioPorId _obtenerUsuarioPorId;
        private IObtenerPagos _obtenerPagosCU;
        private IObtenerPagosFiltrados _obtenerPagosFiltradosCU;

        public PagoController(
            IAltaPago altaPagoCU,
            IObtenerTipoGastos obtenerTipoGastos,
            IGetById obtenerTipoGastoPorId,
            IObtenerUsuarioPorId obtenerUsuarioPorId,
            IObtenerPagos obtenerPagosCU,
            IObtenerPagosFiltrados obtenerPagosFiltradosCU)
        {
            _altaPagoCU = altaPagoCU;
            _obtenerTipoGastos = obtenerTipoGastos;
            _obtenerTipoGastoPorId = obtenerTipoGastoPorId;
            _obtenerUsuarioPorId = obtenerUsuarioPorId;
            _obtenerPagosCU = obtenerPagosCU;
            _obtenerPagosFiltradosCU = obtenerPagosFiltradosCU;
        }

        // GET: PagoController
        [FilterAutenticado]
        [FilterGerente]
        public ActionResult Index()
        {
            //List<PagoDTO> test = new List<PagoDTO>();
            //return View(test);

            return View(_obtenerPagosCU.ObtenerPagos());
        }

        [FilterAutenticado]
        [FilterGerente]
        [HttpPost]
        public ActionResult Index(Mes mes, int anio)
        {
            return View(_obtenerPagosFiltradosCU.ObtenerPagosFiltrados(mes, anio));
        }

        // GET: PagoController/Details/5
        [FilterAutenticado]
        public ActionResult Details(int id)
        {
            return View();
        }

        [FilterAutenticado]
        public IActionResult AltaPago()
        {
            return View();
        }

        [FilterAutenticado]
        // GET: PagoController/Create
        public ActionResult Create(string tipoPago)
        {
            ViewBag.TipoPago = tipoPago;
            ViewBag.TipoGastos = _obtenerTipoGastos.ObtenerTipoGastos();
            return View();
        }

        // POST: PagoController/Create
        [FilterAutenticado]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PagoDTO pagoDto, string tipoPago)
        {
            try
            {
                //pagoDto.TipoGasto = _obtenerTipoGastoPorId.ObtenerTipoGasto(pagoDto.TipoGastoId);
                pagoDto.TipoPago = tipoPago;
                pagoDto.UsuarioId = (int)HttpContext.Session.GetInt32("usuarioId");
                //pagoDto.Usuario = _obtenerUsuarioPorId.ObtenerUsuarioPorId(pagoDto.UsuarioId);
                _altaPagoCU.AgregarPago(pagoDto);
                return RedirectToAction(nameof(AltaPago));
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
    }
}
