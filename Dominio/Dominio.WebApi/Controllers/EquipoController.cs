using Dominio.LogicaAplicacion.CasosDeUso.CasosPago;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosEquipo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dominio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private IObtenerEquipos _obtenerEquiposCU;
        public EquipoController(IObtenerEquipos obtenerEquiposCU)
        {
            _obtenerEquiposCU = obtenerEquiposCU;
        }

        /// <summary>
        /// Permite obtener todos los Equipos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EquipoDTO>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EquipoDTO>> Get()
        {
            try
            {
                return Ok(_obtenerEquiposCU.ObtenerEquipos());
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });

            }
        }
    }
}
