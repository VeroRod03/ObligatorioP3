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
            }catch(AuditoriaException aex)
            {
                throw aex;
            }
            catch (Exception ex)
            {
                throw new AuditoriaException($"Hubo un error: {ex.Message}");
            }

        }

        public IEnumerable<Auditoria> FiltrarAuditoriasPorTipoGasto(int id)
        {
            return _context.Auditorias
                .Where(a => a.TipoGastoId == id)
                .ToList();
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
