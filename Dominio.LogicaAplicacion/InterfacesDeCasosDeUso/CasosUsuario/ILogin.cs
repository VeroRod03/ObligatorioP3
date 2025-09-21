using Dominio.LogicaAplicacion.DTOs;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosUsuario
{
    public interface ILogin
    {
        public UsuarioDTO Login(string email, string pass);

    }
}
