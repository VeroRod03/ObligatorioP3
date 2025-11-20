using Dominio.LogicaAplicacion.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dominio.LogicaAplicacion
{
    public class ManejadorJWT
    {
        private IConfiguration _configuration;
        public ManejadorJWT(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerarToken(UsuarioDTO logueado)
        {
            //el que manjea la creacion del token
            var tokenHandler = new JwtSecurityTokenHandler();
            var claveSecreta = Encoding.ASCII.
                GetBytes(_configuration["SecretTokenKey"]);
            //como se describe el token, lo que va a tener adentro
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //cada claim es lo que tiene adentro el token
                Subject = new ClaimsIdentity
                (new Claim[]
                    {
                        //podria ser GetType en lugar de Rol
                        new Claim(ClaimTypes.Role, logueado.Rol.ToString()),
                        new Claim("usuarioId", logueado.Id.ToString()) //reemplazamos el mail por id
                    }
                ),
                //cuando se vence
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(claveSecreta),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            //generamos el token con todo lo que acabamos de crear
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //le decimos al token handler que escriba el token en base a lo que acaba de crear
            return tokenHandler.WriteToken(token);
        }
    }
}
