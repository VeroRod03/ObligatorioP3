using Dominio.Entidades;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.InterfacesRepositorio
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        public Usuario Login(string email, string pass);
        public IEnumerable<Usuario> FiltrarUsuariosPorMonto(double monto);
        public bool ExisteEmail(Email email);
    }
}
