using Dominio.Enumerations;
using Dominio.Exceptions;
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
        public DateTime? Hasta { get; set; }

        public override double CalcularMontoTotal()
        {
            int cantMeses = (Hasta.Value.Year - Fecha.Year) * 12 + (Hasta.Value.Month - Fecha.Month);
            return cantMeses * Monto;
        }
        public override double CalcularSaldoPendiente()
        {
            if(Fecha > DateTime.Now)
            {
                return CalcularMontoTotal();
            }
            int cantMeses = (Hasta.Value.Year - DateTime.Now.Year) * 12 + (Hasta.Value.Month - DateTime.Now.Month);
            if(cantMeses < 0)
            {
                return 0;
            }
            return (cantMeses + 1) * Monto; // tenemos que incluir el mes en el que estamos parados/termina la suscripcion
        }

        public override DateTime? DevolverFechaHasta()
        {
            return Hasta;
        }

        public override string DevolverRecibo()
        {
            return null;
        }

        public override bool PagoIncluyeFecha(Mes mes, int anio)
        {
            bool incluyeFecha = false;
            //en el caso que sea un pago en el mismo anio, y se verifica que sea el mismo mes
            if (anio == Fecha.Year && anio == Hasta.Value.Year
                && (int)mes >= Fecha.Month && (int)mes <= Hasta.Value.Month)
            {
                incluyeFecha = true;
            }
            /*
            //en el caso de que sea en anios diferentes, pero que la diferencia entre ellos sea mas de un anio
            //por ende, todos los meses estan incluidos 
            else if (anio >= Fecha.Year && anio <= Hasta.Value.Year
                            && Hasta.Value.Year - Fecha.Year > 1)
            {
                incluyeFecha = true; ;
            }
            */
            //en el caso de que sean anios diferentes pero con un solo anio de diferencia (Octubre 2024 - Febrero 2025)
            else if (anio >= Fecha.Year && anio <= Hasta.Value.Year
                    && (((int)mes >= Fecha.Month && (int)mes <= 12)
                            || ((int)mes >= 1 && (int)mes <= Hasta.Value.Month)))

            {
                incluyeFecha = true;
            }
            return incluyeFecha;
        }

        public void Validar()
        {
            if(Hasta == null)
            {
                throw new PagoException("El pago recurrente debe tener una fecha de finalizacion");
            }
            ;
        }
    }
}
