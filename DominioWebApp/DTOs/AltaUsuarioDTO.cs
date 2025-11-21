using System.ComponentModel.DataAnnotations;

namespace DominioWebApp.DTOs
{
    public class AltaUsuarioDTO
    {
        [Required(ErrorMessage = "El nombre del usuario es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido del usuario es requerido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "La contraseña del usuario es requerida")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string Contra { get; set; }
        [Required(ErrorMessage = "El email del usuario es requerido")]
        public int EquipoId { get; set; }
        public int Rol { get; set; }
    }
}
