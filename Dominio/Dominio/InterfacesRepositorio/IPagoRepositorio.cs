using Dominio.Entidades;
using Dominio.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.InterfacesRepositorio
{
    public interface IPagoRepositorio : IRepositorio<Pago>
    {
        public IEnumerable<Pago> FiltrarPagosPorFecha(int mes, int anio);
    }
}
