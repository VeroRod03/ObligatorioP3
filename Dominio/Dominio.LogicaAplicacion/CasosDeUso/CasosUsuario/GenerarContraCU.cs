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
    public class GenerarContraCU : IGenerarContra
    {
        private IUsuarioRepositorio _repositorio;
        public GenerarContraCU(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public UsuarioDTO GenerarContra(int usuarioId)
        {
            Usuario aModificar = _repositorio.FindById(usuarioId);
            if (aModificar == null)
            {
                throw new UsuarioException("El usuario no existe");
            }

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvxwyz1234567890";

            string contraNueva = "";

            Random random = new Random();

            for (int i = 0; i <= 7; i++)
            {
                contraNueva += chars[random.Next(chars.Length)]; //tomamos el largo de chars, generamos un numero dentro de ese rango, y le asignamos a la i de contraNueva el caracter de chars en esa posicion random
            }
            
            aModificar.Contra = contraNueva;
            _repositorio.Update(aModificar);
            return UsuarioMapper.ToDTO(aModificar);
        }
    }
}