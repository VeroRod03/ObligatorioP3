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
    public class ObtenerPagosCU : IObtenerPagos
    {
        private IPagoRepositorio _repositorio;
        public ObtenerPagosCU(IPagoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public IEnumerable<PagoDTO> ObtenerPagos()
        {
            return _repositorio.FindAll()
                    .Select(pago=>PagoMapper.ToDTO(pago));
        }
    }
}
