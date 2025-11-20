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
        private IObtenerPagoPorId _obtenerPagoPorIdCU;
        private IObtenerPagosFiltrados _obtenerPagosFiltradosCU;
        private IObtenerPagos _obtenerPagosCU;
        private IAltaPago _altaPagoCU;
        public PagoController(IObtenerPagoPorId obtenerPagoPorId,
            IObtenerPagosFiltrados obtenerPagosFiltrados,
            IObtenerPagos obtenerPagos,
            IAltaPago altaPago)
        {
            _obtenerPagoPorIdCU = obtenerPagoPorId;
            _obtenerPagosFiltradosCU = obtenerPagosFiltrados;
            _obtenerPagosCU = obtenerPagos;
            _altaPagoCU = altaPago;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PagoDTO>> Get()
        {
            return Ok(_obtenerPagosCU.ObtenerPagos());
        }

        [HttpGet("{id}")]
        public ActionResult<PagoDTO> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id debe ser un número positivo");
            }
            try
            {
                //si sale todo bien que retorne status code 200 y la informacion del pago.
                PagoDTO pago = _obtenerPagoPorIdCU.ObtenerPagoPorId(id);
                return Ok(pago);

            }
            catch (PagoException pe)
            {
                //devolvemos 404 porque el unico pagoException es no encontrar un pago con ese id
                return NotFound(new { error = pe.Message });
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

        [HttpPost]
        public ActionResult<PagoDTO> Post([FromBody] PagoDTO pago)
        {
            if (pago == null)
            {
                return BadRequest("No se proporcionaron datos para el alta");
            }
            try
            {
                _altaPagoCU.AgregarPago(pago);
                return CreatedAtAction(nameof(Get), new { id = pago.Id }, pago); 
            } catch (PagoException pe){
                return BadRequest(pe.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado. Intente nuevamente más tarde");
            }
        }
    }
}
