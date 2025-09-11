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
    public class EditarTipoGastoCU : IEditarTipoGasto
    {
        private ITipoGastoRepositorio _repositorio;
        public EditarTipoGastoCU(ITipoGastoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public void EditarTipoGasto(TipoGastoDTO gasto)
        {
            _repositorio.Update(TipoGastoMapper.FromDTO(gasto));
        }
    }
}
