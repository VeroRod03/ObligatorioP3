using Dominio.Entidades;
using Dominio.Enumerations;
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

         public IEnumerable<PagoDTO> ObtenerPagosFiltrados(Mes mes, int anio)
        {
            IEnumerable<Pago> aRetornar = _repositorio.FiltrarPagosPorFecha(mes, anio);
            return aRetornar.Select(p => PagoMapper.ToDTO(p));
        }
    }
}
