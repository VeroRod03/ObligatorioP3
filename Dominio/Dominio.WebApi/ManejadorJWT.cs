using Dominio.LogicaAplicacion.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dominio.WebApi
{
    public class ManejadorJWT
    {
        internal static object GenerarToken(UsuarioDTO logueado) //test
        {
            //el que manjea la creacion del token
            var tokenHandler = new JwtSecurityTokenHandler();
            //llamamos a la clave (ahora esta ingresada manualmente pero luego hayq ue traerla desde appsettings)
            var clave = Encoding.ASCII.GetBytes("claveSecreta_obligatorio_DDVR");
            //como se describe el token, lo que va a tener adentro
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //cada claim es lo que tiene adentro el token
                Subject = new ClaimsIdentity
                (new Claim[]
                    {
                        new Claim(ClaimTypes.Email,logueado.Email),
                        //podria ser GetType en lugar de Rol
                        new Claim(ClaimTypes.Role, logueado.Rol.ToString())
                    }
                ),
                //cuando se vence
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(clave),
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
