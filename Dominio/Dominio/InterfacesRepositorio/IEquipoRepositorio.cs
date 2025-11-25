using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.InterfacesRepositorio
{
    public interface IEquipoRepositorio : IRepositorio<Equipo>
    {
        public IEnumerable<Equipo> FiltrarEquiposPorMonto(double monto);
    }
}
