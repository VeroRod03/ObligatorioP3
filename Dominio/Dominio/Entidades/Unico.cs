using Dominio.Enumerations;
using Dominio.Exceptions;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Unico : Pago, IValidable
    {
        public string? NumRecibo {  get; set; }
        public Unico() { }
        public override DateTime? DevolverFechaHasta()
        {
            return null;
        }

        public override string DevolverRecibo()
        {
            return NumRecibo;
        }

        public override bool PagoIncluyeFecha(int mes, int anio)
        {
            bool incluyeFecha = false;
            if (Fecha.Month == mes && Fecha.Year == anio)
            {
                incluyeFecha = true;
            }
            return incluyeFecha;
        }

        public override void Validar()
        {
            base.Validar();
            ValidarNumRecibo();
        }
        private void ValidarNumRecibo()
        {
            if (string.IsNullOrEmpty(NumRecibo))
            {
                throw new PagoException("El pago unico debe tener un numero de recibo");
            }
        }
    }
}
