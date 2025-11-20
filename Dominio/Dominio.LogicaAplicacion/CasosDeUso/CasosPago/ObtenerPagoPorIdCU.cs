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

    public class ObtenerPagoPorIdCU : IObtenerPagoPorId
    {
        private IPagoRepositorio _repositorio;

        public ObtenerPagoPorIdCU(IPagoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public PagoDTO ObtenerPagoPorId(int id)
        {
            PagoDTO aRetornar = PagoMapper.ToDTO(_repositorio.FindById(id));
            if(aRetornar == null)
            {
                throw new PagoException("No hay ningun pago con ese id");
            }

            return aRetornar;
        }
    }
}
