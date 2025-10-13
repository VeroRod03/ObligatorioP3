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
        //[Required(ErrorMessage = "El numero de recibo del pago es requerido")]
        public string NumRecibo {  get; set; }

        public override DateTime? DevolverFechaHasta()
        {
            return null;
        }

        public override string DevolverRecibo()
        {
            return NumRecibo;
        }

        public override bool PagoIncluyeFecha(Mes mes, int anio)
        {
            throw new NotImplementedException();
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(NumRecibo))
            {
                throw new PagoException("El pago unico debe tener un numero de recibo");
            }
        }
    }
}
