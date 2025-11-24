using Dominio.Exceptions;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Equipo : IValidable
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public Equipo() { }
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
