using Dominio.Entidades;
using Dominio.Enumerations;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del usuario es requerido")]
        public string Nombre {  get; set; }
        [Required(ErrorMessage = "El apellido del usuario es requerido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "La contraseña del usuario es requerida")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string Contra { get; set; }
        [Required(ErrorMessage = "El email del usuario es requerido")]
        public string Email { get; set; }
        public int EquipoId { get; set; } 
        public EquipoDTO? Equipo { get; set; }
        public RolUsuario Rol { get; set; }

        //par manejar el token
        public string Token { get; set; }
    }
}
