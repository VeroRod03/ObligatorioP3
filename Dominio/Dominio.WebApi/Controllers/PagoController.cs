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
        private IObtenerPagosFiltrados _obtenerPagosFiltradosCU;
        private IObtenerPagos _obtenerPagosCU;
        public PagoController(IObtenerPagoPorId obtenerPagoPorId, 
            IObtenerPagosFiltrados obtenerPagosFiltrados,
            IObtenerPagos obtenerPagosCU)
        {
            _obtenerPagoPorId = obtenerPagoPorId;
            _obtenerPagosFiltradosCU = obtenerPagosFiltrados;
            _obtenerPagosCU = obtenerPagosCU;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PagoDTO>> Get()
        {
            return Ok(_obtenerPagosCU.ObtenerPagos());
        }

        [HttpGet("{id}")]
        public ActionResult<PagoDTO> Get(int id)
        {
            try
            {
                //si sale todo bien que retorne status code 200 y la informacion del pago.
                PagoDTO pago = _obtenerPagoPorId.ObtenerPagoPorId(id);
                return Ok(pago);

            }
            catch (PagoException pe)
            {
                //devolver 400 si el id que el usuario ingreso no existe.
                return BadRequest(new { error = pe.Message });
            }
            catch (Exception ex)
            {
                //con cualquier otro error que mande un 500, internal error.
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet("PagosFiltrados")]
        public ActionResult<IEnumerable<PagoDTO>> GetPagosFiltrados(int mes, int anio)
        {
            try
            {
                IEnumerable<PagoDTO> pagos = _obtenerPagosFiltradosCU.ObtenerPagosFiltrados(mes, anio);
                return Ok(pagos);
            }
            catch (PagoException pe)
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
