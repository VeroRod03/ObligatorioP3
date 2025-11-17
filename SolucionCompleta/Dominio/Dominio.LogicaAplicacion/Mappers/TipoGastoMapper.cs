using Dominio.Entidades;
using Dominio.Exceptions;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.Mappers
{
    public class TipoGastoMapper
    {
        
        public static TipoGasto FromDTO(TipoGastoDTO dto)
        {
            if (dto == null)
            {
                throw new TipoGastoException("El tipo de gasto esta nulo");
            }
            return new TipoGasto
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
            };
        }
        public static TipoGastoDTO ToDTO(TipoGasto gasto)
        {
            if (gasto == null)
            {
                throw new TipoGastoException("El tipo de gasto esta nulo");
            }
            return new TipoGastoDTO
            {
                Id = gasto.Id,
                Nombre = gasto.Nombre,
                Descripcion = gasto.Descripcion,
            };
        }
    }
}
