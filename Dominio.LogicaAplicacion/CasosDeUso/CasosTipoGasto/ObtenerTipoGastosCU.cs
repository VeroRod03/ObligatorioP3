using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;
using Dominio.LogicaAplicacion.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.CasosDeUso.CasosTipoGasto
{
    public class ObtenerTipoGastosCU : IObtenerTipoGastos
    {
        private ITipoGastoRepositorio _repositorio;
        public ObtenerTipoGastosCU(ITipoGastoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public IEnumerable<TipoGastoDTO> ObtenerTipoGastos()
        {
            IEnumerable<TipoGasto> toReturn = _repositorio.FindAll();
            return toReturn.Select(gasto => TipoGastoMapper.ToDTO(gasto));
        }
    }
}
