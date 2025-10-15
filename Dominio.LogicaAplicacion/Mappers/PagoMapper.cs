using Azure;
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
    public class PagoMapper
    {
        public static PagoDTO ToDTO(Pago pago)
        {
            if(pago == null)
            {
                throw new PagoException("El pago esta nulo");
            }
            UsuarioDTO usuario = null;
            if(pago.Usuario != null)
            {
                usuario = UsuarioMapper.ToDTO(pago.Usuario);
            }
            TipoGastoDTO gasto = null;
            if (pago.TipoGasto != null)
            {
                gasto = TipoGastoMapper.ToDTO(pago.TipoGasto);
            }

            return new PagoDTO
            {
                Id = pago.Id,
                TipoGastoId = pago.TipoGastoId,
                TipoGasto = gasto,
                MetodoPago = pago.MetodoPago,
                Descripcion = pago.Descripcion,
                UsuarioId = pago.UsuarioId,
                Usuario = usuario,
                Monto = pago.Monto,
                MontoTotal = pago.CalcularMontoTotal(),
                SaldoPendiente = pago.CalcularSaldoPendiente(),
                TipoPago = pago.GetType().Name,
                Hasta = pago.DevolverFechaHasta(),
                Fecha = pago.Fecha,
                NumRecibo = pago.DevolverRecibo()
            };
        }

        public static Recurrente ToRecurrente(PagoDTO dto)
        {
            Usuario usuario = null;
            if (dto.Usuario != null)
            {
                usuario = UsuarioMapper.FromDTO(dto.Usuario);
            }
            TipoGasto gasto = null;
            if (dto.TipoGasto != null)
            {
                gasto = TipoGastoMapper.FromDTO(dto.TipoGasto);
            }
            return new Recurrente
            {
                Id = dto.Id,
                TipoGastoId=dto.TipoGastoId,
                TipoGasto = gasto,
                MetodoPago = dto.MetodoPago,
                Descripcion = dto.Descripcion,
                UsuarioId = dto.UsuarioId,
                Usuario = usuario,
                Monto = dto.Monto,
                Fecha = dto.Fecha,
                Hasta = dto.Hasta,
            };
        }

        public static Unico ToUnico(PagoDTO dto)
        {
            Usuario usuario = null;
            if (dto.Usuario != null)
            {
                usuario = UsuarioMapper.FromDTO(dto.Usuario);
            }
            TipoGasto gasto = null;
            if (dto.TipoGasto != null)
            {
                gasto = TipoGastoMapper.FromDTO(dto.TipoGasto);
            }
            return new Unico
            {
                Id = dto.Id,
                TipoGastoId = dto.TipoGastoId,
                TipoGasto = gasto,
                MetodoPago = dto.MetodoPago,
                Descripcion = dto.Descripcion,
                UsuarioId= dto.UsuarioId,
                Usuario = usuario,
                Monto = dto.Monto,
                Fecha = dto.Fecha,
                NumRecibo = dto.NumRecibo
            };
        }
    }
}
