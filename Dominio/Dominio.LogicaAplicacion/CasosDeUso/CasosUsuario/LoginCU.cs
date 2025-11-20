using Dominio.Exceptions;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosUsuario;
using Dominio.LogicaAplicacion.Mappers;
using Dominio.ValueObjects;

namespace Dominio.LogicaAplicacion.CasosDeUso.CasosUsuario
{
    public class LoginCU : ILogin
    {
        private IUsuarioRepositorio _repositorio;
        public LoginCU(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public UsuarioDTO Login(string email, string pass)
        {
            UsuarioDTO logueado = UsuarioMapper.ToDTO(_repositorio.Login(email.ToLower(), pass));
            if (logueado == null)
            {
                throw new UsuarioException("Usuario o contraseña incorrecta.");
            }
            return logueado;
        }
    }
}
