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
    public class RepositorioPagosEF : IPagoRepositorio
    {
        private DominioContext _context;

        public RepositorioPagosEF()
        {
            _context = new DominioContext();
        }

        public void Add(Pago obj)
        {
            obj.Validar();
            _context.Pagos.Add(obj);
            if(obj is Recurrente recurrente){
                _context.Recurrentes.Add(recurrente);
            } else
            {
                _context.Unicos.Add(obj as Unico);
            }
            _context.SaveChanges();
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
    }
}
