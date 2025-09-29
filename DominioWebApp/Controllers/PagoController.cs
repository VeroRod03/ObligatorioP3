using Dominio.LogicaAplicacion.CasosDeUso.CasosTipoGasto;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosPago;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;
using Dominio.LogicaAplicacion.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DominioWebApp.Controllers
{
    public class PagoController : Controller
    {
        private IAltaPago _altaPagoCU;
        private IObtenerTipoGastos _obtenerTipoGastos;

        public PagoController(
            IAltaPago altaPagoCU,
            IObtenerTipoGastos obtenerTipoGastos)
        {
            _altaPagoCU = altaPagoCU;
            _obtenerTipoGastos = obtenerTipoGastos;
        }

        // GET: PagoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PagoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public IActionResult AltaPago()
        {
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
                pagoDto.UsuarioId = HttpContext.Session.GetInt32("usuarioId");
                _altaPagoCU.AgregarPago(pagoDto);
                return RedirectToAction(nameof(Create));
            }
            catch
            {
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
