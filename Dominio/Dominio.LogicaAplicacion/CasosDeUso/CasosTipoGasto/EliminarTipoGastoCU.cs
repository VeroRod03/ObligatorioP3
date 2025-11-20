using Dominio.Entidades;
using Dominio.Exceptions;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;
using Dominio.LogicaAplicacion.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.CasosDeUso.CasosTipoGasto
{
    public class EliminarTipoGastoCU : IEliminarTipoGasto
    {
        private ITipoGastoRepositorio _repositorio;
        private IAuditoriaRepositorio _repositorioAuditoria;
        private IPagoRepositorio _repositorioPago;
        public EliminarTipoGastoCU(
            ITipoGastoRepositorio repositorio, 
            IAuditoriaRepositorio repositorioAuditoria,
            IPagoRepositorio repositorioPago
            )
        {
            _repositorio = repositorio;
            _repositorioAuditoria = repositorioAuditoria;
            _repositorioPago = repositorioPago;
        }
        public void EliminarTipoGasto(int id, int usuarioId)
        {
            TipoGastoDTO aBorrar = TipoGastoMapper.ToDTO(_repositorio.FindById(id));
            if (aBorrar == null)
            {
                throw new TipoGastoException("El contenido con id " + id + " no existe o ya fue eliminado");
            }
            
            if (_repositorioPago.TipoGastoEnUso(id))
            {
                //excepcion creada especificamente para error 409; conflictos
                throw new OperacionConflictivaException("El tipo de gasto esta en uso.");
            }
            _repositorio.Remove(id);
            _repositorioAuditoria.Add(new Auditoria
            {
                Accion = "Eliminar",
                Fecha = DateTime.Today,
                UsuarioId = usuarioId
            });
        }
    }
}
