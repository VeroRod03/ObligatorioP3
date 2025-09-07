using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Exceptions
{
    class EquipoException : Exception
    {
        public EquipoException() { }
        public EquipoException(string mensaje) : base(mensaje) { }
        public EquipoException(string mensaje, Exception ex) : base(mensaje, ex) { }
    }
}
