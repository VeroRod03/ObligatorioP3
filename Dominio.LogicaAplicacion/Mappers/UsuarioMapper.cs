using Azure;
using Dominio.Entidades;
using Dominio.Exceptions;
using Dominio.LogicaAplicacion.DTOs;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.LogicaAplicacion.Mappers
{
    public class UsuarioMapper
    {
        public static Usuario FromDTO(UsuarioDTO dto)
        {
            if (dto == null)
            {
                throw new UsuarioException("El usuario esta nulo");
            }
            Equipo equipo = null;
            if (dto.Equipo != null)
            {
                equipo = EquipoMapper.FromDTO(dto.Equipo);
            }
            Email email = null;
            if(dto.Email != null)
            {
                email = new Email(dto.Email);
            }
            else
            {
                email = new Email(new NombreCompleto(dto.Nombre, dto.Apellido));

            }
            return new Usuario
                {
                    Id = dto.Id,
                    NombreCompleto = new NombreCompleto(dto.Nombre, dto.Apellido),
                    Contra = dto.Contra,
                    Email = email,
                    EquipoId = dto.EquipoId,
                    Equipo = equipo,
                    Rol = dto.Rol
                };
        }
        public static UsuarioDTO ToDTO(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new UsuarioException("El usuario esta nulo");
            }
            EquipoDTO equipoDTO = null;
            if (usuario.Equipo != null)
            {
                equipoDTO = EquipoMapper.ToDTO(usuario.Equipo);
            }
            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.NombreCompleto.Nombre,
                Apellido = usuario.NombreCompleto.Apellido,
                Contra = usuario.Contra,
                Email = usuario.Email.EmailUsuario,
                EquipoId = usuario.EquipoId,
                Equipo = equipoDTO,
                Rol = usuario.Rol
            };
        }
    }
}
