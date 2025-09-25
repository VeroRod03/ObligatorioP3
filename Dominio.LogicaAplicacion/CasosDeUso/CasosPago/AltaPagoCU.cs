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

namespace Dominio.LogicaAplicacion.CasosDeUso.CasosPago
{
    public class AltaPagoCU : IAltaPago
    {
        private IPagoRepositorio _repositorio;

        public AltaPagoCU(IPagoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public void AgregarPago(PagoDTO pagodto)
        {
            if(pagodto is RecurrenteDTO recurrentedto)
            {
                _repositorio.Add(PagoMapper.ToRecurrente(recurrentedto));

            }
            else
            {
                _repositorio.Add(PagoMapper.ToUnico(pagodto as UnicoDTO));

            }
        }
    }
}
