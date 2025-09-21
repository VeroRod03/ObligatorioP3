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
    public class RepositorioTipoGastosEF : ITipoGastoRepositorio
    {
        private DominioContext _context;
        //private IPagoRepositorio _repositorio;

        public RepositorioTipoGastosEF(
            //IPagoRepositorio repositorio
            )
        {
            _context = new DominioContext();
            //_repositorio = repositorio;
        }
        public void Add(TipoGasto obj)
        {
            try
            {
                obj.Validar();
                _context.Add(obj);

                _context.Auditorias.Add(new Auditoria
                {
                    Accion = "Alta",
                    Fecha = DateTime.Today,
                    //Usuario =
                });

                _context.SaveChanges();
            }
            catch(TipoGastoException tge)
            {
                throw tge;
            }
            catch(Exception ex)
            {
                throw new TipoGastoException("Hubo un error: ", ex);
            }
        }

        public IEnumerable<TipoGasto> FindAll()
        {
            return _context.TipoGastos;
        }

        public TipoGasto FindById(int id)
        {
            foreach (TipoGasto obj in _context.TipoGastos)
            {
                if (obj.Id == id)
                {
                    return obj;
                }
            }
            throw new TipoGastoException("No fue encontrado un tipo de gasto con ese id");
        }

        //???
        public void Remove(int id)
        {
            try
            {
                TipoGasto aBorrar = new TipoGasto { Id = id };
                //_repositorio.VerificarTipoGastoEnUso(aBorrar);

                foreach (Recurrente r in _context.Recurrentes)
                {
                    if (r.TipoGasto.Equals(aBorrar))
                    {
                        if (r.Hasta == null || r.Hasta >= DateTime.Today)
                        {
                            throw new TipoGastoException("El tipo de gasto esta en uso.");
                        }
                    }
                }
                _context.TipoGastos.Remove(aBorrar);

                _context.Auditorias.Add(new Auditoria
                {
                    Accion = "Borrar",
                    Fecha = DateTime.Today,
                    //Usuario = 
                });

                _context.SaveChanges();
            }
            catch (TipoGastoException tge)
            {
                throw tge;
            }
            catch (Exception ex)
            {
                throw new TipoGastoException("Hubo un error: ", ex);
            }
        }

        public void Update(TipoGasto obj)
        {
            try
            {
                obj.Validar();
                _context.TipoGastos.Update(obj);

                _context.Auditorias.Add(new Auditoria
                {
                    Accion = "Editar",
                    Fecha = DateTime.Today,
                    //Usuario = 
                });

                _context.SaveChanges();
            }
            catch (TipoGastoException tge)
            {
                throw tge;
            }
            catch (Exception ex)
            {
                throw new TipoGastoException("Hubo un error: ", ex);
            }
        }
    }
}

