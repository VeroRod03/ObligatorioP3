using Dominio.Entidades;
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
    public class ObtenerUsuariosCU : IObtenerUsuarios
    {
        private IUsuarioRepositorio _repositorio;
        public ObtenerUsuariosCU(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<UsuarioDTO> ObtenerUsuarios()
        {
            IEnumerable<Usuario> aRetornar = _repositorio.FindAll();
            return aRetornar.Select(usuario => UsuarioMapper.ToDTO(usuario));
        }
    }
}
