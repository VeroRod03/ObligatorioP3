using Dominio.Exceptions;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Auditoria : IValidable
    {
        public int Id { get; set; }
        public string Accion { get; set; }
        public DateTime Fecha { get; set; }
        public int? UsuarioId { get; set; }

        public Auditoria() { }

        //Estas validaciones estan pensadas en caso de necesitarlas en el futuro
        //Actualmente al crear instancias de auditorias manualmente nos aseguramos que los atributos sean correctos

        public void Validar()
        {
            ValidarAccion();
            ValidarFecha();
            ValidarUsuario();
        }

        private void ValidarAccion()
        {
            if (string.IsNullOrEmpty(Accion))
            {
                throw new TipoGastoException("La accion de la auditoria no puede estar vacia");
            }
        }

        private void ValidarFecha()
        {
            if (Fecha != DateTime.Today)
            {
                throw new TipoGastoException("La fecha de la auditoria debe ser la fecha actual");
            }
        }

        private void ValidarUsuario()
        {
            if (UsuarioId ==  null)
            {
                throw new TipoGastoException("El usuario asignado a la auditoria no puede ser nulo");
            }
        }
    }
}
