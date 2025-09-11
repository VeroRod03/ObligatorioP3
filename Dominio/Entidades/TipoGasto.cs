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
    public class TipoGasto : IValidable
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del tipo de gasto es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripcion del tipo de gasto es requerido")]
        public string Descripcion { get; set; }
        public void Validar()
        {
            ValidarNombre();
            ValidarDescripcion();
        }
        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new TipoGastoException("El nombre del tipo de gasto no puede ser vacio.");
            }
        }
        private void ValidarDescripcion()
        {
            if (string.IsNullOrEmpty(Descripcion))
            {
                throw new TipoGastoException("La descripcion del tipo de gasto no puede ser vacia.");
            }
        }
    }
}
