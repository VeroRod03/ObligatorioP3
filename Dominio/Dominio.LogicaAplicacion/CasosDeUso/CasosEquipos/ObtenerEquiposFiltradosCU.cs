using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosEquipo;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosUsuario;
using Dominio.LogicaAplicacion.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.CasosDeUso.CasosEquipos
{
    public class ObtenerEquiposFiltradosCU : IObtenerEquiposFiltrados
    {
        private IEquipoRepositorio _repositorio;
        public ObtenerEquiposFiltradosCU(IEquipoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public IEnumerable<EquipoDTO> ObtenerEquiposFiltrados(double monto)
        {
            IEnumerable<Equipo> aRetornar = _repositorio.FiltrarEquiposPorMonto(monto);
            return aRetornar.Select(equipo => EquipoMapper.ToDTO(equipo));
        }
    }
}
