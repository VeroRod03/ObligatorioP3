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
    public class ObtenerUsuarioPorIdCU : IObtenerUsuarioPorId
    {
        private IUsuarioRepositorio _repositorio;
        public ObtenerUsuarioPorIdCU(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public UsuarioDTO ObtenerUsuarioPorId(int id)
        {
            return UsuarioMapper.ToDTO(_repositorio.FindById(id));
        }
    }
}
