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
            Usuario logueado = _context.Usuarios.Where(
                            user =>
                            user.Email.EmailUsuario == email
                            && user.Contra == pass
                        ).FirstOrDefault();

            if (logueado == null)
            {
                throw new UsuarioException("Usuario o contraseña incorrecta.");
            }

            return logueado;
        }
        public void Add(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> FindAll()
        {
            return _context.Usuarios.Include(content => content.Equipo);

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
