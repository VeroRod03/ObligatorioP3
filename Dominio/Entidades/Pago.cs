using Dominio.Enumerations;
using Dominio.Exceptions;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public abstract class Pago : IValidable
    {
        public int Id { get; set; }
        //[ForeignKey(nameof(TipoGasto))] public int TipoGastoId { get; set; }
        public TipoGasto TipoGasto { get; set; }
        public MetodoPago MetodoPago { get; set; }
        //[ForeignKey(nameof(Usuario))] public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        //[ForeignKey(nameof(Equipo))] public int EquipoId { get; set; }
        public Equipo Equipo { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }

        public Pago() { }

        public void Validar()
        {
            ValidarTipoGasto();
            ValidarMetodoPago();
            ValidarUsuario();
            ValidarEquipo();
            ValidarDescripcion();
            ValidarMonto();

        }

        private void ValidarTipoGasto()
        {
            if(TipoGasto == null)
            {
                throw new PagoException("El tipo de gasto no puede ser nulo");
            }
        }
        private void ValidarMetodoPago()
        {
            if (MetodoPago == null)
            {
                throw new PagoException("El metodo de pago no puede ser nulo");
            }
        }
        private void ValidarUsuario()
        {
            if (Usuario == null)
            {
                throw new PagoException("El usuario no puede ser nulo");
            }
        }
        private void ValidarEquipo()
        {
            if (Equipo == null)
            {
                throw new PagoException("El equipo no puede ser nulo");
            }
        }
        private void ValidarDescripcion()
        {
            if (string.IsNullOrEmpty(Descripcion))
            {
                throw new PagoException("La descripcion no puede ser vacia");
            }
        }
        private void ValidarMonto()
        {
            if (Monto <= 0)
            {
                throw new PagoException("El monto no puede ser mayor a 0");
            }
        }

    }
}
