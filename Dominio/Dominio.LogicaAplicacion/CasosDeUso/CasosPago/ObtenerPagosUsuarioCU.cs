using Dominio.Entidades;
using Dominio.Exceptions;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosPago;
using Dominio.LogicaAplicacion.Mappers;

namespace Dominio.LogicaAplicacion.CasosDeUso.CasosPago
{
    public class ObtenerPagosUsuarioCU : IObtenerPagosUsuario
    {
        private IPagoRepositorio _repositorio;
        private IUsuarioRepositorio _repoUsuario;
        public ObtenerPagosUsuarioCU(IPagoRepositorio repositorio, IUsuarioRepositorio repoUsuario)
        {
            _repositorio = repositorio;
            _repoUsuario = repoUsuario;
        }

        public IEnumerable<PagoDTO> ObtenerPagosUsuario(int id)
        {
            Usuario aEncontrar = _repoUsuario.FindById(id);
            if (aEncontrar == null)
            {
                throw new UsuarioException("No existe un usuario con ese id");
            }
            return _repositorio.FiltrarPagosPorUsuario(id).Select(PagoMapper.ToDTO);
        }
    }
}

