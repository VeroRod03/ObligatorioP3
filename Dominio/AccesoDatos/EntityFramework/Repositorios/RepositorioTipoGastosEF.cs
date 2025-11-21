using Dominio.Entidades;
using Dominio.Exceptions;
using Dominio.InterfacesRepositorio;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.EntityFramework.Repositorios
{
    public class RepositorioTipoGastosEF : ITipoGastoRepositorio
    {
        private DominioContext _context;

        public RepositorioTipoGastosEF(DominioContext context)
        {
            _context = context;
        }
        public void Add(TipoGasto obj)
        {
            try
            {
                obj.Validar();
                _context.Add(obj);
                _context.SaveChanges();
            }
            catch(TipoGastoException tge)
            {
                throw tge;
            }
            catch(Exception ex)
            {
                throw new TipoGastoException($"Hubo un error: {ex.Message}");
            }
        }

        public IEnumerable<TipoGasto> FindAll()
        {
            return _context.TipoGastos;
        }

        public TipoGasto FindById(int id)
        {
            return _context.TipoGastos.Where(tg => tg.Id == id).FirstOrDefault();
        }

        public void Remove(int id)
        {
            try
            {
                //TipoGasto aBorrar = new TipoGasto { Id = id };
                //este cambio se tuvo que hacer para evitar un erro de tracking
                TipoGasto aBorrar = _context.TipoGastos.Find(id);
                _context.TipoGastos.Remove(aBorrar);
                _context.SaveChanges();
            }
            catch (TipoGastoException tge)
            {
                throw tge;
            }
            catch (Exception ex)
            {
                throw new TipoGastoException($"Hubo un error: {ex.Message}");
            }
        }

        public void Update(TipoGasto obj)
        {
            try
            {
                obj.Validar();

                //cambio para evitar error de tracking
                TipoGasto aModificar = _context.TipoGastos.Find(obj.Id);
                aModificar.Nombre = obj.Nombre;
                aModificar.Descripcion = obj.Descripcion;

                _context.TipoGastos.Update(aModificar);
                _context.SaveChanges();
            }
            catch (TipoGastoException tge)
            {
                throw tge;
            }
            catch (Exception ex)
            {
                throw new TipoGastoException($"Hubo un error: {ex.Message}");
            }
        }
    }
}

