using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.DTOs
{
    public class RecurrenteDTO : PagoDTO
    {
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }

    }
}
