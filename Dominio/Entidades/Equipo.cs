using Dominio.Exceptions;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Equipo : IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public void Validar()
        {
            ValidarNombre();
        }
        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new EquipoException("El nombre del equipo no puede ser vacio");
            }
        }
    }
}
