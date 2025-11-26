using Dominio.LogicaAplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosAuditoria
{
    public interface IObtenerAuditoriasTipoGasto
    {
        public IEnumerable<AuditoriaDTO> ObtenerAuditoriasTipoGasto(int id);
    }
}