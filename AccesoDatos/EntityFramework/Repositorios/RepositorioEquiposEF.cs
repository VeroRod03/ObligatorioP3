using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.EntityFramework.Repositorios
{
    public class RepositorioEquiposEF : IEquipoRepositorio
    {
        private DominioContext _context;
        public RepositorioEquiposEF()
        {
            _context = new DominioContext();
        }
        public void Add(Equipo obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Equipo> FindAll()
        {
            return _context.Equipos;
        }

        public Equipo FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Equipo obj)
        {
            throw new NotImplementedException();
        }
    }
}
