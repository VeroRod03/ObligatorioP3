using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.CasosDeUso.CasosTipoGasto
{
    public class EliminarTipoGastoCU : IEliminarTipoGasto
    {
        private ITipoGastoRepositorio _repositorio;
        public EliminarTipoGastoCU(ITipoGastoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public void EliminarTipoGasto(int id)
        {
            _repositorio.Remove(id);
        }
    }
}
