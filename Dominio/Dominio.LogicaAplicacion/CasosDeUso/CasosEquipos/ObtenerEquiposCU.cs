using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosEquipo;
using Dominio.LogicaAplicacion.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.CasosDeUso.CasosEquipos
{
    public class ObtenerEquiposCU : IObtenerEquipos
    {
        private IEquipoRepositorio _repositorio;
        public ObtenerEquiposCU(IEquipoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<EquipoDTO> ObtenerEquipos()
        {
            IEnumerable<Equipo> aRetornar = _repositorio.FindAll();
            return aRetornar
                    .Select(equipo => EquipoMapper.ToDTO(equipo));
        }
    }
}
