using Dominio.Entidades;
using Dominio.Exceptions;
using Dominio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.EntityFramework.Repositorios
{
    public class RepositorioPagoEF : IPagoRepositorio
    {
        private DominioContext _context;

        public RepositorioPagoEF()
        {
            _context = new DominioContext();
        }

        public void Add(Pago obj)
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

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Pago obj)
        {
            throw new NotImplementedException();
        }

        //???
        public void VerificarTipoGastoEnUso(TipoGasto gasto)
        {
            foreach(Recurrente r in _context.Recurrentes)
            {
                if (r.TipoGasto.Equals(gasto))
                {
                    if(r.Hasta == null || r.Hasta > DateTime.Today)
                    {
                        throw new TipoGastoException("El tipo de gasto esta en uso.");
                    }
                }
            }
        }
    }
}
