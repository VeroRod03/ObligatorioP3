using Dominio.Enumerations;
using Dominio.Exceptions;
using Dominio.Interfaces;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
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
        public string Contra {  get; set; }
        public Email Email {  get; set; }
        public Equipo Equipo { get; set; }
        public RolUsuario Rol {  get; set; }

        public Usuario(NombreCompleto nombreCompleto,string contra, Equipo equipo, RolUsuario rol)
        {
            NombreCompleto = nombreCompleto;
            Contra = contra;
            Equipo = equipo;
            Email = new Email(NombreCompleto);
            Rol = rol;
            Validar();
        }
        public Usuario() { }

        public void Validar()
        {
            NombreCompleto.Validar();
            ValidarContra();
            ValidarEquipo();
        }

        private void ValidarContra()
        {
            if (string.IsNullOrEmpty(Contra))
            {
                throw new UsuarioException("La contraseña no puede ser vacia");
            }
        }
        private void ValidarEquipo()
        {
            if (Equipo == null)
            {
                throw new UsuarioException("El equipo no puede ser nulo");
            }
        }
    }
}
