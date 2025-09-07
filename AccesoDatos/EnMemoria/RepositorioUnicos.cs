using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.EnMemoria
{
    public class RepositorioUnicos : IUnicoRepositorio
    {

        private List<Unico> _unicos = new List<Unico>();

        public RepositorioUnicos()
        {
        }
        public bool Add(Unico obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Unico> FindAll()
        {
            throw new NotImplementedException();
        }

        public Unico FindById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Unico obj)
        {
            throw new NotImplementedException();
        }
    }
}
