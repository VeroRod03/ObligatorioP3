using Dominio.Entidades;
using Dominio.Exceptions;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dominio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TipoGastoController : ControllerBase
    {
        private IObtenerTipoGastos _obtenerTipoGastosCU;
        private IAltaTipoGasto _altaTipoGastoCU;
        private IEditarTipoGasto _editarTipoGastoCU;
        private IGetById _obtenerTipoGastoPorIdCU;
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
            _obtenerTipoGastoPorIdCU = getById;
            _eliminarTipoGastoCU = eliminarTipoGastoCU;
        }

        // GET: api/<TipoGastoController>
        /// <summary>
        /// Permite obtener todos los Tipos De Gasto
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TipoGastoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<TipoGastoDTO>>Get()
        {
            try
            {
                return Ok(_obtenerTipoGastosCU.ObtenerTipoGastos());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });

            }
        }

        // GET api/<TipoGastoController>/5
        /// <summary>
        /// Permite obtener los datos de un tipo de gasto en base al id que recibe por parámetro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TipoGastoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipoGastoDTO> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id debe ser un número positivo");
            }
            try
            {
                TipoGastoDTO tipoGasto = _obtenerTipoGastoPorIdCU.ObtenerTipoGasto(id);
                return Ok(tipoGasto);
            }
            catch (TipoGastoException tge)
            {
                return NotFound(new { error = tge.Message }); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        // POST api/<TipoGastoController>
        /// <summary>
        /// Permite dar de alta un Tipo De Gasto dados los datos recibidos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipoGastoDTO> Post([FromBody] TipoGastoDTO tipoGasto)
        {
            if (tipoGasto == null)
            {
                return BadRequest("No se proporcionaron datos para el alta");
            }
            try
            {
                int usuarioId = int.Parse(User.FindFirst("usuarioId")!.Value);
                _altaTipoGastoCU.AgregarTipoGasto(tipoGasto,usuarioId);
                return Created("api/TipoGasto", tipoGasto);
               // return CreatedAtAction(nameof(Get), new { id = tipoGasto.Id }, tipoGasto);            
            }
            catch (TipoGastoException tge)
            {
                return BadRequest(new { error = tge.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        // PUT api/<TipoGastoController>/5
        /// <summary>
        /// Permite modificar un Tipo De Gasto dado el id recibidio por parametro y los datos recibidos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TipoGastoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipoGastoDTO> Put(int id, [FromBody] TipoGastoDTO? dto)
        {
            if (id <= 0) return BadRequest("Id debe ser un número positivo");

            if (dto == null) return BadRequest("No se proporcionó información para la modificación");

            if (dto.Id != id) return BadRequest("No coincide el id del tipo de gasto");
            
            try
            {
                //para traernos el id del usuario logueado hacemos uso del claim "usuarioId" del token
                //de ese usuario. 
                int usuarioId = int.Parse(User.FindFirst("usuarioId")!.Value); 
                _editarTipoGastoCU.EditarTipoGasto(dto, usuarioId);
            }
            catch (TipoGastoException tge)
            {
                return BadRequest(new { error = tge.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }

            return Ok(dto);
        }

        // DELETE api/<TipoGastoController>/5
        /// <summary>
        /// Permite eliminar un Tipo De Gasto dado el id recibido por parametro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(int id) {

            if (id <= 0) return BadRequest("Id debe ser un número positivo");

            try
            {
                int usuarioId = int.Parse(User.FindFirst("usuarioId")!.Value);
                _eliminarTipoGastoCU.EliminarTipoGasto(id,usuarioId);
            }
            catch (TipoGastoException tge)
            {
                return NotFound(new { error = tge.Message });
            }
            catch (OperacionConflictivaException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
            return NoContent();
        }
    }
}
