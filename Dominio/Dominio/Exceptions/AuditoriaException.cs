using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Exceptions
{
    public class AuditoriaException : Auditoria
    {
        public AuditoriaException() { }
        public AuditoriaException(string mensaje) : base(mensaje) { }
        public AuditoriaException(string mensaje, Exception ex) : base(mensaje, ex) { }
    }
}