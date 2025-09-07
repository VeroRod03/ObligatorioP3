using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.EnMemoria
{
    public class RepositorioRecurrentes : IRecurrenteRepositorio
    {
        private List<Recurrente> _recurrentes = new List<Recurrente>();

        public RepositorioRecurrentes()
        {
        }
        public bool Add(Recurrente obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recurrente> FindAll()
        {
            throw new NotImplementedException();
        }

        public Recurrente FindById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Recurrente obj)
        {
            throw new NotImplementedException();
        }
    }
}
