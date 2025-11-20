using Dominio.Entidades;
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
        public AltaUsuarioCU(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public void AgregarUsuario(UsuarioDTO usuariodto)
        {
            Usuario usuario = UsuarioMapper.FromDTO(usuariodto);
            while (_repositorio.ExisteEmail(usuario.Email))
            {
                usuario.Email.AgregarNumeroRandom();
            }
            _repositorio.Add(usuario);
        }
    }
}
