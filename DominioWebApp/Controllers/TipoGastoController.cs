using AccesoDatos.EntityFramework;
using Dominio.Entidades;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DominioWebApp.Controllers
{
    public class TipoGastoController : Controller
    {
        private IObtenerTipoGastos _obtenerTipoGastosCU;
        private IAltaTipoGasto _altaTipoGastoCU;
        private IEditarTipoGasto _editarTipoGastoCU;
        private IGetById _getById;
        private IEliminarTipoGasto _eliminarTipoGastoCU;

        public TipoGastoController(
            IObtenerTipoGastos obtenerTipoGastosCU, 
            IAltaTipoGasto altaTipoGastoCU,
            IEditarTipoGasto editarTipoGastoCU,
            IGetById getById,
            IEliminarTipoGasto eliminarTipoGastoCU)
        {
            _obtenerTipoGastosCU = obtenerTipoGastosCU;
            _altaTipoGastoCU = altaTipoGastoCU;
            _editarTipoGastoCU = editarTipoGastoCU;
            _getById = getById;
            _eliminarTipoGastoCU = eliminarTipoGastoCU;
        }
        // GET: TipoGastoController
        public ActionResult Index()
        {
            return View(_obtenerTipoGastosCU.ObtenerTipoGastos());
        }

        // GET: TipoGastoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoGastoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoGastoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoGastoDTO gasto)
        {
            try
            {
                _altaTipoGastoCU.AgregarTipoGasto(gasto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoGastoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_getById.ObtenerTipoGasto(id));
        }

        // POST: TipoGastoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoGastoDTO dto)
        {
            try
            {
                _editarTipoGastoCU.EditarTipoGasto(dto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoGastoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View((_getById.ObtenerTipoGasto(id)));
        }

        // POST: TipoGastoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TipoGastoDTO dto)
        {
            try
            {

                _eliminarTipoGastoCU.EliminarTipoGasto(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
