using Dominio.Entidades;
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
            return new Usuario
            {
                Id = dto.Id,
                NombreCompleto = new NombreCompleto(dto.Nombre,dto.Apellido),
                Contra = dto.Contra,
                Email = new Email (dto.Email),
                EquipoId = dto.EquipoId,
                Equipo = EquipoMapper.FromDTO(dto.Equipo),
                Rol = dto.Rol
            };
        }
        public static UsuarioDTO ToDTO(Usuario usuario)
        {
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
