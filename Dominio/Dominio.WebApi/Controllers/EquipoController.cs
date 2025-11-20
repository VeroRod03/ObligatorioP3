using Dominio.LogicaAplicacion.CasosDeUso.CasosPago;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosEquipo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dominio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private IObtenerEquipos _obtenerEquiposCU;
        public EquipoController(IObtenerEquipos obtenerEquiposCU)
        {
            _obtenerEquiposCU = obtenerEquiposCU;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PagoDTO>> Get()
        {
            return Ok(_obtenerEquiposCU.ObtenerEquipos());
        }
    }
}
