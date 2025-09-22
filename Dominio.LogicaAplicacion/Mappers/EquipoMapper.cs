using Dominio.Entidades;
using Dominio.LogicaAplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.Mappers
{
    public class EquipoMapper
    {
        public static Equipo FromDTO(EquipoDTO dto)
        {
            return new Equipo
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
            };
        }
        public static EquipoDTO ToDTO(Equipo equipo)
        {
            return new EquipoDTO
            {
                Id = equipo.Id,
                Nombre = equipo.Nombre,
            };
        }
    }
}
