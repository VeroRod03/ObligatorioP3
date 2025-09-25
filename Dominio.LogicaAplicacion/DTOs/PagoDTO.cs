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
        public TipoGastoDTO TipoGasto { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public UsuarioDTO Usuario { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }

    }
}
