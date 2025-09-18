using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Exceptions;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dominio.ValueObjects
{
    [Owned]
    public class NombreCompleto : IValidable
    {
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }

        public NombreCompleto(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
            Validar();
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(this.Nombre)
                || this.Nombre.Length < 1
                || string.IsNullOrEmpty(this.Apellido)
                || this.Apellido.Length < 1)
            {
                throw new UsuarioException("El nombre completo no puede ser vacio");
            }
        }
    }
}
