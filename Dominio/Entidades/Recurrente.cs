using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Recurrente : Pago, IValidable
    {
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
