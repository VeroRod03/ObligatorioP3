using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorio<T>
    {
        bool Add(T obj);
        bool Remove(int id);
        bool Update(T obj);
        IEnumerable<T> FindAll();
        T FindById(int id);
    }
}
