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
    public class RepositorioAuditoriasEF : IAuditoriaRepositorio
    {
        private DominioContext _context;

        public RepositorioAuditoriasEF(DominioContext context)
        {
            _context = context;
        }

        public void Add(Auditoria obj)
        {
            try
            {
                obj.Validar();
                _context.Auditorias.Add(obj);
                _context.SaveChanges();
            }catch(TipoGastoException tge)
            {
                throw tge;
            }
            catch (Exception ex)
            {
                throw new TipoGastoException("Hubo un error: ", ex);
            }

        }

        public IEnumerable<Auditoria> FindAll()
        {
            throw new NotImplementedException();
        }

        public Auditoria FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Auditoria obj)
        {
            throw new NotImplementedException();
        }
    }
}
