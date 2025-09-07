using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.EnMemoria
{
    public class RepositorioPagos : IPagoRepositorio
    {
        private List<Pago> _pagos = new List<Pago>();

        public RepositorioPagos()
        {
        }
        public bool Add(Pago obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pago> FindAll()
        {
            throw new NotImplementedException();
        }

        public Pago FindById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Pago obj)
        {
            throw new NotImplementedException();
        }
    }
}
