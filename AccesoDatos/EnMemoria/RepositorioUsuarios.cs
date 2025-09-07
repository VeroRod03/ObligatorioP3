using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.EnMemoria
{
    public class RepositorioUsuarios : IUsuarioRepositorio
    {
        private List<Usuario> _usuarios = new List<Usuario>();

        public RepositorioUsuarios()
        {
        }

        public bool Add(Usuario obj)
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

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Usuario obj)
        {
            throw new NotImplementedException();
        }
    }
}
