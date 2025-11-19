using Dominio.Enumerations;
using Dominio.LogicaAplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosPago
{
    public interface IObtenerPagosFiltrados
    {
        public IEnumerable<PagoDTO> ObtenerPagosFiltrados(int mes, int anio);
    }
}
