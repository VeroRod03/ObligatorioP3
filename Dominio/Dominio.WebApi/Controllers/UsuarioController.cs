using Dominio.Entidades;
using Dominio.Enumerations;
using Dominio.Exceptions;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosUsuario;
using Dominio.ValueObjects;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dominio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IAltaUsuario _altaUsuarioCU;
        private IObtenerUsuarios _obtenerUsuariosCU;
        private IObtenerUsuariosFiltrados _obtenerUsuariosFiltradosCU;
        private IGenerarContra _generarContraCU;

        public UsuarioController(
            IAltaUsuario altaUsuarioCU,
            IObtenerUsuarios obtenerUsuariosCU,
            IObtenerUsuariosFiltrados obtenerUsuariosFiltradosCU,
            IGenerarContra generarContraCU)
        {
            _altaUsuarioCU = altaUsuarioCU;
            _obtenerUsuariosCU = obtenerUsuariosCU;
            _obtenerUsuariosFiltradosCU = obtenerUsuariosFiltradosCU;
            _generarContraCU = generarContraCU;
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
        [Authorize(Roles = "GERENTE")]
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
        [Authorize(Roles = "ADMINISTRADOR,GERENTE")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UsuarioDTO> Post([FromBody] AltaUsuarioDTO altaUsuario)
        {
            if (altaUsuario == null)
            {
                return BadRequest("No se proporcionaron datos para el alta");
            }
            if(altaUsuario.Rol != 2 && altaUsuario.Rol != 3)
            {
                return BadRequest("Solo se pueden dar de alta Empleados o Gerentes");
            }
            try
            {
                UsuarioDTO usuario = new UsuarioDTO
                {
                    Nombre = altaUsuario.Nombre,
                    Apellido = altaUsuario.Apellido,
                    Contra = altaUsuario.Contra,
                    EquipoId = altaUsuario.EquipoId,
                    Rol = (RolUsuario)altaUsuario.Rol
                };

                _altaUsuarioCU.AgregarUsuario(usuario);
                return Created("api/Usuario", usuario);
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
        
        // PUT api/<UsuarioController>/5
        /// <summary>
        /// Permite modificar la contraseña (por una generada random) de un usuario dado su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("GenerarContra/{id}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        [ProducesResponseType(typeof(UsuarioDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UsuarioDTO> GenerarContraUsuario(int id) //ignoramos el dto, lo sacamos de la firma
        {
            if (id <= 0) return BadRequest("Id debe ser un número positivo");
            
            try
            {
                UsuarioDTO dto = _generarContraCU.GenerarContra(id);
            }
            catch (UsuarioException tge)
            {
                return BadRequest(new { error = tge.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }

            return Ok(dto);
        }

    }
}
