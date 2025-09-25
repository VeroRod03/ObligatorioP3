using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.DTOs
{
    public class UnicoDTO : PagoDTO
    {
        public DateTime Fecha { get; set; }
        public string NumRecibo { get; set; }
    }
}
