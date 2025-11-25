using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosTipoGasto;
using Dominio.LogicaAplicacion.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.CasosDeUso.CasosTipoGasto
{
    public class AltaTipoGastoCU : IAltaTipoGasto
    {
        private ITipoGastoRepositorio _repositorio;
        private IAuditoriaRepositorio _repositorioAuditoria;


        public AltaTipoGastoCU(ITipoGastoRepositorio repositorio, IAuditoriaRepositorio repositorioAuditoria)
        {
            _repositorio = repositorio;
            _repositorioAuditoria = repositorioAuditoria; 
        }

        public void AgregarTipoGasto(TipoGastoDTO nuevo, int usuarioId)
        {
            TipoGasto tipoGasto = TipoGastoMapper.FromDTO(nuevo); //instanciamos el tipoGasto para poder acceder a su id
            _repositorio.Add(tipoGasto);
            _repositorioAuditoria.Add(new Auditoria
            {
                Accion = "Alta",
                Fecha = DateTime.Today,
                UsuarioId = usuarioId,
                TipoGastoId = tipoGasto.Id 
            });
        }
    }
}
