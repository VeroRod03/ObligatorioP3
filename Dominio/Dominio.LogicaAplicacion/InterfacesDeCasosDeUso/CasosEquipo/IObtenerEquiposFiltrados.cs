using Dominio.LogicaAplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosEquipo
{
    public interface IObtenerEquiposFiltrados
    {
        public IEnumerable<EquipoDTO> ObtenerEquiposFiltrados(double monto);
    }
}
