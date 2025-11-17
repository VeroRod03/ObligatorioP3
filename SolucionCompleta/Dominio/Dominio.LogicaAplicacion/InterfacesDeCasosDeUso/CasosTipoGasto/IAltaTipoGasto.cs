using Dominio.LogicaAplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto
{
    public interface IAltaTipoGasto
    {
        public void AgregarTipoGasto(TipoGastoDTO nuevo,int usuarioId);
    }
}
