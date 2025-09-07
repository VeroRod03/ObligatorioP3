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
    public class RepositorioEquipos : IEquipoRepositorio
    {

        private List<Equipo> _equipos = new List<Equipo>();

        public RepositorioEquipos()
        {
        }

        public bool Add(Equipo obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Equipo> FindAll()
        {
            throw new NotImplementedException();
        }

        public Equipo FindById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Equipo obj)
        {
            throw new NotImplementedException();
        }
    }
}
