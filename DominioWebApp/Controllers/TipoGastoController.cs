using AccesoDatos.EntityFramework;
using Dominio.Entidades;
using Dominio.Exceptions;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;
using DominioWebApp.Filters;
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
        [FilterAdministrador]
        public ActionResult Index(string mensaje, string error)
        {
            ViewBag.Mensaje = mensaje;
            ViewBag.Error = error;
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
        [FilterAutenticado]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoGastoDTO gasto)
        {
            try
            {
                _altaTipoGastoCU.AgregarTipoGasto(gasto,(int)HttpContext.Session.GetInt32("usuarioId"));
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
            try
            {
                return View(_getById.ObtenerTipoGasto(id));

            }
            catch (TipoGastoException tge)
            {
                return RedirectToAction(nameof(Index), new { error = tge.Message });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index), new { error = ex.Message });

            }
        }

        // POST: TipoGastoController/Edit/5
        [FilterAutenticado]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoGastoDTO dto)
        {
            try
            {
                _editarTipoGastoCU.EditarTipoGasto(dto,(int)HttpContext.Session.GetInt32("usuarioId"));
                return RedirectToAction(nameof(Index), new { mensaje = "Tipo de Gasto editado correctamente" });
            }
            catch (TipoGastoException tge)
            {
                ViewBag.Error = tge.Message;
                return View((_getById.ObtenerTipoGasto(dto.Id)));
            }
        }

        // GET: TipoGastoController/Delete/5
        public ActionResult Delete(int id, string mensaje)
        {
            try
            {
                ViewBag.Error = mensaje;
                return View((_getById.ObtenerTipoGasto(id)));
            }catch(TipoGastoException tge)
            {
                ViewBag.Error = tge.Message;
                return RedirectToAction(nameof(Index));

            }catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction(nameof(Index));
            }

        }

        // POST: TipoGastoController/Delete/5
        [FilterAutenticado]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TipoGastoDTO dto)
        {
            try
            {
                _eliminarTipoGastoCU.EliminarTipoGasto(id,(int)HttpContext.Session.GetInt32("usuarioId"));
                return RedirectToAction(nameof(Index),new { mensaje = "Tipo de Gasto borrado correctamente" });
            }
            catch (TipoGastoException tge)
            {
                ViewBag.Error = tge.Message;
                return View((_getById.ObtenerTipoGasto(id)));
            }
        }
    }
}
