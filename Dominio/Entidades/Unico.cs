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
        public string NumRecibo {  get; set; }

        public override DateTime? DevolverFechaHasta()
        {
            return null;
        }

        public override string DevolverRecibo()
        {
            return NumRecibo;
        }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
