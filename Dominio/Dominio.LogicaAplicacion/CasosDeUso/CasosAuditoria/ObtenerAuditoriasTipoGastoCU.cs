using Dominio.Entidades;
using Dominio.Exceptions;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosAuditoria;
using Dominio.LogicaAplicacion.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.CasosDeUso.CasosAuditoria
{
    public class ObtenerAuditoriasTipoGastoCU : IObtenerAuditoriasTipoGasto
    {
        private IAuditoriaRepositorio _repositorio;
        private ITipoGastoRepositorio _repositorioTipoGasto;
        public ObtenerAuditoriasTipoGastoCU(
            IAuditoriaRepositorio repositorio,
            ITipoGastoRepositorio repositorioTipoGasto)
        {
            _repositorio = repositorio;
            _repositorioTipoGasto = repositorioTipoGasto;
        }
        public IEnumerable<AuditoriaDTO> ObtenerAuditoriasTipoGasto(int id)
        {
            TipoGasto gasto = _repositorioTipoGasto.FindById(id);
            if (gasto == null)
            {
                throw new AuditoriaException("El tipo de gasto con id " + id + " no existe");
            }
            IEnumerable<Auditoria> aRetornar = _repositorio.FiltrarAuditoriasPorTipoGasto(id);
            return aRetornar.Select(auditoria => AuditoriaMapper.ToDTO(auditoria));
        }
    }
}