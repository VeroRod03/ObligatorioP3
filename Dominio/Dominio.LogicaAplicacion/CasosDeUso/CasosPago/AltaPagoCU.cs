using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosPago;
using Dominio.LogicaAplicacion.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Exceptions;
using Dominio.Enumerations;

namespace Dominio.LogicaAplicacion.CasosDeUso.CasosPago
{
    public class AltaPagoCU : IAltaPago
    {
        private IPagoRepositorio _repositorio;
        private ITipoGastoRepositorio _repoGasto;

        public AltaPagoCU(IPagoRepositorio repositorio, ITipoGastoRepositorio repoGasto)
        {
            _repositorio = repositorio;
            _repoGasto = repoGasto;
        }
        public void AgregarPago(PagoDTO pagodto)
        {
            if (pagodto == null)
            {
                throw new PagoException("Datos incorrectos");
            }
            TipoGasto gasto = _repoGasto.FindById(pagodto.TipoGastoId);
            if(gasto == null)
            {
                throw new PagoException("No existe un tipo de gasto con ese id");
            }
            if ((int)pagodto.MetodoPago != 1 && (int)pagodto.MetodoPago != 2)
            {
                throw new PagoException("El metodo de pago solo puede ser Credito (1) o Efectivo (2)");
            }
            if (pagodto.TipoPago.ToLower() == "unico")
            {
                _repositorio.Add(PagoMapper.ToUnico(pagodto));
            }else if (pagodto.TipoPago.ToLower() == "recurrente")
            {
                _repositorio.Add(PagoMapper.ToRecurrente(pagodto));

            }
            else {
                throw new PagoException("El tipo de pago solo accepta los valores Unico o Recurrente");
            }
        }
    }
}
