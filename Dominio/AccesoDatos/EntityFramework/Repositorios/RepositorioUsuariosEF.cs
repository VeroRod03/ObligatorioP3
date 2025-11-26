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
            return _context.Usuarios.Where(
                                user =>
                                user.Email.EmailUsuario == email
                                && user.Contra == pass
                            ).FirstOrDefault();
        }

        public void Add(Usuario obj)
        {
            try
            {
                obj.Validar();
                _context.Usuarios.Add(obj);
                _context.SaveChanges();
            }
            catch (UsuarioException ue)
            {
                throw ue;
            }
            catch (Exception ex)
            {
                throw new UsuarioException($"Hubo un error: {ex.Message}");
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
                    .Include(pago => pago.Usuario)
                    .ThenInclude(usuario => usuario.Equipo)
                    .Select(pago => pago.Usuario)
                    .Distinct();
        }

        public Usuario FindById(int id)
        {
            return _context.Usuarios
                    .Where(u => u.Id == id)
                    .FirstOrDefault();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario obj)
        {
            try
            {
                obj.Validar();

                //cambio para evitar error de tracking
                Usuario aModificar = _context.Usuarios.Find(obj.Id);
                aModificar.Contra = obj.Contra;

                _context.Usuarios.Update(aModificar);
                _context.SaveChanges();
            }
            catch (UsuarioException uex)
            {
                throw uex;
            }
            catch (Exception ex)
            {
                throw new UsuarioException($"Hubo un error: {ex.Message}");
            }
        }
    }
}
