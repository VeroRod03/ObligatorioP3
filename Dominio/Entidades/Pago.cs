using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public abstract class Pago : IValidable
    {
        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
