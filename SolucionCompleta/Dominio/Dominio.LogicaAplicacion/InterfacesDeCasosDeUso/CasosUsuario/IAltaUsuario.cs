using Dominio.LogicaAplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosUsuario
{
    public interface IAltaUsuario
    {
        public void AgregarUsuario(UsuarioDTO usuario);
    }
}
