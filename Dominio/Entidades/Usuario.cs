using Dominio.Enumerations;
using Dominio.Exceptions;
using Dominio.Interfaces;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio.Entidades
{
    public class Usuario : IValidable
    {
        public int Id {  get; set; }
        public NombreCompleto NombreCompleto { get; set; }
        //[Required(ErrorMessage = "La contraseña del usuario es requerido")]
        //[Range(8, int.MinValue, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string Contra {  get; set; }
        public Email Email {  get; set; }
        [ForeignKey(nameof(Equipo))] public int EquipoId { get; set; }
        public Equipo Equipo { get; set; }
        public RolUsuario Rol {  get; set; }

        public Usuario() { }

        public void Validar()
        {
            NombreCompleto.Validar();
            ValidarContra();
            ValidarEquipo();
            ValidarRol();
        }

        private void ValidarContra()
        {
            if (Contra.Length < 8)
            {
                throw new UsuarioException("La contraseña debe tener al menos 8 caracteres");
            }
        }
        private void ValidarEquipo()
        {
            if (EquipoId == null)
            {
                throw new UsuarioException("El equipo no puede ser nulo");
            }
        }
        private void ValidarRol()
        {
            if (Rol == null)
            {
                throw new UsuarioException("El rol no puede ser nulo");
            }
        }
    }
}
