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
    public class ObtenerTipoGastoPorIdCU : IGetById
    {
        private ITipoGastoRepositorio _repositorio;
        public ObtenerTipoGastoPorIdCU (ITipoGastoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public TipoGastoDTO ObtenerTipoGasto(int id)
        {
            return TipoGastoMapper.ToDTO(_repositorio.FindById(id));
        }
    
}
}
