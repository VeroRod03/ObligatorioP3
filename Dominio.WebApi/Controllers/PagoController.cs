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
        public PagoDTO Get(int id)
        {
            return _obtenerPagoPorId.ObtenerPagoPorId(id);
        }
    }
}
