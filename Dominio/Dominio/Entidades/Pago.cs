using Dominio.Enumerations;
using Dominio.Exceptions;
using Dominio.Interfaces;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public abstract class Pago : IValidable
    {
        public int Id { get; set; }
        [ForeignKey(nameof(TipoGasto))] public int TipoGastoId { get; set; }
        public TipoGasto TipoGasto { get; set; }
        public MetodoPago MetodoPago { get; set; }
        [ForeignKey(nameof(Usuario))] public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public double Monto { get; set; }

        public DateTime Fecha { get; set; }

        public Pago() { }

        public virtual double CalcularMontoTotal()
        {
            return Monto;
        }

        public virtual double CalcularSaldoPendiente()
        {
            return 0;
        }

        public abstract bool PagoIncluyeFecha(int mes, int anio);

        public abstract DateTime? DevolverFechaHasta();

        public abstract string DevolverRecibo();

        public virtual void Validar() 
        {
            ValidarTipoGasto();
            ValidarMetodoPago();
            ValidarUsuario();
            ValidarDescripcion();
            ValidarMonto();
            ValidarFecha();

        }

        private void ValidarTipoGasto()
        {
            if(TipoGastoId == null)
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
            if (UsuarioId == null)
            {
                throw new PagoException("El usuario no puede ser nulo");
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
                throw new PagoException("El monto no puede ser menor a 0");
            }
        }

        private void ValidarFecha()
        {
            if(Fecha > DateTime.Today)
            {
                throw new PagoException("La fecha no puede ser posterior a la fecha actual");
            }
        }



    }
}
