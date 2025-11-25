using Dominio.LogicaAplicacion.CasosDeUso.CasosAuditoria;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosAuditoria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dominio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuditoriaController : ControllerBase
    {
        private IObtenerAuditoriasTipoGasto _obtenerAuditoriasTipoGastoCU;
        public AuditoriaController(
            IObtenerAuditoriasTipoGasto obtenerAuditoriasTipoGastoCU)
        {
            _obtenerAuditoriasTipoGastoCU = obtenerAuditoriasTipoGastoCU;
        }

        /// <summary>
        /// Permite obtener todas las auditorias dado un id de tipo de gasto
        /// </summary>
        /// <returns></returns>
        [HttpGet("AuditoriasTipoGasto")]
        [Authorize(Roles = "ADMINISTRADOR")]
        [ProducesResponseType(typeof(IEnumerable<AuditoriaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AuditoriaDTO> GetAuditoriasTipoGasto(int idTipoGasto)
        {
            if(idTipoGasto < 0)
            {
                return BadRequest("El id debe ser un numero positivo");
            }
            try
            {
                IEnumerable<AuditoriaDTO> auditorias = _obtenerAuditoriasTipoGastoCU.ObtenerAuditoriasTipoGasto(idTipoGasto);
                return Ok(auditorias);
            }
            catch (AuditoriaException pe)
            {
                return BadRequest(new { error = pe.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }
    
    }
}
