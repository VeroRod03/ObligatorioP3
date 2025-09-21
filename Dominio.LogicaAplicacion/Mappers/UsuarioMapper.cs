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
                Email = new Email { EmailUsuario = dto.Email},
                Equipo = dto.Equipo,
                Rol = dto.Rol
            };
        }
        public static UsuarioDTO ToDTO(Usuario usuario)
        {
            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.NombreCompleto.Nombre,
                Apellido = usuario.NombreCompleto.Apellido,
                Contra = usuario.Contra,
                Email = usuario.Email.EmailUsuario,
                Equipo = usuario.Equipo,
                Rol = usuario.Rol
            };
        }
    }
}
