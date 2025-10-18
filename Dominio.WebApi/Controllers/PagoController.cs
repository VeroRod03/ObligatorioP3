using Dominio.Exceptions;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosPago;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dominio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private IObtenerPagoPorId _obtenerPagoPorId;
        public PagoController(IObtenerPagoPorId obtenerPagoPorId)
        {
            _obtenerPagoPorId = obtenerPagoPorId;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                //si sale todo bien que retorne status code 200 y la informacion del pago.
                PagoDTO pago = _obtenerPagoPorId.ObtenerPagoPorId(id);
                return Ok(pago);

            }
            catch (PagoException pe)
            {
                //devolver 404 si el id que el usuario ingreso no existe.
                return NotFound(new { error = pe.Message });
            }
            catch (Exception ex)
            {
                //con cualquier otro error que mande un 500, internal error.
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }
    }
}
