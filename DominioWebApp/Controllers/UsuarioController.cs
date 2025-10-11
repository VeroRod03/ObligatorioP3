using Dominio.Exceptions;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosEquipo;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosUsuario;
using DominioWebApp.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DominioWebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private IObtenerEquipos _obtenerEquiposCU;
        private IAltaUsuario _altaUsuarioCU;
        private IObtenerUsuarios _obtenerUsuariosCU;
        private IObtenerUsuariosFiltrados _obtenerUsuariosFiltradosCU;
        public UsuarioController(
            IObtenerEquipos obtenerEquiposCU,
            IAltaUsuario altaUsuario,
            IObtenerUsuarios obtenerUsuariosCU,
            IObtenerUsuariosFiltrados obtenerUsuariosFiltradosCU)
        {
            _obtenerEquiposCU = obtenerEquiposCU;
            _altaUsuarioCU = altaUsuario;
            _obtenerUsuariosCU = obtenerUsuariosCU;
            _obtenerUsuariosFiltradosCU = obtenerUsuariosFiltradosCU;
        }

        // GET: UsuarioController
        [FilterAutenticado]
        [FilterGerente]
        public ActionResult Index()
        {
            return View(_obtenerUsuariosCU.ObtenerUsuarios());
        }

        [HttpPost]
        public ActionResult Index(double monto)
        {
            try
            {
                return View(_obtenerUsuariosFiltradosCU.ObtenerUsuariosFiltrados(monto));
            }
            catch (UsuarioException us)
            {
                ViewBag.Error = us.Message;
                return View(_obtenerUsuariosFiltradosCU.ObtenerUsuariosFiltrados(monto));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View(_obtenerUsuariosFiltradosCU.ObtenerUsuariosFiltrados(monto));
            }
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        [FilterAutenticado]
        [FilterGerenteAdmin]
        public ActionResult Create()
        {
            ViewBag.Equipos = _obtenerEquiposCU.ObtenerEquipos();
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioDTO usuarioDTO)
        {
            try
            {
                _altaUsuarioCU.AgregarUsuario(usuarioDTO);
                ViewBag.Mensaje = "Usuario creado exitosamente!";
                ViewBag.Equipos = _obtenerEquiposCU.ObtenerEquipos();
                //para que se limpien los campos
                return RedirectToAction(nameof(Create));
            }
            catch (UsuarioException us)
            {
                ViewBag.Error = us.Message;
                ViewBag.Equipos = _obtenerEquiposCU.ObtenerEquipos();
                return RedirectToAction(nameof(Create));
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error inesperado.";
                ViewBag.Equipos = _obtenerEquiposCU.ObtenerEquipos();
                return RedirectToAction(nameof(Create));
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
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

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
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
