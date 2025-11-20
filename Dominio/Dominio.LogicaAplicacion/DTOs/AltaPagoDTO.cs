using Dominio.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.DTOs
{
    public class AltaPagoDTO
    {
        public int TipoGastoId { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "La descripcion del pago es requerida")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El monto del pago es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El monto no puede ser negativo")]
        public double Monto { get; set; }
        [Required(ErrorMessage = "La fecha del pago es requerida")]
        public DateTime Fecha { get; set; }


        //extras
        public string TipoPago { get; set; }
        public double MontoTotal { get; set; }
        public double SaldoPendiente { get; set; }

        //recurrente
        [Required(ErrorMessage = "La fecha hasta del pago recurrente es requerida")]
        public DateTime? Hasta { get; set; }

        //unico
        [Required(ErrorMessage = "El numero de recibo del pago unico es requerido")]
        public string NumRecibo { get; set; }

    }
}
