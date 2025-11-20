using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dominio.WebApi.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet]
        public ActionResult<IEnumerable<TipoGastoDTO>>Get()
        {
            return Ok(_obtenerTipoGastosCU.ObtenerTipoGastos());
        }

        // GET api/<TipoGastoController>/5
        [HttpGet("{id}")]
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
        [HttpPost]
        public ActionResult<TipoGastoDTO> Post([FromBody] TipoGastoDTO tipoGasto)
        {
            if (tipoGasto == null)
            {
                return BadRequest("No se proporcionaron datos para el alta");
            }
            try
            {
                _altaTipoGastoCU.AgregarTipoGasto(tipoGasto);
                return CreatedAtAction(nameof(Get), new { id = tipoGasto.Id }, tipoGasto);            }
            catch (TipoGastoException tge)
            {
                return BadRequest(new { error = tge.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado. Intente nuevamente más tarde");
            }
        }

        // PUT api/<TipoGastoController>/5
        [HttpPut("{id}")]
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
                return StatusCode(500, "Ocurrió un error inesperado. Intente nuevamente más tarde");
            }

            return Ok(dto);
        }
        }

        // DELETE api/<TipoGastoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id) {

            if (id <= 0) return BadRequest("Id debe ser un número positivo");

            try
            {
                _eliminarTipoGastoCU.EliminarTipoGasto(id);
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
                return StatusCode(500, "Ocurrió un error inesperado. Intente nuevamente más tarde");
            }
            return NoContent();
        }
    }
}
