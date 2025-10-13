using Dominio.Entidades;
using Dominio.Exceptions;
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
            if (dto == null)
            {
                throw new EquipoException("El equipo esta nulo");
            }
            return new Equipo
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
            };
        }
        public static EquipoDTO ToDTO(Equipo equipo)
        {
            if (equipo == null)
            {
                throw new EquipoException("El equipo esta nulo");
            }
            return new EquipoDTO
            {
                Id = equipo.Id,
                Nombre = equipo.Nombre,
            };
        }
    }
}
