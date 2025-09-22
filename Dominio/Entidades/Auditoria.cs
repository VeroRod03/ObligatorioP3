using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Auditoria
    {
        public int Id { get; set; }
        public string Accion { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }
    }
}
