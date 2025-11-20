using Dominio.Exceptions;
using Dominio.LogicaAplicacion;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dominio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILogin _loginCU;
        public LoginController(ILogin login)
        {
            _loginCU = login;
        }
        /// <summary>
        /// Permite realizar un Login segun los datos ingresados
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //le sacamos la ruta "login"
        public ActionResult<UsuarioDTO> Login([FromBody] UsuarioLoginDTO logindto)
        {
            try
            {
                UsuarioDTO logueado = _loginCU.Login(logindto.Email, logindto.Contra);
                //generamos el token
                var token = ManejadorJWT.GenerarToken(logueado);
                //se lo asignamos al usuario que se esta logueando
                logueado.Token = token.ToString();
                return Ok(logueado);
            }
            catch (UsuarioException uex)
            {
                return Unauthorized("Credenciales invalidas. Reintentar");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });

            }

        }
    }
}
