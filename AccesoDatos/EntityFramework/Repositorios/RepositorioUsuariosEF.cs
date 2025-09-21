using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using Dominio.Exceptions;
using Dominio.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.EntityFramework.Repositorios
{
    public class RepositorioUsuariosEF : IUsuarioRepositorio
    {
        private DominioContext _context;

        public RepositorioUsuariosEF()
        {
            _context = new DominioContext();
        }

        public Usuario Login(string email, string pass)
        {
            foreach (Usuario usuario in _context.Usuarios)
            {
                if (usuario.Email.EmailUsuario == email)
                {
                    if (pass == usuario.Contra)
                    {
                        return usuario;
                    }
                    throw new UsuarioException("Usuario o contraseña incorrecta.");
                }
            }
            throw new UsuarioException("Usuario o contraseña incorrecta.");
        }
        public void Add(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> FindAll()
        {
            throw new NotImplementedException();
        }

        public Usuario FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario obj)
        {
            throw new NotImplementedException();
        }
    }
}
