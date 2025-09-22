using Dominio.Entidades;
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
    public class EditarTipoGastoCU : IEditarTipoGasto
    {
        private ITipoGastoRepositorio _repositorio;
        private IAuditoriaRepositorio _repositorioAuditoria;
        public EditarTipoGastoCU(ITipoGastoRepositorio repositorio, IAuditoriaRepositorio repositorioAuditoria)
        {
            _repositorio = repositorio;
            _repositorioAuditoria = repositorioAuditoria;
        }
        public void EditarTipoGasto(TipoGastoDTO gasto,int? usuarioId)
        {
            _repositorio.Update(TipoGastoMapper.FromDTO(gasto));

            _repositorioAuditoria.Add(new Auditoria
            {
                Accion = "Editar",
                Fecha = DateTime.Today,
                UsuarioId = usuarioId
            });
        }
    }
}
