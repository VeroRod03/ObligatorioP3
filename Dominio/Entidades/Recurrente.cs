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

            DateTime fechaInicioMes = new DateTime(anio, (int)mes, 1);
            DateTime fechaFinDeMes = fechaInicioMes.AddMonths(1).AddDays(-1);

            //Primero creamos una fecha a partir del mes y el anio para poder comparar
            //Despues mirando si nuestra fecha "desde" es menor al final de ese mes buscado, contemplamos
            //todos los dias de ese mes, y que tambien pueda ser menor (no excluimos meses anteriores)
            //Igual para el hasta; al ser mayor que el primer dia de ese mes, lo incluye entero,
            //y puede ser posterior a ese mes.
            
            if(Fecha <= fechaFinDeMes && Hasta.Value >= fechaInicioMes )
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
