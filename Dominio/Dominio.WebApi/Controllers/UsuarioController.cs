using Dominio.Exceptions;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosUsuario;
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
        /// <summary>
        /// Permite obtener todos los usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsuarioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<UsuarioDTO>> Get()
        {
            try
            {
                return Ok(_obtenerUsuariosCU.ObtenerUsuarios());

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Permite obtener los Usuarios filtrados, segun si realizaron Pagos con un monto mayor al monto recibido por parametro
        /// </summary>
        /// <param name="monto"></param>
        /// <returns></returns>
        [HttpGet("UsuariosFiltrados")]
        [ProducesResponseType(typeof(IEnumerable<UsuarioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<UsuarioDTO>> GetUsuariosFiltrados(double monto)
        {
            try
            {
                if(monto <= 0)
                {
                    return BadRequest("El monto debe ser un número positivo");
                }
                IEnumerable<UsuarioDTO> usuarios = _obtenerUsuariosFiltradosCU.ObtenerUsuariosFiltrados(monto);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        // POST api/<UsuarioController>
        /// <summary>
        /// Permite dar de alta un Usuario dados los datos recibidos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UsuarioDTO> Post([FromBody] UsuarioDTO usuario)
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

    }
}
