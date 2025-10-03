using Azure;
using Dominio.Entidades;
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
                TipoGastoId=pago.TipoGastoId,
                TipoGasto  = gasto,
                MetodoPago = pago.MetodoPago,
                Descripcion = pago.Descripcion,
                UsuarioId = pago.UsuarioId,
                Usuario = usuario,
                Monto = pago.Monto

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
                Desde = dto.Desde,
                Hasta = dto.Hasta
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

        /*
         public static RecurrenteDTO ToRecurrenteDTO(Recurrente recurrente)
        {
            return new RecurrenteDTO
            {
                Id = recurrente.Id,
                TipoGasto = TipoGastoMapper.ToDTO(recurrente.TipoGasto),
                MetodoPago = recurrente.MetodoPago,
                Descripcion = recurrente.Descripcion,
                Usuario = UsuarioMapper.ToDTO(recurrente.Usuario),
                Monto = recurrente.Monto,
                Desde = recurrente.Desde,
                Hasta = recurrente.Hasta
            };
        }*/
    }
}
