using Dominio.Enumerations;
using Dominio.Exceptions;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosPago;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dominio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private IObtenerPagoPorId _obtenerPagoPorIdCU;
        private IObtenerPagosFiltrados _obtenerPagosFiltradosCU;
        private IObtenerPagos _obtenerPagosCU;
        private IAltaPago _altaPagoCU;
        private IObtenerPagosUsuario _obtenerPagosUsuarioCU;
        public PagoController(IObtenerPagoPorId obtenerPagoPorId,
            IObtenerPagosFiltrados obtenerPagosFiltrados,
            IObtenerPagos obtenerPagos,
            IAltaPago altaPago,
            IObtenerPagosUsuario obtenerPagosUsuario)
        {
            _obtenerPagoPorIdCU = obtenerPagoPorId;
            _obtenerPagosFiltradosCU = obtenerPagosFiltrados;
            _obtenerPagosCU = obtenerPagos;
            _altaPagoCU = altaPago;
            _obtenerPagosUsuarioCU = obtenerPagosUsuario;
        }
        /// <summary>
        /// Permite obtener todos los Pagos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PagoDTO>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PagoDTO>> Get()
        {
            try
            {
                return Ok(_obtenerPagosCU.ObtenerPagos());

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });

            }
        }

        /// <summary>
        /// Permite obtener los datos de un pago en base al id que recibe por parámetro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PagoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Permite obtener los Pagos Filtrados dados un mes y anio recibidos por parametro
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="anio"></param>
        /// <returns></returns>
        [HttpGet("PagosFiltrados")]
        [Authorize(Roles = "GERENTE")]
        [ProducesResponseType(typeof(IEnumerable<PagoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<PagoDTO>> GetPagosFiltrados(int mes, int anio)
        {
            if(mes < 0 || anio < 0 || mes > 12)
            {
                return BadRequest("El mes y anio escogidos deben ser validos");
            }
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

        /// <summary>
        /// Permite dar de alta un Pago dados los datos recibidos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PagoDTO> Post([FromBody] AltaPagoDTO altaPago) 
        {
            if (altaPago == null)
            {
                return BadRequest("No se proporcionaron datos para el alta");
            }
            try
            {
                //esta bien esto? O sino como hago para 
                int usuarioId = int.Parse(User.FindFirst("usuarioId")!.Value);

                PagoDTO pago = new PagoDTO
                {
                    TipoGastoId = altaPago.TipoGastoId,
                    //convertimos el string del valor de metodo de pago a un int para parsearlo al enum
                    MetodoPago = (MetodoPago)(altaPago.MetodoPago),
                    Descripcion = altaPago.Descripcion,
                    Monto = altaPago.Monto,
                    Fecha = altaPago.Fecha,
                    TipoPago = altaPago.TipoPago,
                    NumRecibo = altaPago.NumRecibo,
                    MontoTotal = altaPago.MontoTotal,
                    SaldoPendiente = altaPago.SaldoPendiente,
                    Hasta = altaPago.Hasta,
                    UsuarioId = usuarioId
                };

                _altaPagoCU.AgregarPago(pago);
                return Created("api/Pago", pago);
               // return CreatedAtAction(nameof(Get), new { id = pago.Id }, pago); 
            } catch (PagoException pe){
                return BadRequest(pe.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }
        
        
        /// <summary>
        /// Permite obtener los Pagos filtrados por el id de un usuario (el usuario logueado)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("PagosUsuario")]
        [Authorize(Roles = "GERENTE, EMPLEADO")]
        [ProducesResponseType(typeof(IEnumerable<PagoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<PagoDTO>> GetPagosUsuario(int idUsuario)
        {
            if(idUsuario < 0)
            {
                return BadRequest("El id debe ser un numero positivo");
            }
            try
            {
                if (idUsuario != int.Parse(User.FindFirst("usuarioId")!.Value))
                {
                    return BadRequest("El id debe coincidir con el id del usuario logueado");
                }
                IEnumerable<PagoDTO> pagos = _obtenerPagosUsuarioCU.ObtenerPagosUsuario(idUsuario);
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
