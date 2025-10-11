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

    public class ObtenerPagoPorIdCU : IObtenerPagoPorId
    {
        private IPagoRepositorio _repositorio;

        public ObtenerPagoPorIdCU(IPagoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public PagoDTO ObtenerPagoPorId(int id)
        {
            return PagoMapper.ToDTO(_repositorio.FindById(id));
        }
    }
}
