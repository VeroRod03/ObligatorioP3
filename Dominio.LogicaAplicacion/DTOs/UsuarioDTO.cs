using Dominio.Entidades;
using Dominio.Enumerations;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre {  get; set; }
        public string Apellido { get; set; }
        public string Contra { get; set; }
        public string Email { get; set; }
        public int EquipoId { get; set; } //?
        public EquipoDTO? Equipo { get; set; }
        public RolUsuario Rol { get; set; }
    }
}
