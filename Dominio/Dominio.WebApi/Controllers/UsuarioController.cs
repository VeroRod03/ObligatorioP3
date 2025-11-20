using Dominio.LogicaAplicacion.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dominio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IAltaUsuario _altaUsuarioCU;
        private IObtenerUsuarios _obtenerUsuariosCU;
        private IObtenerUsuariosFiltrados _obtenerUsuariosFiltradosCU;

        public UsuarioController(
            IAltaUsuario altaUsuarioCU,
            IObtenerUsuarios obtenerUsuariosCU,
            IObtenerUsuariosFiltrados obtenerUsuariosFiltradosCU)
        {
            _altaUsuarioCU = altaUsuarioCU;
            _obtenerUsuariosCU = obtenerUsuariosCU;
            _obtenerUsuariosFiltradosCU = obtenerUsuariosFiltradosCU;
        }
        // GET: api/<UsuarioController>
        [HttpGet]
        public ActionResult<IEnumerable<UsuarioDTO>> Get()
        {
            try
            {
                return Ok(_obtenerUsuariosCU.ObtenerUsuarios());

            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado. Intente nuevamente más tarde");
            }
        }

        [HttpGet("UsuariosFiltrados")]
        public ActionResult<IEnumerable<UsuarioDTO>> GetUsuariosFiltrados(double monto)
        {
            try
            {
                if(monto <= 0)
                {
                    return BadRequest("El monto debe ser un número positivo");
                }
                IEnumerable<UsuarioDTO> usuarios = _obtenerUsuariosFiltradosCU.ObtenerUsuariosFiltrados(monto);
                Ok(usuarios);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado. Intente nuevamente más tarde");
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public ActionResult<UsuarioDTO> Post([FromBody] usuarioDTO usuario)
        {
            if (usuario == null)
            {
                return BadRequest("No se proporcionaron datos para el alta");
            }
            try
            {
                _altaUsuarioCU.AgregarUsuario(usuario);
                return Created("api/Usuario/" + usuario.Id, usuario);
            }
            catch (UsuarioException ue)
            {
                return BadRequest(ue.Message);
    }
            catch (Exception ex)
            {
                return StatusCode(500, "sOcurrió un error inesperado. Intente nuevamente más tarde");
}
        }

    }
}
