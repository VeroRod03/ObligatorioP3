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

        public override double CalcularMontoTotal()
        {
            int cantMeses = (Hasta.Year - Desde.Year) * 12 + (Hasta.Month - Desde.Month);
            return cantMeses * Monto;
        }
        public override double CalcularSaldoPendiente()
        {
            if(Desde > DateTime.Now)
            {
                return CalcularMontoTotal();
            }
            int cantMeses = (Hasta.Year - DateTime.Now.Year) * 12 + (Hasta.Month - DateTime.Now.Month);
            if(cantMeses < 0)
            {
                return 0;
            }
            return cantMeses * Monto;
        }
        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
