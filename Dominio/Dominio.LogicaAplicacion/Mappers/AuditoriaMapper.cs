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
    public class AuditoriaMapper
    {
        public static AuditoriaDTO ToDTO(Auditoria auditoria)
        {
            if (auditoria == null)
            {
                throw new AuditoriaException("La auditoria esta nula");
            }
            return new AuditoriaDTO
            {
                Id = auditoria.Id,
                Accion = auditoria.Accion,
                Fecha = auditoria.Fecha,
                UsuarioId = auditoria.UsuarioId,
                TipoGastoId = auditoria.TipoGastoId
            };
        }
    }
}