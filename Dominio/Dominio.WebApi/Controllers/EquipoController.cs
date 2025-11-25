using Dominio.LogicaAplicacion.CasosDeUso.CasosPago;
using Dominio.LogicaAplicacion.CasosDeUso.CasosUsuario;
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
        private IObtenerEquiposFiltrados _obtenerEquiposFiltradosCU;
        public EquipoController(
            IObtenerEquipos obtenerEquiposCU,
            IObtenerEquiposFiltrados obtenerEquiposFiltradosCU)
        {
            _obtenerEquiposCU = obtenerEquiposCU;
            _obtenerEquiposFiltradosCU = obtenerEquiposFiltradosCU;
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


        /// <summary>
        /// Permite obtener los equipos filtrados, ordenados por nombre segun si los usuarios pertenecientes realizaron Pagos con un monto mayor al monto recibido por parametro
        /// </summary>
        /// <param name="monto"></param>
        /// <returns></returns>
        [HttpGet("EquiposFiltrados")]
        [Authorize(Roles = "GERENTE")]
        [ProducesResponseType(typeof(IEnumerable<EquipoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<EquipoDTO>> GetEquiposFiltrados(double monto)
        {
            try
            {
                if (monto <= 0)
                {
                    return BadRequest("El monto debe ser un número positivo");
                }
                IEnumerable<EquipoDTO> equipos = _obtenerEquiposFiltradosCU.ObtenerEquiposFiltrados(monto);
                return Ok(equipos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }
    }
}
