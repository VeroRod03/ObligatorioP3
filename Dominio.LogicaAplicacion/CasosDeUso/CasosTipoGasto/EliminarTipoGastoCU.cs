using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;
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
        public EliminarTipoGastoCU(ITipoGastoRepositorio repositorio, IAuditoriaRepositorio repositorioAuditoria)
        {
            _repositorio = repositorio;
            _repositorioAuditoria = repositorioAuditoria;
        }
        public void EliminarTipoGasto(int id, int? usuarioId)
        {
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
