using Dominio.Entidades;
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
            return new TipoGasto
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
            };
        }
        public static TipoGastoDTO ToDTO(TipoGasto gasto)
        {
            return new TipoGastoDTO
            {
                Id = gasto.Id,
                Nombre = gasto.Nombre,
                Descripcion = gasto.Descripcion,
            };
        }
    }
}
