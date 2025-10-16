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

        public RepositorioUsuariosEF(DominioContext context)
        {
            _context = context;
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
            try
            {
                obj.Validar();
                while (ExisteEmail(obj.Email))
                {
                    obj.Email.AgregarNumeroRandom();
                }
                _context.Usuarios.Add(obj);
                _context.SaveChanges();
            }
            catch (UsuarioException ue)
            {
                throw ue;
            }
            catch (Exception ex)
            {
                throw new UsuarioException("Hubo un error: ",ex);
            }
        }
        public bool ExisteEmail(Email email)
        {
            return _context.Usuarios
                    .Any(usuario => usuario.Email.EmailUsuario == email.EmailUsuario);
        }

        public IEnumerable<Usuario> FindAll()
        {
            return _context.Usuarios.Include(content => content.Equipo);

        }

        public IEnumerable<Usuario> FiltrarUsuariosPorMonto(double monto)
        {
            return _context.Pagos
                    .Where(pago => pago.Monto >= monto)
                    .Select(pago => pago.Usuario)
                    .Distinct();
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
