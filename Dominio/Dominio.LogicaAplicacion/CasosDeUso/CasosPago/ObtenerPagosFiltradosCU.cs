using Dominio.Entidades;
using Dominio.Enumerations;
using Dominio.Exceptions;
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
    public class ObtenerPagosFiltradosCU : IObtenerPagosFiltrados
    {
        private IPagoRepositorio _repositorio;

        public ObtenerPagosFiltradosCU(IPagoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<PagoDTO> ObtenerPagosFiltrados(int mes, int anio)
        {
            if (mes == 0 && anio == 0)
                return _repositorio.FindAll().Select(PagoMapper.ToDTO); 

            if (mes == 0)
                throw new PagoException("Debe seleccionar un mes");

            if (anio == 0)
                throw new PagoException("Debe seleccionar un año");

            IEnumerable<Pago> pagos = _repositorio.FiltrarPagosPorFecha(mes, anio);
            return pagos.Select(PagoMapper.ToDTO);
        }

    }
}
