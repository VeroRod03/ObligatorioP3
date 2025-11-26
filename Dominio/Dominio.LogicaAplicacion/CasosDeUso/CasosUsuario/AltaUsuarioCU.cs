using Dominio.Entidades;
using Dominio.Exceptions;
using Dominio.InterfacesRepositorio;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.LogicaAplicacion.InterfacesDeCasosDeUso.CasosUsuario;
using Dominio.LogicaAplicacion.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.CasosDeUso.CasosUsuario
{
    public class AltaUsuarioCU : IAltaUsuario
    {
        private IUsuarioRepositorio _repositorio;
        private IEquipoRepositorio _repoEquipo;
        public AltaUsuarioCU(IUsuarioRepositorio repositorio, IEquipoRepositorio repoEquipo)
        {
            _repositorio = repositorio;
            _repoEquipo = repoEquipo;
        }

        public void AgregarUsuario(UsuarioDTO usuariodto)
        {
            Equipo equipo = _repoEquipo.FindById(usuariodto.EquipoId);
            if(equipo == null)
            {
                throw new UsuarioException("No existe un equipo con ese id");
            }
            Usuario usuario = UsuarioMapper.FromDTO(usuariodto);
            while (_repositorio.ExisteEmail(usuario.Email))
            {
                usuario.Email.AgregarNumeroRandom();
            }
            _repositorio.Add(usuario);
        }
    }
}
