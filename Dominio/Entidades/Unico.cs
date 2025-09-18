using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Unico : Pago, IValidable
    {
        public DateTime Fecha { get; set; }
        public string NumRecibo {  get; set; }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
