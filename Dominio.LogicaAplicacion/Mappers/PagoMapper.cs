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
            return new PagoDTO
            {
                Id = pago.Id,
                TipoGastoId=pago.TipoGastoId,
                TipoGasto  = TipoGastoMapper.ToDTO(pago.TipoGasto),
                MetodoPago = pago.MetodoPago,
                Descripcion = pago.Descripcion,
                Usuario = UsuarioMapper.ToDTO(pago.Usuario),
                Monto = pago.Monto

            };
        }

        public static Recurrente ToRecurrente(PagoDTO dto)
        {
            return new Recurrente
            {
                Id = dto.Id,
                TipoGastoId=dto.TipoGastoId,
                TipoGasto = TipoGastoMapper.FromDTO(dto.TipoGasto),
                MetodoPago = dto.MetodoPago,
                Descripcion = dto.Descripcion,
                Usuario = UsuarioMapper.FromDTO(dto.Usuario),
                Monto = dto.Monto,
                Desde = dto.Desde,
                Hasta = dto.Hasta
            };
        }

        public static Unico ToUnico(PagoDTO dto)
        {
            return new Unico
            {
                Id = dto.Id,
                TipoGastoId = dto.TipoGastoId,
                TipoGasto = TipoGastoMapper.FromDTO(dto.TipoGasto),
                MetodoPago = dto.MetodoPago,
                Descripcion = dto.Descripcion,
                Usuario = UsuarioMapper.FromDTO(dto.Usuario),
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
