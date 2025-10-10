using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosUsuario;
using Dominio.LogicaAplicacion.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.CasosDeUso.CasosUsuario
{
    public class AltaUsuarioCU : IAltaUsuario
    {
        private IUsuarioRepositorio _repositorio;
        public AltaUsuarioCU(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public void AgregarUsuario(UsuarioDTO usuario)
        {
            _repositorio.Add(UsuarioMapper.FromDTO(usuario));
        }
    }
}
