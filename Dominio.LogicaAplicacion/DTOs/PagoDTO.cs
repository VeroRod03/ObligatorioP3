using Dominio.Entidades;
using Dominio.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.DTOs
{
    public class PagoDTO
    {
        public int Id { get; set; }
        public int? TipoGastoId { get; set; }
        public TipoGastoDTO TipoGasto { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioDTO Usuario { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
        public string TipoPago { get; set; }

        //recurrente
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }

        //unico
        public DateTime Fecha { get; set; }
        public string NumRecibo { get; set; }

    }
}
