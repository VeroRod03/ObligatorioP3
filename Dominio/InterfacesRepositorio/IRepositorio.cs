using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorio<T>
    {
        void Add(T obj);
        void Remove(int id);
        void Update(T obj);
        IEnumerable<T> FindAll();
        T FindById(int id);
    }
}
