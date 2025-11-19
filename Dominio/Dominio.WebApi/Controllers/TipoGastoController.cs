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

        public TipoGastoController(IObtenerTipoGastos obtenerTipoGastos)
        {
            _obtenerTipoGastosCU = obtenerTipoGastos;
        }

        // GET: api/<TipoGastoController>
        [HttpGet]
        public ActionResult<IEnumerable<TipoGastoDTO>>Get()
        {
            return Ok(_obtenerTipoGastosCU.ObtenerTipoGastos());
        }

        // GET api/<TipoGastoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TipoGastoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TipoGastoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TipoGastoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
